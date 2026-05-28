'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.IO
Imports System.Web
Imports System.Web.Caching
Imports System.Web.Configuration
Namespace DevExpress.Web.Demos
    Public NotInheritable Class UploadingUtils

        Private Sub New()
        End Sub

        Private Const RemoveTaskKeyPrefix As String = "DXRemoveTask_"
        Public Shared Sub RemoveFileWithDelay(ByVal key As String, ByVal fullPath As String, ByVal delay As Integer)
            RemoveFileWithDelayInternal(key, fullPath, delay, AddressOf FileSystemRemoveAction)
        End Sub
        Private Shared Sub FileSystemRemoveAction(ByVal key As String, ByVal value As Object, ByVal reason As CacheItemRemovedReason)
            Dim fileFullPath As String = value.ToString()
            If File.Exists(fileFullPath) Then
                File.Delete(fileFullPath)
            End If
        End Sub
        Private Shared Sub RemoveFileWithDelayInternal(ByVal fileKey As String, ByVal fileData As Object, ByVal delay As Integer, ByVal removeAction As CacheItemRemovedCallback)
            Dim key As String = RemoveTaskKeyPrefix & fileKey
            If HttpRuntime.Cache(key) Is Nothing Then
                Dim absoluteExpiration As Date = Date.UtcNow.Add(New TimeSpan(0, delay, 0))
                HttpRuntime.Cache.Insert(key, fileData, Nothing, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, removeAction)
            End If
        End Sub
    End Class
End Namespace
