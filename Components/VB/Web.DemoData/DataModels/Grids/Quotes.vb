'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web
Imports System.Web.SessionState
Imports DevExpress.Data.Utils
Namespace DevExpress.Web.Demos
    Public Class Quote
        Private Shared ReadOnly random As NonCryptographicRandom = NonCryptographicRandom.Default
        Public Property Symbol() As String
        Public Property Price() As Decimal
        Public Property Change() As Decimal
        Public Property DayMax() As Decimal
        Public Property DayMin() As Decimal
        Public Property LastUpdated() As Date
        Public Sub Update()
            If LastUpdated.Day <> Date.Now.Day Then
                DayMax = 0
                DayMin = 0
            End If
            Change = CDec((0.5 - random.NextDouble()) / 5.0)
            Dim newPrice As Decimal = Price + Price * Change
            If newPrice < 0 Then
                newPrice = 0
            End If
            If Price > 0 Then
                Change = (newPrice - Price) / Price
            Else
                Change = 0
            End If
            Price = newPrice
            LastUpdated = Date.Now
            If Price > DayMax OrElse DayMax = 0 Then
                DayMax = Price
            End If
            If Price < DayMin OrElse DayMin = 0 Then
                DayMin = Price
            End If
        End Sub
    End Class
    Public Class QuotesProvider
        Private Shared symbolsList() As String = { "MSFT", "INTC", "CSCO", "SIRI", "AAPL", "HOKU", "ORCL", "AMAT", "YHOO", "LVLT", "DELL", "GOOG" }
        Private Shared yahooUrl As String = "http://finance.yahoo.com/d/quotes.csv?s={0}&f=sl1h0g0v0d1"
        Private Shared ReadOnly random As NonCryptographicRandom = NonCryptographicRandom.Default
        Private Shared ReadOnly Property Session() As HttpSessionState
            Get
                Return HttpContext.Current.Session
            End Get
        End Property
        Public Shared Function GetQuotes() As List(Of Quote)
            If Session("Quotes") Is Nothing Then
                Session("Quotes") = LoadQuotes()
            End If
            Dim quotes = DirectCast(Session("Quotes"), List(Of Quote))
            UpdateQuotes(quotes)
            Return quotes
        End Function
        Public Shared Function LoadQuotes() As List(Of Quote)
            Try
                Return LoadQuotesFromYahoo()
            Catch
                Return GenerateQuotes()
            End Try
        End Function
        Private Shared Function LoadQuotesFromYahoo() As List(Of Quote)
            Dim quotes = New List(Of Quote)()
            Dim url = String.Format(yahooUrl, String.Join("+", symbolsList))
            Dim request = HttpWebRequest.Create(url)
            Using stream = request.GetResponse().GetResponseStream()
                Using reader = New StreamReader(stream, Encoding.UTF8)
                    Do While Not reader.EndOfStream
                        Dim values = reader.ReadLine().Replace("""", "").Split(New Char() { ","c })
                        Dim quote As New Quote()
                        quote.Symbol = values(0).Trim()
                        Dim value As Decimal = Nothing
                        If Decimal.TryParse(values(1), value) Then
                            quote.Price = value
                        Else
                            quote.Price = 0
                        End If
                        If Decimal.TryParse(values(2), value) Then
                            quote.DayMax = value
                        Else
                            quote.DayMax = 0
                        End If
                        If Decimal.TryParse(values(3), value) Then
                            quote.DayMin = value
                        Else
                            quote.DayMin = 0
                        End If
                        Dim [date] As Date = Nothing
                        If Date.TryParse(values(4), [date]) Then
                            quote.LastUpdated = [date]
                        Else
                            quote.LastUpdated = Date.Now
                        End If
                        quotes.Add(quote)
                    Loop
                End Using
            End Using
            Return quotes
        End Function
        Private Shared Function GenerateQuotes() As List(Of Quote)
            Return New List(Of Quote) From {
                New Quote With {.Symbol = "MSFT", .Price = 37.95D, .DayMax = 37.95D, .DayMin = 36.06D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "INTC", .Price = 24.85D, .DayMax = 25.80D, .DayMin = 24.85D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "CSCO", .Price = 22.99D, .DayMax = 22.99D, .DayMin = 22.64D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "SIRI", .Price = 3.71D, .DayMax = 3.75D, .DayMin = 3.69D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "AAPL", .Price = 586.73D, .DayMax = 586.73D, .DayMin = 540.42D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "HOKU", .Price = 0, .DayMax = 0, .DayMin = 0, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "ORCL", .Price = 38.11D, .DayMax = 38.52D, .DayMin = 37.80D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "AMAT", .Price = 17.61D, .DayMax = 17.69D, .DayMin = 17.47D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "YHOO", .Price = 40.80D, .DayMax = 40.80D, .DayMin = 38.86D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "LVLT", .Price = 31.85D, .DayMax = 34.25D, .DayMin = 31.85D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "DELL", .Price = 20.63D, .DayMax = 26.64D, .DayMin = 14.38D, .LastUpdated = Date.Now},
                New Quote With {.Symbol = "GOOG", .Price = 1163.70D, .DayMax = 1164D, .DayMin = 1151.30D, .LastUpdated = Date.Now}
            }
        End Function
        Private Shared Sub UpdateQuotes(ByVal quotes As List(Of Quote))
            For Each quote As Quote In quotes
                If random.NextDouble() >= 0.7 Then
                    quote.Update()
                End If
            Next quote
        End Sub
    End Class
End Namespace
