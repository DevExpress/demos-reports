'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic
Namespace DevExpress.Web.Demos
    Public Class ThemeModelBase
        Private _name, _title As String
        Private _isGroup As Boolean
        <XmlAttribute>
        Public Property Name() As String
            Get
                If _name Is Nothing Then
                    Return ""
                End If
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
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
        Public Property IsGroup() As Boolean
            Get
                Return _isGroup
            End Get
            Set(ByVal value As Boolean)
                _isGroup = value
            End Set
        End Property
    End Class
End Namespace
