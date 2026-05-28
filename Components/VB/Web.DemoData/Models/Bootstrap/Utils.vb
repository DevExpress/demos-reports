'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Linq
Imports System.Configuration
Namespace DevExpress.Web.Demos
    Public NotInheritable Class BootstrapUtils

        Private Sub New()
        End Sub

        Private Shared ReadOnly Property Context() As HttpContext
            Get
                Return HttpContext.Current
            End Get
        End Property
        Public Shared ReadOnly Property BootstrapVersion() As Integer
            Get
                Dim ver As Integer = 3
                Return If(Integer.TryParse(ConfigurationManager.AppSettings("DemoBSVer"), ver), ver, 3)
            End Get
        End Property
        Public Shared ReadOnly Property CurrentDemo() As BootstrapDemoPageModelBase
            Get
                Return If(Context IsNot Nothing, DirectCast(Context.Items(Utils.CurrentDemoKey), BootstrapDemoPageModelBase), Nothing)
            End Get
        End Property
        Public Shared ReadOnly Property CurrentDemoSections() As IEnumerable(Of BootstrapDemoModelBase)
            Get
                If TypeOf CurrentDemo Is BootstrapDemoPageModel Then
                    Return DirectCast(CurrentDemo, BootstrapDemoPageModel).Sections
                End If
                If TypeOf CurrentDemo Is GettingStartedPageModel Then
                    Return DirectCast(CurrentDemo, GettingStartedPageModel).Sections
                End If
                Return Nothing
            End Get
        End Property
        Private Shared sourceFilesCode As New ConcurrentDictionary(Of String, String)()
        Public Shared Function GetSourceFileCode(ByVal fileName As String) As String
            fileName = fileName.ToLowerInvariant()
            If Not sourceFilesCode.ContainsKey(fileName) Then
                Dim code As String = BootstrapDemoSectionModel.GetCodeFromFile(fileName)
                sourceFilesCode.AddOrUpdate(fileName, BootstrapDemoSectionCodeParser.GetFormattedCustomCode(code), Function(exCode, nCode) nCode)
            End If
            Return sourceFilesCode(fileName)
        End Function
        Public Shared Function IsSourceFileExist(ByVal virtualPath As String) As Boolean
            If Context IsNot Nothing Then
                Dim path As String = Context.Server.MapPath(virtualPath)
                Return File.Exists(path)
            End If
            Return False
        End Function
        Public Shared Sub RegisterCurrentDemo(ByVal page As Page)
            If page.AppRelativeVirtualPath.ToLowerInvariant().Contains("gettingstarted") Then
                Context.Items(Utils.CurrentDemoKey) = BootstrapDemosModel.Instance.GettingsStarted
                DevExpress.Web.Internal.DemoUtils.RegisterDemo("Bootstrap", "GettingStarted", "")
            Else
                Dim groupKey As String = ""
                Dim demoKey As String = ""
                Utils.FindDemoKeysByVirtualPath(page.AppRelativeVirtualPath, groupKey, demoKey)
                RegisterCurrentDemo(groupKey, demoKey)
            End If
        End Sub
        Private Shared Sub RegisterCurrentDemo(ByVal groupKey As String, ByVal demoKey As String)
            Dim demo As BootstrapDemoPageModelBase = Nothing
            Dim group As BootstrapDemoGroupPageModel = BootstrapDemosModel.Instance.FindGroup(groupKey)
            If group IsNot Nothing Then
                demo = group.FindDemo(demoKey)
                If demo Is Nothing Then
                    demo = group
                End If
            End If
            Context.Items(Utils.CurrentDemoKey) = demo
            DevExpress.Web.Internal.DemoUtils.RegisterDemo("Bootstrap", groupKey, demoKey)
        End Sub
        Public Shared Function GenerateDemoPageUrl(ByVal demo As BootstrapDemoPageModelBase) As String
            Dim stb As New StringBuilder()
            stb.Append("~/")
            If TypeOf demo Is BootstrapDemoGroupPageModel Then
                stb.Append(demo.Key)
                stb.Append("/Default.aspx")
            ElseIf TypeOf demo Is BootstrapDemoPageModel Then
                Dim group = DirectCast(demo, BootstrapDemoPageModel).Group
                If group IsNot Nothing Then
                    stb.Append(group.Key & "/")
                End If
                stb.Append(demo.Key & ".aspx")
            End If
            Return stb.ToString()
        End Function
        Public Shared Function GenerateDemoSectionUrl(ByVal section As BootstrapDemoSectionModel) As String
            If section.Demo IsNot Nothing Then
                Return String.Format("{0}#{1}", GenerateDemoPageUrl(section.Demo), section.Key)
            End If
            Return String.Empty
        End Function
        Public Shared Function GenerateGettingStartedSectionUrl(ByVal section As GettingStartedSectionModel) As String
            Return String.Format("GettingStarted.aspx#{0}", section.Key)
        End Function
        Public Shared Function GetCurrentDemoTitle() As String
            Dim title = "DevExpress Bootstrap Controls for ASP.NET"
            If CurrentDemo Is Nothing Then
                Return title
            End If
            Return String.Format("{0} | {1}", CurrentDemo.GetSeoTitle(), title)
        End Function
    End Class
End Namespace
