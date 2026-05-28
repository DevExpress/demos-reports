'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports System.Text
Namespace DevExpress.Web.Demos
    Public Class BootstrapDemoSectionModelBase
        Inherits BootstrapDemoModelBase

        Public Const SourceCodeUnavailable As String = "The source code is unavailable."
        Public Sub New()
            SourceFiles = New List(Of BootstrapDemoSectionSourceFileModel)()
        End Sub
        Private privateSourceFiles As List(Of BootstrapDemoSectionSourceFileModel)
        <XmlElement("SourceFile")>
        Public Property SourceFiles() As List(Of BootstrapDemoSectionSourceFileModel)
            Get
                Return privateSourceFiles
            End Get
            Private Set(ByVal value As List(Of BootstrapDemoSectionSourceFileModel))
                privateSourceFiles = value
            End Set
        End Property
        Private privateDemo As BootstrapDemoPageModel
        <XmlIgnore>
        Public Property Demo() As BootstrapDemoPageModel
            Get
                Return privateDemo
            End Get
            Friend Set(ByVal value As BootstrapDemoPageModel)
                privateDemo = value
            End Set
        End Property
        Public Overridable Function GetSourceFiles() As List(Of BootstrapDemoSectionSourceFileModel)
            Return SourceFiles
        End Function
    End Class
    Public Class BootstrapDemoSectionSourceFileModel
        Implements IEquatable(Of BootstrapDemoSectionSourceFileModel)

        <XmlText>
        Public Property Path() As String
        <XmlAttribute("Title")>
        Public Property CustomTitle() As String
        <XmlIgnore>
        Public Property Content() As String
        Public Overloads Function Equals(ByVal other As BootstrapDemoSectionSourceFileModel) As Boolean Implements IEquatable(Of BootstrapDemoSectionSourceFileModel).Equals
            Return other.Path.Equals(Path)
        End Function
        Public Overrides Function GetHashCode() As Integer
            Return Path.GetHashCode()
        End Function
    End Class
    Public Enum BootstrapDemoSectionCollapseRegionType
        Code
        Description
    End Enum
    Public Class BootstrapDemoSectionCodeParserBase
        Public Shared ReadOnly ScriptRegex As New Regex("<script[^>]*?>(?<Code>.*?)</script>", RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Public Shared Function TrimCode(ByVal code As String) As String
            Dim lines() As String = code.Split(New String() { ControlChars.CrLf, ControlChars.Lf }, StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To lines.Length - 1
                lines(i) = lines(i).TrimEnd(" "c, ControlChars.Tab)
            Next i
            Return String.Join(ControlChars.CrLf, lines).Trim(ControlChars.Cr, ControlChars.Lf)
        End Function
        Public Shared Function ReplaceBrackets(ByVal code As String) As String
            Dim stb As New StringBuilder()
            Dim skipRegion As Boolean = False
            For i As Integer = 0 To code.Length - 1
                If code.Chars(i) = "<"c Then
                    If i + 3 < code.Length AndAlso code.Chars(i + 1) = "%"c AndAlso code.Chars(i + 2) = "-"c AndAlso code.Chars(i + 3) = "-"c Then
                        skipRegion = True
                        i += 3
                    ElseIf Not skipRegion Then
                        stb.Append("&lt;")
                    Else
                        stb.Append(code.Chars(i))
                    End If
                ElseIf code.Chars(i) = ">"c Then
                    If i - 3 >= 0 AndAlso code.Chars(i - 1) = "%"c AndAlso code.Chars(i - 2) = "-"c AndAlso code.Chars(i - 3) = "-"c Then
                        skipRegion = False
                        stb.Remove(stb.Length - 3, 3)
                    ElseIf Not skipRegion Then
                        stb.Append("&gt;")
                    Else
                        stb.Append(code.Chars(i))
                    End If
                Else
                    stb.Append(code.Chars(i))
                End If
            Next i
            Return stb.ToString()
        End Function
        Public Shared Function GetCollapseRegionHtml(ByVal code As String, Optional ByVal regionType As BootstrapDemoSectionCollapseRegionType = BootstrapDemoSectionCollapseRegionType.Code) As String
            Dim startComment As String = If(regionType = BootstrapDemoSectionCollapseRegionType.Code, "<%--", "")
            Dim endComment As String = If(regionType = BootstrapDemoSectionCollapseRegionType.Code, "--%>", "")
            Dim cssClass As String = If(regionType = BootstrapDemoSectionCollapseRegionType.Code, "more-code", "more-description")
            Dim wrapperTag As String = If(regionType = BootstrapDemoSectionCollapseRegionType.Code, "span", "div")
            Dim btnCssClass As String = If(regionType = BootstrapDemoSectionCollapseRegionType.Code, "btn btn-secondary", "btn btn-link")
            Dim coverHtml As String = If(regionType = BootstrapDemoSectionCollapseRegionType.Code, "", "<span class=""more-description-cover""></span>")
            Return code.Trim(" "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf)
        End Function
        Public Shared Function FixCodeIndent(ByVal code As String) As String
            Dim lines() As String = code.Split(New String() { ControlChars.CrLf, ControlChars.Lf }, StringSplitOptions.RemoveEmptyEntries)
            If lines.Length > 1 Then
                Dim whiteSpaceCount As Integer = 0
                Do While lines(0).Length > whiteSpaceCount AndAlso (lines(0).Chars(whiteSpaceCount) = " "c OrElse lines(0).Chars(whiteSpaceCount) = ControlChars.Tab)
                    whiteSpaceCount += 1
                Loop
                If whiteSpaceCount > 0 Then
                    Dim whiteSpaces = lines(0).Substring(0, whiteSpaceCount)
                    For i As Integer = 0 To lines.Length - 1
                        If lines(i).StartsWith(whiteSpaces) Then
                            lines(i) = lines(i).Substring(whiteSpaceCount)
                        End If
                    Next i
                End If
            End If
            Return String.Join(ControlChars.CrLf, lines)
        End Function
        Public Shared Function ReplaceHideRegion(ByVal regex As Regex, ByVal code As String) As String
            Return regex.Replace(code, "")
        End Function
        Public Shared Function ReplaceCollapseRegion(ByVal regex As Regex, ByVal code As String, Optional ByVal regionType As BootstrapDemoSectionCollapseRegionType = BootstrapDemoSectionCollapseRegionType.Code) As String
            Return regex.Replace(code, Function(match) GetCollapseRegionHtml(match.Groups("Code").Value, regionType))
        End Function
    End Class
End Namespace
