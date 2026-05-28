'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    Public Class ExternalDemoModel
        Private _imageUrl As String
        Private _url As String
        Private _title As String
        <XmlAttribute>
        Public Property ImageUrl() As String
            Get
                Return _imageUrl
            End Get
            Set(ByVal value As String)
                _imageUrl = value
            End Set
        End Property
        <XmlAttribute>
        Public Property Url() As String
            Get
                Return _url
            End Get
            Set(ByVal value As String)
                _url = value
            End Set
        End Property
        <XmlAttribute>
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property
    End Class
End Namespace
