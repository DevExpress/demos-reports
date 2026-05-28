'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Namespace DevExpress.Web.Demos
    Public Class SearchAgregator
        Friend Shared DefaultStringComparison As StringComparison = StringComparison.InvariantCultureIgnoreCase
        Private Shared ReadOnly separators() As String = { " ", ",", "/", "\", "-", "+" }
        Friend Sub New(ByVal searchModel As SearchModel)
            Me.SearchModel = searchModel
        End Sub
        Private _requestExclusions() As String
        Private _prefixes() As String
        Private _postfixes() As String
        Private _synonyms()() As String
        Public ReadOnly Property WordsExclusions() As String()
            Get
                If _requestExclusions Is Nothing Then
                    _requestExclusions = GetWordsExclusions(SearchModel)
                End If
                Return _requestExclusions
            End Get
        End Property
        Private Shared Function GetWordsExclusions(ByVal search As SearchModel) As String()
            Return search.Exclusions.Words.Split(separators, StringSplitOptions.RemoveEmptyEntries)
        End Function
        Public ReadOnly Property PrefixesExclusions() As String()
            Get
                If _prefixes Is Nothing Then
                    _prefixes = GetPrefixesExclusions(SearchModel)
                End If
                Return _prefixes
            End Get
        End Property
        Friend Shared Function GetPrefixesExclusions(ByVal search As SearchModel) As String()
            Return search.Exclusions.Prefixes.Split(separators, StringSplitOptions.RemoveEmptyEntries)
        End Function
        Public ReadOnly Property PostfixesExclusions() As String()
            Get
                If _postfixes Is Nothing Then
                    _postfixes = GetPostfixesExclusions(SearchModel)
                End If
                Return _postfixes
            End Get
        End Property
        Friend Shared Function GetPostfixesExclusions(ByVal search As SearchModel) As String()
            Return search.Exclusions.Postfixes.Split(separators, StringSplitOptions.RemoveEmptyEntries)
        End Function
        Public ReadOnly Property Synonyms() As String()()
            Get
                If _synonyms Is Nothing Then
                    _synonyms = GetSynonyms(SearchModel)
                End If
                Return _synonyms
            End Get
        End Property
        Friend Shared Function GetSynonyms(ByVal search As SearchModel) As String()()
            Return search.Synonyms.Groups.Select(Function(s) s.Split(separators, StringSplitOptions.RemoveEmptyEntries)).ToArray()
        End Function
        Private Property SearchModel() As SearchModel
        Public Shared Function CalculateRank(ByVal synonyms() As String, ByVal keywordsRankList As Dictionary(Of String, Integer), <System.Runtime.InteropServices.Out()> ByRef rank As Integer) As Boolean
            Dim keyword = keywordsRankList.Keys.FirstOrDefault(Function(k) MatchWord(synonyms(0), k))
            rank = If(keyword IsNot Nothing, keywordsRankList(keyword), -1)
            If rank = -1 Then
                For Each syn In synonyms.Skip(1)
                    keyword = keywordsRankList.Keys.FirstOrDefault(Function(k) MatchWord(syn, k))
                    rank += If(keyword IsNot Nothing, keywordsRankList(keyword), -1)
                    If rank > -1 Then
                        Exit For
                    End If
                Next syn
            End If
            Return rank > -1
        End Function
        Friend Shared Function HighlightOccurences(ByVal text As String, ByVal requests As List(Of String())) As String
            Return HighlightOccurences(text, requests, False)
        End Function
        Friend Shared Function HighlightOccurences(ByVal text As String, ByVal requests As List(Of String()), ByVal combineMatches As Boolean) As String
            Dim validRequest = New Regex("[0-9a-zA-Z]{2,}", RegexOptions.IgnoreCase)
            For Each request In requests.SelectMany(Function(r) r)
                If validRequest.IsMatch(request) Then
                    Dim re As New Regex("([a-zA-Z0-9]*" & request & "[a-zA-Z0-9]*)", RegexOptions.IgnoreCase)
                    text = re.Replace(text, "<b>$0</b>")
                End If
            Next request
            If combineMatches Then
                Dim reCombine As New Regex("</b>\s<b>", RegexOptions.IgnoreCase)
                text = reCombine.Replace(text, " ")
            End If
            Return text
        End Function
        Public Function SplitRequests(ByVal request As String) As List(Of String())
            Dim words = request.Split(separators, StringSplitOptions.RemoveEmptyEntries)
            Dim result = New List(Of String())()
            For Each word In words
                Dim resultWord = PrepareWord(word)
                Dim synonymList = Synonyms.FirstOrDefault(Function(list) list.Any(Function(s) MatchWord(resultWord, s)))
                Dim wordSynonyms = New List(Of String)() From {resultWord}
                If synonymList IsNot Nothing Then
                    wordSynonyms.AddRange(synonymList.Where(Function(s) (Not MatchWord(resultWord, s))))
                End If
                result.Add(wordSynonyms.Distinct().ToArray())
            Next word
            Return result
        End Function
        Private Function PrepareWord(ByVal word As String) As String
            For Each prefix In PrefixesExclusions
                If word.StartsWith(prefix, DefaultStringComparison) AndAlso word.Length > prefix.Length Then
                    word = word.Remove(0, prefix.Length)
                End If
            Next prefix
            For Each postfix In PostfixesExclusions
                If word.EndsWith(postfix, DefaultStringComparison) AndAlso word.Length > postfix.Length Then
                    word = word.Substring(0, word.Length - postfix.Length)
                End If
            Next postfix
            Return word
        End Function
        Private Shared Function MatchWord(ByVal request As String, ByVal word As String) As Boolean
            Return word.IndexOf(request, DefaultStringComparison) > -1
        End Function
        Friend Shared Function GetKeywordsList(ParamArray ByVal words() As String) As String()
            Return words.SelectMany(Function(w) w.Split(separators, StringSplitOptions.RemoveEmptyEntries)).Distinct().ToArray()
        End Function
        Public Function GetKeywordsRankList(ByVal textRanks As List(Of TextRank)) As Dictionary(Of String, Integer)
            Dim result = New Dictionary(Of String, Integer)()
            For Each textRank In textRanks
                Dim words = textRank.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                For Each word In words
                    Dim rankWord = PrepareWord(word)
                    If Not result.ContainsKey(rankWord) Then
                        result(rankWord) = textRank.Rank
                    End If
                Next word
            Next textRank
            Return result.OrderByDescending(Function(keyValuePair) keyValuePair.Value).ToDictionary(Function(keyValuePair) keyValuePair.Key, Function(keyValuePair) keyValuePair.Value)
        End Function
    End Class
    Public Class TextRank
        Public Sub New(ByVal text As String, ByVal rank As Integer)
            Me.Text = text
            Me.Rank = rank
        End Sub
        Public Property Text() As String
        Public Property Rank() As Integer
    End Class
End Namespace
