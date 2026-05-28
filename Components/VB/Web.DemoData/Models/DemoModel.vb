'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Web.Configuration
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    Public Class DemoPageModel
        Inherits DemoModel

        Private _group As DemoGroupModel
        Private _seeAlsoLinks As New List(Of SeeAlsoLinkModel)()
        Private _highlightedDescription As String = String.Empty
        <XmlIgnore>
        Public Property Group() As DemoGroupModel
            Get
                Return _group
            End Get
            Friend Set(ByVal value As DemoGroupModel)
                _group = value
            End Set
        End Property
        <XmlElement("SeeAlso")>
        Public Property SeeAlsoLinks() As List(Of SeeAlsoLinkModel)
            Get
                Return _seeAlsoLinks
            End Get
            Set(ByVal value As List(Of SeeAlsoLinkModel))
                _seeAlsoLinks = value
            End Set
        End Property
        <XmlElement("HighlightedDescription")>
        Public Property HighlightedDescription() As String
            Get
                Return _highlightedDescription
            End Get
            Set(ByVal value As String)
                _highlightedDescription = value
            End Set
        End Property
    End Class
    Public Class DemoModel
        Inherits DemoModelBase

        Private _description As String
        Private _links As String
        Private _metaDescription As String
        Private _hideSourceCode As Boolean
        Private _sourceFiles As New List(Of String)()
        Private _resourceKeys As New List(Of String)()
        Private _highlightedIndex As Integer = -1
        Private _highlightedImageUrl As String
        Private _highlightedTitle As String
        Private _highlightedLink As String
        Private _virtualGroupKey As String

        Private isErrorPage_Renamed As Boolean = False
        Private _product As DemoProductModel
        Private _descriptionProcessed As Boolean
        Private _linksProcessed As Boolean
        Public _sourceFilesPathsProcessed As Boolean
        <XmlIgnore>
        Public Property Product() As DemoProductModel
            Get
                Return _product
            End Get
            Friend Set(ByVal value As DemoProductModel)
                _product = value
            End Set
        End Property
        <XmlIgnore>
        Public Property IsErrorPage() As Boolean
            Get
                Return isErrorPage_Renamed
            End Get
            Set(ByVal value As Boolean)
                isErrorPage_Renamed = value
            End Set
        End Property
        <XmlAttribute>
        Public Overridable Property HideSourceCode() As Boolean
            Get
                Return _hideSourceCode
            End Get
            Set(ByVal value As Boolean)
                _hideSourceCode = value
            End Set
        End Property
        <XmlElement>
        Public Property Description() As String
            Get
                If Not _descriptionProcessed Then
                    ParseLinks()
                    _description = DemoModelParser.ProcessDescription(_description)
                    _descriptionProcessed = True
                End If
                Return DemoModelParser.ProcessPageControlTags(_description)
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _description = value
            End Set
        End Property
        <XmlIgnore>
        Public ReadOnly Property Links() As String
            Get
                If Not _linksProcessed Then
                    ParseLinks()
                End If
                Return _links
            End Get
        End Property
        <XmlElement>
        Public Property MetaDescription() As String
            Get
                If _metaDescription Is Nothing Then
                    Return ""
                End If
                Return _metaDescription
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _metaDescription = value
            End Set
        End Property
        <XmlElement("SourceFile")>
        Public ReadOnly Property SourceFiles() As List(Of String)
            Get
                Return _sourceFiles
            End Get
        End Property
        <XmlElement("ResourceKey")>
        Public ReadOnly Property ResourceKeys() As List(Of String)
            Get
                Return _resourceKeys
            End Get
        End Property
        <XmlAttribute>
        Public Property VirtualGroupKey() As String
            Get
                If _virtualGroupKey Is Nothing Then
                    Return ""
                End If
                Return _virtualGroupKey
            End Get
            Set(ByVal value As String)
                _virtualGroupKey = value
            End Set
        End Property
        <XmlAttribute>
        Public Property HighlightedIndex() As Integer
            Get
                Return _highlightedIndex
            End Get
            Set(ByVal value As Integer)
                _highlightedIndex = value
            End Set
        End Property
        <XmlAttribute>
        Public Property HighlightedImageUrl() As String
            Get
                If _highlightedImageUrl Is Nothing Then
                    Return ""
                End If
                Return _highlightedImageUrl
            End Get
            Set(ByVal value As String)
                _highlightedImageUrl = value
            End Set
        End Property
        <XmlAttribute>
        Public Property HighlightedTitle() As String
            Get
                If _highlightedTitle Is Nothing Then
                    Return ""
                End If
                Return _highlightedTitle
            End Get
            Set(ByVal value As String)
                _highlightedTitle = value
            End Set
        End Property
        <XmlAttribute>
        Public Property HighlightedLink() As String
            Get
                Return _highlightedLink
            End Get
            Set(ByVal value As String)
                _highlightedLink = value
            End Set
        End Property
        Public Function GetHighlightedTitle() As String
            If Not String.IsNullOrEmpty(HighlightedTitle) Then
                Return HighlightedTitle
            End If
            Return Title
        End Function
        Private Sub ParseLinks()
            _links = DemoModelParser.ParseLinks(_description)
            _linksProcessed = True
        End Sub
        Public Function GetSeoTitle() As String
            If Not String.IsNullOrEmpty(SeoTitle) Then
                Return SeoTitle
            End If
            Return Title
        End Function
    End Class
    Public Class SeeAlsoLinkModel
        Private _src As String
        Private _title As String
        <XmlAttribute>
        Public Property Url() As String
            Get
                Return _src
            End Get
            Set(ByVal value As String)
                _src = value
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
