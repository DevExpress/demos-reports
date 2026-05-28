'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Serialization
Namespace DevExpress.Web.Demos
    Public Class BootstrapDemoModelBase
        Inherits DemoModelBase

        Private processedDescriptions As New Dictionary(Of String, String)()

        Private links_Renamed As String
        <XmlElement>
        Public Overridable Property Description() As String
        <XmlIgnore>
        Public ReadOnly Property Links() As String
            Get
                If String.IsNullOrEmpty(links_Renamed) Then
                    links_Renamed = ParseLinks()
                End If
                Return links_Renamed
            End Get
        End Property
        Public Function GetProcessedDescription() As String
            Return GetProcessedDescription(Description, "Description")
        End Function
        Protected Function GetProcessedDescription(ByVal description As String, ByVal key As String) As String
            If Not processedDescriptions.ContainsKey(key) Then
                processedDescriptions(key) = ProcessDescription(description)
            End If
            Return processedDescriptions(key)
        End Function
        Protected Overridable Function ParseLinks() As String
            Return DemoModelParser.ParseLinks(Description)
        End Function
        Protected Overrides Function GetKeywordsRankList() As Dictionary(Of String, Integer)
            Return BootstrapSearchUtils.GetKeywordsRankList(Me)
        End Function
        Protected Overridable Function ProcessDescription(ByVal description As String) As String
            Return DemoModelParser.ProcessDescription(description)
        End Function
    End Class
    Public Class BootstrapDemoPageModelBase
        Inherits BootstrapDemoModelBase

        Public Function GetSeoTitle() As String
            If Not String.IsNullOrEmpty(SeoTitle) Then
                Return SeoTitle
            End If
            Return Title
        End Function
        Public Overridable Function HasPreviewStatus() As Boolean
            Return IsPreview
        End Function
        Public Overridable Function HasNewStatus() As Boolean
            Return IsNew
        End Function
        Public Overridable Function HasUpdatedStatus() As Boolean
            Return IsUpdated
        End Function
    End Class
    Public Class BootstrapDemoPageModel
        Inherits BootstrapDemoPageModelBase


        Private sections_Renamed As New List(Of BootstrapDemoSectionModel)()
        <XmlElement(Type := GetType(BootstrapDemoSectionModel), ElementName := "DemoSection")>
        Public ReadOnly Property Sections() As List(Of BootstrapDemoSectionModel)
            Get
                Return sections_Renamed
            End Get
        End Property
        Private privateGroup As BootstrapDemoGroupPageModel
        <XmlIgnore>
        Public Property Group() As BootstrapDemoGroupPageModel
            Get
                Return privateGroup
            End Get
            Friend Set(ByVal value As BootstrapDemoGroupPageModel)
                privateGroup = value
            End Set
        End Property
        Public Function FindSection(ByVal key As String) As BootstrapDemoSectionModel
            If Not String.IsNullOrEmpty(key) Then
                key = key.ToLower()
                For Each section As BootstrapDemoSectionModel In Sections
                    If key = section.Key.ToLower() Then
                        Return section
                    End If
                Next section
            End If
            Return Nothing
        End Function
        Public Overrides Function HasUpdatedStatus() As Boolean
            Return MyBase.HasUpdatedStatus() OrElse Sections.Where(Function(s) s.IsNew OrElse s.IsUpdated).Any()
        End Function
    End Class
End Namespace
