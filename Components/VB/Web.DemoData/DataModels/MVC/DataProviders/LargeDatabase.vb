'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Linq
Imports System.Web
Imports System.Collections.Generic
Imports DevExpress.Web.Demos
Namespace DevExpress.Web.Demos
    Public NotInheritable Class LargeDatabaseDataProvider

        Private Sub New()
        End Sub

        Private Const LargeDatabaseDataContextKey As String = "DXLargeDatabaseDataContext"
        Public Shared ReadOnly Property DB() As LargeDatabaseContext
            Get
                If HttpContext.Current.Items(LargeDatabaseDataContextKey) Is Nothing Then
                    HttpContext.Current.Items(LargeDatabaseDataContextKey) = New LargeDatabaseContext()
                End If
                Return DirectCast(HttpContext.Current.Items(LargeDatabaseDataContextKey), LargeDatabaseContext)
            End Get
        End Property
        Public Shared ReadOnly Property Emails() As IQueryable(Of Email)
            Get
                Return DB.Emails
            End Get
        End Property
        Public Shared Function GetPersonsRange(ByVal args As ListEditItemsRequestedByFilterConditionEventArgs) As IEnumerable(Of Person)
            Dim skip = args.BeginIndex
            Dim take = args.EndIndex - args.BeginIndex + 1
            Dim query = (
                From person In DB.Persons
                Where (person.FirstName & " " & person.LastName & " " & person.Phone).Contains(args.Filter)
                Order By person.LastName
                Select person).Skip(skip).Take(take)
            Return query.ToList()
        End Function
        Public Shared Function GetPersonByID(ByVal args As ListEditItemRequestedByValueEventArgs) As Person
            Dim id As Integer = Nothing
            If args.Value Is Nothing OrElse (Not Integer.TryParse(args.Value.ToString(), id)) Then
                Return Nothing
            End If
            Return DB.Persons.Where(Function(p) p.ID = id).Take(1).SingleOrDefault()
        End Function
    End Class
End Namespace
