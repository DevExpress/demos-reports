'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Linq
Imports System.Collections.Generic
Namespace DevExpress.Web.Demos
    Public NotInheritable Class BootstrapSearchUtils

        Private Sub New()
        End Sub

        Shared Sub New()
            Search = New SearchAgregator(BootstrapDemosModel.Instance.Search)
        End Sub
        Private Shared Property Search() As SearchAgregator
        Public Shared Function DoSearch(ByVal request As String) As List(Of BootstrapSearchResult)
            Dim results = New List(Of BootstrapSearchResult)()
            If Not String.IsNullOrEmpty(request) Then
                Dim requests = Search.SplitRequests(request)
                Try
                    For Each group In BootstrapDemosModel.Instance.SortedDemoGroups
                        results.AddRange(DoSearch(requests, group))
                    Next group
                Catch
                End Try
                results = results.OrderByDescending(Function(sr) sr.Rank).ThenBy(Function(sr) sr.Group.OrderIndex).ToList()
            End If
            Return results
        End Function
        Private Shared Function DoSearch(ByVal requests As List(Of String()), ByVal group As BootstrapDemoGroupPageModel) As IEnumerable(Of BootstrapSearchResult)
            Dim results = New List(Of BootstrapSearchResult)()
            For Each demo In group.Demos
                For Each section In demo.Sections
                    results.AddRange(GetRes(requests, demo, section, SearchAgregator.HighlightOccurences(section.Title, requests)))
                Next section
                If results.Count = 0 Then
                    results.AddRange(GetRes(requests, demo, Nothing, SearchAgregator.HighlightOccurences(demo.Title, requests)))
                End If
            Next demo
            results.AddRange(GetRes(requests, group, Nothing, SearchAgregator.HighlightOccurences(group.Title, requests)))
            Return results
        End Function
        Private Shared Function GetRes(ByVal requests As List(Of String()), ByVal demo As BootstrapDemoPageModelBase, ByVal section As BootstrapDemoSectionModel, ByVal text As String) As IEnumerable(Of BootstrapSearchResult)
            Dim results = New List(Of BootstrapSearchResult)()
            Dim rank = CalculateRank(requests, demo, section)
            If rank > -1 Then
                Dim sr = New BootstrapSearchResult(demo, section, rank)
                sr.Text = text
                results.Add(sr)
            End If
            Return results
        End Function
        Private Shared Function CalculateRank(ByVal requests As List(Of String()), ByVal demo As BootstrapDemoModelBase, ByVal section As BootstrapDemoSectionModel) As Integer
            Dim resultRank As Integer = 0
            Dim keywordRank As Integer = 0
            For Each request In requests
                Dim requestRank As Integer = -1
                If section IsNot Nothing AndAlso TypeOf demo Is BootstrapDemoPageModel AndAlso DirectCast(demo, BootstrapDemoPageModel).Sections.Count > 1 AndAlso SearchAgregator.CalculateRank(request, section.KeywordsRankList, keywordRank) Then
                    requestRank += keywordRank
                End If
                If SearchAgregator.CalculateRank(request, demo.KeywordsRankList, keywordRank) Then
                    requestRank += keywordRank
                End If
                If TypeOf demo Is BootstrapDemoGroupPageModel AndAlso SearchAgregator.CalculateRank(request, DirectCast(demo, BootstrapDemoGroupPageModel).KeywordsRankList, keywordRank) Then
                    requestRank += keywordRank
                End If
                If requestRank = -1 AndAlso Search.WordsExclusions.Any(Function(re) re.Equals(request(0), SearchAgregator.DefaultStringComparison)) Then
                    requestRank = 0
                End If
                If requestRank > -1 Then
                    resultRank += requestRank
                Else
                    Return -1
                End If
            Next request
            Return resultRank
        End Function
        Public Shared Function GetKeywordsRankList(ByVal model As ModelBase) As Dictionary(Of String, Integer)
            Dim textRanks As New List(Of TextRank)() From {New TextRank(model.Keywords, 3)}
            Dim group = TryCast(model, BootstrapDemoGroupPageModel)
            Dim demo = TryCast(model, BootstrapDemoPageModel)
            Dim section = TryCast(model, BootstrapDemoSectionModel)
            If group IsNot Nothing Then
                textRanks.Add(New TextRank(group.Title, 15))
                textRanks.Add(New TextRank(group.Key, 7))
                textRanks.Add(New TextRank(group.SeoTitle, 5))
                textRanks.Add(New TextRank(group.Links, 2))
            ElseIf demo IsNot Nothing Then
                textRanks.Add(New TextRank(demo.Title, 5))
                textRanks.Add(New TextRank(demo.Key, 3))
                textRanks.Add(New TextRank(demo.SeoTitle, 2))
                textRanks.Add(New TextRank(demo.Links, 2))
            ElseIf section IsNot Nothing Then
                textRanks.Add(New TextRank(section.Title, 5))
                textRanks.Add(New TextRank(section.Key, 3))
                textRanks.Add(New TextRank(section.SeoTitle, 2))
                textRanks.Add(New TextRank(section.Links, 2))
            End If
            Return Search.GetKeywordsRankList(textRanks)
        End Function
    End Class
    Public Class BootstrapSearchResult
        Implements IComparable(Of BootstrapSearchResult)

        Public Sub New(ByVal demo As BootstrapDemoPageModelBase, ByVal section As BootstrapDemoSectionModel, ByVal rank As Integer)
            Me.Demo = demo
            Me.Rank = rank
            Me.Section = section
        End Sub
        Public Property Section() As BootstrapDemoSectionModel
        Public Property Demo() As BootstrapDemoPageModelBase
        Public ReadOnly Property Group() As BootstrapDemoGroupPageModel
            Get
                If TypeOf Demo Is BootstrapDemoGroupPageModel Then
                    Return DirectCast(Demo, BootstrapDemoGroupPageModel)
                End If
                If TypeOf Demo Is BootstrapDemoPageModel Then
                    Return DirectCast(Demo, BootstrapDemoPageModel).Group
                End If
                Return Nothing
            End Get
        End Property
        Public Property Text() As String
        Public Property Rank() As Integer
        Private Function IComparableGeneric_CompareTo(ByVal other As BootstrapSearchResult) As Integer Implements IComparable(Of BootstrapSearchResult).CompareTo
            Return other.Rank.CompareTo(Rank)
        End Function
    End Class
End Namespace
