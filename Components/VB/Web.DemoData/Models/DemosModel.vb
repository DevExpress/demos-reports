'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.IO
Imports System.Web
Imports System.Linq
Imports System.Configuration
Imports System
Imports DevExpress.Utils
Namespace DevExpress.Web.Demos
    <XmlRoot("Demos")>
    Public Class DemosModel
        Private Shared _instance As DemosModel
        Private Shared _current As DemoProductModel
        Private Shared _availableDemos As New Dictionary(Of String, DemoModel)()
        Private Const DemosModelPath As String = "Data.Asp.Demos.xml"
        Private Const DemosModelMvcPath As String = "Data.Mvc.Demos.xml"
        Private _globalHeader As New GlobalHeaderModel()
        Private Shared ReadOnly _instanceLock As New Object()
        Private Shared ReadOnly _currentLock As New Object()
        Private _expandAllDemosAtFirstTime As Boolean
        Private _disableTextWrap As Boolean
        <XmlIgnore>
        Public Shared ReadOnly Property AvailableDemos() As Dictionary(Of String, DemoModel)
            Get
                Return _availableDemos
            End Get
        End Property
        Public Shared ReadOnly Property IsMvc() As Boolean
            Get
                Dim assemblies = AppDomain.CurrentDomain.GetAssemblies()
                Return assemblies.Any(Function(x) x.FullName.Contains("System.Web.Mvc"))
            End Get
        End Property
        Public Shared ReadOnly Property Instance() As DemosModel
            Get
                SyncLock _instanceLock
                    If _instance Is Nothing Then
                        _instance = CreateInstance()
                    End If
                    Return _instance
                End SyncLock
            End Get
        End Property
        Public Shared Function CreateInstance() As DemosModel
            Return CreateInstance(IsMvc)
        End Function
        Public Shared Function CreateInstance(ByVal isMvc As Boolean) As DemosModel

            Dim instance_Renamed As DemosModel = Nothing
            Using stream As Stream = GetDemosModelContentStream(isMvc)
                instance_Renamed = SafeXml.Deserialize(Of DemosModel)(stream)
            End Using
            For Each demoProduct In instance_Renamed.DemoProducts
                For Each group In demoProduct.Groups
                    For Each demo In group.GetAllDemos(False)
                        demo.Group = group
                        demo.Product = demoProduct
                        AddDemoToCache(demo)
                    Next demo
                    group.Product = demoProduct
                Next group
                If demoProduct.Intro IsNot Nothing Then
                    demoProduct.Intro.Product = demoProduct
                End If
            Next demoProduct
            Return instance_Renamed
        End Function
        Private Shared Sub AddDemoToCache(ByVal demo As DemoPageModel)
            Dim productKey = demo.Product.Key
            Dim groupKey = Utils.GetDemoGroupKey(demo)
            Dim fullKey = (productKey & groupKey & demo.Key).ToLower()
            AvailableDemos(fullKey) = demo
        End Sub
        Public Function FindDemo(ByVal productKey As String, ByVal groupKey As String, ByVal demoKey As String) As DemoModel
            Dim fullKey = (productKey & groupKey & demoKey).ToLower()
            If AvailableDemos.ContainsKey(fullKey) Then
                Return AvailableDemos(fullKey)
            End If
            Return Nothing
        End Function
        Public Shared ReadOnly Property Current() As DemoProductModel
            Get
                SyncLock _currentLock
                    If _current Is Nothing Then
                        _current = Instance.DemoProducts.FirstOrDefault(Function(dp) dp.Key = ConfigurationManager.AppSettings("DemoProduct"))
                    End If
                    If _current Is Nothing Then
                        Throw New Exception("The current demo is not found")
                    End If
                    Return _current
                End SyncLock
            End Get
        End Property
        Private Shared Function GetDemosModelContentStream(ByVal isMvc As Boolean) As Stream
            Dim demosModelFilePath = HttpContext.Current.Server.MapPath("~/App_Data/Demos.xml")
            If File.Exists(demosModelFilePath) Then
                Return File.OpenRead(demosModelFilePath)
            End If
            Return ResourcesUtils.GetEmbeddedResource(If(isMvc, DemosModelMvcPath, DemosModelPath))
        End Function
        Private _demoProducts As New List(Of DemoProductModel)()
        Private _sortedDemoProducts As List(Of DemoProductModel)
        Private _search As SearchModel
        <XmlElement("DemoProduct")>
        Public ReadOnly Property DemoProducts() As List(Of DemoProductModel)
            Get
                Return _demoProducts
            End Get
        End Property
        <XmlIgnore>
        Public ReadOnly Property SortedDemoProducts() As List(Of DemoProductModel)
            Get
                If _sortedDemoProducts Is Nothing Then
                    _sortedDemoProducts = _demoProducts.OrderBy(Function(p) p.OrderIndex).ToList()
                End If
                Return _sortedDemoProducts
            End Get
        End Property
        <XmlElement("Search")>
        Public Property Search() As SearchModel
            Get
                Return _search
            End Get
            Set(ByVal value As SearchModel)
                _search = value
            End Set
        End Property
        <XmlElement("GlobalHeader")>
        Public Property GlobalHeader() As GlobalHeaderModel
            Get
                Return _globalHeader
            End Get
            Set(ByVal value As GlobalHeaderModel)
                _globalHeader = value
            End Set
        End Property
        <XmlAttribute>
        Public Property ExpandAllDemosAtFirstTime() As Boolean
            Get
                Return _expandAllDemosAtFirstTime
            End Get
            Set(ByVal value As Boolean)
                _expandAllDemosAtFirstTime = value
            End Set
        End Property
        <XmlAttribute>
        Public Property DisableTextWrap() As Boolean
            Get
                Return _disableTextWrap
            End Get
            Set(ByVal value As Boolean)
                _disableTextWrap = value
            End Set
        End Property
    End Class
    Public Class GlobalHeaderModel
        Private _logoPlatformSubject As String = "ASP.NET AJAX"
        Private _logoPlatformDescription As String = "WHEN THE WEB MEANS BUSINESS"
        <XmlAttribute>
        Public Property LogoPlatformSubject() As String
            Get
                Return _logoPlatformSubject
            End Get
            Set(ByVal value As String)
                _logoPlatformSubject = value
            End Set
        End Property
        <XmlAttribute>
        Public Property LogoPlatformDescription() As String
            Get
                Return _logoPlatformDescription
            End Get
            Set(ByVal value As String)
                _logoPlatformDescription = value
            End Set
        End Property
    End Class
    Public Class DemoProductModel
        Inherits ModelBase

        Private _isMvc As Boolean
        Private _isMvcRazor As Boolean
        Private _isRootDemo As Boolean
        Private _isPreview As Boolean
        Private _isNew As Boolean
        Private _key As String
        Private _url As String
        Private _title As String
        Private _seoTitle As String
        Private _navItemTitle As String
        Private _supportsTheming As Boolean = True
        Private _supportsThemeParameters As Boolean = True
        Private _hideNavItem As Boolean = False
        Private _hideSourceCode As Boolean
        Private _groups As New List(Of DemoGroupModel)()
        Private _intro As IntroPageModel
        Private _orderIndex As Integer = 1000
        Private _integrationHighlighted As Boolean = False
        Private _integrationImageUrl As String
        Private _integrationDescription As String
        Private _highlightedDemos As List(Of DemoPageModel)
        <XmlAttribute>
        Public Property IsMvc() As Boolean
            Get
                Return _isMvc
            End Get
            Set(ByVal value As Boolean)
                _isMvc = value
            End Set
        End Property
        <XmlAttribute>
        Public Property IsMvcRazor() As Boolean
            Get
                Return _isMvcRazor
            End Get
            Set(ByVal value As Boolean)
                _isMvcRazor = value
            End Set
        End Property
        <XmlAttribute>
        Public Property IsRootDemo() As Boolean
            Get
                Return _isRootDemo
            End Get
            Set(ByVal value As Boolean)
                _isRootDemo = value
            End Set
        End Property
        <XmlAttribute>
        Public Property IsPreview() As Boolean
            Get
                Return _isPreview
            End Get
            Set(ByVal value As Boolean)
                _isPreview = value
            End Set
        End Property
        <XmlAttribute>
        Public Property IsNew() As Boolean
            Get
                Return _isNew
            End Get
            Set(ByVal value As Boolean)
                _isNew = value
            End Set
        End Property
        <XmlAttribute>
        Public Property HideNavItem() As Boolean
            Get
                Return _hideNavItem
            End Get
            Set(ByVal value As Boolean)
                _hideNavItem = value
            End Set
        End Property
        <XmlAttribute>
        Public Property Key() As String
            Get
                If _key Is Nothing Then
                    Return ""
                End If
                Return _key
            End Get
            Set(ByVal value As String)
                _key = value
            End Set
        End Property
        <XmlAttribute>
        Public Property Url() As String
            Get
                If _url Is Nothing Then
                    Throw New Exception("URL is not defined")
                End If
                Return _url
            End Get
            Set(ByVal value As String)
                _url = value
            End Set
        End Property
        <XmlAttribute>
        Public Property Title() As String
            Get
                If _title Is Nothing Then
                    Return ""
                End If
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property
        <XmlAttribute>
        Public Property SeoTitle() As String
            Get
                If _seoTitle Is Nothing Then
                    Return ""
                End If
                Return _seoTitle
            End Get
            Set(ByVal value As String)
                _seoTitle = value
            End Set
        End Property
        <XmlAttribute>
        Public Property NavItemTitle() As String
            Get
                If _navItemTitle Is Nothing Then
                    Return ""
                End If
                Return _navItemTitle
            End Get
            Set(ByVal value As String)
                _navItemTitle = value
            End Set
        End Property
        <XmlAttribute>
        Public Property OrderIndex() As Integer
            Get
                Return _orderIndex
            End Get
            Set(ByVal value As Integer)
                _orderIndex = value
            End Set
        End Property
        <XmlAttribute>
        Public Property IntegrationHighlighted() As Boolean
            Get
                Return _integrationHighlighted
            End Get
            Set(ByVal value As Boolean)
                _integrationHighlighted = value
            End Set
        End Property
        <XmlElement>
        Public Property IntegrationImageUrl() As String
            Get
                Return _integrationImageUrl
            End Get
            Set(ByVal value As String)
                _integrationImageUrl = value
            End Set
        End Property
        <XmlElement>
        Public Property IntegrationDescription() As String
            Get
                Return _integrationDescription
            End Get
            Set(ByVal value As String)
                _integrationDescription = value
            End Set
        End Property
        <XmlElement("DemoGroup")>
        Public ReadOnly Property Groups() As List(Of DemoGroupModel)
            Get
                Return _groups
            End Get
        End Property
        Public Function GetVisibleGroups() As List(Of DemoGroupModel)
            Return Groups.Where(Function(g) (Not g.HideNavItem)).ToList()
        End Function
        <XmlAttribute>
        Public Property SupportsTheming() As Boolean
            Get
                Return _supportsTheming
            End Get
            Set(ByVal value As Boolean)
                _supportsTheming = value
            End Set
        End Property
        <XmlAttribute>
        Public Property SupportsThemeParameters() As Boolean
            Get
                Return _supportsThemeParameters
            End Get
            Set(ByVal value As Boolean)
                _supportsThemeParameters = value
            End Set
        End Property
        <XmlIgnore>
        Public ReadOnly Property HighlightedDemos() As List(Of DemoPageModel)
            Get
                If _highlightedDemos Is Nothing Then
                    _highlightedDemos = CreateHighlightedDemos()
                End If
                Return _highlightedDemos
            End Get
        End Property
        <XmlElement(Type := GetType(IntroPageModel), ElementName := "Intro")>
        Public Property Intro() As IntroPageModel
            Get
                Return _intro
            End Get
            Set(ByVal value As IntroPageModel)
                _intro = value
            End Set
        End Property
        <XmlIgnore>
        Public ReadOnly Property IsCurrent() As Boolean
            Get
                Return Me Is DemosModel.Current
            End Get
        End Property
        <XmlAttribute>
        Public Overridable Property HideSourceCode() As Boolean
            Get
                Return _hideSourceCode
            End Get
            Set(ByVal value As Boolean)
                _hideSourceCode = value
            End Set
        End Property
        Private Function CreateHighlightedDemos() As List(Of DemoPageModel)
            Dim result As New List(Of DemoPageModel)()
            For Each group As DemoGroupModel In GetVisibleGroups()
                For Each demo As DemoPageModel In group.GetVisibleDemos()
                    If demo.HighlightedIndex > -1 Then
                        result.Add(demo)
                    End If
                Next demo
            Next group
            result.Sort(AddressOf CompareHighlightedDemos)
            Return result
        End Function
        Private Function CompareHighlightedDemos(ByVal x As DemoModel, ByVal y As DemoModel) As Integer
            Return Comparer(Of Integer).Default.Compare(x.HighlightedIndex, y.HighlightedIndex)
        End Function
        Public Function GetSeoTitle() As String
            If Not String.IsNullOrEmpty(SeoTitle) Then
                Return SeoTitle
            End If
            Return Title
        End Function
    End Class
End Namespace
