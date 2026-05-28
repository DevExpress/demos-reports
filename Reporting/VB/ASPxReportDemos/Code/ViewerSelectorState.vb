Imports System
Imports System.Linq
Imports System.Web

Namespace DevExpress.Web.Demos
    Public NotInheritable Class ViewerSelectorState

        Private Sub New()
        End Sub

        Friend Const Key As String = "CurrentViewer"
        Public Const ModernViewer As String = "HTML5"
        Public Const MobileViewer As String = "Mobile"
        Public Const DefaultViewer As String = ModernViewer

        Public Shared ReadOnly Items() As Item

        Shared Sub New()
            Dim generateHtml5ViewerUrlDelegate = New Func(Of Uri, String)(AddressOf GenerateHtml5ViewerUrl)
            Dim generateMobileViewerUrlDelegate = New Func(Of Uri, String)(AddressOf GenerateMobileViewerUrl)
            Items = New Item() {
                New Item(generateHtml5ViewerUrlDelegate) With {.Name = ModernViewer, .Text = "HTML5 Viewer", .CssClass = "viewer-selector-html5"},
                New Item(generateMobileViewerUrlDelegate) With {.Name = MobileViewer, .Text = "Mobile Viewer", .CssClass = "viewer-selector-html5"}
            }
        End Sub

        Public Shared Function GetCurrentViewer() As String
            Return If(HttpContext.Current Is Nothing, DefaultViewer, Items.Select(Function(item) item.Name).Where(Function(name) name = HttpContext.Current.Request.Params(Key)).DefaultIfEmpty(DefaultViewer).First())
        End Function

        Public Shared Function GetByName(ByVal name As String) As Item
            Return Items.FirstOrDefault(Function(x) x.Name = name)
        End Function

        Private Shared Function GenerateHtml5ViewerUrl(ByVal requestUrl As Uri) As String
            Dim builder = New UriBuilder(requestUrl)
            builder.Query = ""
            Return builder.ToString()
        End Function

        Private Shared Function GenerateMobileViewerUrl(ByVal requestUrl As Uri) As String
            Dim builder = New UriBuilder(requestUrl)
            builder.Query = Key & "=" & MobileViewer
            Return builder.ToString()
        End Function

        Public Class Item
            Private ReadOnly _generateUrl As Func(Of Uri, String)

            Public Sub New(ByVal generateUrl As Func(Of Uri, String))
                Me._generateUrl = generateUrl
            End Sub

            Public Function GenerateUrl(ByVal requestUri As Uri) As String
                Return _generateUrl(requestUri)
            End Function

            Public Property Name() As String
            Public Property Text() As String
            Public Property CssClass() As String
        End Class
    End Class
End Namespace
