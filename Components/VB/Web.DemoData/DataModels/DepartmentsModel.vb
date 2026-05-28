'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.ModelConfiguration
Namespace DevExpress.Web.Demos
    Partial Public Class DepartmentsContext
        Inherits DevExpress.Web.Demos.ContextBase

        Shared Sub New()
            Database.SetInitializer(Of DepartmentsContext)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("DepartmentsConnectionString")
        End Sub
        Public Property Departments() As DbSet(Of Department)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New DepartmentMap())
        End Sub
    End Class
    Partial Public Class Department
        Public Property ID() As Integer
        Public Property ParentID() As Integer?
        Public Property ImageIndex() As Double?
        Public Property DepartmentName() As String
        Public Property Budget() As Decimal?
        Public Property Location() As String
        Public Property Phone1() As String
        Public Property Phone2() As String
    End Class
    Public Class DepartmentMap
        Inherits EntityTypeConfiguration(Of Department)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.DepartmentName).HasMaxLength(100)
            Me.Property(Function(t) t.Location).HasMaxLength(50)
            Me.Property(Function(t) t.Phone1).HasMaxLength(15)
            Me.Property(Function(t) t.Phone2).HasMaxLength(15)
            Me.ToTable("Departments")
            Me.Property(Function(t) t.ID).HasColumnName("ID").HasDatabaseGeneratedOption(Nothing)
            Me.Property(Function(t) t.ParentID).HasColumnName("ParentID")
            Me.Property(Function(t) t.ImageIndex).HasColumnName("ImageIndex")
            Me.Property(Function(t) t.DepartmentName).HasColumnName("Department")
            Me.Property(Function(t) t.Budget).HasColumnName("Budget")
            Me.Property(Function(t) t.Location).HasColumnName("Location")
            Me.Property(Function(t) t.Phone1).HasColumnName("Phone1")
            Me.Property(Function(t) t.Phone2).HasColumnName("Phone2")
        End Sub
    End Class
End Namespace
