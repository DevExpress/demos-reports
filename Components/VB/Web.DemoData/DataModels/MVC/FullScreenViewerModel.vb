'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.Linq
Namespace DevExpress.Web.Demos
    Public Class FullScreenViewerModel
        Public Sub New()
        End Sub
        Public Sub New(ByVal url As String, ByVal deviceHeight As Integer, ByVal deviceWidth As Integer)
            Me.DeviceHeight = deviceHeight
            Me.DeviceWidth = deviceWidth
            Me.Url = url
        End Sub
        Private Const DocumentFrameSize As Integer = 40
        Public Property DeviceHeight() As Integer
        Public Property DeviceWidth() As Integer
        Public Property Url() As String
        Public ReadOnly Property IsFullScreen() As Boolean
            Get
                Return DeviceHeight = 0 AndAlso DeviceWidth = 0
            End Get
        End Property
        Public ReadOnly Property FullDeviceHeight() As Integer
            Get
                Return DeviceHeight + DocumentFrameSize
            End Get
        End Property
        Public ReadOnly Property FullDeviceWidth() As Integer
            Get
                Return DeviceWidth + DocumentFrameSize
            End Get
        End Property
        Public ReadOnly Property ComboBoxSizeDataSource() As List(Of ComboBoxViewModel)
            Get
                If comboBoxSizeDataSource_Renamed Is Nothing Then
                    comboBoxSizeDataSource_Renamed = GetComboBoxSizeDataSource()
                End If
                Return comboBoxSizeDataSource_Renamed
            End Get
        End Property
        Public ReadOnly Property ComboBoxThemeDataSource() As List(Of ComboBoxViewModel)
            Get
                If comboBoxThemeDataSource_Renamed Is Nothing Then
                    comboBoxThemeDataSource_Renamed = GetComboBoxThemeDataSource()
                End If
                Return comboBoxThemeDataSource_Renamed
            End Get
        End Property
        Public ReadOnly Property ComboBoxColorDataSource() As List(Of ComboBoxViewModel)
            Get
                If comboBoxColorDataSource_Renamed Is Nothing Then
                    comboBoxColorDataSource_Renamed = GetComboBoxColorDataSource()
                End If
                Return comboBoxColorDataSource_Renamed
            End Get
        End Property

        Private comboBoxSizeDataSource_Renamed As List(Of ComboBoxViewModel)

        Private comboBoxThemeDataSource_Renamed As List(Of ComboBoxViewModel)

        Private comboBoxColorDataSource_Renamed As List(Of ComboBoxViewModel)
        Private Function GetComboBoxSizeDataSource() As List(Of ComboBoxViewModel)
            Return FullScreenModeUtils.Resolutions.Select(Function(r) New ComboBoxViewModel() With {.Text = r.Name, .Value = "?width=" & r.Width & "&height=" & r.Height & "&Url=" & Url, .ImageUrl = r.ImageUrl, .Selected = r.Width = DeviceWidth AndAlso r.Height = DeviceHeight}).ToList()
        End Function
        Private Function GetComboBoxThemeDataSource() As List(Of ComboBoxViewModel)
            Return Utils.CurrentThemesModel.MobileThemes.Select(Function(t) New ComboBoxViewModel() With {.Value = t, .ImageUrl = Utils.GetColoredSquareUrl(Utils.GetDefaultBaseColor(t)), .Selected = t = Utils.CurrentTheme}).ToList()
        End Function
        Private Function GetComboBoxColorDataSource() As List(Of ComboBoxViewModel)
            Dim currentBaseColor As String = If((Not String.IsNullOrWhiteSpace(Utils.CurrentBaseColor)), Utils.CurrentBaseColor, Utils.GetDefaultBaseColor(Utils.CurrentTheme))
            Return Utils.CustomBaseColors.Select(Function(c) New ComboBoxViewModel() With {.Value = c, .ImageUrl = Utils.GetColoredSquareUrl(c), .Selected = c = currentBaseColor}).ToList()
        End Function
    End Class
    Public Class ComboBoxViewModel
        Public Property Text() As String
        Public Property Value() As String
        Public Property ImageUrl() As String
        Public Property Selected() As Boolean
    End Class
End Namespace
