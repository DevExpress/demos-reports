Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.Demos

Namespace DevExpress.Web.Demos
    Partial Public Class UserControls_DemoTabs
        Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim index = 0
            If Utils.CurrentDemoPage IsNot Nothing AndAlso (Not Utils.IsIntro) AndAlso (Not Utils.CurrentDemo.Product.HideSourceCode) AndAlso (Not Utils.CurrentDemo.HideSourceCode) Then

                For Each page_Renamed As SourceCodePage In Utils.GetCurrentSourceCodePages()
                    Dim tab = DemoPageControl.TabPages.Add(page_Renamed.Title, page_Renamed.Title)
                    Dim codeViewer = CType(LoadControl("~\UserControls\CodeViewer.ascx"), CodeViewerControl)
                    codeViewer.SourceCodePage = page_Renamed
                    codeViewer.Index = index
                    Dim currentModel As DemosModel = DemosModel.Instance
                    If currentModel IsNot Nothing Then
                        codeViewer.DisableTextWrap = DemosModel.Instance.DisableTextWrap
                    End If
                    tab.Controls.Add(codeViewer)
                    index += 1
                Next page_Renamed
            End If
        End Sub
        Protected Sub btnOpenSolution_Init(ByVal sender As Object, ByVal e As EventArgs)
            If Utils.CurrentDemoPage Is Nothing Then
                Return
            End If
            Dim button As DevExpress.Web.ASPxButton = TryCast(sender, DevExpress.Web.ASPxButton)
            If Utils.CurrentDemoPage.Product.Key.ToLower().Contains("report") Then
                button.Visible = False
            Else
                button.ClientSideEvents.Click = "function (s, e) { window.open('" & GenerateOpenSolutionLink(If(button.ID.Contains("VB"), "VB", "CS")) & "', '_self'); }"
            End If
        End Sub
        Private Function GenerateOpenSolutionLink(ByVal language As String) As String
            Dim pageModel As DevExpress.Web.Demos.DemoPageModel = Utils.CurrentDemoPage
            Dim viewsWithSeparatePages As New List(Of String)() From {"adaptivelayout", "responsivelayout", "floatingactionbuttonforgridview"}
            Dim viewName As String = If(viewsWithSeparatePages.Contains(pageModel.Key.ToLower()), pageModel.Key & "Page", pageModel.Key)
'#pragma warning disable DX0025 ' not a path (and no travesal here)
            Return String.Format("dxdemo://{0}/{1}/MainDemo/{2}/{3}/{4}",If(pageModel.Product.Key.ToLower() = "officefileapi", "Docs", "Asp"),If(pageModel.Product.Key.ToLower() = "officefileapi", "DocumentServerForAsp", pageModel.Product.Key.Replace(".", "")),If(pageModel.Group Is Nothing, "", pageModel.Group.Key), viewName, language.ToUpper())
'#pragma warning restore DX0025
        End Function
        Protected Sub lblPath_Init(ByVal sender As Object, ByVal e As EventArgs)
            If Utils.CurrentDemoPage Is Nothing Then
                Return
            End If
            Dim lbl As DevExpress.Web.ASPxLabel = DirectCast(sender, DevExpress.Web.ASPxLabel)
            Dim pathPrefix As String = If(lbl.ID.Contains("VB"), "VB", "CS")
            If Utils.CurrentDemoPage.Product.Key.ToLower().Contains("report") Then
                Dim pathSuffix = If(DevExpress.Web.Demos.Utils.CurrentDemoPage.Group.Key = "WebSpecificFeatures", "ASPxReportDemos\WebSpecificFeatures", "DevExpress.DemoReports")
                lbl.Text = String.Format("\Users\Public\Documents\DevExpress Demos XX.X\Components\Reporting\{0}\{1}", pathPrefix, pathSuffix)
            Else
                lbl.Text = String.Format("\Users\Public\Documents\DevExpress Demos XX.X\Components\ASP.NET\{0}\{1}", pathPrefix, DevExpress.Web.Demos.Utils.CurrentDemoPage.Product.Url)
            End If
        End Sub
    End Class
End Namespace
