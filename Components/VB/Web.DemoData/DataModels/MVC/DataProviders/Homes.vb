'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Web
Imports DevExpress.Web.Internal
Namespace DevExpress.Web.Demos
    Public Class HomesDataProvider
        Private Const ImageWidth As Integer = 250
        Private Const ImageFileNameFormat As String = "{0}.jpg", DataFileVirtualPath As String = "~/App_Data/Homes.xml", PhotoDirVirtualPath As String = "~/Content/HomesPhoto"
        Private Shared ReadOnly createLocker As New Object()

        Private Shared homes_Renamed As List(Of Home) = Nothing
        Private Shared HomesDataSet As DataSet
        Private Shared Function MapPath(ByVal path As String) As String
            Return HttpContext.Current.Server.MapPath(path)
        End Function
        Private Shared ReadOnly Property DataFilePath() As String
            Get
                Return MapPath(DataFileVirtualPath)
            End Get
        End Property
        Private Shared ReadOnly Property PhotoDirPath() As String
            Get
                Return MapPath(PhotoDirVirtualPath)
            End Get
        End Property
        Private Shared Function PhotoUrl(ByVal id As Integer) As String
            Return Path.Combine(PhotoDirVirtualPath, String.Format(ImageFileNameFormat, id))
        End Function
        Public Shared ReadOnly Property Homes() As List(Of Home)
            Get
                If homes_Renamed Is Nothing Then
                    homes_Renamed = CreateHomes()
                End If
                Return homes_Renamed
            End Get
        End Property
        Public Shared Function GetHomesDataSet() As DataSet
            If HomesDataSet Is Nothing Then
                HomesDataSet = New DataSet()
                HomesDataSet.ReadXml(DataFilePath)
            End If
            Return HomesDataSet
        End Function
        Public Shared Function SelectHomes() As List(Of Home)
            Return Homes
        End Function
        Private Shared Function CreateHomes() As List(Of Home)
            SyncLock createLocker
                Dim dataSet = New DataSet()
                dataSet.ReadXml(DataFilePath)
                Return dataSet.Tables(0).Select().Select(Function(r) ConvertToHome(r)).ToList()
            End SyncLock
        End Function
        Private Shared Function ConvertToHome(ByVal row As DataRow) As Home
            Dim result = New Home With {.ID = row.Field(Of Integer)("ID"), .Address = row.Field(Of String)("Address"), .Baths = row.Field(Of Short)("Baths"), .Beds = row.Field(Of Short)("Beds"), .HouseSize = row.Field(Of Double)("HouseSize"), .LotSize = row.Field(Of Double)("LotSize"), .Price = row.Field(Of Decimal)("Price"), .YearBuilt = row.Field(Of String)("YearBuilt"), .Photo = row.Field(Of Byte())("Photo"), .Features = row.Field(Of String)("Features"), .Type = CType(row.Field(Of Integer)("Type"), HomeType), .Status = CType(row.Field(Of Integer)("Status"), HomeStatus)}
            result.PhotoUrl = GetPhotoUrl(result)
            Return result
        End Function
        Private Shared Function GetPhotoUrl(ByVal item As Home) As String
            Dim url = PhotoUrl(item.ID)
            Dim serverPath = MapPath(url)
            If Not File.Exists(serverPath) Then
                If Not Directory.Exists(PhotoDirPath) Then
                    Directory.CreateDirectory(PhotoDirPath)
                End If
                SaveImage(item.Photo, serverPath)
            End If
            Return url
        End Function
        Private Shared Sub SaveImage(ByVal imageBytes() As Byte, ByVal path As String)
            Using stream = New MemoryStream(imageBytes)
            Using img = Image.FromStream(stream)
                Dim size As New Size(ImageWidth, CInt(img.Height) * ImageWidth \ img.Width)
                Using thumbnail As Image = (New ImageThumbnailCreator(img)).CreateImageThumbnail(size)
                    ImageUtils.SaveImage(thumbnail, path)
                End Using
            End Using
            End Using
        End Sub
    End Class
    Public Enum HomeType
        <Description("Single Family Home")>
        SingleFamilyHome
        <Description("Condo/Townhouse")>
        Townhome
        <Description("Multi-Family Home")>
        MultiFamilyHome
    End Enum
    Public Enum HomeStatus
        <Description("New Construction")>
        NewConstruction
        <Description("Foreclosures")>
        Foreclosures
        <Description("Recently Sold")>
        RecentlySold
    End Enum
    Public Class Home
        Public Property ID() As Integer
        Public Property Address() As String
        Public Property Baths() As Short
        Public Property Beds() As Short
        Public Property HouseSize() As Double
        Public Property LotSize() As Double
        Public Property Price() As Decimal
        Public Property YearBuilt() As String
        Public Property Photo() As Byte()
        Public Property PhotoUrl() As String
        Public Property Features() As String
        Public Property Type() As HomeType
        Public Property Status() As HomeStatus
    End Class
End Namespace
