'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    Public Class GettingStartedPageModel
        Inherits BootstrapDemoPageModelBase


        Private sections_Renamed As New List(Of GettingStartedSectionModel)()
        <XmlElement(Type := GetType(GettingStartedSectionModel), ElementName := "Section")>
        Public ReadOnly Property Sections() As List(Of GettingStartedSectionModel)
            Get
                Return sections_Renamed
            End Get
        End Property
    End Class
    Public Class GettingStartedSectionModel
        Inherits BootstrapDemoModelBase

        Private Shared ReadOnly CodeRegex As New Regex("(<pre[^>]*?>[\W]*<code[^>]*?>)(.*?)(</code>[\W]*</pre>)", RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        <XmlIgnore>
        Public Overrides Property Key() As String
            Get
                Return Title.ToLowerInvariant().Replace(" "c, "-"c).Replace("'", "")
            End Get
            Set(ByVal value As String)
                MyBase.Key = value
            End Set
        End Property
        Protected Overrides Function ProcessDescription(ByVal text As String) As String
            text = MyBase.ProcessDescription(text)
            text = CodeRegex.Replace(text, AddressOf CodeReplacer)
            Return text
        End Function
        Private Shared Function CodeReplacer(ByVal match As Match) As String
            Return match.Groups(1).Value & match.Groups(2).Value.Trim().Replace("<", "&lt;").Replace(">", "&gt;").Replace("[HL]", "<span class=""code-hl"">").Replace("[/HL]", "</span>") & match.Groups(3).Value
        End Function
    End Class
End Namespace
