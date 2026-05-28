'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.UI.WebControls
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    <XmlRoot("Themes")>
    Public Class DevExtremeThemesModel
        Inherits ThemesModelBase

        Private Const ResourceName As String = "Data.DevExtremeThemes.xml"
        Public Overrides ReadOnly Property IconHeight() As Unit
            Get
                Return Unit.Pixel(24)
            End Get
        End Property
        Public Overrides ReadOnly Property IconWidth() As Unit
            Get
                Return Unit.Pixel(26)
            End Get
        End Property
        Private Shared _current As DevExtremeThemesModel
        Private Shared ReadOnly _currentLock As New Object()
        Public Shared ReadOnly Property Current() As DevExtremeThemesModel
            Get
                SyncLock _currentLock
                    If _current Is Nothing Then
                        _current = GetCurrentModel(Of DevExtremeThemesModel)(ResourceName)
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
