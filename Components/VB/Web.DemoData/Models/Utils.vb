'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports DevExpress.Web.Internal
Imports System.Drawing
Imports DevExpress.Data.Extensions
Namespace DevExpress.Web.Demos
    Public Class SourceCodePage
        Public Title As String = ""
        Public Code As String = ""
        Public Expanded As Boolean = False
        Public Sub New(ByVal title As String, ByVal code As String, ByVal expanded As Boolean)
            Me.Title = title
            Me.Code = code
            Me.Expanded = expanded
        End Sub
    End Class
    Public Class FeaturedDemoInfo
        Public Property Title() As String
        Public Property Description() As String
        Public Property NavigateUrl() As String
        Public Property ImageUrl() As String
    End Class
    Public Class ProductInfo
        Public Property Title() As String
        Public Property Description() As String
        Public Property NavigateUrl() As String
        Public Property ImageUrl() As String
    End Class
    Public NotInheritable Class Utils

        Private Sub New()
        End Sub

        Friend Const CurrentDemoKey As String = "DXCurrentDemo", CurrentThemeAspCookieKeyPrefix As String = "DXCurrentTheme", CurrentThemeDevExtremeCookieKeyPrefix As String = "DXCurrentDevExtremeTheme", CurrentBaseColorCookieKeyPrefix As String = "DXCurrentBaseColor", CurrentFontCookieKey As String = "DXCurrentFont", TunableThemeCookieKeyPrefix As String = "DXTunableTheme", DefaultThemeName As String = "MaterialCompact", DevextremeDefaultThemeName As String = "light", BogusDemoTitle As String = "Delivered!", IsAccessibilityDemoKey As String = "IsAccessibilityDemo", ResourseFilePath As String = "DevExpress.Web.Demos.Properties.Resources", CorpColor As String = "#F78119"
        Private Shared TreeControlNoTreeLinesThemes() As String = { "DevEx", "Metropolis", "MetropolisBlue", "Moderno", "Mulberry", "iOS", "Material", "MaterialCompact", "Office2010Blue", "Office2010Black", "Office2010Silver", "Office365", "Office365Dark" }
        Private Shared TreeListNoGridLinesThemes() As String = { "DevEx", "Office2010Blue", "Office2010Black", "Office2010Silver" }
        Private Shared ReadOnly sourceCodeCache As New Dictionary(Of DemoModel, IEnumerable(Of SourceCodePage))()
        Private Shared ReadOnly sourceCodeCacheLock As New Object()
        Private Shared _codeLanguage As String
        Private Shared ReadOnly Property Context() As HttpContext
            Get
                Return HttpContext.Current
            End Get
        End Property
        Private Shared ReadOnly Property Request() As HttpRequest
            Get
                Return Context.Request
            End Get
        End Property
        Public Shared ReadOnly Property IsMvc() As Boolean
            Get
                Return DemosModel.Current.IsMvc
            End Get
        End Property
        Public Shared ReadOnly Property IsIncludeThirdPartyClientLibraries() As Boolean
            Get
                Dim resources = DevExpress.Web.Internal.ConfigurationSettings.Resources
                Return resources IsNot Nothing AndAlso resources.Contains(ResourcesType.ThirdParty)
            End Get
        End Property
        Public Shared ReadOnly Property CurrentDemo() As DemoModel
            Get
                Return DirectCast(Context.Items(CurrentDemoKey), DemoModel)
            End Get
        End Property
        Public Shared ReadOnly Property RootProduct() As DemoProductModel
            Get
                Return DemosModel.Instance.SortedDemoProducts.Find(Function(p) p.IsRootDemo)
            End Get
        End Property
        Public Shared ReadOnly Property CurrentDemoNodeName() As String
            Get
                If CurrentDemoPage IsNot Nothing Then
                    Return String.Format("{0}_{1}_{2}", CurrentDemoPage.Product.Key, CurrentDemoPage.Group.Key, CurrentDemoPage.Key)
                End If
                If CurrentDemo IsNot Nothing Then
                    Return CurrentDemo.Product.Key
                End If
                Return Nothing
            End Get
        End Property
        Public Shared ReadOnly Property CurrentDemoTitleHtml() As String
            Get
                Return GetDemoTitleHtml(CurrentDemo)
            End Get
        End Property
        Public Shared ReadOnly Property CurrentIntro() As IntroPageModel
            Get
                If TypeOf CurrentDemo Is IntroPageModel OrElse CurrentDemo Is Nothing Then
                    Return DirectCast(CurrentDemo, IntroPageModel)
                End If
                Return CurrentDemo.Product.Intro
            End Get
        End Property
        Public Shared ReadOnly Property CurrentDemoPage() As DemoPageModel
            Get
                Return If((Not IsIntro), TryCast(CurrentDemo, DemoPageModel), Nothing)
            End Get
        End Property
        Public Shared ReadOnly Property CurrentThemeAspCookieKey() As String
            Get
                Return CurrentThemeAspCookieKeyPrefix
            End Get
        End Property
        Public Shared ReadOnly Property CurrentThemeDevExtremeCookieKey() As String
            Get
                Return CurrentThemeDevExtremeCookieKeyPrefix
            End Get
        End Property
        Public Shared ReadOnly Property CurrentBaseColorCookieKey() As String
            Get
                Return CurrentBaseColorCookieKeyPrefix
            End Get
        End Property
        Public Shared ReadOnly Property TunableThemeCookieKey() As String
            Get
                Return TunableThemeCookieKeyPrefix
            End Get
        End Property
        Public Shared ReadOnly Property IsLargeTheme() As Boolean
            Get
                Return ThemesProvider.IsLargeTheme(CurrentTheme)
            End Get
        End Property
        Public Shared ReadOnly Property CurrentTheme() As String
            Get
                Dim storedTheme As String = Nothing
                If CanChangeTheme AndAlso TryGetStoredTheme(storedTheme) Then
                    Return storedTheme
                End If
                Return If((Not DemoHelper.Instance.UseDevExtremeThemeSelector), DefaultThemeName, DevextremeDefaultThemeName)
            End Get
        End Property
        Public Shared Function CurrentStoredTheme(ByVal cookieKey As String) As String
            Return If(Request.Cookies(cookieKey) IsNot Nothing, HttpUtility.UrlDecode(Request.Cookies(cookieKey).Value), Utils.CurrentTheme)
        End Function
        Public Shared ReadOnly Property CurrentThemesModel() As ThemesModelBase
            Get
                If Not DemoHelper.Instance.UseDevExtremeThemeSelector Then
                    Return ThemesModel.Current
                Else
                    Return DevExtremeThemesModel.Current
                End If
            End Get
        End Property
        Public Shared ReadOnly Property CurrentThemeCookieKey() As String
            Get
                If Not DemoHelper.Instance.UseDevExtremeThemeSelector Then
                    Return CurrentThemeAspCookieKey
                Else
                    Return CurrentThemeDevExtremeCookieKey
                End If
            End Get
        End Property
        Public Shared Function TryGetStoredTheme(<System.Runtime.InteropServices.Out()> ByRef storedTheme As String) As Boolean
            storedTheme = Nothing
            If Request.Cookies(CurrentThemeCookieKey) IsNot Nothing Then
                Dim rawTheme = HttpUtility.UrlDecode(Request.Cookies(CurrentThemeCookieKey).Value)
                If CurrentThemesModel.GetAllThemes().Any(Function(t) t.Name = rawTheme) Then
                    storedTheme = rawTheme
                    Return True
                Else
                    Request.Cookies.Remove(CurrentThemeCookieKey)
                    Context.Response.Cookies(CurrentThemeCookieKey).Expires = Date.Now.AddDays(-1)
                End If
            End If
            Return False
        End Function
        Public Shared ReadOnly Property CurrentBaseColor() As String
            Get
                If Request.Cookies(CurrentBaseColorCookieKey) IsNot Nothing Then
                    Return HttpUtility.UrlDecode(Request.Cookies(CurrentBaseColorCookieKey).Value)
                End If
                If Request.Cookies(CurrentThemeAspCookieKey) Is Nothing Then
                    Return CorpColor
                End If
                Return CurrentThemeDefaultBaseColor
            End Get
        End Property
        Private Shared Sub SetCurrentBaseColorCookie(ByVal value As String)
            If String.IsNullOrWhiteSpace(value) Then
                Request.Cookies.Remove(CurrentBaseColorCookieKey)
            Else
                Request.Cookies(CurrentBaseColorCookieKey).Value = value
            End If
            Context.Response.Cookies(CurrentBaseColorCookieKey).Value = value
        End Sub
        Public Shared ReadOnly Property CurrentFont() As String
            Get
                If Request.Cookies(CurrentFontCookieKey) IsNot Nothing Then
                    Return HttpUtility.UrlDecode(Request.Cookies(CurrentFontCookieKey).Value)
                End If
                Return CurrentThemeDefaultFont
            End Get
        End Property
        Private Shared Sub SetCurrentFontCookie(ByVal value As String)
            Context.Response.Cookies(CurrentFontCookieKey).Value = value
        End Sub
        Public Shared ReadOnly Property CurrentThemeTitle() As String
            Get
                Dim theme = CurrentTheme
                Dim themeModel = (CurrentThemesModel.GetThemeModel(theme))
                Return If(themeModel IsNot Nothing, themeModel.Title, theme)
            End Get
        End Property
        Private Shared Property TunableTheme() As String
            Get
                If Request.Cookies(TunableThemeCookieKey) IsNot Nothing Then
                    Return HttpUtility.UrlDecode(Request.Cookies(TunableThemeCookieKey).Value)
                End If
                Return CurrentTheme
            End Get
            Set(ByVal value As String)
                Context.Response.Cookies(TunableThemeCookieKey).Value = value
            End Set
        End Property
        Public Shared ReadOnly Property IsIntro() As Boolean
            Get
                Return TypeOf Utils.CurrentDemo Is IntroPageModel
            End Get
        End Property
        Public Shared ReadOnly Property IsPageRequest() As Boolean
            Get
                Return Request.Url.AbsolutePath.ToLower().Contains(".aspx")
            End Get
        End Property
        Public Shared ReadOnly Property IsBogusDemo() As Boolean
            Get
                Return If(CurrentDemo IsNot Nothing, CurrentDemo.Title = BogusDemoTitle, False)
            End Get
        End Property
        Public Shared Function GetDemoTitleHtml(ByVal demo As DemoModel) As String
            Dim result As String = String.Empty
            If TypeOf demo Is DemoPageModel Then
                result = String.Format("{0} - {1}", DirectCast(demo, DemoPageModel).Group.Title, demo.Title)
            End If
            If String.IsNullOrEmpty(result) Then
                result = demo.Title
            ElseIf result.Length > 60 Then
                result = demo.Title
            End If
            Return HttpUtility.HtmlEncode(result)
        End Function
        Public Shared ReadOnly Property CodeLanguage() As String
            Get
                If _codeLanguage Is Nothing Then
                    Try
                        Using _file As FileStream = File.OpenRead(Context.Server.MapPath("~/Site.master"))
                        Using reader As TextReader = New StreamReader(_file)
                            Dim line As String = reader.ReadLine()
                            Dim match As Match = Regex.Match(line, "language=""([^""]+)", RegexOptions.IgnoreCase)
                            If match.Success Then
                                _codeLanguage = match.Groups(1).Value.ToUpper()
                            End If
                        End Using
                        End Using
                    Catch
                    End Try
                    If String.IsNullOrEmpty(_codeLanguage) Then
                        _codeLanguage = "C#"
                    End If
                End If
                Return _codeLanguage
            End Get
        End Property
        Public Shared Function GetCurrentSourceCodePages() As IEnumerable(Of SourceCodePage)
            Return GetSourceCodePages(TryCast(CurrentDemo, DemoPageModel))
        End Function
        Public Shared Function GetSourceCodePages(ByVal demo As DemoPageModel) As IEnumerable(Of SourceCodePage)
            SyncLock sourceCodeCacheLock
                If Not sourceCodeCache.ContainsKey(demo) Then
                    sourceCodeCache(demo) = CreateSourceCodePages(demo)
                End If
                Return sourceCodeCache(demo)
            End SyncLock
        End Function
        Private Shared Function CreateSourceCodePages(ByVal demo As DemoPageModel) As IEnumerable(Of SourceCodePage)
            Dim result As New List(Of SourceCodePage)()
            If IsMvc Then
                FillMvcModelPages(demo, result)
                FillMvcControllerPages(demo, result)
                FillMvcViewPages(demo, result)
                FillMvcOtherPages(demo, result)
            Else
                Dim baseUrl = GenerateDemoUrl(demo)
                Dim highlightedTagNames = GetHighlightedTagNames(demo)
                AddSourceCodePage(result, "ASPX", baseUrl, True, True, highlightedTagNames)
                If HasCodeFile(baseUrl) Then
                    AddSourceCodePage(result, "C#", baseUrl & ".cs", CodeLanguage = "C#")
                    AddSourceCodePage(result, "VB", baseUrl & ".vb", CodeLanguage = "VB")
                End If
                For Each fileName As String In demo.SourceFiles
                    AddSourceCodePage(result, Path.GetFileName(fileName), fileName, False, True, highlightedTagNames)
                Next fileName
            End If
            Return result
        End Function
        Private Shared Function GetHighlightedTagNames(ByVal demo As DemoPageModel) As String()
            Dim highlightedTagNames = demo.Group.GetHighlightedTagNames().Concat(demo.GetHighlightedTagNames()).Concat(demo.Product.GetHighlightedTagNames()).ToArray()
            Return highlightedTagNames
        End Function
        Private Shared Sub FillMvcModelPages(ByVal demo As DemoPageModel, ByVal result As List(Of SourceCodePage))
            FillMvcModelFromResources(demo, result)
            For Each fileName As String In demo.SourceFiles
                If fileName.StartsWith("~/Models/") Then
                    AddSourceCodePage(result, String.Format("Model ({0})", Path.GetFileNameWithoutExtension(fileName)), fileName, False)
                End If
            Next fileName
        End Sub
        Public Shared Sub FillMvcModelFromResources(ByVal demo As DemoPageModel, ByVal result As List(Of SourceCodePage))
            For Each key As String In demo.ResourceKeys
                Dim keyParts = key.Split("_"c)
                If keyParts(0).Equals("Model") OrElse keyParts(0).Equals("DataProvider") Then
                    AddSourceCodePage(result, String.Format("Model ({0})", keyParts(1)), key, False, True)
                End If
            Next key
        End Sub
        Public Shared Function GetDemoGroupKey(ByVal demo As DemoPageModel) As String
            If Not String.IsNullOrEmpty(demo.VirtualGroupKey) Then
                Return demo.VirtualGroupKey
            End If
            Dim group = demo.Group
            If Not String.IsNullOrEmpty(group.VirtualGroupKey) Then
                Return group.VirtualGroupKey
            End If
            Return group.Key
        End Function
        Private Shared Sub FillMvcControllerPages(ByVal demo As DemoPageModel, ByVal result As List(Of SourceCodePage))
            Dim controllerUrl As String = String.Format("~/Controllers/{0}/{0}Controller.{1}.cs", GetDemoGroupKey(demo), demo.Key)
            AddSourceCodePageIfExists(result, "Controller", controllerUrl, True)
            Dim commonControllerUrl As String = String.Format("~/Controllers/{0}Controller.cs", GetDemoGroupKey(demo))
            AddSourceCodePageIfExists(result, "Controller (common)", commonControllerUrl, False)
        End Sub
        Private Shared Sub FillMvcViewPages(ByVal demo As DemoPageModel, ByVal result As List(Of SourceCodePage))
            Dim viewUrl As String = String.Format("~/Views/{0}/{1}.cshtml", GetDemoGroupKey(demo), demo.Key)
            AddSourceCodePage(result, "View", viewUrl, True, False)
        End Sub
        Private Shared Sub FillMvcOtherPages(ByVal demo As DemoPageModel, ByVal result As List(Of SourceCodePage))
            FillMvcOtherPagesFromResources(demo, result)
            For Each fileName As String In demo.SourceFiles
                If fileName.StartsWith("~/Views/") Then
                    AddSourceCodePage(result, String.Format("View ({0})", Path.GetFileNameWithoutExtension(fileName)), fileName, True)
                ElseIf Not fileName.StartsWith("~/Models/") Then
                    AddSourceCodePage(result, Path.GetFileName(fileName), fileName, True)
                End If
            Next fileName
        End Sub
        Public Shared Sub FillMvcOtherPagesFromResources(ByVal demo As DemoPageModel, ByVal result As List(Of SourceCodePage))
            For Each key As String In demo.ResourceKeys
                Dim keyParts = key.Split("_"c)
                If keyParts(0).Equals("Code") Then
                    AddSourceCodePage(result, keyParts(1), key, False, True)
                End If
            Next key
        End Sub
        Private Shared Sub AddSourceCodePageIfExists(ByVal list As List(Of SourceCodePage), ByVal title As String, ByVal url As String, ByVal expanded As Boolean)
            AddSourceCodePage(list, title, url, expanded, False, New String(){}, False)
        End Sub
        Private Shared Sub AddSourceCodePage(ByVal list As List(Of SourceCodePage), ByVal title As String, ByVal url As String, ByVal expanded As Boolean, Optional ByVal isResource As Boolean = False)
            AddSourceCodePage(list, title, url, expanded, True, New String(){}, isResource)
        End Sub
        Private Shared Sub AddSourceCodePage(ByVal list As List(Of SourceCodePage), ByVal title As String, ByVal url As String, ByVal expanded As Boolean, ByVal showIfError As Boolean, Optional ByVal isResource As Boolean = False)
            AddSourceCodePage(list, title, url, expanded, showIfError, New String(){}, isResource)
        End Sub
        Private Shared Sub AddSourceCodePage(ByVal list As List(Of SourceCodePage), ByVal title As String, ByVal url As String, ByVal expanded As Boolean, ByVal showIfError As Boolean, ByVal highlightedTagNames() As String, Optional ByVal isResource As Boolean = False)
            Dim content As String = String.Empty
            Try
                content = GetHighlightedFileContent(url, highlightedTagNames, isResource)
            Catch
                content = If(showIfError, "Error rendering source code", String.Empty)
            End Try
            If Not String.IsNullOrEmpty(content) Then
                list.Add(New SourceCodePage(title, content, expanded))
            End If
        End Sub
        Private Shared Function HasCodeFile(ByVal url As String) As Boolean
            Dim filePath As String = GetHighlightedFilePath(url)
            If Not File.Exists(filePath) Then
                Return False
            End If
            Dim text As String = File.ReadAllText(filePath)
            Return Regex.IsMatch(text, "<[^>]*\bCodebehind\s*=\s*""[\w\.]+\""[^>]*>")
        End Function
        Private Shared Function GetHighlightedFileContent(ByVal virtualPath As String, ByVal highlightedTagNames() As String, ByVal isResource As Boolean) As String
            Dim text As String = String.Empty
            Dim filePath As String = virtualPath
            If Not isResource Then
                filePath = GetHighlightedFilePath(virtualPath)
                text = File.ReadAllText(filePath)
            Else
                Dim resourceManager = New System.Resources.ResourceManager(ResourseFilePath, System.Reflection.Assembly.GetExecutingAssembly())
                text = resourceManager.GetString(virtualPath)
            End If
            Return CodeFormatter.GetFormattedCode(Path.GetExtension(filePath.Replace("_", ".")), text, IsMvc, highlightedTagNames)
        End Function
        Private Shared Function GetHighlightedFilePath(ByVal virtualPath As String) As String
            Dim result As String = (New DirectoryInfo(Context.Server.MapPath("~/"))).FullName
            result = Path.Combine(result, ".Source")
            result = Path.Combine(result, virtualPath.Substring(2))
            If File.Exists(result) Then
                Return result
            End If
            result = Context.Server.MapPath(virtualPath)
            If Not File.Exists(result) Then
                result = CorrectSourceFilePath(result)
            End If
            Return result
        End Function
        Private Shared Function CorrectSourceFilePath(ByVal filePath As String) As String
            Dim csPathItem As String = String.Format("{0}cs{0}", Path.DirectorySeparatorChar)
            Dim vbPathItem As String = String.Format("{0}vb{0}", Path.DirectorySeparatorChar)
            filePath = filePath.ToLower()
            If filePath.EndsWith(".cs") Then
                Return filePath.Replace(vbPathItem, csPathItem)
            End If
            If filePath.EndsWith(".vb") Then
                Return filePath.Replace(csPathItem, vbPathItem)
            End If
            Return filePath
        End Function
        Public Shared Function GetVersionText() As String
            Dim parts() As String = AssemblyInfo.Version.Split("."c)
            Return String.Format("v{0} vol {1}.{2}", 2000 + Integer.Parse(parts(0)), parts(1), parts(2))
        End Function
        Public Shared Function GetVersionSuffix() As String
            Return AssemblyInfo.Version.Replace(".", "_")
        End Function
        Public Shared Sub FindDemoKeysByVirtualPath(ByVal virtualPath As String, <System.Runtime.InteropServices.Out()> ByRef groupKey As String, <System.Runtime.InteropServices.Out()> ByRef demoKey As String)
            Dim path As String = virtualPath.ToLower().Replace("~/", "").Replace(".aspx", "").Trim("/"c)
            Dim parts() As String = path.Split("/"c)
            If parts.Length < 1 Then
                Throw New ArgumentException("Invalid demo URL")
            End If
            If parts.Length > 1 Then
                groupKey = parts(parts.Length - 2)
                demoKey = parts(parts.Length - 1)
            Else
                groupKey = ""
                demoKey = parts(0)
            End If
        End Sub
        Public Shared Sub RegisterCurrentWebFormsDemo(ByVal page As Page)
            Dim groupKey As String = ""
            Dim demoKey As String = ""
            FindDemoKeysByVirtualPath(page.AppRelativeVirtualPath, groupKey, demoKey)
            RegisterCurrentDemo(groupKey, demoKey)
        End Sub
        Public Shared Sub RegisterCurrentMvcDemoOnCallback()
            Dim groupKey As String = Nothing, demoKey As String = Nothing
            Dim controllerName As String = Request.RequestContext.RouteData.Values("controller").ToString()
            FindDemoKeysByRoute(controllerName, Request.UrlReferrer.Segments, Request.AppRelativeCurrentExecutionFilePath, groupKey, demoKey)
            RegisterCurrentMvcDemo(groupKey, demoKey)
        End Sub
        Public Shared Sub FindDemoKeysByRoute(ByVal controllerName As String, ByVal referrerSegments() As String, ByVal appRelativeCurrentExecutionFilePath As String, <System.Runtime.InteropServices.Out()> ByRef groupKey As String, <System.Runtime.InteropServices.Out()> ByRef demoKey As String)
            groupKey = controllerName
            demoKey = String.Empty
            Dim controllerNamePartIndex = referrerSegments.FindIndex(Function(part) String.Equals(part.Trim("/"c), controllerName, StringComparison.InvariantCultureIgnoreCase))
            If controllerNamePartIndex > -1 AndAlso appRelativeCurrentExecutionFilePath.Contains(controllerName) Then
                If String.Equals(controllerName, "overview", StringComparison.InvariantCultureIgnoreCase) Then
                    demoKey = controllerName
                    groupKey = String.Empty
                ElseIf referrerSegments.Length > controllerNamePartIndex + 1 Then
                    demoKey = referrerSegments(controllerNamePartIndex + 1)
                End If
            End If
            demoKey = If((Not String.IsNullOrEmpty(demoKey)), demoKey, "Index")
        End Sub
        Public Shared Sub RegisterCurrentMvcDemo(ByVal controllerName As String, ByVal actionName As String)
            RegisterCurrentDemo(controllerName, actionName)
        End Sub
        Public Shared Function IsIntroPage(ByVal groupKey As String, ByVal demoKey As String) As Boolean
            If IsMvc Then
                Return groupKey.Equals("Home", StringComparison.InvariantCultureIgnoreCase) AndAlso demoKey.Equals("Index", StringComparison.InvariantCultureIgnoreCase)
            End If
            Return demoKey.Equals("default", StringComparison.InvariantCultureIgnoreCase)
        End Function
        Public Shared Function IsOverviewPage(ByVal demoKey As String) As Boolean
            Return demoKey.Equals("overview", StringComparison.InvariantCultureIgnoreCase)
        End Function
        Private Shared Sub RegisterCurrentDemo(ByVal groupKey As String, ByVal demoKey As String)
            Dim demo As DemoModel = Nothing
            If IsIntroPage(groupKey, demoKey) Then
                demo = DemosModel.Current.Intro
            ElseIf IsErrorPage(demoKey) Then
                demo = CreateErrorPageDemoModel()
            Else
                demo = DemosModel.Instance.FindDemo(DemosModel.Current.Key, groupKey, demoKey)
            End If
            If demo Is Nothing Then
                demo = CreateBogusDemoModel()
            End If
            Context.Items(CurrentDemoKey) = demo
            DevExpress.Web.Internal.DemoUtils.RegisterDemo(DemosModel.Current.Key, groupKey, demoKey)
        End Sub
        Private Shared Function CreateBogusDemoModel() As DemoPageModel
            Dim group As New DemoGroupModel()
            group.Title = "ASP.NET"
            Dim result As New DemoPageModel()
            result.Group = group
            result.HideSourceCode = True
            result.Title = BogusDemoTitle
            Return result
        End Function
        Private Shared Function CreateErrorPageDemoModel() As DemoPageModel
            Dim result As DemoPageModel = New DemoPageModel With {.IsErrorPage = True, .Product = DemosModel.Current, .Group = New DemoGroupModel()}
            Return result
        End Function
        Public Shared Function GetCurrentDemoPageTitle() As String
            Dim builder As New StringBuilder()
            If CurrentDemo.IsErrorPage Then
                builder.Append("Error Page - ")
                builder.Append(DemosModel.Current.GetSeoTitle())
                If Not DemosModel.Current.IsRootDemo Then
                    builder.Append(" Demo")
                End If
            ElseIf TypeOf CurrentDemo Is IntroPageModel Then
                builder.Append(CurrentDemo.SeoTitle)
            ElseIf TypeOf CurrentDemo Is DemoPageModel Then
                Dim product As String = DemosModel.Current.GetSeoTitle()
                Dim demoGroup As DemoGroupModel = DirectCast(CurrentDemo, DemoPageModel).Group
                Dim group As String = If(demoGroup IsNot Nothing, demoGroup.SeoTitle, Nothing)
                builder.Append(CurrentDemo.GetSeoTitle())
                builder.Append(" - ")
                builder.Append(If(String.IsNullOrEmpty(group), product, group))
                builder.Append(" Demo")
            End If
            builder.Append(" | DevExpress")
            Return builder.ToString()
        End Function
        Public Shared Function GetDemoNavigationTitle() As String
            Dim result As String = Utils.CurrentDemo.Product.NavItemTitle
            If Utils.CurrentDemoPage.Group IsNot Nothing Then
                result &= " - " & Utils.CurrentDemoPage.Group.Title
            End If
            Return result
        End Function
        Public Shared Sub RegisterCssLink(ByVal page As Page, ByVal url As String)
            RegisterCssLink(page, url, Nothing)
        End Sub
        Public Shared Sub RegisterCssLink(ByVal page As Page, ByVal url As String, ByVal clientId As String)
            If IsMvc Then
                Throw New NotImplementedException()
            End If
            Dim link As New HtmlLink()
            page.Header.Controls.Add(link)
            link.EnableViewState = False
            link.Attributes("type") = "text/css"
            link.Attributes("rel") = "stylesheet"
            If Not String.IsNullOrEmpty(clientId) Then
                link.Attributes("id") = clientId
            End If
            link.Href = url
        End Sub
        Public Shared Function GenerateDemoUrl(ByVal demo As DemoModel) As String
            If Not String.IsNullOrEmpty(demo.HighlightedLink) Then
                Return demo.HighlightedLink
            End If
            Dim str As New StringBuilder()
            If demo.Product.IsCurrent Then
                str.Append("~/")
            Else
                Dim url = HttpContext.Current.Request.Url.AbsolutePath
                Dim productUrl = "/" & CurrentDemo.Product.Url
                url = url.Substring(0, url.IndexOf(productUrl, StringComparison.InvariantCultureIgnoreCase) + 1)
                str.AppendFormat("{0}{1}{2}/", url, demo.Product.Url, AppUrlPostfix)
            End If
            Dim demoPage As DemoPageModel = TryCast(demo, DemoPageModel)
            Dim hasDemoVirtualGroupKey As Boolean = demoPage IsNot Nothing AndAlso Not String.IsNullOrEmpty(demoPage.VirtualGroupKey)
            Dim hasGroupKey As Boolean = demoPage IsNot Nothing AndAlso demoPage.Group IsNot Nothing AndAlso Not String.IsNullOrEmpty(demoPage.Group.Key)
            If hasDemoVirtualGroupKey OrElse hasGroupKey Then
                str.Append(GetDemoGroupKey(demoPage))
                str.Append("/")
            End If
            If (Not IsMvc) OrElse (Not String.Equals("Index", demo.Key)) Then
                str.Append(demo.Key)
            End If
            If Not IsMvc Then
                str.Append(".aspx")
            End If
            Return str.ToString()
        End Function
        Public Shared Function GenerateFeaturedDemos() As List(Of FeaturedDemoInfo)
            Dim result = New List(Of FeaturedDemoInfo)()
            For Each demo In DemosModel.Current.HighlightedDemos
                result.Add(New FeaturedDemoInfo With {.Title = demo.GetHighlightedTitle(), .ImageUrl = demo.HighlightedImageUrl, .NavigateUrl = GenerateDemoUrl(demo), .Description = demo.HighlightedDescription})
            Next demo
            If Utils.CurrentIntro IsNot Nothing Then
                For Each demo In Utils.CurrentIntro.ExternalDemos
                    result.Add(New FeaturedDemoInfo With {.Title = demo.Title, .ImageUrl = demo.ImageUrl, .NavigateUrl = demo.Url})
                Next demo
            End If
            Return result
        End Function
        Public Shared Function GenerateProductDemos(ByVal highlited As Boolean) As List(Of ProductInfo)
            Dim result = New List(Of ProductInfo)()
            For Each item In DemosModel.Instance.SortedDemoProducts.Where(Function(p) (Not p.HideNavItem) AndAlso (Not p.IsRootDemo) AndAlso p.IntegrationHighlighted = highlited)
                result.Add(New ProductInfo() With {.NavigateUrl = GenerateDemoUrl(item.Intro), .ImageUrl = item.IntegrationImageUrl, .Description = item.IntegrationDescription, .Title = item.NavItemTitle})
            Next item
            Return result
        End Function
        Public Shared Sub EnsureRequestValidationMode()
            Try
                If Environment.Version.Major >= 4 Then
                    Dim type As Type = DevExpress.Data.Internal.SafeTypeResolver.GetKnownType(GetType(WebControl).Assembly, "System.Web.Configuration.RuntimeConfig")
                    Dim getConfig As MethodInfo = type.GetMethod("GetConfig", BindingFlags.Static Or BindingFlags.NonPublic, Nothing, New Type() { }, Nothing)
                    Dim runtimeConfig As Object = getConfig.Invoke(Nothing, Nothing)
                    Dim getSection As MethodInfo = type.GetMethod("GetSection", BindingFlags.Instance Or BindingFlags.NonPublic, Nothing, New Type() { GetType(String), GetType(Type) }, Nothing)
                    Dim httpRuntimeSection As HttpRuntimeSection = DirectCast(getSection.Invoke(runtimeConfig, New Object() { "system.web/httpRuntime", GetType(HttpRuntimeSection) }), HttpRuntimeSection)
                    Dim bReadOnly As FieldInfo = GetType(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance Or BindingFlags.NonPublic)
                    bReadOnly.SetValue(httpRuntimeSection, False)
                    Dim pi As PropertyInfo = GetType(HttpRuntimeSection).GetProperty("RequestValidationMode")
                    If pi IsNot Nothing Then
                        Dim version As Version = DirectCast(pi.GetValue(httpRuntimeSection, Nothing), Version)
                        Dim RequiredRequestValidationMode As New Version(2, 0)
                        If version IsNot Nothing AndAlso (Not System.Version.Equals(version, RequiredRequestValidationMode)) Then
                            pi.SetValue(httpRuntimeSection, RequiredRequestValidationMode, Nothing)
                        End If
                    End If
                    bReadOnly.SetValue(httpRuntimeSection, True)
                End If
            Catch
            End Try
        End Sub
        Private Shared _isSiteMode? As Boolean
        Public Shared ReadOnly Property IsSiteMode() As Boolean
            Get
                If Not _isSiteMode.HasValue Then
                    _isSiteMode = ConfigurationManager.AppSettings("SiteMode").Equals("true", StringComparison.InvariantCultureIgnoreCase)
                End If
                Return _isSiteMode.Value
            End Get
        End Property
        Private Shared _appUrlPostfix As String
        Public Shared ReadOnly Property AppUrlPostfix() As String
            Get
                If _appUrlPostfix Is Nothing Then
                    _appUrlPostfix = If(ConfigurationManager.AppSettings("AppUrlPostfix"), "")
                End If
                Return _appUrlPostfix
            End Get
        End Property
        Private Shared _patchConnectionStrings? As Boolean
        Public Shared ReadOnly Property PatchConnectionStrings() As Boolean
            Get
                If Not _patchConnectionStrings.HasValue Then
                    Dim appSettingsValue As String = ConfigurationManager.AppSettings("PatchConnectionStrings")
                    _patchConnectionStrings = appSettingsValue IsNot Nothing AndAlso appSettingsValue.Equals("true", StringComparison.InvariantCultureIgnoreCase)
                End If
                Return _patchConnectionStrings.Value
            End Get
        End Property
        Public Shared Function GetReadOnlyMessageHtml() As String
            Return String.Format("<b>Data modifications are not allowed in the online demo.</b><br />" & "If you need to test data editing functionality, please install " & "{0} on your machine and run the demo locally.", DemosModel.Current.Title)
        End Function
        Public Shared Function GetReadOnlyMessageText() As String
            Return String.Format("Data modifications are not allowed in the online demo." & ControlChars.Lf & "If you need to test data editing functionality, please install " & ControlChars.Lf & "{0} on your machine and run the demo locally.", DemosModel.Current.Title)
        End Function
        Public Shared Sub AssertNotReadOnly()
            If Not IsSiteMode Then
                Return
            End If
            Throw New DemoException(GetReadOnlyMessageHtml())
        End Sub
        Public Shared Sub AssertNotReadOnlyText()
            If Not IsSiteMode Then
                Return
            End If
            Throw New DemoException(GetReadOnlyMessageText())
        End Sub
        Public Shared ReadOnly Property CanChangeTheme() As Boolean
            Get
                Return (Not IsIntro) AndAlso (Not IsIE6()) AndAlso DemosModel.Current.SupportsTheming AndAlso Not DemoHelper.Instance.SuppressThemeSelector
            End Get
        End Property
        Public Shared ReadOnly Property CanChangeBaseColor() As Boolean
            Get
                Return CurrentThemesModel.GetAllGroups().Where(Function(g) g.Themes.Where(Function(t) t.Name = CurrentTheme AndAlso (Not String.IsNullOrEmpty(t.BaseColor))).Count() <> 0).Count() > 0
            End Get
        End Property
        Public Shared ReadOnly Property CanApplyThemeParameters() As Boolean
            Get
                Return DemosModel.Current.SupportsThemeParameters AndAlso ((Not String.IsNullOrEmpty(CurrentThemeDefaultFont)) OrElse (Not String.IsNullOrEmpty(CurrentThemeDefaultBaseColor)))
            End Get
        End Property
        Public Shared ReadOnly Property CurrentThemeDefaultFont() As String
            Get
                Return CurrentThemeModel.Font
            End Get
        End Property
        Public Shared ReadOnly Property CurrentThemeDefaultFontSize() As String
            Get
                Return CurrentThemeModel.FontSize
            End Get
        End Property
        Public Shared ReadOnly Property CurrentThemeDefaultBaseColor() As String
            Get
                Return CurrentThemeModel.BaseColor
            End Get
        End Property
        Private Shared ReadOnly Property CurrentThemeModel() As ThemeModel
            Get
                Return CurrentThemesModel.GetAllGroups().SelectMany(Function(g) g.Themes).FirstOrDefault(Function(t) t.Name = CurrentTheme)
            End Get
        End Property
        Public Shared ReadOnly Property CustomFonts() As String()
            Get
                Return New String() { CurrentThemeDefaultFont, CurrentThemeDefaultFontSize & " " & "'Asap', normal", CurrentThemeDefaultFontSize & " " & "'Arima Madurai', normal", CurrentThemeDefaultFontSize & " " & "'Comfortaa', normal" }
            End Get
        End Property
        Public Shared Function GetFontFamiliesDataSource() As List(Of ListEditItem)
            Return CustomFonts.Select(Function(f) New ListEditItem() With {.Text = GetShortFontName(f), .Value = f}).ToList()
        End Function
        Private Shared Function GetShortFontName(ByVal fullName As String) As String
            If String.IsNullOrWhiteSpace(fullName) Then
                Return fullName
            End If
            Dim indexOfFirstSpace As Integer = fullName.IndexOf(" "c)
            Dim indexOfFirstComma As Integer = fullName.IndexOf(","c)
            Dim endPosition As Integer = If(indexOfFirstComma > -1, indexOfFirstComma - 1, fullName.Length - 1)
            Return fullName.Substring(indexOfFirstSpace + 1, endPosition - indexOfFirstSpace).Trim("'"c)
        End Function
        Public Shared ReadOnly Property CustomBaseColors() As String()
            Get
                Return New String() { GetFirstItemForBaseColorList(), "#4796CE", "#35B86B", "#CE5B47", CorpColor, "#9F47CE", "#5C57C9", "#CE4776"}
            End Get
        End Property
        Private Shared Function GetFirstItemForBaseColorList() As String
            If CurrentTheme.Equals("MaterialCompact") Then
                Return (CurrentThemesModel.GetThemeModel("MaterialCompact")).BaseColor
            End If
            Return CurrentThemeDefaultBaseColor
        End Function
        Public Shared Function GetDefaultBaseColor(ByVal themeName As String) As String
            Return (CurrentThemesModel.GetThemeModel(themeName)).BaseColor
        End Function
        Public Shared Function EncodeSvgIconBase64(ByVal svg As String) As String
            Dim svgEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg))
