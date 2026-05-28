'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.Linq
Namespace DevExpress.Web.Demos
    Public Class DemoGroupModel
        Inherits DemoModelBase


        Private virtualGroupKey_Renamed As String
        Private _demos As New List(Of DemoPageModel)()
        <XmlAttribute>
        Public Property VirtualGroupKey() As String
            Get
                If virtualGroupKey_Renamed Is Nothing Then
                    Return ""
                End If
                Return virtualGroupKey_Renamed
            End Get
            Set(ByVal value As String)
                virtualGroupKey_Renamed = value
            End Set
        End Property
        <XmlElement(Type := GetType(DemoPageModel), ElementName := "Demo")>
        Public ReadOnly Property Demos() As List(Of DemoPageModel)
            Get
                Return _demos
            End Get
        End Property
        Public Function GetVisibleDemos() As List(Of DemoPageModel)
            Return Demos.Where(Function(d) (Not d.HideNavItem)).ToList()
        End Function
        Public Function GetAllDemos(ByVal onlyVisible As Boolean) As List(Of DemoPageModel)
            Dim result As New List(Of DemoPageModel)()
            If onlyVisible Then
                result.AddRange(GetVisibleDemos())
            Else
                result.AddRange(Demos)
            End If
            Return result
        End Function
        <XmlIgnore>
        Public Property Product() As DemoProductModel
    End Class
End Namespace
