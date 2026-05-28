'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Namespace DevExpress.Web.Demos
    Partial Public Class OrgItemsContextSL
        Inherits DbContext

        Shared Sub New()
            Database.SetInitializer(Of FlowContextSL)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("OrgItemsConnectionStringSL")
        End Sub
        Public Property Nodes() As DbSet(Of OrgItem)
        Public Property Edges() As DbSet(Of OrgLink)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New OrgItemMap())
            modelBuilder.Configurations.Add(New OrgLinkMap())
        End Sub
    End Class
    Public Class OrgItemMap
        Inherits EntityTypeConfiguration(Of OrgItem)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(Nothing)
            Me.Property(Function(t) t.Text).HasMaxLength(100)
            Me.ToTable("Nodes")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Text).HasColumnName("Text")
            Me.Property(Function(t) t.Type).HasColumnName("Type")
            Me.Property(Function(t) t.Picture).HasColumnName("Picture")
        End Sub
    End Class
    Public Class OrgLinkMap
        Inherits EntityTypeConfiguration(Of OrgLink)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(Nothing)
            Me.ToTable("Edges")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.FromID).HasColumnName("FromID")
            Me.Property(Function(t) t.ToID).HasColumnName("ToID")
        End Sub
    End Class
    Partial Public Class OrgItem
        Inherits OrgItemBase

        Public Property Text() As String
        Public Property Type() As String
        Public Property Picture() As String
    End Class
    Partial Public Class OrgLink
        Inherits OrgItemBase

        Public Property FromID() As Integer?
        Public Property ToID() As Integer?
    End Class
    Public Class OrgItemBase
        Public Property ID() As Integer
    End Class
End Namespace
