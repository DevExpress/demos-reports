'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports DevExpress.Web.Demos
Namespace DevExpress.Web.Demos
    Partial Public Class BinaryImagesContext
        Inherits ContextBase

        Shared Sub New()
            Database.SetInitializer(Of BinaryImagesContext)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("BinaryImagesConnectionString")
        End Sub
        Public Property BinaryImages() As DbSet(Of BinaryImage)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New BinaryImageMap())
        End Sub
    End Class
    Partial Public Class BinaryImage
        Public Property ID() As Integer
        Public Property Text() As String
        Public Property Picture() As Byte()
        Public Property CategoryID() As Integer?
    End Class
    Public Class BinaryImageMap
        Inherits EntityTypeConfiguration(Of BinaryImage)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.ID)
            Me.Property(Function(t) t.Text).IsRequired().HasMaxLength(50)
            Me.ToTable("BinaryImages")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Text).HasColumnName("Text")
            Me.Property(Function(t) t.Picture).HasColumnName("Picture")
            Me.Property(Function(t) t.CategoryID).HasColumnName("CategoryID")
        End Sub
    End Class
End Namespace
