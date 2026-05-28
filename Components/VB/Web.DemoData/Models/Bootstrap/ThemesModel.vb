'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports DevExpress.Utils
Namespace DevExpress.Web.Demos
    <XmlRoot("Themes")>
    Public Class BootstrapThemesModel
        Private Const ResourceName As String = "Data.Bootstrap.Themes.xml"
        Private Shared _current As BootstrapThemesModel
        Private Shared ReadOnly _currentLock As New Object()
        Public Shared ReadOnly Property Current() As BootstrapThemesModel
            Get
                SyncLock _currentLock
                    If _current Is Nothing Then
                        Using stream As Stream = ResourcesUtils.GetEmbeddedResource(ResourceName)
                            _current = SafeXml.Deserialize(Of BootstrapThemesModel)(stream)
                        End Using
                    End If
                    Return _current
                End SyncLock
            End Get
        End Property
        Private _themes As New List(Of ThemeModelBase)()
        Private _themes4 As New List(Of ThemeModelBase)()
        <XmlElement("Theme")>
        Public ReadOnly Property Themes() As List(Of ThemeModelBase)
            Get
                Return _themes
            End Get
        End Property
        <XmlElement("Theme4")>
        Public ReadOnly Property Themes4() As List(Of ThemeModelBase)
            Get
                Return _themes4
            End Get
        End Property
    End Class
End Namespace
