'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Utils
Imports DevExpress.Web.Internal
Namespace DevExpress.Web.Demos
    Public Enum RecalculateColumnCountMode
        RootGroup
        ChildGroups
    End Enum
    Public Structure ControlOptionsSettings
        Public Property RightBlockWidth() As Integer
        Public Property ColumnMinWidth() As Integer
        Public Property ColumnCountMode() As RecalculateColumnCountMode
    End Structure
    Public Class DemoHelper
        Private Const DefaultControlAreaMaxWidth As Integer = 960
        Private Const DefaultControlAreaMinWidth As Integer = 0
        Private Const DarkThemeName As String = "Office365Dark"
        Private Const MaterialCompactOrangeTheme As String = "MaterialCompactOrange"
        Friend Const CurrentDemoHelperKey As String = "DXCurrentDemoHelper"
        Public Sub New()
            ControlAreaMaxWidth = DefaultControlAreaMaxWidth
            ControlAreaMinWidth = DefaultControlAreaMinWidth
            ControlOptionsRightBlockWidth = 0
            ControlAreaMinHeight = ControlOptionsRightBlockWidth
        End Sub
        Public Property SuppressThemeSelector() As Boolean
        Public Property UseDevExtremeThemeSelector() As Boolean
        Public Property ControlAreaMinHeight() As Unit
        Public Property ControlAreaMaxWidth() As Unit
        Public Property ControlAreaMinWidth() As Unit
        Public Property ControlOptionsRightBlockWidth() As Unit
        Public ReadOnly Property Theme() As String
            Get
                Return If(DevExpress.Web.ASPxWebControl.GlobalTheme <> DarkThemeName, MaterialCompactOrangeTheme, DarkThemeName)
            End Get
        End Property
        Private Sub AppyCssClassesForFormLayout(ByVal formLayout As ASPxFormLayout)
            formLayout.CssClass = " form-layout"
            formLayout.Styles.LayoutGroupBox.CssClass &= " group-box"
            formLayout.Styles.LayoutGroupBox.Caption.CssClass &= " group-box-caption"
            formLayout.Styles.LayoutGroup.CssClass &= " group"
            formLayout.Styles.LayoutGroup.Cell.CssClass &= " cell"
            formLayout.Styles.LayoutItem.Caption.CssClass &= " item-caption"
        End Sub
        Private Sub ApplySettingsForFormLayout(ByVal formLayout As ASPxFormLayout)
            formLayout.AlignItemCaptionsInAllGroups = True
            formLayout.SettingsItemCaptions.HorizontalAlign = FormLayoutHorizontalAlign.Left
            formLayout.SettingsAdaptivity.AdaptivityMode = FormLayoutAdaptivityMode.SingleColumnWindowLimit
        End Sub
        Public Sub PrepareControlOptions(ByVal formLayout As ASPxFormLayout, Optional ByVal settings? As ControlOptionsSettings = Nothing)
            PrepareFormLayout(formLayout, True, settings)
        End Sub
        Public Sub PrepareControlLayout(ByVal formLayout As ASPxFormLayout, Optional ByVal settings? As ControlOptionsSettings = Nothing)
            PrepareFormLayout(formLayout, False, settings)
        End Sub
        Private Sub PrepareFormLayout(ByVal formLayout As ASPxFormLayout, ByVal isOptionFormLayout As Boolean, Optional ByVal settings? As ControlOptionsSettings = Nothing)
            If settings IsNot Nothing Then
                Dim optionsValue = settings.Value
                If optionsValue.ColumnMinWidth <> 0 Then
                    Dim breakpoints As LayoutBreakpoints = formLayout.SettingsAdaptivity.GridSettings.Breakpoints
                    If optionsValue.ColumnCountMode = RecalculateColumnCountMode.RootGroup AndAlso breakpoints.IsEmpty Then
                        For i As Integer = 0 To 9
                            breakpoints.Add(New LayoutBreakpoint(i.ToString(), ((i + 2) * optionsValue.ColumnMinWidth), i + 1))
                        Next i
                    End If
                    formLayout.SettingsAdaptivity.GridSettings.StretchLastItem = DefaultBoolean.True
                End If
                If optionsValue.RightBlockWidth <> 0 Then
                    ControlOptionsRightBlockWidth = Unit.Pixel(optionsValue.RightBlockWidth)
                End If
            End If
            formLayout.ForRootChildGroups(Sub(group) PrepareFLGroup(group, isOptionFormLayout, settings))
            AppyCssClassesForFormLayout(formLayout)
            ApplySettingsForFormLayout(formLayout)
            If isOptionFormLayout Then
                formLayout.Theme = Theme
            End If
            formLayout.ForEach(Sub(itemBase) PrepareFLItem(itemBase, isOptionFormLayout))
        End Sub
        Public Sub PrepareFLGroup(ByVal group As LayoutGroup, ByVal isOptionFormLayout As Boolean, ByVal settings? As ControlOptionsSettings)
            If isOptionFormLayout Then
                group.GroupBoxDecoration = GroupBoxDecoration.HeadingLine
                group.Height = Unit.Percentage(100R)
            End If
            If settings IsNot Nothing Then
                Dim optionsValue = settings.Value
                Dim layoutGroupMaxWidth As Integer = 1240
                Dim maxLayoutColumnsInGroup As Integer = layoutGroupMaxWidth \ optionsValue.ColumnMinWidth
                Dim breakpoints As LayoutBreakpoints = group.GridSettings.Breakpoints
                If optionsValue.ColumnMinWidth <> 0 AndAlso optionsValue.ColumnCountMode = RecalculateColumnCountMode.ChildGroups AndAlso breakpoints.IsEmpty Then
                    Dim itemsCount As Integer = 10
                    If isOptionFormLayout Then
                        itemsCount = maxLayoutColumnsInGroup
                        group.ColumnCount = maxLayoutColumnsInGroup
                    End If
                    For i As Integer = 0 To itemsCount - 1
                        Dim breakpointMaxWidth As Integer = (i + 2) * optionsValue.ColumnMinWidth
                        breakpoints.Add(New LayoutBreakpoint(i.ToString(), breakpointMaxWidth, i + 1))
                    Next i
                End If
                group.GridSettings.StretchLastItem = DefaultBoolean.False
                group.GridSettings.ChangeCaptionLocationAtWidth = optionsValue.ColumnMinWidth
            End If
        End Sub
        Public Sub PrepareFLItem(ByVal itemBase As LayoutItemBase, ByVal isOptionFormLayout As Boolean)
            Dim layoutItem = TryCast(itemBase, LayoutItem)
            If layoutItem Is Nothing Then
                Return
            End If
            Dim controls = layoutItem.Controls.OfType(Of Control)().ToList()
            If Utils.IsMvc Then
                controls.Add(layoutItem.GetNestedControl())
            End If
            For Each rawControl In controls
                Dim control = TryCast(rawControl, ASPxWebControl)
                If control Is Nothing Then
                    Continue For
                End If
                control.CssClass &= " field"
                If isOptionFormLayout Then
                    control.Theme = Theme
                End If
                If TypeOf control Is ASPxCheckBox Then
                    layoutItem.CaptionSettings.ChangeCaptionLocationInAdaptiveMode = DefaultBoolean.False
                End If
            Next rawControl
        End Sub
        Public Shared ReadOnly Property Instance() As DemoHelper
            Get
                If HttpContext.Current.Items(CurrentDemoHelperKey) Is Nothing Then
                    HttpContext.Current.Items(CurrentDemoHelperKey) = New DemoHelper()
                End If
                Return DirectCast(HttpContext.Current.Items(CurrentDemoHelperKey), DemoHelper)
            End Get
        End Property
    End Class
    Public Module FormLayoutDemoExtenstion
        <System.Runtime.CompilerServices.Extension> _
        Public Sub ForEachNestedControl(ByVal formLayout As ASPxFormLayout, ByVal callback As Action(Of Control))
            formLayout.ForEach(Sub(itemBase)
                Dim item = TryCast(itemBase, LayoutItem)
                If item IsNot Nothing Then
                    For Each nestedControl As Control In item.LayoutItemNestedControlCollection(0).Controls
                        callback(nestedControl)
                    Next nestedControl
                End If
            End Sub)
        End Sub
        <System.Runtime.CompilerServices.Extension> _
        Public Sub ForRootChildGroups(ByVal formLayout As ASPxFormLayout, ByVal callback As Action(Of LayoutGroup))
            For Each item As LayoutItemBase In formLayout.Items
                Dim group = TryCast(item, LayoutGroup)
                If group IsNot Nothing Then
                    callback(group)
                End If
            Next item
        End Sub
    End Module
End Namespace
