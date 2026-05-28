'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Xml.Serialization
Imports System.Linq
Imports System.Collections.Generic
Namespace DevExpress.Web.Demos
    Public Class BootstrapDemoGroupPageModel
        Inherits BootstrapDemoPageModelBase


        Private demos_Renamed As New List(Of BootstrapDemoPageModel)()

        Private demoGroupMarkerName_Renamed As String
        <XmlElement(Type := GetType(BootstrapDemoPageModel), ElementName := "Demo")>
        Public ReadOnly Property Demos() As List(Of BootstrapDemoPageModel)
            Get
                Return demos_Renamed
            End Get
        End Property
        <XmlElement>
        Public Property PreDescription() As String
        <XmlAttribute>
        Public Property IconCssClass() As String
        <XmlAttribute>
        Public Property OrderIndex() As Integer
        Public ReadOnly Property DemoGroupMarkerName() As String
            Get
                If String.IsNullOrEmpty(demoGroupMarkerName_Renamed) Then
                    demoGroupMarkerName_Renamed = Key & "Group"
                End If
                Return demoGroupMarkerName_Renamed
            End Get
        End Property
        Public Function GetProcessedPreDescription() As String
            Return GetProcessedDescription(PreDescription, "PreDescription")
        End Function
        Public Function FindDemo(ByVal key As String) As BootstrapDemoPageModel
            If Not String.IsNullOrEmpty(key) Then
                key = key.ToLower()
                For Each demo As BootstrapDemoPageModel In Demos
                    If key = demo.Key.ToLower() Then
                        Return demo
                    End If
                Next demo
            End If
            Return Nothing
        End Function
        Public Overrides Function HasUpdatedStatus() As Boolean
            Return MyBase.HasUpdatedStatus() OrElse Demos.Where(Function(d) d.HasNewStatus() OrElse d.HasUpdatedStatus()).Any()
        End Function
        Protected Overrides Function ParseLinks() As String
            Return DemoModelParser.ParseLinks(Description & PreDescription)
        End Function
    End Class
End Namespace
