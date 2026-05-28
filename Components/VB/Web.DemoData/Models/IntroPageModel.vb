'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Xml.Serialization
Imports System.Collections.Generic
Namespace DevExpress.Web.Demos
    Public Class IntroPageModel
        Inherits DemoPageModel

        Private _bannerTitle As String
        Private _bannerText As String
        Private _bannerImageUrl As String
        Private _bannerUrl As String
        Private _descriptionFooter As String
        Private _bannerHeaderUrl As String
        Private _externalDemos As New List(Of ExternalDemoModel)()
        <XmlIgnore>
        Public Overrides Property Key() As String
            Get
                Return If(Utils.IsMvc, "Index", "Default")
            End Get
            Set(ByVal value As String)
            End Set
        End Property
        <XmlElement>
        Public Property BannerTitle() As String
            Get
                If _bannerTitle Is Nothing Then
                    Return ""
                End If
                Return _bannerTitle
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _bannerTitle = value
            End Set
        End Property
        <XmlElement>
        Public Property BannerText() As String
            Get
                If _bannerText Is Nothing Then
                    Return ""
                End If
                Return _bannerText
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _bannerText = value
            End Set
        End Property
        <XmlElement>
        Public Property BannerHeaderUrl() As String
            Get
                If _bannerHeaderUrl Is Nothing Then
                    Return ""
                End If
                Return _bannerHeaderUrl
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _bannerHeaderUrl = value
            End Set
        End Property
        <XmlElement>
        Public Property BannerImageUrl() As String
            Get
                If _bannerImageUrl Is Nothing Then
                    Return ""
                End If
                Return _bannerImageUrl
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _bannerImageUrl = value
            End Set
        End Property
        <XmlElement>
        Public Property BannerUrl() As String
            Get
                If _bannerUrl Is Nothing Then
                    Return ""
                End If
                Return _bannerUrl
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _bannerUrl = value
            End Set
        End Property
        <XmlElement>
        Public Property DescriptionFooter() As String
            Get
                If _descriptionFooter Is Nothing Then
                    Return ""
                End If
                Return _descriptionFooter
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _descriptionFooter = value
            End Set
        End Property
        <XmlElement("ExternalDemo")>
        Public ReadOnly Property ExternalDemos() As List(Of ExternalDemoModel)
            Get
                Return _externalDemos
            End Get
        End Property
    End Class
End Namespace
