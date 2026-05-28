Imports DevExpress.Web
Imports DevExpress.Web.Demos
Imports System
Imports System.Web
Imports System.Web.UI

Namespace DevExpress.Web.Demos
    Partial Public Class Search
        Inherits UserControl

        Public Property FocusOnLoad() As Boolean
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If FocusOnLoad Then
                SearchEditor.Focus()
            End If
        End Sub
        Public Function GetSearchItemUrl(ByVal dataItem As Object) As String
            Dim sr As SearchResult = DirectCast(dataItem, SearchResult)
            Dim url As String = Utils.GenerateDemoUrl(If((Not Object.Equals(sr.Demo, sr.Product.Intro)), sr.Demo, sr.Product.Intro))
            Return ResolveClientUrl(url)
        End Function
        Public Function HasAdditionalInfo(ByVal dataItem As Object) As Boolean
            Dim sr As SearchResult = DirectCast(dataItem, SearchResult)
            Return sr.Group IsNot Nothing
        End Function
        Public Function GetDemoGroupTittle(ByVal dataItem As Object) As String
            Dim sr As SearchResult = DirectCast(dataItem, SearchResult)
            Return If(sr.Group IsNot Nothing, sr.Group.Title, String.Empty)
        End Function
        Public Function GetAdditionalInfoMarker(ByVal dataItem As Object) As String
            Return If(HasAdditionalInfo(dataItem), "", "no-ai")
        End Function
        Protected Sub SearchResults_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
            Dim text = e.Parameter
            Dim results = SearchUtils.DoSearch(text)
            If results.Count > 0 Then
                resultsContainer.Visible = True
                noResultsContainer.Visible = False
                resultsRepeater.DataSource = results
                resultsRepeater.DataBind()
            Else
                resultsContainer.Visible = False
                noResultsContainer.Visible = True
                requestText.InnerHtml = HttpUtility.HtmlEncode(text)
            End If
        End Sub
    End Class
End Namespace
