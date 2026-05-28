Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace DevExpress.Web.Demos
    Partial Public Class UserControls_ServerEventMonitor
        Inherits UserControl

        Private _eventNames As String
        Private _eventLogWidth As Integer

        Public Property EventNames() As String
            Get
                Return _eventNames
            End Get
            Set(ByVal value As String)
                _eventNames = value
            End Set
        End Property
        Public Property EventLogWidth() As Integer
            Get
                Return _eventLogWidth
            End Get
            Set(ByVal value As Integer)
                _eventLogWidth = value
            End Set
        End Property

        Protected Property Items() As List(Of KeyValuePair(Of String, String)())
            Get
                ViewState("Items") = If(ViewState("Items"), New List(Of KeyValuePair(Of String, String)())())
                Return DirectCast(ViewState("Items"), List(Of KeyValuePair(Of String, String)()))
            End Get
            Set(ByVal value As List(Of KeyValuePair(Of String, String)()))
                ViewState("Items") = value
            End Set
        End Property

        Public Sub TraceEvent(ByVal sender As Object, ByVal e As EventArgs, ByVal eventName As String)
            If Not IsTraceEnabled(eventName) Then
                Return
            End If
            Dim rows = New List(Of KeyValuePair(Of String, String))()
            rows.Add(New KeyValuePair(Of String, String)("Sender", (TryCast(sender, Control)).ID))
            rows.Add(New KeyValuePair(Of String, String)("EventType", eventName))

            Dim count As Integer = 0
            For Each prop As System.Reflection.PropertyInfo In e.GetType().GetProperties(System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.Public)
                Try
                    Dim value As Object = If(prop.PropertyType.IsValueType, prop.GetValue(e, Nothing), "[" & prop.PropertyType.Name & "]")
                    rows.Add(New KeyValuePair(Of String, String)(If(count > 0, "", "Arguments"), prop.Name & " = " & value))
                    If Not prop.PropertyType.IsValueType Then
                        TryTraceCollectionItemArgument(prop, e, rows)
                    End If
                    count += 1
                Catch
                End Try
            Next prop
            Items.Insert(0, rows.ToArray())
            RenderTable()
        End Sub

        Private Sub RenderTable()
            EventLog.Controls.Clear()
            For Each rows In Items
                Dim table = New Table() With {.CssClass = "event-table"}
                For Each row In rows
                    Dim tRow = New TableRow()
                    tRow.Cells.Add(New TableCell() With {.CssClass = "event-header", .Text = HttpUtility.HtmlEncode(row.Key)})
                    tRow.Cells.Add(New TableCell() With {.CssClass = "event-content", .Text = HttpUtility.HtmlEncode(row.Value)})
                    table.Rows.Add(tRow)
                Next row
                EventLog.Controls.Add(table)
            Next rows
        End Sub

        Private Sub TryTraceCollectionItemArgument(ByVal prop As System.Reflection.PropertyInfo, ByVal e As EventArgs, ByVal rows As List(Of KeyValuePair(Of String, String)))
            Try
                Dim item As CollectionItem = TryCast(prop.GetValue(e, Nothing), CollectionItem)
                If item Is Nothing Then
                    Return
                End If
                Dim itemPropNames() As String = { "Name", "Text" }
                For Each itemPropName As String In itemPropNames
                    Dim itemProp As System.Reflection.PropertyInfo = item.GetType().GetProperty(itemPropName)
                    If itemProp Is Nothing Then
                        Continue For
                    End If
                    Dim itemPropValue As String = TryCast(itemProp.GetValue(item, Nothing), String)
                    If Not String.IsNullOrEmpty(itemPropValue) Then
                        rows.Add(New KeyValuePair(Of String, String)("", prop.Name & "." & itemPropName & " = " & itemPropValue))
                        Return
                    End If
                Next itemPropName
            Catch
            End Try
        End Sub

        Private Function IsTraceEnabled(ByVal eventName As String) As Boolean
            For Each item As RepeaterItem In Repeater.Items
                For Each control As Control In item.Controls
                    Dim box As ASPxCheckBox = TryCast(control, ASPxCheckBox)
                    If box IsNot Nothing AndAlso box.Text = eventName Then
                        Return box.Checked
                    End If
                Next control
            Next item
            Return False
        End Function

        Protected Function GetEventNameList() As IEnumerable(Of String)
            Dim eventNameList() As String = Regex.Split(EventNames, "[^A-Za-z_]+")
            For index As Integer = 0 To eventNameList.Length - 1
                eventNameList.SetValue(eventNameList(index).Replace("_", " "), index)
            Next index
            Return eventNameList
        End Function

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            Repeater.DataSource = GetEventNameList()
            Repeater.DataBind()

            If EventLogWidth > 0 Then
                EventLog.Style.Add("width", EventLogWidth & "px")
            End If
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            ClearButton.Theme = DemoHelper.Instance.Theme
            For Each item As RepeaterItem In Repeater.Items
                For Each control As Control In item.Controls
                    Dim checkbox = TryCast(control, ASPxCheckBox)
                    If checkbox IsNot Nothing Then
                        checkbox.Theme = DemoHelper.Instance.Theme
                    End If
                Next control
            Next item
        End Sub

        Protected Sub ClearButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Items.Clear()
            EventLog.Controls.Clear()
        End Sub
    End Class
End Namespace
