'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Namespace DevExpress.Web.Demos
    Partial Public Class NewsGroupsContext
        Inherits ContextBase

        Shared Sub New()
            Database.SetInitializer(Of NewsGroupsContext)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("NewsGroupsConnectionString")
        End Sub
        Public Property Threads() As DbSet(Of Threads)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New ThreadsMap())
        End Sub
    End Class
    Partial Public Class Threads
        Public Property ID() As Integer
        Public Property ParentID() As Integer?
        Public Property Subject() As String
        Public Property [From]() As String
        Public Property Text() As String
        Public Property DateCreated() As Date?
        Public Property IsNew() As Boolean?
        Public Property HasAttachment() As Boolean?
    End Class
    Public Class ThreadsMap
        Inherits EntityTypeConfiguration(Of Threads)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Subject).HasMaxLength(50)
            Me.Property(Function(t) t.From).HasMaxLength(50)
            Me.ToTable("Threads")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.ParentID).HasColumnName("ParentID")
            Me.Property(Function(t) t.Subject).HasColumnName("Subject")
            Me.Property(Function(t) t.From).HasColumnName("From")
            Me.Property(Function(t) t.Text).HasColumnName("Text")
            Me.Property(Function(t) t.DateCreated).HasColumnName("Date")
            Me.Property(Function(t) t.IsNew).HasColumnName("IsNew")
            Me.Property(Function(t) t.HasAttachment).HasColumnName("HasAttachment")
        End Sub
    End Class
End Namespace