'#pragma warning disable DX0025 ' not a path (and no traversal here)
            Return "data:image/svg+xml;base64," & svgEncoded
'#pragma warning restore DX0025
        End Function
        Public Shared Function GetColoredSquareUrl(ByVal color As String) As String
            Dim svg = "<svg xmlns='http://www.w3.org/2000/svg'><g><rect height='100%' width='100%' y='0' x='0' fill='" & color & "'/></g></svg>"
            Return EncodeSvgIconBase64(svg)
        End Function
        Public Shared Function GetCurrentResource(ByVal resourceName As String) As String
            Using stream As Stream = ResourcesUtils.GetEmbeddedResource(resourceName)
                stream.Position = 0
                Using reader As New StreamReader(stream, Encoding.UTF8)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Function
        Public Shared Sub ResolveThemeParametes()
            If (Not CanChangeTheme) OrElse (Not DemosModel.Current.SupportsThemeParameters) Then
                Return
            End If
            Dim baseColor As String = CurrentBaseColor
            Dim font As String = CurrentFont
            If IsThemeChanged OrElse (Not String.IsNullOrEmpty(baseColor)) AndAlso (Not CustomBaseColors.Contains(baseColor)) OrElse baseColor = CurrentThemeDefaultBaseColor Then
                baseColor = String.Empty
                SetCurrentBaseColorCookie(baseColor)
            End If
            If IsThemeChanged OrElse (Not String.IsNullOrEmpty(font)) AndAlso (Not CustomFonts.Contains(font)) OrElse font = CurrentThemeDefaultFont Then
                font = String.Empty
                SetCurrentFontCookie(font)
            End If
            TunableTheme = CurrentTheme
            ASPxWebControl.GlobalThemeBaseColor = baseColor
            ASPxWebControl.GlobalThemeFont = font
        End Sub
        Public Shared Sub ResetThemeParameters()
            ASPxWebControl.GlobalThemeBaseColor = Nothing
            ASPxWebControl.GlobalThemeFont = Nothing
        End Sub
        Private Shared ReadOnly Property IsThemeChanged() As Boolean
            Get
                Return CurrentTheme <> TunableTheme
            End Get
        End Property
        Public Shared Function GetAccessibilityAttribute(ByVal attributeName As String, ByVal value As String) As String
            If IsAccessibilityDemo Then
                Return String.Format("{0}=""{1}""", attributeName, value)
            End If
            Return String.Empty
        End Function
        Public Shared ReadOnly Property IsAccessibilityDemo() As Boolean
            Get
                If Request Is Nothing Then
                    Return False
                End If
                Dim demoUrl As String = Request.Url.AbsolutePath.ToLower()
                Return demoUrl.IndexOf("compliance") <> -1 OrElse (demoUrl.IndexOf("accessibility") <> -1 AndAlso demoUrl.IndexOf("linkedcontrols") <> -1)
            End Get
        End Property
        Public Shared Sub InjectDescriptionMeta(ByVal parent As Control)
            If String.IsNullOrEmpty(Utils.CurrentDemo.MetaDescription) Then
                Return
            End If
            Dim page As Page = TryCast(parent, Page)
            Dim header As HtmlHead = If(page IsNot Nothing AndAlso page.Header IsNot Nothing, page.Header, RenderUtils.FindHead(parent))
            If header IsNot Nothing Then
                Dim metaControl As New LiteralControl(String.Format("<meta name=""description"" content=""{0}"" />", Utils.CurrentDemo.MetaDescription))
                header.Controls.AddAt(0, metaControl)
            End If
        End Sub
        Public Shared Function IsIE6() As Boolean
            Return RenderUtils.Browser.IsIE AndAlso RenderUtils.Browser.Version < 7
        End Function
        Public Shared Function IsIE9() As Boolean
            Return RenderUtils.Browser.IsIE AndAlso RenderUtils.Browser.Version > 8
        End Function
        Public Shared Function IsIE10() As Boolean
            Return RenderUtils.Browser.IsIE AndAlso RenderUtils.Browser.Version > 9
        End Function
        Public Shared Function EncodeUrl(ByVal url As String) As String
            Return Uri.EscapeUriString(url.Trim())
        End Function
        Public Shared Function FormatSize(ByVal value As Object) As String
            Dim amount As Double = Convert.ToDouble(value)
            Dim unit As String = "KB"
            If amount <> 0 Then
                If amount <= 1024 Then
                    amount = 1
                Else
                    amount /= 1024
                End If
                If amount > 1024 Then
                    amount /= 1024
                    unit = "MB"
                End If
                If amount > 1024 Then
                    amount /= 1024
                    unit = "GB"
                End If
            End If
            Return String.Format("{0:#,0} {1}", Math.Round(amount, MidpointRounding.AwayFromZero), unit)
        End Function
        Public Shared Sub PrepareSecurityProtocol()
            System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol Or System.Net.SecurityProtocolType.Tls12
        End Sub
        Private Shared Function IsErrorPage(ByVal demoKey As String) As Boolean
            Return demoKey.Equals("Error404", StringComparison.OrdinalIgnoreCase) OrElse demoKey.Equals("Error500", StringComparison.OrdinalIgnoreCase)
        End Function
        Public Shared Function CanChangeTreeControlTreeLines() As Boolean
            Return Not TreeControlNoTreeLinesThemes.Contains(ASPxWebControl.GlobalTheme)
        End Function
        Public Shared Function CanChangeTreeListGridLines() As Boolean
            Return Not TreeListNoGridLinesThemes.Contains(ASPxWebControl.GlobalTheme)
        End Function
        Public Shared Function GetInfoMessageForTreeControlNoTreeLinesThemes() As String
            Return GetInfoMessageForTreeControl(True)
        End Function
        Public Shared Function GetInfoMessageForTreeListNoGridLinesThemes() As String
            Return GetInfoMessageForTreeControl(False)
        End Function
        Private Shared Function GetInfoMessageForTreeControl(ByVal forTreeLines As Boolean) As String
            Dim excludedThemes = If(forTreeLines, TreeControlNoTreeLinesThemes, TreeListNoGridLinesThemes)
            Dim infoFeatures = If(forTreeLines, "tree lines", "grid lines")
            Dim infoFeature = If(forTreeLines, "tree line", "grid line")
            Dim themesInfos = ThemesModel.Current.GetAllThemes()
'#pragma warning disable DX0025 ' not a path (and no traversal here)
            Dim result = String.Join(", ", themesInfos.Where(Function(x) excludedThemes.Contains(x.Name)).Select(Function(x) String.Format("<b>{0}</b>", x.Title)))
'#pragma warning restore DX0025
            Return HttpUtility.HtmlEncode(String.Format("By design, the {0} themes do not display {1}. Switch to a different theme to view and manipulate {2} setting in this demo.", result, infoFeatures, infoFeature))
        End Function
    End Class
    Public Class ErrorMessageModel
        Public Property Title() As String
        Public Property NavigateUrl() As String
    End Class
    Public Class DemoException
        Inherits Exception

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
    End Class
    Public Module ExtensionMethods
        <System.Runtime.CompilerServices.Extension> _
        Public Function GetUrlWithVersionSuffix(ByVal control As Control, ByVal url As String) As String
            Return String.Format("{0}?v={1}", control.ResolveClientUrl(url), Utils.GetVersionSuffix())
        End Function
    End Module
End Namespace
