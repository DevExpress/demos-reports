'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web.UI.WebControls
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    <XmlRoot("Themes")>
    Public MustInherit Class ThemesModelBase
        Private Const DefaultSize As Integer = 18
        Public Overridable ReadOnly Property IconHeight() As Unit
            Get
                Return Unit.Pixel(DefaultSize)
            End Get
        End Property
        Public Overridable ReadOnly Property IconWidth() As Unit
            Get
                Return Unit.Pixel(DefaultSize)
            End Get
        End Property
        Private _groups As New List(Of ThemeGroupModel)()
        Public MustOverride Function GetAllThemes() As IEnumerable(Of ThemeModel)
        Protected Shared Function GetCurrentModel(Of T As Class)(ByVal resourceName As String) As T
            Using stream As Stream = ResourcesUtils.GetEmbeddedResource(resourceName)
                Return DevExpress.Utils.SafeXml.Deserialize(Of T)(stream)
            End Using
        End Function
        Public Function GetAllGroups() As List(Of ThemeGroupModel)
            Dim topThemes = GetAllThemes().Where(Function(t) t.ShowAsTop).ToList()
            Dim currentTheme = GetAllThemes().First(Function(t) t.Name = Utils.CurrentTheme)
            If Not topThemes.Contains(currentTheme) Then
                topThemes.Add(currentTheme)
            End If
            Dim firstGroup = New ThemeGroupModel() With {.Name = "FirstGroup", .Themes = topThemes}
            Return Groups.Concat( { firstGroup }).ToList()
        End Function
        Public ReadOnly Property MobileThemes() As IEnumerable(Of String)
            Get
                Return Groups.First(Function(g) g.Name = "Mobile").Themes.Select(Function(t) t.Name)
            End Get
        End Property
        Public ReadOnly Property NewThemes() As IEnumerable(Of ThemeModel)
            Get
                Return GetAllThemes().Where(Function(theme) theme.IsNew)
            End Get
        End Property
        Public Function GetThemeModel(ByVal themeName As String) As ThemeModel
            Return GetAllThemes().FirstOrDefault(Function(t) t.Name = themeName)
        End Function
        <XmlElement("ThemeGroup")>
        Public ReadOnly Property Groups() As List(Of ThemeGroupModel)
            Get
                Return _groups
            End Get
        End Property
        Public ReadOnly Property RightGroups() As List(Of ThemeGroupModel)
            Get
                Return (
                    From g In Groups
                    Where g.Float = "Right"
                    Select g).ToList()
            End Get
        End Property
        Public ReadOnly Property LeftGroups() As List(Of ThemeGroupModel)
            Get
                Return (
                    From g In Groups
                    Where g.Float = "Left"
                    Select g).ToList()
            End Get
        End Property
    End Class
End Namespace
