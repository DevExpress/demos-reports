'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Web
Namespace DevExpress.Web.Demos.Mvc
    Public NotInheritable Class WorldCitiesDataProvider

        Private Sub New()
        End Sub

        Private Const WorldCitiesDataContextName As String = "DXWorldCitiesDataContext"
        Public Shared ReadOnly Property DB() As WorldCitiesContext
            Get
                If HttpContext.Current.Items(WorldCitiesDataContextName) Is Nothing Then
                    HttpContext.Current.Items(WorldCitiesDataContextName) = New WorldCitiesContext()
                End If
                Return DirectCast(HttpContext.Current.Items(WorldCitiesDataContextName), WorldCitiesContext)
            End Get
        End Property
        Public Shared Function GetCountries() As IEnumerable(Of Country)
            Return DB.Countries.OrderBy(Function(c) c.CountryName).ToList()
        End Function
        Public Shared Function GetCities(ByVal countryID As Integer) As IEnumerable(Of City)
            Return DB.Cities.Where(Function(c) c.CountryId = countryID).OrderBy(Function(c) c.CityName).GroupBy(Function(c) c.CityName).Select(Function(g) g.FirstOrDefault()).ToList()
        End Function
        Public Shared Function GetAllCities() As IEnumerable(Of City)
            Return DB.Cities.OrderBy(Function(c) c.CityName).ToList()
        End Function
        Private Const SessionName As String = "DXDemoWorldCustomers"
        Public Shared Function GetEditableCustomers() As IList(Of EditableWorldCustomer)
            Dim customersList As IList(Of EditableWorldCustomer) = TryCast(HttpContext.Current.Session(SessionName), IList(Of EditableWorldCustomer))
            If customersList Is Nothing Then
                customersList = New List(Of EditableWorldCustomer)()
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 1, .CustomerName = "Maria Anders", .CountryId = 63, .CityId = 775})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 2, .CustomerName = "Thomas Hardy", .CountryId = 1, .CityId = 125})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 3, .CustomerName = "Yang Wang", .CountryId = 17, .CityId = 2025})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 4, .CustomerName = "Elizabeth Brown", .CountryId = 35, .CityId = 332})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 5, .CustomerName = "Sven Ottlieb", .CountryId = 217, .CityId = 1459})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 6, .CustomerName = "Janine Labrune", .CountryId = 220, .CityId = 1651})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 7, .CustomerName = "Martine Rance", .CountryId = 218, .CityId = 844})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 8, .CustomerName = "Maria Larsson", .CountryId = 219, .CityId = 2444})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 9, .CustomerName = "Peter Franken", .CountryId = 234, .CityId = 1900})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 10, .CustomerName = "Carlos Hernandez", .CountryId = 5, .CityId = 2445})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 11, .CustomerName = "Philip Cramer", .CountryId = 154, .CityId = 356})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 12, .CustomerName = "Yoshi Tannamuri", .CountryId = 10, .CityId = 2447})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 13, .CustomerName = "Peter Heinken", .CountryId = 159, .CityId = 475})
                customersList.Add(New EditableWorldCustomer() With {.CustomerId = 14, .CustomerName = "Mike Taison", .CountryId = 216, .CityId = 2219})
                HttpContext.Current.Session(SessionName) = customersList
            End If
            Return customersList
        End Function
        Public Shared Function GetEditableCustomerByID(ByVal customerID As Integer) As EditableWorldCustomer
            Return GetEditableCustomers().Where(Function(c) c.CustomerId.Equals(customerID)).SingleOrDefault()
        End Function
        Public Shared Function GetNewEditableCustomerID() As Integer
            Dim editableCustomers As IList(Of EditableWorldCustomer) = GetEditableCustomers()
            If editableCustomers.Count() = 0 Then
                Return 1
            End If
            Return editableCustomers.OrderBy(Function(c) c.CustomerId).Last().CustomerId + 1
        End Function
        Public Shared Sub InsertCustomer(ByVal customer As EditableWorldCustomer)
            Dim newCustomer = New EditableWorldCustomer() With {.CustomerId = GetNewEditableCustomerID(), .CustomerName = customer.CustomerName, .CountryId = customer.CountryId, .CityId = customer.CityId}
            GetEditableCustomers().Add(newCustomer)
        End Sub
        Public Shared Sub UpdateCustomer(ByVal customer As EditableWorldCustomer)
            Dim editableCustomer As EditableWorldCustomer = GetEditableCustomerByID(customer.CustomerId)
            If editableCustomer Is Nothing Then
                Return
            End If
            editableCustomer.CustomerName = customer.CustomerName
            editableCustomer.CountryId = customer.CountryId
            editableCustomer.CityId = customer.CityId
        End Sub
    End Class
    Public Class EditableWorldCustomer
        Public Property CustomerId() As Integer
        <Required(ErrorMessage := "Customer Name is required"), StringLength(50, ErrorMessage := "Must be under 50 characters")>
        Public Property CustomerName() As String
        Public Property CountryId() As Integer?
        Public Property CityId() As Integer?
    End Class
End Namespace
