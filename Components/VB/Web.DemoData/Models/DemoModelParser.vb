'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports DevExpress.Web.Internal
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls
Namespace DevExpress.Web.Demos
    Public NotInheritable Class DemoModelParser

        Private Sub New()
        End Sub

        Private Shared HelpLinkRegex As New Regex("<helplink([^>]*)>(.*?)</helplink>", RegexOptions.Singleline)
        Private Shared AttributesRegex As New Regex("(\S+)=[""']{1}((?:.(?![""']?\s+(?:\S+)|[>""']))+.)[""']{1}")
        Private Shared LinkGroupName As String = "content"
        Private Shared HelpLinkContentRegex As New Regex(String.Format("(<helplink([^>]*)>)(?<{0}>.+?)(</helplink>)", LinkGroupName))
        Public Shared Function ParseLinks(ByVal text As String) As String
            If text Is Nothing Then
                Return String.Empty
            End If
            Dim matches = HelpLinkContentRegex.Matches(text)
            Dim result = New List(Of String)()
            For Each match As Match In matches
                Dim group = match.Groups(LinkGroupName)
                If group.Success Then
                    result.Add(group.Value)
                End If
            Next match
            Return String.Join(" ", result.Distinct())
        End Function
        Public Shared Function ProcessDescription(ByVal text As String) As String
            If text Is Nothing Then
                text = ""
            End If
            text = Regex.Replace(text, "<code\s+lang=([^>]+)>(.*?)</code>", AddressOf DescriptionCodeReplacer, RegexOptions.Singleline)
            text = HelpLinkRegex.Replace(text, AddressOf DescriptionHelpLinkReplacer)
            text = BootstrapDemoSectionCodeParser.ReplaceCollapseRegion(text, BootstrapDemoSectionCollapseRegionType.Description)
            Return text
        End Function
        Private Shared Function DescriptionHelpLinkReplacer(ByVal match As Match) As String
            Dim attributes As New Dictionary(Of String, String)()
            Dim attrMatches = AttributesRegex.Matches(match.Groups(1).Value)
            For Each am As Match In attrMatches
                attributes(am.Groups(1).Value) = am.Groups(2).Value
            Next am
            If Not attributes.ContainsKey("href") Then
                attributes("href") = "https://help.devexpress.com/"
            End If
'#pragma warning disable DX0025 ' not a path (and no traversal here)
            Return String.Format("<a href=""{0}"" class=""{1}"">{2}</a>", attributes("href"), "helplink", match.Groups(2).Value)
'#pragma warning restore DX0025
        End Function
        Public Shared Function ProcessPageControlTags(ByVal text As String) As String
            If text Is Nothing Then
                text = ""
            End If
            Return If(text.Contains("<pageControl>"), Regex.Replace(text, "<pageControl>(.*?)</pageControl>", AddressOf DescriptionPageControlReplacer, RegexOptions.Singleline), text)
        End Function
        Private Shared Function DescriptionCodeReplacer(ByVal match As Match) As String
            Dim lang As String = match.Groups(1).Value.Trim(""""c, "'"c)
            Dim code As String = match.Groups(2).Value
'#pragma warning disable DX0025 ' not a path (and no traversal here)
            Return "<code>" & CodeFormatter.GetFormattedCode(CodeFormatter.ParseLanguage(lang), code, Utils.IsMvc, New String(){}) & "<br /></code>"
'#pragma warning restore DX0025
        End Function
        Private Shared Function DescriptionPageControlReplacer(ByVal match As Match) As String
            Dim tabPages As MatchCollection = Regex.Matches(match.Value, "<tabPage\s+text=([^>]+)>(.*?)</tabPage>", RegexOptions.Singleline)
            If tabPages.Count = 0 Then
                Return match.Groups(1).Value
            End If
            Dim pageControl As New ASPxPageControl()
            pageControl.ID = "OverviewPageControl"
            pageControl.CssClass = "DemoPageControl"
            pageControl.EnableTabScrolling = True
            pageControl.TabAlign = TabAlign.Justify
            pageControl.EnableTheming = False
            pageControl.ActiveTabIndex = 0
            pageControl.EnableViewState = False
            pageControl.Width = Unit.Percentage(100)
            For Each tabPage As Match In tabPages
                Dim text As String = tabPage.Groups(1).Value.Trim(""""c, "'"c)
                Dim tab = pageControl.TabPages.Add(text, text)
                tab.Controls.Add(RenderUtils.CreateLiteralControl(tabPage.Groups(2).Value))
            Next tabPage
            Return RenderUtils.GetRenderResult(pageControl)
        End Function
    End Class
End Namespace
