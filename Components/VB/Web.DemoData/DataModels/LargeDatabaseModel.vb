'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports DevExpress.Web.Demos
Namespace DevExpress.Web.Demos
    Partial Public Class LargeDatabaseContext
        Inherits ContextBase

        Shared Sub New()
            Database.SetInitializer(Of LargeDatabaseContext)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("LargeDatabaseConnectionString")
        End Sub
        Public Property Emails() As DbSet(Of Email)
        Public Property Persons() As DbSet(Of Person)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New EmailMap())
            modelBuilder.Configurations.Add(New PersonMap())
        End Sub
    End Class
    Partial Public Class Person
        Public Property ID() As Integer
        Public Property FirstName() As String
        Public Property LastName() As String
        Public Property Phone() As String
    End Class
    Partial Public Class Email
        Public Property ID() As Integer
        Public Property Subject() As String
        Public Property [From]() As String
        Public Property Sent() As Date
        Public Property Size() As Long
        Public Property HasAttachment() As Boolean
    End Class
    Public Class PersonMap
        Inherits EntityTypeConfiguration(Of Person)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.FirstName).IsRequired().HasMaxLength(32)
            Me.Property(Function(t) t.LastName).IsRequired().HasMaxLength(32)
            Me.Property(Function(t) t.Phone).IsRequired().HasMaxLength(20)
            Me.ToTable("Persons")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.FirstName).HasColumnName("FirstName")
            Me.Property(Function(t) t.LastName).HasColumnName("LastName")
            Me.Property(Function(t) t.Phone).HasColumnName("Phone")
        End Sub
    End Class
    Public Class EmailMap
        Inherits EntityTypeConfiguration(Of Email)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Subject).IsRequired().HasMaxLength(100)
            Me.Property(Function(t) t.From).IsRequired().HasMaxLength(32)
            Me.ToTable("Emails")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Subject).HasColumnName("Subject")
            Me.Property(Function(t) t.From).HasColumnName("From")
            Me.Property(Function(t) t.Sent).HasColumnName("Sent")
            Me.Property(Function(t) t.Size).HasColumnName("Size")
            Me.Property(Function(t) t.HasAttachment).HasColumnName("HasAttachment")
        End Sub
    End Class
End Namespace
