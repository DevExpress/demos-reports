'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports DevExpress.Web.Demos
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web
Public NotInheritable Class DirectoryManagmentUtils

    Private Sub New()
    End Sub

    Public Const DisposeTimeout As Integer = 5
    Private Const FolderKey As String = "WorkSessionDirectory"
    Private Const DemoPathKey As String = "CurrentDemoPath"
    Private Shared ReadOnly modifyUserDirectoriesLocker As New Object()
    Public Class DemoDirectoryInfo
        Public Property Name() As String
        Public Property LastUsageTime() As Date
    End Class
    Public Shared ReadOnly Property DocumentBrowsingFolderPath() As String
        Get
            Return Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "DocumentBrowsing")
        End Get
    End Property
    Public Shared ReadOnly Property SampleDocumentsFolderPath() As String
        Get
            Return Path.Combine(DirectoryManagmentUtils.CurrentDataDirectory, "SampleDocuments")
        End Get
    End Property
    Public Shared ReadOnly Property CurrentDataDirectory() As String
        Get
            If Not Utils.IsSiteMode Then
                Return InitialDemoFilesPath
            End If
            SyncLock modifyUserDirectoriesLocker

                Dim currentDataDirectory_Renamed = DirectCast(Context.Session(FolderKey), String)
                Dim directoryInfo As DemoDirectoryInfo = ActualDemoDirectories.Where(Function(i) i.Name Is currentDataDirectory_Renamed).SingleOrDefault()
                If directoryInfo Is Nothing OrElse (DirectCast(Context.Session(DemoPathKey), String) <> Context.Request.Path AndAlso Context.Request.HttpMethod = "GET") Then
                    Context.Session(DemoPathKey) = Context.Request.Path
                    currentDataDirectory_Renamed = CreateNewDemoFolder()
                    Context.Session(FolderKey) = currentDataDirectory_Renamed
                    directoryInfo = New DemoDirectoryInfo With {.Name = currentDataDirectory_Renamed, .LastUsageTime = Date.Now}
                    ActualDemoDirectories.Add(directoryInfo)
                Else
                    directoryInfo.LastUsageTime = Date.Now
                End If
                Return currentDataDirectory_Renamed
            End SyncLock
        End Get
    End Property

    Private Shared actualDemoDirectories_Renamed As IList(Of DemoDirectoryInfo)
    Private Shared ReadOnly Property ActualDemoDirectories() As IList(Of DemoDirectoryInfo)
        Get
            If actualDemoDirectories_Renamed Is Nothing Then
                actualDemoDirectories_Renamed = New List(Of DemoDirectoryInfo)()
            End If
            Return actualDemoDirectories_Renamed
        End Get
    End Property
    Private Shared ReadOnly Property Context() As HttpContext
        Get
            Return HttpContext.Current
        End Get
    End Property
    Private Shared ReadOnly Property RootDemoFilesPath() As String
        Get
            Return Context.Request.MapPath("~/App_Data/")
        End Get
    End Property
    Private Shared ReadOnly Property InitialDemoFilesPath() As String
        Get
            Return Path.Combine(RootDemoFilesPath, "Documents")
        End Get
    End Property
    Private Shared Function CreateNewDemoFolder() As String
        Dim demoFilesDirectoty As String = GenerateDemoFilesFolderName()
        CopyDemoFiles(InitialDemoFilesPath, demoFilesDirectoty)
        Return demoFilesDirectoty
    End Function
    Private Shared Sub CopyDemoFiles(ByVal sourceFilePath As String, ByVal destinationPath As String)
        Dim documentFileCollection As IEnumerable(Of String) = GetFilesInDirectory(sourceFilePath, "*.xlsx", "*.xls", "*.csv", "*.docx", "*.doc", "*.rtf", "*.txt")
        If Not Directory.Exists(destinationPath) Then
            Directory.CreateDirectory(destinationPath)
        End If
        For Each filePath In documentFileCollection
            Dim destinationFile As String = Path.Combine(destinationPath, Path.GetFileName(filePath))
            File.Copy(filePath, destinationFile, True)
            File.SetAttributes(destinationFile, FileAttributes.Normal)
        Next filePath
        For Each directoryPath As String In Directory.GetDirectories(sourceFilePath)
            Dim directoryName As String = Path.GetFileName(directoryPath)
            CopyDemoFiles(directoryPath, Path.Combine(destinationPath, directoryName))
        Next directoryPath
    End Sub
    Private Shared Function GetFilesInDirectory(ByVal path As String, ParamArray ByVal allowedExtensions() As String) As IEnumerable(Of String)
        Dim documentFileCollection As IEnumerable(Of String) = New String(){}
        For Each extension As String In allowedExtensions
            documentFileCollection = documentFileCollection.Concat(Directory.GetFiles(path, extension))
        Next extension
        Return documentFileCollection
    End Function
    Private Shared Function GenerateDemoFilesFolderName() As String
        Dim currentFolder As String = Nothing
        Do While String.IsNullOrEmpty(currentFolder) OrElse Directory.Exists(Path.Combine(RootDemoFilesPath, currentFolder))
            currentFolder = Guid.NewGuid().ToString()
        Loop
        Return Path.Combine(RootDemoFilesPath, currentFolder)
    End Function
    Public Shared Sub PurgeOldUserDirectories()
        If Not Utils.IsSiteMode Then
            Return
        End If
        SyncLock modifyUserDirectoriesLocker
            Dim existingDirectories() As String = Directory.GetDirectories(RootDemoFilesPath)
            For Each directoryPath As String In existingDirectories
                Dim guid As Guid = System.Guid.Empty
                If Not System.Guid.TryParse(Path.GetFileName(directoryPath), guid) Then
                    Continue For
                End If
                Dim directoryInfo As DemoDirectoryInfo = ActualDemoDirectories.Where(Function(i) i.Name = directoryPath).SingleOrDefault()
                If directoryInfo Is Nothing OrElse (Date.Now.Subtract(directoryInfo.LastUsageTime)).TotalMinutes > DisposeTimeout Then
                    Directory.Delete(directoryPath, True)
                    If directoryInfo IsNot Nothing Then
                        ActualDemoDirectories.Remove(directoryInfo)
                    End If
                End If
            Next directoryPath
        End SyncLock
    End Sub
    Public Shared Function GetDeletedByTimeoutMessage() As String
        Return "The current work session has been expired. The page will be automatically re-requested."
    End Function
    Public Shared Sub AssertTimeout()
        If Not Utils.IsSiteMode Then
            Return
        End If
        Throw New DemoException(GetDeletedByTimeoutMessage())
    End Sub
End Class
Public Class OfficeDemoPage
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnPreLoad(ByVal e As EventArgs)
        MyBase.OnPreLoad(e)
        DirectoryManagmentUtils.PurgeOldUserDirectories()
    End Sub
End Class
