'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Namespace DevExpress.Web.Demos
    Public NotInheritable Class SearchUtils

        Private Sub New()
        End Sub

        Shared Sub New()
            Search = New SearchAgregator(DemosModel.Instance.Search)
        End Sub
        Private Shared Property Search() As SearchAgregator
        Public Shared Function DoSearch(ByVal request As String) As List(Of SearchResult)
            Dim results = New List(Of SearchResult)()
            If Not String.IsNullOrEmpty(request) Then
                Dim requests = Search.SplitRequests(request)
                Try
                    Dim products = DemosModel.Instance.SortedDemoProducts.Where(Function(dp) (Not dp.IsRootDemo) AndAlso (dp Is DemosModel.Current OrElse ((Not dp.HideNavItem) AndAlso Utils.IsSiteMode)))
                    For Each product In products
                        results.AddRange(DoSearch(requests, product))
                    Next product
                Catch
                End Try
                results = results.OrderByDescending(Function(sr) sr.Rank).ThenBy(Function(sr) sr.Product.OrderIndex).ToList()
            End If
            Return results
        End Function
        Private Shared Function DoSearch(ByVal requests As List(Of String()), ByVal product As DemoProductModel) As IEnumerable(Of SearchResult)
            Dim results = New List(Of SearchResult)()
            If product.Intro IsNot Nothing Then
                results.AddRange(GetRes(requests, product.Intro, String.Format("{0}", SearchAgregator.HighlightOccurences(product.NavItemTitle, requests, True)), product.Intro.Product.Title.ToUpper()))
            End If
            For Each group In product.GetVisibleGroups()
                For Each demo In group.GetVisibleDemos()
                    results.AddRange(GetRes(requests, demo, String.Format("{0}", SearchAgregator.HighlightOccurences(demo.Title, requests, True)), demo.Product.Title.ToUpper()))
                Next demo
            Next group
            Return results
        End Function
        Private Shared Function GetRes(ByVal requests As List(Of String()), ByVal demo As DemoPageModel, ByVal text As String, ByVal productText As String) As IEnumerable(Of SearchResult)
            Dim results = New List(Of SearchResult)()
            Dim rank = CalculateRank(requests, demo)
            If rank > -1 Then
                Dim sr = New SearchResult(demo, rank)
                sr.Text = text
                sr.ProductText = productText
                results.Add(sr)
            End If
            Return results
        End Function
        Private Shared Function CalculateRank(ByVal requests As List(Of String()), ByVal demo As DemoPageModel) As Integer
            Dim resultRank As Integer = 0
            Dim keywordRank As Integer = 0
            For Each request In requests
                Dim requestRank As Integer = -1
                If SearchAgregator.CalculateRank(request, demo.KeywordsRankList, keywordRank) Then
                    requestRank += keywordRank
                End If
                If demo.Group IsNot Nothing AndAlso SearchAgregator.CalculateRank(request, demo.Group.KeywordsRankList, keywordRank) Then
                    requestRank += keywordRank
                End If
                If SearchAgregator.CalculateRank(request, demo.Product.KeywordsRankList, keywordRank) Then
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
            Dim textRanks = New List(Of TextRank)() From {New TextRank(model.Keywords, 3)}
            Dim product = TryCast(model, DemoProductModel)
            Dim group = TryCast(model, DemoGroupModel)
            Dim demo = TryCast(model, DemoPageModel)
            Dim intro = TryCast(model, IntroPageModel)
            If product IsNot Nothing Then
                textRanks.Add(New TextRank(product.NavItemTitle, 5))
                textRanks.Add(New TextRank(product.Key, 3))
                textRanks.Add(New TextRank(product.Title, 3))
                textRanks.Add(New TextRank(product.SeoTitle, 2))
            ElseIf group IsNot Nothing Then
                textRanks.Add(New TextRank(group.Title, 5))
                textRanks.Add(New TextRank(group.Key, 3))
                textRanks.Add(New TextRank(group.SeoTitle, 2))
            ElseIf demo IsNot Nothing Then
                If intro IsNot Nothing AndAlso intro.Product IsNot Nothing Then
                    textRanks.Add(New TextRank(intro.Product.Title, 15))
                Else
                    textRanks.Add(New TextRank(demo.Title, 5))
                End If
                textRanks.Add(New TextRank(demo.Key, 3))
                textRanks.Add(New TextRank(demo.SeoTitle, 2))
                textRanks.Add(New TextRank(demo.Links, 2))
            End If
            Return Search.GetKeywordsRankList(textRanks)
        End Function
    End Class
    Public Class SearchResult
        Implements IComparable(Of SearchResult)

        Public Sub New(ByVal demo As DemoModel, ByVal rank As Integer)
            Me.Demo = demo
            Me.Rank = rank
            Product = demo.Product
            If TypeOf demo Is DemoPageModel Then
                Group = (TryCast(demo, DemoPageModel)).Group
            End If
        End Sub
        Public Property Product() As DemoProductModel
        Public Property Demo() As DemoModel
        Public Property Group() As DemoGroupModel
        Public Property Text() As String
        Public Property ProductText() As String
        Public Rank As Integer = 0
#Region "IComparable<SearchResult> Members"
        Public Function CompareTo(ByVal other As SearchResult) As Integer Implements IComparable(Of SearchResult).CompareTo
            Return other.Rank.CompareTo(Rank)
        End Function
#End Region
    End Class
End Namespace
