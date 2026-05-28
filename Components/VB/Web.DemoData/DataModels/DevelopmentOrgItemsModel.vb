'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Namespace DevExpress.Web.Demos
    Partial Public Class DevelopmentOrgItemsContextSL
        Inherits DbContext

        Shared Sub New()
        End Sub
        Public Sub New()
            MyBase.New("OrgItemsConnectionStringSL")
        End Sub
        Public Property DevelopmentOrgItems() As DbSet(Of DevelopmentOrgItem)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New DevelopmentOrgItemMap())
        End Sub
    End Class
    Public Class DevelopmentOrgItemMap
        Inherits EntityTypeConfiguration(Of DevelopmentOrgItem)

        Public Sub New()
            Me.HasKey(Function(t) t.Id)
            Me.Property(Function(t) t.Id).HasDatabaseGeneratedOption(Nothing)
            Me.Property(Function(t) t.Text).HasMaxLength(100)
            Me.ToTable("DevelopmentOrgItems")
            Me.Property(Function(t) t.Id).HasColumnName("Id")
            Me.Property(Function(t) t.ParentId).HasColumnName("ParentId")
            Me.Property(Function(t) t.Type).HasColumnName("Type")
            Me.Property(Function(t) t.Text).HasColumnName("Text")
        End Sub
    End Class
    Partial Public Class DevelopmentOrgItem
        Public Property Id() As Integer
        Public Property ParentId() As Integer?
        Public Property Type() As String
        Public Property Text() As String
    End Class
End Namespace
