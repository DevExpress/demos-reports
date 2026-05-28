Imports System
Imports System.Web.UI
Imports DevExpress.Web.Demos

Namespace DevExpress.Web.Demos
    Partial Public Class UserControls_ViewerSelectorControl
        Inherits UserControl
        Implements IMobileEmulator

        Public ReadOnly Property CurrentViewer() As String
            Get
                Return ViewerSelectorState.GetCurrentViewer()
            End Get
        End Property

        Protected Sub AddMenuItem(ByVal selectorItem As ViewerSelectorState.Item)
            Dim menuItem = ViewerSelectorMenu.Items.Add()
            menuItem.Name = selectorItem.Name
            menuItem.Selected = selectorItem.Name = CurrentViewer
            menuItem.NavigateUrl = selectorItem.GenerateUrl(Request.Url)
            menuItem.Image.Url = "../Content/1x1.gif"
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            For Each selectorItem As ViewerSelectorState.Item In ViewerSelectorState.Items
                AddMenuItem(selectorItem)
            Next selectorItem
        End Sub

        Public Property ReportTypeName() As String Implements IMobileEmulator.ReportTypeName
    End Class
End Namespace
