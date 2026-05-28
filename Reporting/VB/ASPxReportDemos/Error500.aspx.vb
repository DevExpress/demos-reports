Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.Demos

Namespace DevExpress.Web.Demos
    Partial Public Class Error500
        Inherits Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Response.StatusCode = 500
        End Sub
    End Class
End Namespace
