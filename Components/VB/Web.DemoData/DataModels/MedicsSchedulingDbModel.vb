'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports DevExpress.Web.Demos
Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.ModelConfiguration
Namespace DevExpress.Web.Demos
    Partial Public Class MedicsSchedulingDBContext
        Inherits ContextBase

        Shared Sub New()
            Database.SetInitializer(Of MedicsSchedulingDBContext)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("MedicsSchedulingDbConnectionString")
        End Sub
        Public Property MedicalAppointments() As DbSet(Of MedicsSchedulingDb_MedicalAppointments)
        Public Property Medics() As DbSet(Of MedicsSchedulingDb_Medics)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New MedicsSchedulingDb_MedicalAppointmentsMap())
            modelBuilder.Configurations.Add(New MedicsSchedulingDb_MedicsMap())
        End Sub
    End Class
    Partial Public Class MedicsSchedulingDBContextSL
        Inherits DbContext

        Shared Sub New()
            Database.SetInitializer(Of MedicsSchedulingDBContextSL)(Nothing)
        End Sub
        Public Sub New()
            MyBase.New("MedicsSchedulingDbConnectionStringSL")
        End Sub
        Public Property MedicalAppointments() As DbSet(Of MedicsSchedulingDb_MedicalAppointments)
        Public Property Medics() As DbSet(Of MedicsSchedulingDb_Medics)
        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Configurations.Add(New MedicsSchedulingDb_MedicalAppointmentsMap())
            modelBuilder.Configurations.Add(New MedicsSchedulingDb_MedicsMap())
        End Sub
    End Class
    Partial Public Class MedicsSchedulingDb_MedicalAppointments
        Public Property ID() As Integer
        Public Property MedicId() As Integer?
        Public Property MedicIds() As String
        Public Property Status() As Integer?
        Public Property Subject() As String
        Public Property Description() As String
        Public Property Label() As Integer?
        Public Property StartTime() As Date?
        Public Property EndTime() As Date?
        Public Property Location() As String
        Public Property AllDay() As Boolean
        Public Property EventType() As Integer?
        Public Property RecurrenceInfo() As String
        Public Property ReminderInfo() As String
        Public Property ContactInfo() As String
    End Class
    Partial Public Class MedicsSchedulingDb_Medics
        Public Property ID() As Integer
        Public Property Name() As String
        Public Property DisplayName() As String
        Public Property Department() As String
        Public Property Phone() As String
        Public Property PhotoBytes() As Byte()
    End Class
    #Region "Mapping"
    Public Class MedicsSchedulingDb_MedicalAppointmentsMap
        Inherits EntityTypeConfiguration(Of MedicsSchedulingDb_MedicalAppointments)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.Subject).HasMaxLength(50)
            Me.Property(Function(t) t.Location).HasMaxLength(50)
            Me.ToTable("MedicalAppointments")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.MedicId).HasColumnName("MedicId")
            Me.Property(Function(t) t.MedicIds).HasColumnName("MedicIds")
            Me.Property(Function(t) t.Status).HasColumnName("Status")
            Me.Property(Function(t) t.Subject).HasColumnName("Subject")
            Me.Property(Function(t) t.Description).HasColumnName("Description")
            Me.Property(Function(t) t.Label).HasColumnName("Label")
            Me.Property(Function(t) t.StartTime).HasColumnName("StartTime")
            Me.Property(Function(t) t.EndTime).HasColumnName("EndTime")
            Me.Property(Function(t) t.Location).HasColumnName("Location")
            Me.Property(Function(t) t.AllDay).HasColumnName("AllDay")
            Me.Property(Function(t) t.EventType).HasColumnName("EventType")
            Me.Property(Function(t) t.RecurrenceInfo).HasColumnName("RecurrenceInfo")
            Me.Property(Function(t) t.ReminderInfo).HasColumnName("ReminderInfo")
            Me.Property(Function(t) t.ContactInfo).HasColumnName("ContactInfo")
        End Sub
    End Class
    Public Class MedicsSchedulingDb_MedicsMap
        Inherits EntityTypeConfiguration(Of MedicsSchedulingDb_Medics)

        Public Sub New()
            Me.HasKey(Function(t) t.ID)
            Me.Property(Function(t) t.ID)
            Me.Property(Function(t) t.Name).HasMaxLength(50)
            Me.Property(Function(t) t.DisplayName).HasMaxLength(50)
            Me.Property(Function(t) t.Department).HasMaxLength(50)
            Me.Property(Function(t) t.Phone).HasMaxLength(50)
            Me.ToTable("Medics")
            Me.Property(Function(t) t.ID).HasColumnName("ID")
            Me.Property(Function(t) t.Name).HasColumnName("Name")
            Me.Property(Function(t) t.DisplayName).HasColumnName("DisplayName")
            Me.Property(Function(t) t.Department).HasColumnName("Department")
            Me.Property(Function(t) t.Phone).HasColumnName("Phone")
            Me.Property(Function(t) t.PhotoBytes).HasColumnName("PhotoBytes")
        End Sub
    End Class
    #End Region
End Namespace
