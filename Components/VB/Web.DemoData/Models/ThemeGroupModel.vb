'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    Public Class ThemeGroupModel
        Inherits ThemeModelBase

        Private _themes As New List(Of ThemeModel)()
        Private _float As String
        <XmlElement(ElementName := "Theme")>
        Public Property Themes() As List(Of ThemeModel)
            Get
                Return _themes
            End Get
            Set(ByVal value As List(Of ThemeModel))
                _themes = value
            End Set
        End Property
        <XmlAttribute>
        Public Property Float() As String
            Get
                Return _float
            End Get
            Set(ByVal value As String)
                _float = value
            End Set
        End Property
    End Class
End Namespace
