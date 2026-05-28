'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Web.UI
Imports System.Web.UI.Adapters
Namespace DevExpress.Web.Demos
    Public Class HiddenHtmlFormAdapter
        Inherits ControlAdapter

        Protected Overrides Sub OnInit(ByVal e As EventArgs)
        End Sub
        Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
        End Sub
        Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
            For Each child As Control In Control.Controls
                child.RenderControl(writer)
            Next child
        End Sub
    End Class
End Namespace
