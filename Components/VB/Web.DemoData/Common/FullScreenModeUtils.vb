'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Web.Internal
Imports DevExpress.XtraPrinting.BarCode
Imports System.Reflection
Imports System.Text
Imports DevExpress.Drawing
Namespace DevExpress.Web.Demos
    Public Class FullScreenModeResolution
        Public ReadOnly Property Name() As String
            Get
                If Width <> 0 AndAlso Height <> 0 Then
                    Return String.Format("{0}x{1}", Width, Height)
                End If
                Return "Fullscreen"
            End Get
        End Property
        Public Property Width() As Integer
        Public Property Height() As Integer
        Public Property ImageUrl() As String
    End Class
    Public NotInheritable Class FullScreenModeUtils

        Private Sub New()
        End Sub

        Public Shared ReadOnly FullScreenViewerUrl As String = "~/UserControls/FullScreenViewer.aspx"
        Public Shared ReadOnly FullScreenViewerQRCodeUrl As String = "~/UserControls/FullScreenViewerQRCode.aspx"

        Private Shared resourceManager_Renamed As System.Resources.ResourceManager

        Private Shared resolutions_Renamed As List(Of FullScreenModeResolution)
        Private Shared ReadOnly Property ResourceManager() As System.Resources.ResourceManager
            Get
                If resourceManager_Renamed Is Nothing Then
                    resourceManager_Renamed = New System.Resources.ResourceManager("Resources", System.Reflection.Assembly.GetExecutingAssembly())
                End If
                Return resourceManager_Renamed
            End Get
        End Property
        Public Shared ReadOnly Property Resolutions() As List(Of FullScreenModeResolution)
            Get
                If resolutions_Renamed Is Nothing Then
                    resolutions_Renamed = New List(Of FullScreenModeResolution)()
                    resolutions_Renamed.Add(New FullScreenModeResolution With {.Width = 0, .Height = 0, .ImageUrl = ResourceManager.GetString("IconDataUriFullscreen")})
                    resolutions_Renamed.Add(New FullScreenModeResolution With {.Width = 1200, .Height = 800, .ImageUrl = ResourceManager.GetString("IconDataUri1200x800")})
                    resolutions_Renamed.Add(New FullScreenModeResolution With {.Width = 800, .Height = 1200, .ImageUrl = ResourceManager.GetString("IconDataUri800x1200")})
                    resolutions_Renamed.Add(New FullScreenModeResolution With {.Width = 768, .Height = 576, .ImageUrl = ResourceManager.GetString("IconDataUri768x576")})
                    resolutions_Renamed.Add(New FullScreenModeResolution With {.Width = 576, .Height = 768, .ImageUrl = ResourceManager.GetString("IconDataUri576x768")})
                End If
                Return resolutions_Renamed
            End Get
        End Property
        Public Shared Function GetUrl(ByVal page As Page, ByVal targetUrl As String, ByVal width As Integer, ByVal height As Integer) As String
            Dim url As String = FullScreenViewerUrl
            If Not String.IsNullOrEmpty(targetUrl) Then
                url &= "?url=" & page.ResolveUrl(targetUrl)
                If width > 0 Then
                    url &= "&width=" & width.ToString()
                End If
                If height > 0 Then
                    url &= "&height=" & height.ToString()
                End If
            End If
            Return url
        End Function
        Public Shared Function GetQRCodeImageUrl(ByVal page As Page, ByVal url As String) As String
            Return String.Format("{0}?url={1}", page.ResolveUrl(FullScreenViewerQRCodeUrl), page.ResolveUrl(url))
        End Function
        Public Shared Function GetQRCodeImage(ByVal url As String) As System.Drawing.Image
            Const imageSize As Integer = 120
            Dim request As HttpRequest = HttpUtils.GetRequest()
            Dim resolvedUrl As String = String.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url)
            Dim image = New Bitmap(imageSize, imageSize)
            Using graphicsObj = Graphics.FromImage(image)
                Dim gdiGraphics = New DevExpress.XtraPrinting.GdiGraphicsWrapper(graphicsObj)
                Dim qrcode = New QRCodeGenerator() With {.CompactionMode = QRCodeCompactionMode.Byte}
                Dim rectf = New RectangleF(0, 0, imageSize, imageSize)
                Dim barcodeData = New QRBarCodeData(resolvedUrl)
                qrcode.DrawContent(gdiGraphics, rectf, barcodeData)
            End Using
            Return image
        End Function
    End Class
    Friend Class QRBarCodeData
        Implements IBarCodeData


        Private ReadOnly text_Renamed As String

        Private ReadOnly style_Renamed As DevExpress.XtraPrinting.BrickStyle
        Public Sub New(ByVal text As String)
            Me.text_Renamed = text
            Me.style_Renamed = New DevExpress.XtraPrinting.BrickStyle With {.ForeColor = Color.Black, .BackColor = Color.Transparent, .Padding = New DevExpress.XtraPrinting.PaddingInfo()}
        End Sub
        Private ReadOnly Property IBarCodeData_ModuleUnit() As DXGraphicsUnit Implements IBarCodeData.ModuleUnit
            Get
                Return DXGraphicsUnit.Pixel
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_Alignment() As DevExpress.XtraPrinting.TextAlignment Implements IBarCodeData.Alignment
            Get
                Return DevExpress.XtraPrinting.TextAlignment.MiddleCenter
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_AutoModule() As Boolean Implements IBarCodeData.AutoModule
            Get
                Return True
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_TargetDeviceDpi() As Integer? Implements IBarCodeData.TargetDeviceDpi
            Get
                Return Nothing
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_Module() As Double Implements IBarCodeData.Module
            Get
                Return 3.0
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_Orientation() As BarCodeOrientation Implements IBarCodeData.Orientation
            Get
                Return BarCodeOrientation.Normal
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_ShowText() As Boolean Implements IBarCodeData.ShowText
            Get
                Return False
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_Style() As DevExpress.XtraPrinting.BrickStyle Implements IBarCodeData.Style
            Get
                Return style_Renamed
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_Text() As String Implements IBarCodeData.Text
            Get
                Return text_Renamed
            End Get
        End Property
        Private ReadOnly Property IBarCodeData_BinaryData() As Byte() Implements IBarCodeData.BinaryData
            Get
                Return Encoding.UTF8.GetBytes(text_Renamed)
            End Get
        End Property
    End Class
End Namespace
