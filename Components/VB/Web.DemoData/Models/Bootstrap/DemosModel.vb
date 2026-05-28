'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports DevExpress.Utils
Namespace DevExpress.Web.Demos
    <XmlRoot("Demos")>
    Public Class BootstrapDemosModel
        Private Const Demos3ModelPath As String = "Data.Bootstrap.Demos3.xml"
        Private Const DemosModelPath As String = "Data.Bootstrap.Demos.xml"
        Private Shared _instance4 As BootstrapDemosModel
        Private Shared ReadOnly _instance4Lock As New Object()
        Private Shared _instance3 As BootstrapDemosModel
        Private Shared ReadOnly _instance3Lock As New Object()

        Private demoGroups_Renamed As New List(Of BootstrapDemoGroupPageModel)()

        Private sortedDemoGroups_Renamed As List(Of BootstrapDemoGroupPageModel)
        Public Shared ReadOnly Property Instance() As BootstrapDemosModel
            Get
                Return If(BootstrapUtils.BootstrapVersion > 3, Instance4, Instance3)
            End Get
        End Property
        Protected Shared ReadOnly Property Instance4() As BootstrapDemosModel
            Get
                SyncLock _instance4Lock
                    If _instance4 Is Nothing Then
                        Using stream As Stream = ResourcesUtils.GetEmbeddedResource(DemosModelPath)
                            _instance4 = SafeXml.Deserialize(Of BootstrapDemosModel)(stream)
                        End Using
                        For Each group In _instance4.DemoGroups
                            For Each demo In group.Demos
                                demo.Group = group
                                For Each section In demo.Sections
                                    section.Demo = demo
                                Next section
                            Next demo
                        Next group
                    End If
                    Return _instance4
                End SyncLock
            End Get
        End Property
        Protected Shared ReadOnly Property Instance3() As BootstrapDemosModel
            Get
                SyncLock _instance3Lock
                    If _instance3 Is Nothing Then
                        Using stream As Stream = ResourcesUtils.GetEmbeddedResource(Demos3ModelPath)
                            _instance3 = SafeXml.Deserialize(Of BootstrapDemosModel)(stream)
                        End Using
                        For Each group In _instance3.DemoGroups
                            For Each demo In group.Demos
                                demo.Group = group
                                For Each section In demo.Sections
                                    section.Demo = demo
                                Next section
                            Next demo
                        Next group
                    End If
                    Return _instance3
                End SyncLock
            End Get
        End Property
        <XmlElement("DemoGroup")>
        Public ReadOnly Property DemoGroups() As List(Of BootstrapDemoGroupPageModel)
            Get
                Return demoGroups_Renamed
            End Get
        End Property
        <XmlElement("GettingStarted")>
        Public Property GettingsStarted() As GettingStartedPageModel
        <XmlIgnore>
        Public ReadOnly Property SortedDemoGroups() As List(Of BootstrapDemoGroupPageModel)
            Get
                If sortedDemoGroups_Renamed Is Nothing Then
                    sortedDemoGroups_Renamed = DemoGroups.OrderBy(Function(p) p.OrderIndex).ToList()
                End If
                Return sortedDemoGroups_Renamed
            End Get
        End Property
        <XmlElement("Search")>
        Public Property Search() As SearchModel
        Public Function FindGroup(ByVal key As String) As BootstrapDemoGroupPageModel
            If Not String.IsNullOrEmpty(key) Then
                key = key.ToLower()
                For Each group As BootstrapDemoGroupPageModel In DemoGroups
                    If key = group.Key.ToLower() Then
                        Return group
                    End If
                Next group
            End If
            Return Nothing
        End Function
    End Class
End Namespace
