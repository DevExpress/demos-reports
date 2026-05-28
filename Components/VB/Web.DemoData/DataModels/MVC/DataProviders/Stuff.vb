'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web
Namespace DevExpress.Web.Demos
    Public Class CompanyEmployee
        Public Property EmployeeID() As Integer
        <Required(ErrorMessage := "First Name is required"), StringLength(10, ErrorMessage := "Must be under 10 characters"), Display(Name := "First Name")>
        Public Property FirstName() As String
        <Required(ErrorMessage := "Last Name is required"), StringLength(20, ErrorMessage := "Must be under 20 characters"), Display(Name := "Last Name")>
        Public Property LastName() As String
        <StringLength(30, ErrorMessage := "Must be under 30 characters"), Display(Name := "Position")>
        Public Property Title() As String
        <Display(Name := "Address")>
        Public Property Address() As String
        <Display(Name := "Birth Date")>
        Public Property BirthDate() As Date?
        <Display(Name := "Hire Date")>
        Public Property HireDate() As Date?
        <Display(Name := "Fire Date")>
        Public Property FireDate() As Date?
        <Display(Name := "Reason")>
        Public Property FireReason() As String
        <Display(Name := "New Company")>
        Public Property NewCompany() As String
        <Display(Name := "References")>
        Public Property References() As String
        Public Property Notes() As String
    End Class
    Public Class CompanyEmployeesDataProviderSL
        Inherits CompanyEmployeesDataProviderBase

        Protected Overrides ReadOnly Property SessionName() As String
            Get
                Return "2E8D8636-1414-4652-AD49-F0A2EE1499F9"
            End Get
        End Property
        Protected Overrides Function GetEmployees() As IList(Of Employee)
            Using dataContext = New NorthwindContextSL()
                Return dataContext.Employees.ToList()
            End Using
        End Function
    End Class
    Public Class CompanyEmployeesDataProvider
        Inherits CompanyEmployeesDataProviderBase

        Protected Overrides ReadOnly Property SessionName() As String
            Get
                Return "A3B3E8EC-CA0D-4735-9615-5E0CB40F215A"
            End Get
        End Property
        Protected Overrides Function GetEmployees() As IList(Of Employee)
            Return Mvc.NorthwindDataProvider.GetEmployees().OfType(Of Employee)().ToList()
        End Function
    End Class
    Public MustInherit Class CompanyEmployeesDataProviderBase
        Private Shared ReadOnly GetEmployeesLocker As New Object()
        Protected MustOverride ReadOnly Property SessionName() As String
        Protected MustOverride Function GetEmployees() As IList(Of Employee)
        Public Function GetCompanyEmployees() As IList(Of CompanyEmployee)
            Dim customersList = TryCast(HttpContext.Current.Session(SessionName), IList(Of CompanyEmployee))
            SyncLock GetEmployeesLocker
                If customersList Is Nothing Then
                    customersList = New List(Of CompanyEmployee)()
                    For Each emp In GetEmployees()
                        customersList.Add(New CompanyEmployee() With {.EmployeeID = emp.EmployeeID, .FirstName = emp.FirstName, .LastName = emp.LastName, .Address = String.Format("{0}, {1}, {2}", emp.Address, emp.City, emp.Country), .BirthDate = emp.BirthDate.Value, .HireDate = emp.HireDate.Value, .Notes = emp.Notes, .Title = emp.Title})
                    Next emp
                    HttpContext.Current.Session(SessionName) = customersList
                End If
            End SyncLock
            Return customersList
        End Function
        Public Function GetCompanyEmployeeByID(ByVal emnployeeID As Integer) As CompanyEmployee
            Return GetCompanyEmployees().Where(Function(e) e.EmployeeID.Equals(emnployeeID)).SingleOrDefault()
        End Function
        Public Function GetNewCompanyEmployeeID() As Integer
            Dim companyEmployees As IList(Of CompanyEmployee) = GetCompanyEmployees()
            If companyEmployees.Count() = 0 Then
                Return 1
            End If
            Return companyEmployees.OrderBy(Function(e) e.EmployeeID).Last().EmployeeID + 1
        End Function
        Public Sub InsertCompanyEmployee(ByVal employee As CompanyEmployee)
            Dim newEmployee = New CompanyEmployee() With {.EmployeeID = GetNewCompanyEmployeeID(), .FirstName = employee.FirstName, .LastName = employee.FirstName, .Title = employee.Title, .Address = employee.Address, .BirthDate = employee.BirthDate, .HireDate = employee.HireDate, .Notes = employee.Notes}
            GetCompanyEmployees().Add(newEmployee)
        End Sub
        Public Sub UpdateCompanyEmployee(ByVal employee As CompanyEmployee)
            Dim editableEmployee As CompanyEmployee = GetCompanyEmployeeByID(employee.EmployeeID)
            If editableEmployee Is Nothing Then
                Return
            End If
            editableEmployee.FirstName = employee.FirstName
            editableEmployee.LastName = employee.FirstName
            editableEmployee.Title = employee.Title
            editableEmployee.Address = employee.Address
            editableEmployee.BirthDate = employee.BirthDate
            editableEmployee.HireDate = employee.HireDate
            editableEmployee.Notes = employee.Notes
            editableEmployee.FireDate = employee.FireDate
            editableEmployee.FireReason = employee.FireReason
            editableEmployee.NewCompany = employee.NewCompany
            editableEmployee.References = employee.References
        End Sub
        Public Sub DeleteCompanyEmployee(ByVal employee As CompanyEmployee)
            Dim employeeToRemove As CompanyEmployee = GetCompanyEmployeeByID(employee.EmployeeID)
            If employeeToRemove Is Nothing Then
                Return
            End If
            GetCompanyEmployees().Remove(employeeToRemove)
        End Sub
    End Class
End Namespace
