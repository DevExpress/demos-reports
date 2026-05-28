'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.ModelConfiguration
Imports DevExpress.Web.Demos
Namespace DevExpress.Web.Demos
    Partial Public Class DataContext
        Inherits ContextBase

        Shared Sub New()
            Database.SetInitializer(Of DataContext)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("DataConnectionString")
        End Sub
        Public Property Arts() As DbSet(Of Art)
        Public Property Cameras() As DbSet(Of Camera)
        Public Property countries() As DbSet(Of OriginCountry)
        Public Property CountryLocations() As DbSet(Of CountryLocation)
        Public Property Films() As DbSet(Of Film)
        Public Property Topics() As DbSet(Of Topic)
        Public Property VideoClips() As DbSet(Of VideoClip)
        Public Property XPObjectTypes() As DbSet(Of XPObjectType)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New ArtMap())
            modelBuilder.Configurations.Add(New CameraMap())
            modelBuilder.Configurations.Add(New CountriesMap())
            modelBuilder.Configurations.Add(New CountryLocationMap())
            modelBuilder.Configurations.Add(New FilmMap())
            modelBuilder.Configurations.Add(New TopicMap())
            modelBuilder.Configurations.Add(New VideoClipMap())
            modelBuilder.Configurations.Add(New XPObjectTypeMap())
        End Sub
    End Class
    Partial Public Class Art
        Public Property LastWriteTime() As Date?
        Public Property Name() As String
        Public Property ID() As Integer
        Public Property ParentID() As Integer?
        Public Property IsFolder() As Boolean?
        Public Property Data() As Byte()
        Public Property OptimisticLockField() As Integer?
        Public Property GCRecord() As Integer?
        Public Property SSMA_TimeStamp() As Byte()
    End Class
    Partial Public Class Camera
        Public Property ID() As Integer
        Public Property Model() As String
        Public Property Pixels() As Double?
        Public Property ImageFileName() As String
        Public Property SSMA_TimeStamp() As Byte()
    End Class
    Partial Public Class OriginCountry
        Public Property ID() As Integer
        Public Property Name() As String
        Public Property Capital() As String
        Public Property Continent() As String
        Public Property Area() As Double?
        Public Property Population() As Double?
        Public Property SSMA_TimeStamp() As Byte()
    End Class
    Partial Public Class CountryLocation
        Public Property ID() As Integer
        Public Property Text() As String
        Public Property NavigateUrl() As String
        Public Property Location() As String
    End Class
    Partial Public Class Film
        Public Property ID() As Integer
        Public Property Name() As String
        Public Property Genre() As String
        Public Property Year() As Integer?
        Public Property NavigateUrl() As String
    End Class
    Partial Public Class Topic
        Public Property ID() As Integer
        Public Property Text() As String
        Public Property NavigateURL() As String
        Public Property Popularity() As Integer?
    End Class
    Partial Public Class VideoClip
        Public Property ID() As Integer
        Public Property CategoryName() As String
        Public Property NavigateURL() As String
        Public Property Popularity() As Integer?
    End Class
    Partial Public Class XPObjectType
        Public Property OID() As Integer
        Public Property TypeName() As String
        Public Property AssemblyName() As String
    End Class
    #Region "Mapping"
    Public Class ArtMap
        Inherits EntityTypeConfiguration(Of Art)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Name).HasMaxLength(100)
            Me.Property(Function(t) t.SSMA_TimeStamp).IsRequired().IsFixedLength().HasMaxLength(8).IsRowVersion()
            Me.ToTable("Arts")
            Me.Property(Function(t) t.LastWriteTime).HasColumnName("LastWriteTime")
            Me.Property(Function(t) t.Name).HasColumnName("Name")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.ParentID).HasColumnName("ParentID")
            Me.Property(Function(t) t.IsFolder).HasColumnName("IsFolder")
            Me.Property(Function(t) t.Data).HasColumnName("Data")
            Me.Property(Function(t) t.OptimisticLockField).HasColumnName("OptimisticLockField")
            Me.Property(Function(t) t.GCRecord).HasColumnName("GCRecord")
            Me.Property(Function(t) t.SSMA_TimeStamp).HasColumnName("SSMA_TimeStamp")
        End Sub
    End Class
    Public Class CameraMap
        Inherits EntityTypeConfiguration(Of Camera)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Model).HasMaxLength(50)
            Me.Property(Function(t) t.ImageFileName).HasMaxLength(50)
            Me.Property(Function(t) t.SSMA_TimeStamp).IsRequired().IsFixedLength().HasMaxLength(8).IsRowVersion()
            Me.ToTable("Cameras")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Model).HasColumnName("Model")
            Me.Property(Function(t) t.Pixels).HasColumnName("Pixels")
            Me.Property(Function(t) t.ImageFileName).HasColumnName("ImageFileName")
            Me.Property(Function(t) t.SSMA_TimeStamp).HasColumnName("SSMA_TimeStamp")
        End Sub
    End Class
    Public Class CountryLocationMap
        Inherits EntityTypeConfiguration(Of CountryLocation)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Text).HasMaxLength(50)
            Me.Property(Function(t) t.NavigateUrl).HasMaxLength(50)
            Me.Property(Function(t) t.Location).HasMaxLength(50)
            Me.ToTable("CountryLocation")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Text).HasColumnName("Text")
            Me.Property(Function(t) t.NavigateUrl).HasColumnName("NavigateUrl")
            Me.Property(Function(t) t.Location).HasColumnName("Location")
        End Sub
    End Class
    Public Class CountriesMap
        Inherits EntityTypeConfiguration(Of OriginCountry)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Name).HasMaxLength(255)
            Me.Property(Function(t) t.Capital).HasMaxLength(255)
            Me.Property(Function(t) t.Continent).HasMaxLength(255)
            Me.Property(Function(t) t.SSMA_TimeStamp).IsRequired().IsFixedLength().HasMaxLength(8).IsRowVersion()
            Me.ToTable("country")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Name).HasColumnName("Name")
            Me.Property(Function(t) t.Capital).HasColumnName("Capital")
            Me.Property(Function(t) t.Continent).HasColumnName("Continent")
            Me.Property(Function(t) t.Area).HasColumnName("Area")
            Me.Property(Function(t) t.Population).HasColumnName("Population")
            Me.Property(Function(t) t.SSMA_TimeStamp).HasColumnName("SSMA_TimeStamp")
        End Sub
    End Class
    Public Class FilmMap
        Inherits EntityTypeConfiguration(Of Film)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Name).HasMaxLength(50)
            Me.Property(Function(t) t.Genre).HasMaxLength(50)
            Me.Property(Function(t) t.NavigateUrl).HasMaxLength(50)
            Me.ToTable("Films")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Name).HasColumnName("Name")
            Me.Property(Function(t) t.Genre).HasColumnName("Genre")
            Me.Property(Function(t) t.Year).HasColumnName("Year")
            Me.Property(Function(t) t.NavigateUrl).HasColumnName("NavigateUrl")
        End Sub
    End Class
    Public Class TopicMap
        Inherits EntityTypeConfiguration(Of Topic)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Text).HasMaxLength(50)
            Me.Property(Function(t) t.NavigateURL).HasMaxLength(50)
            Me.ToTable("Topics")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Text).HasColumnName("Text")
            Me.Property(Function(t) t.NavigateURL).HasColumnName("NavigateURL")
            Me.Property(Function(t) t.Popularity).HasColumnName("Popularity")
        End Sub
    End Class
    Public Class VideoClipMap
        Inherits EntityTypeConfiguration(Of VideoClip)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.CategoryName).HasMaxLength(50)
            Me.Property(Function(t) t.NavigateURL).HasMaxLength(50)
            Me.ToTable("VideoClips")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.CategoryName).HasColumnName("CategoryName")
            Me.Property(Function(t) t.NavigateURL).HasColumnName("NavigateURL")
            Me.Property(Function(t) t.Popularity).HasColumnName("Popularity")
        End Sub
    End Class
    Public Class XPObjectTypeMap
        Inherits EntityTypeConfiguration(Of XPObjectType)

        Public Sub New()
            Me.HasKey(Function(t) t.OID)
            Me.Property(Function(t) t.TypeName).HasMaxLength(254)
            Me.Property(Function(t) t.AssemblyName).HasMaxLength(254)
            Me.ToTable("XPObjectType")
            Me.Property(Function(t) t.OID).HasColumnName("OID")
            Me.Property(Function(t) t.TypeName).HasColumnName("TypeName")
            Me.Property(Function(t) t.AssemblyName).HasColumnName("AssemblyName")
        End Sub
    End Class
    #End Region
End Namespace
