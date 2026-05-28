'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports System.Web
Imports System.Web.Configuration
Imports System.Text
Namespace DevExpress.Web.Demos
    Public Class BootstrapDemoSectionModel
        Inherits BootstrapDemoSectionModelBase






        Private code_Renamed, codeASPX_Renamed, codeCS_Renamed, codeVB_Renamed, codeJS_Renamed As String
        <XmlIgnore>
        Public Property Code() As String
            Get
                If String.IsNullOrEmpty(code_Renamed) Then
                    code_Renamed = GetCodeFromFile()
                    code_Renamed = GetPatchedCode(code_Renamed)
                End If
                Return code_Renamed
            End Get
            Set(ByVal value As String)
                code_Renamed = GetPatchedCode(value)
            End Set
        End Property
        <XmlIgnore>
        Public ReadOnly Property CodeASPX() As String
            Get
                If String.IsNullOrEmpty(codeASPX_Renamed) Then
                    codeASPX_Renamed = If((Not String.IsNullOrEmpty(Code)), BootstrapDemoSectionCodeParser.GetFormattedASPXCode(Code), SourceCodeUnavailable)
                End If
                Return codeASPX_Renamed
            End Get
        End Property
        <XmlIgnore>
        Public ReadOnly Property CodeCS() As String
            Get
                If String.IsNullOrEmpty(codeCS_Renamed) Then
                    codeCS_Renamed = If((Not String.IsNullOrEmpty(Code)) AndAlso (Not IsVBCode(Code)), BootstrapDemoSectionCodeParser.GetFormattedCSCode(Code), String.Empty)
                End If
                Return codeCS_Renamed
            End Get
        End Property
        <XmlIgnore>
        Public ReadOnly Property CodeVB() As String
            Get
                If String.IsNullOrEmpty(codeVB_Renamed) Then
                    codeVB_Renamed = If((Not String.IsNullOrEmpty(Code)) AndAlso IsVBCode(Code), BootstrapDemoSectionCodeParser.GetFormattedVBCode(Code), String.Empty)
                End If
                Return codeVB_Renamed
            End Get
        End Property
        <XmlIgnore>
        Public ReadOnly Property CodeJS() As String
            Get
                If String.IsNullOrEmpty(codeJS_Renamed) Then
                    codeJS_Renamed = If((Not String.IsNullOrEmpty(Code)), BootstrapDemoSectionCodeParser.GetFormattedJSCode(Code), String.Empty)
                End If
                Return codeJS_Renamed
            End Get
        End Property
        Public Overrides Function GetSourceFiles() As List(Of BootstrapDemoSectionSourceFileModel)
            Dim regex = New Regex("[\\/]Code[\\/]")
            Return SourceFiles.Select(Function(file) New BootstrapDemoSectionSourceFileModel() With {.Path = file.Path, .CustomTitle = file.CustomTitle}).ToList()
        End Function
        Protected Function IsVBCode(ByVal code As String) As Boolean
            Return BootstrapDemoSectionCodeParser.VBCodeFileRegex.IsMatch(code) OrElse BootstrapDemoSectionCodeParser.VBEndFunctionRegex.IsMatch(code)
        End Function
        Protected Function GetPatchedCode(ByVal code As String) As String
            Dim iFrameDemoUrl = BootstrapDemoSectionCodeParser.FindIFrameDemoUrlInCode(code)
            If Not String.IsNullOrEmpty(iFrameDemoUrl) Then
                code = GetCodeFromFile(iFrameDemoUrl)
            End If
            Return code
        End Function
        Protected Function GetCodeFromFile() As String
            Dim virtualPath As String = BootstrapUtils.GenerateDemoPageUrl(Demo)
            Return GetCodeFromFile(virtualPath, Key)
        End Function
        Public Shared Function GetCodeFromFile(ByVal virtualPath As String, Optional ByVal sectionKey As String = Nothing) As String
            If HttpContext.Current IsNot Nothing Then
                Dim path As String = HttpContext.Current.Server.MapPath(virtualPath)
                Dim content As String = File.ReadAllText(path)
                If sectionKey IsNot Nothing Then
                    content = BootstrapDemoSectionCodeParser.GetSectionCode(content, sectionKey)
                End If
                Return content
            End If
            Return String.Empty
        End Function
    End Class
    Public Class BootstrapDemoSectionCodeParser
        Inherits BootstrapDemoSectionCodeParserBase

        Public Shared ReadOnly RunatServerRegex As New Regex("\s+runat=""server""", RegexOptions.IgnoreCase)
        Public Shared ReadOnly ControlIDRegex As New Regex("\s+ID=""(?<ID>[^""]+)""", RegexOptions.IgnoreCase)
        Public Shared ReadOnly CollapseRegex As New Regex("<%--BeginCollapse--%>(?<Code>.*?)<%--EndCollapse--%>", RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Public Shared ReadOnly HideRegex As New Regex("<%--BeginHide--%>.*?<%--EndHide--%>", RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Public Shared ReadOnly HideRegexCS As New Regex("/\*BeginHide\*/.*?/\*EndHide\*/", RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Public Shared ReadOnly IFrameViewerRegex As New Regex("<demo:IFrameViewer[^>]+DemoUrl=""(?<DemoUrl>[^""]+)""", RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Public Shared ReadOnly VBCodeFileRegex As New Regex("CodeFile =""\w+\.as(p|c)x\.vb""", RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Public Shared ReadOnly VBEndFunctionRegex As New Regex("End Sub", RegexOptions.Singleline Or RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        Public Shared Function GetSectionCode(ByVal pageSourceCode As String, ByVal sectionKey As String) As String
            Dim regex As New Regex("<demo:BootstrapDemoItem[^>]+ID=""" & sectionKey & """[^>]*>(?<Code>.+?)<\/demo:BootstrapDemoItem>", RegexOptions.Multiline Or RegexOptions.Singleline)
            Dim match As Match = regex.Match(pageSourceCode)
            If match.Success Then
                Return match.Groups("Code").Value
            End If
            Return String.Empty
        End Function
        Public Shared Function GetFormattedASPXCode(ByVal code As String) As String
            code = RemoveControlID(code)
            code = ScriptRegex.Replace(code, "")
            code = ReplaceCollapseRegion(code)
            code = ReplaceHideRegion(code)
            code = ReplaceBrackets(code)
            code = TrimCode(code)
            code = FixCodeIndent(code)
            Return code
        End Function
        Public Shared Function GetFormattedCSCode(ByVal code As String) As String
            Return GetFormattedScriptCode(code, True)
        End Function
        Public Shared Function GetFormattedVBCode(ByVal code As String) As String
            Return GetFormattedScriptCode(code, True)
        End Function
        Public Shared Function GetFormattedJSCode(ByVal code As String) As String
            Return GetFormattedScriptCode(code, False)
        End Function
        Private Shared Function GetFormattedScriptCode(ByVal code As String, ByVal serverSide As Boolean) As String
            code = ReplaceCollapseRegion(code)
            code = ReplaceHideRegion(code)
            Dim scripts As New List(Of String)()
            Dim matches As MatchCollection = ScriptRegex.Matches(code)
            For i As Integer = 0 To matches.Count - 1
                Dim script As String = matches(i).Groups("Code").Value
                If serverSide AndAlso RunatServerRegex.IsMatch(matches(i).Value) OrElse (Not serverSide) AndAlso (Not RunatServerRegex.IsMatch(matches(i).Value)) Then
                    scripts.Add(script)
                End If
            Next i
            code = String.Join(ControlChars.CrLf, scripts)
            code = ReplaceBrackets(code)
            code = TrimCode(code)
            code = FixCodeIndent(code)
            Return code
        End Function
        Public Shared Function GetFormattedCustomCode(ByVal code As String) As String
            code = ReplaceCollapseRegion(code)
            code = ReplaceHideRegion(code)
            code = ReplaceHideRegionCS(code)
            code = ReplaceBrackets(code)
            code = TrimCode(code)
            code = FixCodeIndent(code)
            Return code
        End Function
        Public Shared Function FindIFrameDemoUrlInCode(ByVal code As String) As String
            Dim match As Match = IFrameViewerRegex.Match(code)
            If match.Success Then
                Return match.Groups("DemoUrl").Value
            End If
            Return String.Empty
        End Function
        Private Shared Function RemoveControlID(ByVal code As String) As String
            Return ControlIDRegex.Replace(code, Function(match)
                Dim pos As Integer = 0
                Dim id As String = match.Groups("ID").Value
                Do While pos <> -1
                    pos = code.IndexOf(id, pos + id.Length)
                    If pos > 0 AndAlso match.Groups("ID").Index <> pos Then
                        Return match.Value
                    End If
                Loop
                Return ""
            End Function)
        End Function
        Public Shared Overloads Function ReplaceHideRegion(ByVal code As String) As String
            Return ReplaceHideRegion(HideRegex, code)
        End Function
        Public Shared Function ReplaceHideRegionCS(ByVal code As String) As String
            Return ReplaceHideRegion(HideRegexCS, code)
        End Function
        Public Shared Overloads Function ReplaceCollapseRegion(ByVal code As String, Optional ByVal regionType As BootstrapDemoSectionCollapseRegionType = BootstrapDemoSectionCollapseRegionType.Code) As String
            Return ReplaceCollapseRegion(CollapseRegex, code, regionType)
        End Function
    End Class
End Namespace
