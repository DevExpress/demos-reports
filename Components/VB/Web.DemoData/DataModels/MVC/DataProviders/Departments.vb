'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Data
Imports System.Data.Entity
Imports System.Data.Linq
Imports System.Linq
Imports System.Web
Namespace DevExpress.Web.Demos
    Public Class DepartmentDataProvider
        Private Const DepartmentDataContextKey As String = "DXDepartmentContext"
        Public Shared ReadOnly Property DB() As DepartmentsContext
            Get
                If HttpContext.Current.Items(DepartmentDataContextKey) Is Nothing Then
                    HttpContext.Current.Items(DepartmentDataContextKey) = New DepartmentsContext()
                End If
                Return DirectCast(HttpContext.Current.Items(DepartmentDataContextKey), DepartmentsContext)
            End Get
        End Property
        Public Shared Function GetDepartments() As IList(Of Department)
            Dim departments As IList(Of Department) = TryCast(HttpContext.Current.Session("DXDepartments"), IList(Of Department))
            If departments Is Nothing Then
                departments = (
                    From department In DB.Departments
                    Select department).ToList()
                HttpContext.Current.Session("DXDepartments") = departments
            End If
            Return departments
        End Function
        Public Shared Function GetEditableDepartments() As IList(Of EditableDepartment)
            Dim objects As IList(Of EditableDepartment) = DirectCast(HttpContext.Current.Session("DXEditableDepartments"), IList(Of EditableDepartment))
            If objects Is Nothing Then
                objects = (
                    From dep In DB.Departments
                    Select New EditableDepartment With {.ID = dep.ID, .ParentID = dep.ParentID, .DepartmentName = dep.DepartmentName}).ToList()
                HttpContext.Current.Session("DXEditableDepartments") = objects
            End If
            Return objects
        End Function
        Public Shared Sub Update(ByVal department As EditableDepartment)
            Dim editObject = GetEditableObject(department.ID)
            If editObject IsNot Nothing Then
                editObject.DepartmentName = department.DepartmentName
                editObject.ParentID = department.ParentID
            End If
        End Sub
        Public Shared Sub Delete(ByVal departmentID As Integer)
            Dim editObject = GetEditableObject(departmentID)
            If editObject IsNot Nothing Then
                GetEditableDepartments().Remove(editObject)
            End If
        End Sub
        Private Shared ObjectInsertLock As New Object()
        Public Shared Function Insert(ByVal department As EditableDepartment) As EditableDepartment
            SyncLock ObjectInsertLock
                Dim editObject = New EditableDepartment()
                editObject.ID = GetNextDepartmentID()
                editObject.DepartmentName = department.DepartmentName
                editObject.ParentID = department.ParentID
                GetEditableDepartments().Add(editObject)
                Return editObject
            End SyncLock
        End Function
        Private Shared Function GetNextDepartmentID() As Integer
            Dim deps = GetEditableDepartments()
            Return If(deps.Any(), deps.Select(Function(d) d.ID).Max() + 1, 0)
        End Function
        Private Shared Function GetEditableObject(ByVal departmentID As Integer) As EditableDepartment
            Return (
                From obj In GetEditableDepartments()
                Where obj.ID = departmentID
                Select obj).FirstOrDefault()
        End Function
    End Class
    Public Class EditableDepartment
        Public Property ID() As Integer
        Public Property ParentID() As Integer?
        Public Property DepartmentName() As String
    End Class
End Namespace
