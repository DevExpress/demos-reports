'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports DevExpress.Internal
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Text
Imports DevExpress.Web.DemoUtils
Imports DevExpress.Data.Utils
Namespace DevExpress.Web.Demos
    Public NotInheritable Class DatabaseGenerator
        Public Class TableData
            Public ConnectionStringName As String
            Public Name As String
            Public Fields As New List(Of FieldData)()
            Public RecordCount As Integer
            Public ReadOnly Property ConnectionString() As String
                Get
                    Dim sqlExpressString As String = ConfigurationManager.ConnectionStrings(ConnectionStringName).ConnectionString
                    Return If(Utils.PatchConnectionStrings, DbEngineDetector.PatchConnectionString(sqlExpressString), sqlExpressString)
                End Get
            End Property
        End Class
        Public Class FieldData
            Private Shared rnd As NonCryptographicRandom = NonCryptographicRandom.Default
            Public Delegate Function ValueGeneratorDelegate(ByVal rnd As NonCryptographicRandom) As Object
            Protected Sub New(ByVal name As String)
                Me.Name = name
            End Sub
            Public Sub New(ByVal name As String, ByVal possibleValues As IList)
                Me.New(name)
                Me.PossibleValues = possibleValues
            End Sub
            Public Sub New(ByVal name As String, ByVal valueGenerator As ValueGeneratorDelegate)
                Me.New(name)
                Me.ValueGenerator = valueGenerator
            End Sub
            Public Name As String
            Public PossibleValues As IList
            Public ValueGenerator As ValueGeneratorDelegate
            Public Function GenerateValue() As Object
                If PossibleValues IsNot Nothing Then
                    Return PossibleValues(rnd.Next(PossibleValues.Count - 1))
                End If
                If ValueGenerator IsNot Nothing Then
                    Return ValueGenerator(rnd)
                End If
                Return Nothing
            End Function
        End Class

        Private Sub New()
        End Sub

        Private Shared ReadOnly _lockers As New Dictionary(Of String, Object)()
        Private Shared ReadOnly _lockersLock As New Object()
        Private Shared _recordCountTable As New Dictionary(Of String, Integer)()
        Private Shared _tables As New Dictionary(Of String, TableData)()
        Public Shared Sub RegisterTable(ByVal key As String, ByVal table As TableData)
            _tables(key) = table
        End Sub
        Public Shared Function GetTable(ByVal key As String) As TableData
            Return _tables(key)
        End Function
        Public Shared Function IsReady(ByVal tableKey As String) As Boolean
            Return (Not IsDatabaseCreating(tableKey)) AndAlso IsDatabaseCreated(tableKey)
        End Function
        Public Shared Function TryCreateDatabase(ByVal tableKey As String) As Boolean
            If IsDatabaseCreating(tableKey) Then
                Return False
            End If
            If Not _lockers.ContainsKey(tableKey) Then
                SyncLock _lockersLock
                    _lockers(tableKey) = New Object()
                End SyncLock
            End If
            SyncLock _lockers(tableKey)
                If IsDatabaseCreated(tableKey) Then
                    Return True
                End If
                _recordCountTable.Add(tableKey, 0)
                Try
                    GenerateDatabase(tableKey)
                Finally
                    _recordCountTable.Remove(tableKey)
                End Try
                Return False
            End SyncLock
        End Function
        Public Shared Function GetCreatingDatabaseRecordCount(ByVal tableKey As String) As Integer
            If _recordCountTable.ContainsKey(tableKey) Then
                Return _recordCountTable(tableKey)
            End If
            Return GetRecordCount(tableKey)
        End Function
        Private Shared Function IsDatabaseCreated(ByVal tableKey As String) As Boolean
            Return GetRecordCount(tableKey) > 0
        End Function
        Private Shared Function IsDatabaseCreating(ByVal tableKey As String) As Boolean
            Return _recordCountTable.ContainsKey(tableKey)
        End Function
        Private Shared Sub GenerateDatabase(ByVal tableKey As String)
            Dim table As TableData = _tables(tableKey)
            Using connection As New SqlConnection(table.ConnectionString)
                connection.Open()
                Dim transaction As SqlTransaction = Nothing
                For i As Integer = 0 To table.RecordCount - 1
                    If i = 1 OrElse i Mod 1000 = 0 Then
                        CommitTransaction(transaction)
                        _recordCountTable(tableKey) = i
                        transaction = connection.BeginTransaction()
                    End If
'#pragma warning disable DX0024 ' The Insert command is parameterized and doesn't depend on user input
                    Using cmd As SqlCommand = CreateInsertCommand(table.Name, table.Fields)
                        cmd.Connection = connection
                        cmd.Transaction = transaction
                        cmd.ExecuteNonQuery()
'#pragma warning restore DX0024
                    End Using
                Next i
                CommitTransaction(transaction)
            End Using
        End Sub
        Private Shared Sub CommitTransaction(ByVal transaction As SqlTransaction)
            If transaction Is Nothing Then
                Return
            End If
            Try
                transaction.Commit()
            Catch
                transaction.Rollback()
                Throw
            End Try
        End Sub
        Private Shared Function CreateInsertCommand(ByVal tableName As String, ByVal fields As List(Of FieldData)) As SqlCommand
            Dim builder As New StringBuilder()
            builder.AppendFormat("insert into [{0}] (", tableName)
            For i As Integer = 0 To fields.Count - 1
                If i > 0 Then
                    builder.Append(", ")
                End If
                builder.AppendFormat("[{0}]", fields(i).Name)
            Next i
            builder.Append(") values (")
            For i As Integer = 0 To fields.Count - 1
                If i > 0 Then
                    builder.Append(", ")
                End If
                builder.Append("@p" & i)
            Next i
            builder.Append(")")
'#pragma warning disable DX0024
            Dim result As New SqlCommand(builder.ToString())
'#pragma warning restore DX0024
            For i As Integer = 0 To fields.Count - 1
                result.Parameters.Add(New SqlParameter("@p" & i, fields(i).GenerateValue()))
            Next i
            Return result
        End Function
        Private Shared Function GetRecordCount(ByVal tableKey As String) As Integer
            Try
                Dim table As TableData = _tables(tableKey)
                Using connection As New SqlConnection(table.ConnectionString)
                    connection.Open()
'#pragma warning disable DX0024
                    Using cmd = New SqlCommand("select count(*) from [" & table.Name & "]", connection)
                        Return DirectCast(cmd.ExecuteScalar(), Integer)
                    End Using
'#pragma warning restore DX0024
                End Using
            Catch
                Return 0
            End Try
        End Function
    End Class
End Namespace
