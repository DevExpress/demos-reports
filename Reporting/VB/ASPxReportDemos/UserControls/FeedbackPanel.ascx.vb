Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace DevExpress.Web.Demos
    Partial Public Class UserControls_FeedbackPanel
        Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Protected Sub linkSupportCenter_Init(ByVal sender As Object, ByVal e As EventArgs)
            Dim link = DirectCast(sender, ASPxHyperLink)
            link.Text = "on the DevExpress Support Forum."
            link.Target = "_blank"

            Dim currentDemo = DevExpress.Web.Demos.Utils.CurrentDemo
            If currentDemo IsNot Nothing AndAlso currentDemo.Product IsNot Nothing Then
                Dim currentProductID As String = GetProductPlatformId(currentDemo.Product.Key)
                Dim currentProductFilter As String = "filters=%5B%7B%22filterName%22:%22PlatformedProductId%22,%22selectedValues%22:%5B%22" & currentProductID & "%22%5D%7D%5D"

                Dim demoTitle As String = currentDemo.GetSeoTitle()
                Dim demoGroup As String = ""

                If currentDemo.Product.Key.Contains("ASPxperience") Then
                    Dim pageModel As DevExpress.Web.Demos.DemoPageModel = TryCast(currentDemo, DevExpress.Web.Demos.DemoPageModel)
                    If pageModel IsNot Nothing AndAlso pageModel.Group IsNot Nothing Then
                        If pageModel.Group.Key.Contains("Accessibility") Then
                            demoGroup = currentDemo.Product.NavItemTitle
                        ElseIf Not pageModel.Group.Key.Contains("Cloud Storage") Then
                            demoGroup = pageModel.Group.Title
                        End If
                    End If
                End If
                If (Not String.IsNullOrEmpty(demoGroup)) AndAlso demoTitle <> "Icon Library Explorer" Then
                    demoTitle = demoGroup & " " & demoTitle
                End If

                demoTitle = demoTitle.Replace("/", " ").Replace("Support for", "")
                link.NavigateUrl = String.Format("https://supportcenter.devexpress.com/ticket/list?{0}&searchString={1}&sorting=Relevance", currentProductFilter, HttpUtility.UrlEncode(demoTitle))
            Else
                link.NavigateUrl = "https://supportcenter.devexpress.com/ticket/list"
            End If
        End Sub
        Private Shared instanceLocker As New Object()
        Private Shared _productPlatformCache As Dictionary(Of String, String)
        Private Shared ReadOnly Property ProductPlatformCache() As Dictionary(Of String, String)
            Get
                If _productPlatformCache Is Nothing Then
                    SyncLock instanceLocker
                        If _productPlatformCache Is Nothing Then
                            _productPlatformCache = New Dictionary(Of String, String)()
                            _productPlatformCache.Add("ASPxCardView", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:9d99daa7-fe83-11e6-82d9-54271e19360e")
                            _productPlatformCache.Add("Dashboard", "35e0b143-40ca-11e2-b3e9-c0cb38ffce8d:e6bb47fe-cc0b-11e6-82d1-54271e19360e")
                            _productPlatformCache.Add("ASPxDiagram", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:e2102f3e-c99c-48ad-a83f-c2086aa6b006")
                            _productPlatformCache.Add("ASPxEditors", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:7b499dd4-c4bd-4fd0-bc84-b78f207758d0")
                            _productPlatformCache.Add("ASPxGantt", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:6199c828-a8b6-45d1-8736-0c80ee9b5e62")
                            _productPlatformCache.Add("ASPxGauges", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:eee44732-43af-44e3-afc6-9464fc076599")
                            _productPlatformCache.Add("ASPxGridView", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:22713479-a995-45d4-9ed4-72ffa096d83d")
                            _productPlatformCache.Add("ASPxHtmlEditor", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:3a1ffe09-446f-4b93-9c49-3730b33e839e")
                            _productPlatformCache.Add("ASPxperience.DockAndPopups", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed")
                            _productPlatformCache.Add("ASPxperience.FileManagerAndUpload", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed")
                            _productPlatformCache.Add("ASPxperience.ImageAndDataNavigation", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed")
                            _productPlatformCache.Add("ASPxperience.MultiUseControls", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed")
                            _productPlatformCache.Add("ASPxperience.NavigationAndLayout", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed")
                            _productPlatformCache.Add("ASPxPivotGrid", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:48b469a5-cd05-48aa-b992-ed28e84dd7d7")
                            _productPlatformCache.Add("XtraReports", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:e9f1506a-471c-4b72-82df-151effca6b0d")
                            _productPlatformCache.Add("ASPxRichEdit", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:4a9d252d-6e4e-11e4-b1b2-5442496457d0")
                            _productPlatformCache.Add("ASPxScheduler", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:f8aad72b-2600-4fe2-9047-07c820fadd48")
                            _productPlatformCache.Add("ASPxSpellChecker", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:c0d297f4-d920-452b-89c3-f348b4f4deb4")
                            _productPlatformCache.Add("ASPxSpreadsheet", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:0ac3bc5b-4e0a-11e3-8230-5442496457d0")
                            _productPlatformCache.Add("ASPxTreeList", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:a6a6028e-e0cd-418e-a4c9-31e297bb0d69")
                            _productPlatformCache.Add("ASPxVerticalGrid", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:9d99da9a-fe83-11e6-82d9-54271e19360e")
                        End If
                    End SyncLock
                End If
                Return _productPlatformCache
            End Get
        End Property

        Private Function GetProductPlatformId(ByVal demoNodeName As String) As String
            Return If(ProductPlatformCache.ContainsKey(demoNodeName), ProductPlatformCache(demoNodeName), "")
        End Function
    End Class
End Namespace
