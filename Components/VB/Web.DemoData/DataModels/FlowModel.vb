'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Namespace DevExpress.Web.Demos
    Partial Public Class FlowContextSL
        Inherits DbContext

        Shared Sub New()
            Database.SetInitializer(Of FlowContextSL)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("FlowConnectionStringSL")
        End Sub
        Public Property Nodes() As DbSet(Of FlowObject)
        Public Property Edges() As DbSet(Of FlowConnection)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New NodeMap())
            modelBuilder.Configurations.Add(New EdgeMap())
        End Sub
    End Class
    Public Class NodeMap
        Inherits EntityTypeConfiguration(Of FlowObject)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(Nothing)
            Me.Property(Function(t) t.Text).HasMaxLength(100)
            Me.ToTable("Nodes")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Text).HasColumnName("Text")
            Me.Property(Function(t) t.Type).HasColumnName("Type")
            Me.Property(Function(t) t.Width).HasColumnName("Width")
            Me.Property(Function(t) t.Height).HasColumnName("Height")
        End Sub
    End Class
    Public Class EdgeMap
        Inherits EntityTypeConfiguration(Of FlowConnection)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.ID).HasDatabaseGeneratedOption(Nothing)
            Me.ToTable("Edges")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.FromID).HasColumnName("FromID")
            Me.Property(Function(t) t.ToID).HasColumnName("ToID")
            Me.Property(Function(t) t.Text).HasColumnName("Text")
        End Sub
    End Class
    Partial Public Class FlowObject
        Inherits FlowEntity

        Public Property Type() As String
        Public Property Width() As Integer
        Public Property Height() As Integer
    End Class
    Partial Public Class FlowConnection
        Inherits FlowEntity

        Public Property FromID() As Integer?
        Public Property ToID() As Integer?
    End Class
    Public Class FlowEntity
        Public Property ID() As Integer
        Public Property Text() As String
    End Class
End Namespace
