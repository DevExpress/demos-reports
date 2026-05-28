'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    <XmlRoot("Themes")>
    Public Class ThemesModel
        Inherits ThemesModelBase

        Private Const ResourceName As String = "Data.Themes.xml"
        Private Shared _current As ThemesModel
        Private Shared ReadOnly _currentLock As New Object()
        Public Shared ReadOnly Property Current() As ThemesModel
            Get
                SyncLock _currentLock
                    If _current Is Nothing Then
                        _current = GetCurrentModel(Of ThemesModel)(ResourceName)
                    End If
                    Return _current
                End SyncLock
            End Get
        End Property
        Public Overrides Function GetAllThemes() As IEnumerable(Of ThemeModel)
            Return Current.Groups.SelectMany(Function(g) g.Themes)
        End Function
    End Class
End Namespace
