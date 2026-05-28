'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml.Serialization
Namespace DevExpress.Web
    Public Class SearchModel
        Private _synonyms As SynonymsSearchModel
        Private _exclusions As ExclusionsSearchModel
        <XmlElement>
        Public Property Synonyms() As SynonymsSearchModel
            Get
                Return _synonyms
            End Get
            Set(ByVal value As SynonymsSearchModel)
                _synonyms = value
            End Set
        End Property
        <XmlElement>
        Public Property Exclusions() As ExclusionsSearchModel
            Get
                Return _exclusions
            End Get
            Set(ByVal value As ExclusionsSearchModel)
                _exclusions = value
            End Set
        End Property
    End Class
    Public Class ExclusionsSearchModel
        Private _words As String
        Private _prefixes As String
        Private _postfixes As String
        <XmlElement>
        Public Property Words() As String
            Get
                Return _words
            End Get
            Set(ByVal value As String)
                _words = value
            End Set
        End Property
        <XmlElement>
        Public Property Prefixes() As String
            Get
                Return _prefixes
            End Get
            Set(ByVal value As String)
                _prefixes = value
            End Set
        End Property
        <XmlElement>
        Public Property Postfixes() As String
            Get
                Return _postfixes
            End Get
            Set(ByVal value As String)
                _postfixes = value
            End Set
        End Property
    End Class
    Public Class SynonymsSearchModel
        Private _groups As New List(Of String)()
        <XmlElement("Group")>
        Public Property Groups() As List(Of String)
            Get
                Return _groups
            End Get
            Set(ByVal value As List(Of String))
                _groups = value
            End Set
        End Property
    End Class
End Namespace
