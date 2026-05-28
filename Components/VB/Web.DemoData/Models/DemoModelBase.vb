'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    Public Class DemoModelBase
        Inherits ModelBase

        Private _key As String
        Private _title As String
        Private _seoTitle As String
        Private _isNew As Boolean
        Private _isPreview As Boolean
        Private _isUpdated As Boolean
        Private _hideNavItem As Boolean
        <XmlAttribute>
        Public Overridable Property Key() As String
            Get
                If _key Is Nothing Then
                    Return ""
                End If
                Return _key
            End Get
            Set(ByVal value As String)
                _key = value
            End Set
        End Property
        <XmlAttribute>
        Public Property Title() As String
            Get
                If _title Is Nothing Then
                    Return ""
                End If
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property
        <XmlAttribute>
        Public Property HideNavItem() As Boolean
            Get
                Return _hideNavItem
            End Get
            Set(ByVal value As Boolean)
                _hideNavItem = value
            End Set
        End Property
        <XmlAttribute>
        Public Property SeoTitle() As String
            Get
                If _seoTitle Is Nothing Then
                    Return ""
                End If
                Return _seoTitle
            End Get
            Set(ByVal value As String)
                _seoTitle = value
            End Set
        End Property
        <XmlAttribute>
        Public Property IsNew() As Boolean
            Get
                Return _isNew
            End Get
            Set(ByVal value As Boolean)
                _isNew = value
            End Set
        End Property
        <XmlAttribute>
        Public Property IsPreview() As Boolean
            Get
                Return _isPreview
            End Get
            Set(ByVal value As Boolean)
                _isPreview = value
            End Set
        End Property
        <XmlAttribute>
        Public Property IsUpdated() As Boolean
            Get
                Return _isUpdated
            End Get
            Set(ByVal value As Boolean)
                _isUpdated = value
            End Set
        End Property
        Public Overrides Function ToString() As String
            Return Title
        End Function
    End Class
    Public Class ModelBase
        Private _keywords As String
        Private _keywordsRankList As Dictionary(Of String, Integer)
        Private _highlightedTagNames As String
        <XmlAttribute>
        Public Property HighlightedTagNames() As String
            Get
                Return _highlightedTagNames
            End Get
            Set(ByVal value As String)
                _highlightedTagNames = value
            End Set
        End Property
        <XmlElement>
        Public Property Keywords() As String
            Get
                If _keywords Is Nothing Then
                    Return ""
                End If
                Return _keywords
            End Get
            Set(ByVal value As String)
                If value IsNot Nothing Then
                    value = value.Trim()
                End If
                _keywords = value
            End Set
        End Property
        <XmlIgnore>
        Public ReadOnly Property KeywordsRankList() As Dictionary(Of String, Integer)
            Get
                If _keywordsRankList Is Nothing Then
                    _keywordsRankList = GetKeywordsRankList()
                End If
                Return _keywordsRankList
            End Get
        End Property
        Protected Overridable Function GetKeywordsRankList() As Dictionary(Of String, Integer)
            Return SearchUtils.GetKeywordsRankList(Me)
        End Function
        Public Function GetHighlightedTagNames() As String()
            If Not String.IsNullOrEmpty(HighlightedTagNames) Then
                Return HighlightedTagNames.Split()
            End If
            Return New String(){}
        End Function
    End Class
End Namespace
