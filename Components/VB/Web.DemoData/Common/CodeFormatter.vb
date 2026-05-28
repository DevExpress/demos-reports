'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.CodeParser
Imports DevExpress.CodeParser.CSharp
Imports DevExpress.CodeParser.Html
Imports DevExpress.CodeParser.VB
Imports DevExpress.CodeParser.JavaScript
Imports DevExpress.CodeParser.Xml
Imports DevExpress.CodeParser.Css
Imports System.Text
Namespace DevExpress.Web.Demos
    Public NotInheritable Class CodeFormatter

        Private Sub New()
        End Sub

        Private Const StartHighlightedCodeBlockMarker As String = "<%--start highlighted block--%>"
        Private Const EndHighlightedCodeBlockMarker As String = "<%--end highlighted block--%>"
        Private Const StartHighlightedDivElement As String = "<div class=""HighlightedBlock"">"
        Private Const StartNotHighlightedDivElement As String = "<div class=""NotHighlightedBlock"">"
        Private Const StartHighlightedMinorElement As String = "<div class=""HighlightedMinorBlock"">"
        Private Shared _cssClasses As New Dictionary(Of TokenCategory, TokenCategoryClassProvider)()
        Shared Sub New()
            CssClasses.Add(TokenCategory.Text, New TokenCategoryClassProvider("cr-text", New KeyValuePair(Of TokenLanguage, String)(TokenLanguage.Html, "cr-text-html")))
            CssClasses.Add(TokenCategory.Keyword, New TokenCategoryClassProvider("cr-keyword", New KeyValuePair(Of TokenLanguage, String)(TokenLanguage.Html, "cr-keyword-html"), New KeyValuePair(Of TokenLanguage, String)(TokenLanguage.Css, "cr-keyword-css")))
            CssClasses.Add(TokenCategory.Operator, New TokenCategoryClassProvider("cr-operator"))
            CssClasses.Add(TokenCategory.PreprocessorKeyword, New TokenCategoryClassProvider("cr-preproc", New KeyValuePair(Of TokenLanguage, String)(TokenLanguage.Html, "cr-preproc-html")))
            CssClasses.Add(TokenCategory.String, New TokenCategoryClassProvider("cr-string", New KeyValuePair(Of TokenLanguage, String)(TokenLanguage.Html, "cr-string-html"), New KeyValuePair(Of TokenLanguage, String)(TokenLanguage.Css, "cr-string-css")))
            CssClasses.Add(TokenCategory.Number, New TokenCategoryClassProvider("cr-number"))
            CssClasses.Add(TokenCategory.Identifier, New TokenCategoryClassProvider("cr-identifier"))
            CssClasses.Add(TokenCategory.HtmlServerSideScript, New TokenCategoryClassProvider("cr-htmlserverscript"))
            CssClasses.Add(TokenCategory.HtmlString, New TokenCategoryClassProvider("cr-htmlstring"))
            CssClasses.Add(TokenCategory.Unknown, New TokenCategoryClassProvider("cr-unknown"))
            CssClasses.Add(TokenCategory.Comment, New TokenCategoryClassProvider("cr-comment"))
            CssClasses.Add(TokenCategory.XmlComment, New TokenCategoryClassProvider("cr-xmlcomment"))
            CssClasses.Add(TokenCategory.CssComment, New TokenCategoryClassProvider("cr-csscomment"))
            CssClasses.Add(TokenCategory.CssKeyword, New TokenCategoryClassProvider("cr-csskeyword"))
            CssClasses.Add(TokenCategory.CssPropertyName, New TokenCategoryClassProvider("cr-csspropertyname"))
            CssClasses.Add(TokenCategory.CssPropertyValue, New TokenCategoryClassProvider("cr-csspropertyvalue"))
            CssClasses.Add(TokenCategory.CssSelector, New TokenCategoryClassProvider("cr-cssselector"))
            CssClasses.Add(TokenCategory.CssStringValue, New TokenCategoryClassProvider("cr-cssstringvalue"))
            CssClasses.Add(TokenCategory.HtmlElementName, New TokenCategoryClassProvider("cr-htmlelementname"))
            CssClasses.Add(TokenCategory.HtmlEntity, New TokenCategoryClassProvider("cr-htmlentity"))
            CssClasses.Add(TokenCategory.HtmlOperator, New TokenCategoryClassProvider("cr-htmloperator"))
            CssClasses.Add(TokenCategory.HtmlComment, New TokenCategoryClassProvider("cr-htmlcomment"))
            CssClasses.Add(TokenCategory.HtmlAttributeName, New TokenCategoryClassProvider("cr-htmlattributename"))
            CssClasses.Add(TokenCategory.HtmlAttributeValue, New TokenCategoryClassProvider("cr-htmlattributevalue"))
            CssClasses.Add(TokenCategory.HtmlTagDelimiter, New TokenCategoryClassProvider("cr-htmltagdelimiter"))
        End Sub
        Public Shared ReadOnly Property CssClasses() As Dictionary(Of TokenCategory, TokenCategoryClassProvider)
            Get
                Return _cssClasses
            End Get
        End Property
        Public Shared Function ParseLanguage(ByVal lang As String) As TokenLanguage
            Return DirectCast(System.Enum.Parse(GetType(TokenLanguage), lang, True), TokenLanguage)
        End Function
        Public Shared Function GetLanguageByFileExtension(ByVal extension As String) As TokenLanguage
            Select Case extension.ToLower()
                Case ".cs"
                    Return TokenLanguage.CSharp
                Case ".vb"
                    Return TokenLanguage.Basic
                Case ".html", ".htm", ".aspx", ".ascx", ".asax", ".master", ".cshtml"
                    Return TokenLanguage.Html
                Case ".js"
                    Return TokenLanguage.JavaScript
                Case ".xml"
                    Return TokenLanguage.Xml
                Case ".css"
                    Return TokenLanguage.Css
                Case Else
                    Return TokenLanguage.Unknown
            End Select
        End Function
        Public Shared Function GetFormattedCode(ByVal fileExtension As String, ByVal code As String) As String
            Return GetFormattedCode(GetLanguageByFileExtension(fileExtension), code, False, New String(){})
        End Function
        Public Shared Function GetFormattedCode(ByVal fileExtension As String, ByVal code As String, ByVal isMvc As Boolean) As String
            Return GetFormattedCode(GetLanguageByFileExtension(fileExtension), code, isMvc, New String(){})
        End Function
        Public Shared Function GetFormattedCode(ByVal fileExtension As String, ByVal code As String, ByVal isMvc As Boolean, ByVal highlightedTagNames() As String) As String
            Return GetFormattedCode(GetLanguageByFileExtension(fileExtension), code, isMvc, highlightedTagNames)
        End Function
        Public Shared Function GetFormattedCode(ByVal language As TokenLanguage, ByVal code As String) As String
            Return GetFormattedCode(language, code, False, New String(){})
        End Function
        Public Shared Function GetFormattedCode(ByVal language As TokenLanguage, ByVal code As String, ByVal isMvc As Boolean) As String
            Return GetFormattedCode(language, code, isMvc, New String(){})
        End Function
        Public Shared Function GetFormattedCode(ByVal language As TokenLanguage, ByVal code As String, ByVal isMvc As Boolean, ByVal highlightedTagNames() As String) As String
            Dim tokens As DevExpress.CodeParser.TokenCollection = GetTokens(language, code, isMvc)
            If tokens IsNot Nothing Then
                Return GetFormattedCode(language, code, tokens, highlightedTagNames)
            End If
            Return String.Empty
        End Function
        Private Class CodeLine
            Public Indent As Integer
            Public Html As String = ""
            Public ReadOnly Property IsEmpty() As Boolean
                Get
                    Return Html.Trim().Length < 1
                End Get
            End Property
        End Class
        Private Shared Function ContainsTokenInTagNames(ByVal tokenValue As String, ByVal highlightedTagNames() As String) As Boolean
            For Each tagName As String In highlightedTagNames
                If tokenValue = tagName Then
                    Return True
                End If
            Next tagName
            Return False
        End Function
        Private Shared Sub StartNotHighlightedBlock(ByVal lines As List(Of CodeLine))
            If lines.Count = 0 Then
                lines.Add(New CodeLine With {.Html = StartNotHighlightedDivElement})
            Else
                lines(lines.Count - 1).Html &= StartNotHighlightedDivElement
            End If
        End Sub
        Private Shared Sub EndNotHighlightedBlock(ByRef currentLine As CodeLine)
            currentLine.Html &= "</div>"
        End Sub
        Private Shared Sub StartHighlightedBlockAndCloseNotHighlighted(ByVal lines As List(Of CodeLine), ByVal isComment As Boolean)
            lines(lines.Count - 1).Html &= "</div>" & (If(isComment, StartHighlightedMinorElement, StartHighlightedDivElement))
        End Sub
        Private Shared Sub EndHighlightedBlockAndStartNotHighlighted(ByRef currentLine As CodeLine)
            currentLine.Html &= "</div>" & StartNotHighlightedDivElement
        End Sub
        Private Shared Function HasHighlightedTagsOnPage(ByVal tokens As DevExpress.CodeParser.TokenCollection, ByVal highlightedTagNames() As String) As Boolean
            For Each token As CategorizedToken In tokens
                If ContainsTokenInTagNames(token.Value, highlightedTagNames) Then
                    Return True
                End If
            Next token
            Return False
        End Function
        Private Shared Function IsComplexTag(ByVal tokens As DevExpress.CodeParser.TokenCollection, ByVal index As Integer) As Boolean
            For i As Integer = index To tokens.Count - 1
                If tokens(i).Value = "/>" Then
                    Return False
                End If
                If tokens(i).Value = ">" Then
                    Return True
                End If
            Next i
            Return True
        End Function
        Private Shared Function HasHighlightedCodeBlockMarker(ByVal code As String) As Boolean
            Return code.Contains(StartHighlightedCodeBlockMarker)
        End Function
        Private Shared Function GetFormattedCode(ByVal language As TokenLanguage, ByVal code As String, ByVal tokens As DevExpress.CodeParser.TokenCollection, ByVal highlightedTagNames() As String) As String
            If CInt((language)) = CInt((TokenLanguage.Html)) AndAlso highlightedTagNames.Length <> 0 AndAlso HasHighlightedTagsOnPage(tokens, highlightedTagNames) OrElse HasHighlightedCodeBlockMarker(code) Then
                Return GetFormattedAspxCode(code, tokens, highlightedTagNames)
            Else
                Return GetFormattedOtherCode(code, tokens, highlightedTagNames)
            End If
        End Function
        Private Shared Function GetFormattedAspxCode(ByVal code As String, ByVal tokens As DevExpress.CodeParser.TokenCollection, ByVal highlightedTagNames() As String) As String
            Dim position As Integer = 0
            Dim highlightedTagsCount As Integer = 0
            Dim needCloseHighlightedBlock As Boolean = False
            Dim isStartedHighlightedComment As Boolean = False
            Dim thisTagIsComplex As Boolean = True
            Dim currentLine As New CodeLine()
            Dim lines As New List(Of CodeLine)()
            StartNotHighlightedBlock(lines)
            For i As Integer = 0 To tokens.Count - 1
                Dim token As CategorizedToken = TryCast(tokens(i), CategorizedToken)
                If token.Value = StartHighlightedCodeBlockMarker Then
                    isStartedHighlightedComment = True
                    position = token.EndPosition
                    Continue For
                End If
                If ContainsTokenInTagNames(token.Value, highlightedTagNames) Then
                    If tokens(i - 1).Value = "<" AndAlso tokens(i + 1).Value <> ">" Then
                        highlightedTagsCount += 1
                        thisTagIsComplex = IsComplexTag(tokens, i)
                    End If
                    If highlightedTagsCount > 0 Then
                        If highlightedTagsCount = 1 AndAlso tokens(i + 1).Value <> ">" AndAlso lines.Count > 0 Then
                            StartHighlightedBlockAndCloseNotHighlighted(lines, False)
                        End If
                        AppendCodes(lines, currentLine, code, position, token)
                        If tokens(i - 1).Value = "</" AndAlso tokens(i + 1).Value = ">" Then
                            highlightedTagsCount -= 1
                            needCloseHighlightedBlock = highlightedTagsCount = 0
                        End If
                    End If
                Else
                    If token.Value <> StartHighlightedCodeBlockMarker AndAlso token.Value <> EndHighlightedCodeBlockMarker Then
                        AppendCodes(lines, currentLine, code, position, token)
                    End If
                    If lines.Count > 0 AndAlso isStartedHighlightedComment Then
                        StartHighlightedBlockAndCloseNotHighlighted(lines, True)
                        isStartedHighlightedComment = False
                    End If
                    If (Not thisTagIsComplex) AndAlso token.Value = "/>" OrElse needCloseHighlightedBlock OrElse token.Value = EndHighlightedCodeBlockMarker Then
                        EndHighlightedBlockAndStartNotHighlighted(currentLine)
                    End If
                    If (Not thisTagIsComplex) AndAlso token.Value = "/>" Then
                        If highlightedTagsCount > 0 Then
                            highlightedTagsCount -= 1
                        End If
                    End If
                    If needCloseHighlightedBlock Then
                        needCloseHighlightedBlock = False
                    End If
                End If
                position = token.EndPosition
            Next i
            AppendCode(lines, currentLine, code.Substring(position), Nothing)
            EndNotHighlightedBlock(currentLine)
            lines.Add(currentLine)
            Return MergeCodeLines(lines)
        End Function
        Private Shared Function GetFormattedOtherCode(ByVal code As String, ByVal tokens As DevExpress.CodeParser.TokenCollection, ByVal highlightedTagNames() As String) As String
            Dim currentLine As New CodeLine()
            Dim lines As New List(Of CodeLine)()
            Dim position As Integer = 0
            For Each token As CategorizedToken In tokens
                AppendCode(lines, currentLine, code.Substring(position, token.StartPosition - position), Nothing)
                AppendCode(lines, currentLine, token.Value, CssClasses(token.Category).GetClassName(token.Language))
                position = token.EndPosition
            Next token
            AppendCode(lines, currentLine, code.Substring(position), Nothing)
            lines.Add(currentLine)
            Return MergeCodeLines(lines)
        End Function
        Private Shared Sub AppendCodes(ByVal lines As List(Of CodeLine), ByRef currentLine As CodeLine, ByVal code As String, ByVal pos As Integer, ByVal token As CategorizedToken)
            AppendCode(lines, currentLine, code.Substring(pos, token.StartPosition - pos), Nothing)
            AppendCode(lines, currentLine, token.Value, CssClasses(token.Category).GetClassName(token.Language))
        End Sub
        Private Shared Sub AppendCode(ByVal lines As List(Of CodeLine), ByRef currentLine As CodeLine, ByVal code As String, ByVal cssClass As String)
            Dim hasCss As Boolean = Not String.IsNullOrEmpty(cssClass)
            Dim first As Boolean = True
            code = code.Replace(ControlChars.Cr, "").Replace(ControlChars.Tab, "    ")
            For Each line As String In code.Split(ControlChars.Lf)
                Dim text As String = line
                If Not first Then
                    lines.Add(currentLine)
                    currentLine = New CodeLine()
                    text = text.TrimStart()
                    currentLine.Indent = line.Length - text.Length
                End If
                If first OrElse text.Trim().Length > 0 Then
                    If hasCss Then
                        currentLine.Html &= String.Format("<span class=""{0}"">", cssClass)
                    End If
                    currentLine.Html &= HttpUtility.HtmlEncode(text)
                    If hasCss Then
                        currentLine.Html &= "</span>"
                    End If
                End If
                first = False
            Next line
        End Sub
        Private Shared Function MergeCodeLines(ByVal lines As List(Of CodeLine)) As String
            Dim minIndent As Integer = Integer.MaxValue
            For Each line As CodeLine In lines
                If line.IsEmpty Then
                    Continue For
                End If
                If line.Indent < minIndent Then
                    minIndent = line.Indent
                End If
            Next line
            Dim result As New StringBuilder()
            Dim emptyLineCount As Integer = 0
            For Each line As CodeLine In lines
                If line.IsEmpty Then
                    If result.Length > 0 Then
                        emptyLineCount += 1
                    End If
                    Continue For
                End If
                If emptyLineCount > 0 Then
                    Dim i As Integer = 0
                    Do While i < emptyLineCount
                        result.Append("<br />")
                        i += 1
                    Loop
                    emptyLineCount = 0
                End If
                Dim indent As Integer = line.Indent - minIndent
                For i As Integer = 0 To indent - 1
                    result.Append("&nbsp;")
                Next i
                result.Append(line.Html)
                If (Not line.Html.Contains(StartHighlightedDivElement)) AndAlso (Not line.Html.Contains("</div>")) AndAlso result.Length > 0 Then
                    result.Append("<br />")
                End If
            Next line
            Return result.ToString().Trim()
        End Function
        Private Shared Function GetTokens(ByVal language As TokenLanguage, ByVal code As String, ByVal isMvc As Boolean) As DevExpress.CodeParser.TokenCollection
            Select Case language
                Case TokenLanguage.CSharp
                    Return CSharpTokensHelper.GetTokens(code)
                Case TokenLanguage.Basic
                    Return VBTokensHelper.GetTokens(code)
                Case TokenLanguage.JavaScript
                    Return JavaScriptTokensHelper.GetTokens(code)
                Case TokenLanguage.Html
                    If Not isMvc Then
                        Return HtmlTokensHelper.GetTokens(code)
                    Else
                        Return HtmlTokensHelper.GetTokens(code, LanguageKind.Razor, DotNetLanguageType.CSharp)
                    End If
                Case TokenLanguage.Xml
                    Return (New XmlTokensCategoryHelper()).GetTokens(code)
                Case TokenLanguage.Css
                    Return (New CssTokensCategoryHelper()).GetTokens(code)
                Case Else
                    Return Nothing
            End Select
        End Function
    End Class
    Public Class TokenCategoryClassProvider
        Private className As String
        Private languagesClassNames As New Dictionary(Of TokenLanguage, String)()
        Public Sub New(ByVal className As String)
            Me.New(className, Nothing)
        End Sub
        Public Sub New(ByVal className As String, ParamArray ByVal languagesClassNames() As KeyValuePair(Of TokenLanguage, String))
            Me.className = className
            If languagesClassNames IsNot Nothing Then
                For Each languageClassName As KeyValuePair(Of TokenLanguage, String) In languagesClassNames
                    Me.languagesClassNames(languageClassName.Key) = languageClassName.Value
                Next languageClassName
            End If
        End Sub
        Public Function GetClassName(ByVal language As TokenLanguage) As String
            If Me.languagesClassNames.ContainsKey(language) Then
                Return Me.languagesClassNames(language)
            End If
            Return Me.className
        End Function
    End Class
End Namespace
