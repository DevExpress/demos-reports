Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Windows.Forms

Namespace DevExpress.Web.Demos
    Partial Public Class DeviceViewer
        Inherits System.Web.UI.MasterPage


        Private enableAnimation_Renamed As Boolean = True

        Public Property Url() As String
        Public Property DefaultRotate() As Boolean
        Public Property DefaultMobile() As Boolean
        Public Property HideRotate() As Boolean

        Public Property EnableAnimation() As Boolean
            Get
                Return enableAnimation_Renamed
            End Get
            Set(ByVal value As Boolean)
                enableAnimation_Renamed = value
            End Set
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Utils.RegisterCssLink(Page, "~/Content/deviceViewer.css")

            If Not DevExpress.Web.Internal.RenderUtils.Browser.Platform.IsMobileUI Then
                viewer.Attributes("src") = ResolveUrl(Url)
            End If

            QrcodeLink.Attributes("href") = ResolveUrl(Url)
            QrcodeImg.Attributes("src") = FullScreenModeUtils.GetQRCodeImageUrl(Me.Page, Url)
        End Sub

        Protected Function GetUrl() As String
            Return ResolveUrl(Url)
        End Function
    End Class
End Namespace
