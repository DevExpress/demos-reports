'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.Internal
Namespace DevExpress.Web.Demos
    Public Class BootstrapDemoItemBase
        Inherits Panel

        Public Sub New()
            IECompatible = True
        End Sub
        Public Property IECompatible() As Boolean
        Protected ReadOnly Property IsContentIncompatibleWithBrowser() As Boolean
            Get
                Return (Not IECompatible) AndAlso RenderUtils.Browser.IsIE
            End Get
        End Property
        Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
            If IsContentIncompatibleWithBrowser Then
                Controls.Clear()
            Else
                MyBase.OnPreRender(e)
            End If
        End Sub
        Protected Overrides Sub RenderContents(ByVal writer As HtmlTextWriter)
            If IsContentIncompatibleWithBrowser Then
                RenderBrowserIncompatibilityInfo(writer)
            Else
                MyBase.RenderContents(writer)
            End If
        End Sub
        Protected Overridable Sub RenderBrowserIncompatibilityInfo(ByVal writer As HtmlTextWriter)
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "incompatibility-info table-bordered")
            writer.RenderBeginTag(HtmlTextWriterTag.Div)
                If BootstrapUtils.BootstrapVersion > 3 Then
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "d-table-cell")
                End If
                writer.RenderBeginTag(HtmlTextWriterTag.Div)
                    writer.WriteLine(FileUtils.GetResourceFileText(GetType(BootstrapDemoItemBase).Assembly, "Data.Icons.info.svg"))
                writer.RenderEndTag()
                If BootstrapUtils.BootstrapVersion > 3 Then
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "d-table-cell w-100")
                End If
                writer.RenderBeginTag(HtmlTextWriterTag.Div)
                    writer.RenderBeginTag(HtmlTextWriterTag.Div)
                        writer.RenderBeginTag(HtmlTextWriterTag.H4)
                            writer.WriteEncodedText("Unsupported Web Browser")
                        writer.RenderEndTag()
                        writer.RenderBeginTag(HtmlTextWriterTag.P)
                            writer.WriteEncodedText("To explore the capabilities of the DevExpress product line, please reload this webpage with a different web browser.")
                        writer.RenderEndTag()
                    writer.RenderEndTag()
                writer.RenderEndTag()
            writer.RenderEndTag()
        End Sub
        #Region "Unnecessary properties"
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ClientIDMode() As ClientIDMode
            Get
                Return MyBase.ClientIDMode
            End Get
            Set(ByVal value As ClientIDMode)
                MyBase.ClientIDMode = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property EnableViewState() As Boolean
            Get
                Return MyBase.EnableViewState
            End Get
            Set(ByVal value As Boolean)
                MyBase.EnableViewState = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ViewStateMode() As ViewStateMode
            Get
                Return MyBase.ViewStateMode
            End Get
            Set(ByVal value As ViewStateMode)
                MyBase.ViewStateMode = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Visible() As Boolean
            Get
                Return MyBase.Visible
            End Get
            Set(ByVal value As Boolean)
                MyBase.Visible = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property AccessKey() As String
            Get
                Return MyBase.AccessKey
            End Get
            Set(ByVal value As String)
                MyBase.AccessKey = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackColor() As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As Color)
                MyBase.BackColor = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BorderColor() As Color
            Get
                Return MyBase.BorderColor
            End Get
            Set(ByVal value As Color)
                MyBase.BorderColor = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BorderWidth() As Unit
            Get
                Return MyBase.BorderWidth
            End Get
            Set(ByVal value As Unit)
                MyBase.BorderWidth = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BorderStyle() As BorderStyle
            Get
                Return MyBase.BorderStyle
            End Get
            Set(ByVal value As BorderStyle)
                MyBase.BorderStyle = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property CssClass() As String
            Get
                Return MyBase.CssClass
            End Get
            Set(ByVal value As String)
                MyBase.CssClass = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Enabled() As Boolean
            Get
                Return MyBase.Enabled
            End Get
            Set(ByVal value As Boolean)
                MyBase.Enabled = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property EnableTheming() As Boolean
            Get
                Return MyBase.EnableTheming
            End Get
            Set(ByVal value As Boolean)
                MyBase.EnableTheming = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides ReadOnly Property Font() As FontInfo
            Get
                Return MyBase.Font
            End Get
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As Color)
                MyBase.ForeColor = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Height() As Unit
            Get
                Return MyBase.Height
            End Get
            Set(ByVal value As Unit)
                MyBase.Height = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property SkinID() As String
            Get
                Return MyBase.SkinID
            End Get
            Set(ByVal value As String)
                MyBase.SkinID = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property TabIndex() As Short
            Get
                Return MyBase.TabIndex
            End Get
            Set(ByVal value As Short)
                MyBase.TabIndex = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ToolTip() As String
            Get
                Return MyBase.ToolTip
            End Get
            Set(ByVal value As String)
                MyBase.ToolTip = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Width() As Unit
            Get
                Return MyBase.Width
            End Get
            Set(ByVal value As Unit)
                MyBase.Width = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property BackImageUrl() As String
            Get
                Return MyBase.BackImageUrl
            End Get
            Set(ByVal value As String)
                MyBase.BackImageUrl = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property DefaultButton() As String
            Get
                Return MyBase.DefaultButton
            End Get
            Set(ByVal value As String)
                MyBase.BackImageUrl = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Direction() As ContentDirection
            Get
                Return MyBase.Direction
            End Get
            Set(ByVal value As ContentDirection)
                MyBase.Direction = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property GroupingText() As String
            Get
                Return MyBase.GroupingText
            End Get
            Set(ByVal value As String)
                MyBase.GroupingText = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property HorizontalAlign() As HorizontalAlign
            Get
                Return MyBase.HorizontalAlign
            End Get
            Set(ByVal value As HorizontalAlign)
                MyBase.HorizontalAlign = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property ScrollBars() As ScrollBars
            Get
                Return MyBase.ScrollBars
            End Get
            Set(ByVal value As ScrollBars)
                MyBase.ScrollBars = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides Property Wrap() As Boolean
            Get
                Return MyBase.Wrap
            End Get
            Set(ByVal value As Boolean)
                MyBase.Wrap = value
            End Set
        End Property
        #End Region
    End Class
    Public Class BootstrapDemoOverviewItem
        Inherits BootstrapDemoItemBase

        Public ReadOnly Property OverViewPage() As BootstrapDemoGroupPageModel
            Get
                Return TryCast(BootstrapUtils.CurrentDemo, BootstrapDemoGroupPageModel)
            End Get
        End Property
        Public ReadOnly Property Title() As String
            Get
                Return If(OverViewPage IsNot Nothing, OverViewPage.Title, "Demo Title")
            End Get
        End Property
        Public ReadOnly Property PreDescription() As String
            Get
                Return If(OverViewPage IsNot Nothing, OverViewPage.GetProcessedPreDescription(), "PreDescription")
            End Get
        End Property
        Public ReadOnly Property Description() As String
            Get
                Return If(OverViewPage IsNot Nothing, OverViewPage.GetProcessedDescription(), "Description")
            End Get
        End Property
        Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
            MyBase.OnPreRender(e)
            If (Not DesignMode) AndAlso OverViewPage Is Nothing Then
                Throw New Exception("The BootstrapDemoOverviewItem with ID='" & ID & "' doesn't have a corresponding DemoGroup in the Demos.xml file.")
            End If
        End Sub
        Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
            writer.RenderBeginTag(HtmlTextWriterTag.H2)
            writer.WriteEncodedText(String.Format("{0} - Overview", Title))
            writer.RenderEndTag()
            writer.Write(PreDescription)
            RenderContents(writer)
            writer.Write(Description)
        End Sub
    End Class
    <ControlBuilder(GetType(BootstrapDemoItemControlBuilder))>
    Public Class BootstrapDemoItem
        Inherits BootstrapDemoItemBase

        Public Sub New()
            Orientation = Orientation.Vertical
        End Sub
        Public Property Orientation() As Orientation
        Public ReadOnly Property Section() As BootstrapDemoSectionModel
            Get
                Dim demoPageModel As BootstrapDemoPageModel = TryCast(BootstrapUtils.CurrentDemo, BootstrapDemoPageModel)
                If demoPageModel IsNot Nothing AndAlso (Not String.IsNullOrEmpty(ID)) Then
                    Return demoPageModel.FindSection(ID)
                End If
                Return Nothing
            End Get
        End Property
        Public ReadOnly Property Title() As String
            Get
                Return If(Section IsNot Nothing, Section.Title, "Demo Section Title")
            End Get
        End Property
        Public ReadOnly Property HeaderID() As String
            Get
                Return If(Section IsNot Nothing, Section.Key, "")
            End Get
        End Property
        Public ReadOnly Property Description() As String
            Get
                Return If(Section IsNot Nothing, Section.GetProcessedDescription(), "Description")
            End Get
        End Property
        Public ReadOnly Property CodeASPX() As String
            Get
                Return If(Section IsNot Nothing, Section.CodeASPX, "")
            End Get
        End Property
        Public ReadOnly Property CodeCS() As String
            Get
                Return If(Section IsNot Nothing, Section.CodeCS, "")
            End Get
        End Property
        Public ReadOnly Property CodeVB() As String
            Get
                Return If(Section IsNot Nothing, Section.CodeVB, "")
            End Get
        End Property
        Public ReadOnly Property CodeJS() As String
            Get
                Return If(Section IsNot Nothing, Section.CodeJS, "")
            End Get
        End Property
        Public Function LoadCustomFiles() As List(Of BootstrapDemoSectionSourceFileModel)
            Dim sourceFiles = Section.GetSourceFiles()
            For Each file In sourceFiles
                If Not BootstrapUtils.IsSourceFileExist(file.Path) Then
                    Dim vbFilePath = Path.ChangeExtension(file.Path, ".vb")
                    If BootstrapUtils.IsSourceFileExist(vbFilePath) Then
                        file.Path = vbFilePath
                    End If
                End If
                If BootstrapUtils.IsSourceFileExist(file.Path) AndAlso String.IsNullOrEmpty(file.Content) Then
                    file.Content = BootstrapUtils.GetSourceFileCode(file.Path)
                End If
            Next file
            Return sourceFiles
        End Function
        Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
            MyBase.OnPreRender(e)
            If (Not DesignMode) AndAlso Section Is Nothing Then
                Throw New Exception("The BootstrapDemoItem with ID='" & ID & "' doesn't have a corresponding DemoSection in the Demos.xml file.")
            End If
        End Sub
        Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
            If Title IsNot Nothing Then
                RenderHeader(writer)
            End If
            If Orientation = Orientation.Vertical Then
                If Description IsNot Nothing Then
                    RenderDescription(writer)
                End If
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "example")
                writer.RenderBeginTag(HtmlTextWriterTag.Div)
                RenderContents(writer)
                RenderCode(writer)
                writer.RenderEndTag()
            Else
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "row")
                writer.RenderBeginTag(HtmlTextWriterTag.Div)
                    If Description IsNot Nothing Then
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-8")
                        writer.RenderBeginTag(HtmlTextWriterTag.Div)
                            RenderDescription(writer)
                        writer.RenderEndTag()
                    End If
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-4")
                    writer.RenderBeginTag(HtmlTextWriterTag.Div)
                        RenderContents(writer)
                    writer.RenderEndTag()
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "example col-lg-12")
                    writer.RenderBeginTag(HtmlTextWriterTag.Div)
                        RenderCode(writer)
                    writer.RenderEndTag()
                writer.RenderEndTag()
            End If
        End Sub
        Private Sub RenderCode(ByVal writer As HtmlTextWriter)
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "code")
            writer.RenderBeginTag(HtmlTextWriterTag.Div)
            Dim customFiles = LoadCustomFiles()
            If BootstrapUtils.BootstrapVersion = 4 Then
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "lang-group nav nav-code")
                writer.RenderBeginTag(HtmlTextWriterTag.Div)
                RenderLanguageButton(writer, "aspx", "ASPX", True)
            End If
            If customFiles.Any() OrElse (Not String.IsNullOrEmpty(CodeCS)) OrElse (Not String.IsNullOrEmpty(CodeVB)) OrElse (Not String.IsNullOrEmpty(CodeJS)) Then
                If BootstrapUtils.BootstrapVersion = 3 Then
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "lang-group btn-group")
                    writer.RenderBeginTag(HtmlTextWriterTag.Div)
                    RenderLanguageButton(writer, "aspx", "ASPX", True)
                End If
                If Not String.IsNullOrEmpty(CodeCS) Then
                    RenderLanguageButton(writer, "cs", "C#", False)
                End If
                If Not String.IsNullOrEmpty(CodeVB) Then
                    RenderLanguageButton(writer, "vb", "VB", False)
                End If
                If Not String.IsNullOrEmpty(CodeJS) Then
                    RenderLanguageButton(writer, "js", "JS", False)
                End If
                For Each file In customFiles
                    Dim fileName = Path.GetFileName(file.Path)

                    Dim title_Renamed = If(String.IsNullOrEmpty(file.CustomTitle), fileName, file.CustomTitle)
                    RenderLanguageButton(writer, fileName, title_Renamed, False)
                Next file
                If BootstrapUtils.BootstrapVersion = 3 Then
                    writer.RenderEndTag()
                End If
            End If
            If BootstrapUtils.BootstrapVersion = 4 Then
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "nav-link nav-copy")
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:;")
                writer.RenderBeginTag(HtmlTextWriterTag.A)
            Else
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "button")
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default btn-copy")
                writer.RenderBeginTag(HtmlTextWriterTag.Button)
            End If
            writer.WriteEncodedText("Copy")
            writer.RenderEndTag()
            If BootstrapUtils.BootstrapVersion = 4 Then
                writer.RenderEndTag()
            End If
            If Not String.IsNullOrEmpty(CodeASPX) Then
                RenderLanguageContent(writer, "aspx", "xml", CodeASPX, True)
            End If
            If Not String.IsNullOrEmpty(CodeCS) Then
                RenderLanguageContent(writer, "cs", "cs", CodeCS, False)
            End If
            If Not String.IsNullOrEmpty(CodeVB) Then
                RenderLanguageContent(writer, "vb", "vb", CodeVB, False)
            End If
            If Not String.IsNullOrEmpty(CodeJS) Then
                RenderLanguageContent(writer, "js", "js", CodeJS, False)
            End If
            For Each file In customFiles
                Dim fileName = Path.GetFileName(file.Path)
                RenderLanguageContent(writer, fileName, GetDataCodeByFileName(fileName), file.Content, False)
            Next file
            writer.RenderEndTag()
        End Sub
        Private Sub RenderDescription(ByVal writer As HtmlTextWriter)
            writer.Write(Description)
        End Sub
        Private Sub RenderHeader(ByVal writer As HtmlTextWriter)
            writer.AddAttribute(HtmlTextWriterAttribute.Id, HeaderID)
            writer.RenderBeginTag(HtmlTextWriterTag.H2)
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "alink icon icon-link")
            writer.AddAttribute(HtmlTextWriterAttribute.Href, ResolveUrl(BootstrapUtils.GenerateDemoSectionUrl(Section)))
            writer.AddAttribute(HtmlTextWriterAttribute.Target, "_top")
            writer.RenderBeginTag(HtmlTextWriterTag.A)
            writer.RenderEndTag()
            writer.WriteEncodedText(Title)
            writer.RenderEndTag()
        End Sub
        Protected Function GetDataCodeByFileName(ByVal fileName As String) As String
            Dim ext As String = Path.GetExtension(fileName.ToLower())
            Select Case ext
                Case ".xml", ".aspx", ".ascx"
                    Return "xml"
                Case ".js"
                    Return "js"
                Case ".cs"
                    Return "cs"
                Case ".vb"
                    Return "vb"
                Case ".asax"
                    Return "cs"
                Case Else
                    Throw New Exception("Unknown extension")
            End Select
        End Function
        Protected Sub RenderLanguageButton(ByVal writer As HtmlTextWriter, ByVal dataCode As String, ByVal text As String, ByVal isActive As Boolean)
            If BootstrapUtils.BootstrapVersion = 4 Then
                writer.AddAttribute("data-code", dataCode)
                writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:;")
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "nav-link" & (If(isActive, " active", "")))
                writer.RenderBeginTag(HtmlTextWriterTag.A)
                writer.WriteEncodedText(text)
                writer.RenderEndTag()
            Else
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "button")
                writer.AddAttribute("data-code", dataCode)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default" & (If(isActive, " active", "")))
                writer.RenderBeginTag(HtmlTextWriterTag.Button)
                writer.WriteEncodedText(text)
                writer.RenderEndTag()
            End If
        End Sub
        Protected Sub RenderLanguageContent(ByVal writer As HtmlTextWriter, ByVal dataCode As String, ByVal codeClass As String, ByVal code As String, ByVal isActive As Boolean)
            Dim className As String = codeClass
            If Not isActive Then
                If BootstrapUtils.BootstrapVersion = 4 Then
                    className &= " d-none"
                Else
                    className &= " hidden"
                End If
            End If
            writer.Write($"<pre><code class=""{className}"" data-code=""{dataCode}"">{code}</code></pre>")
        End Sub
    End Class
    Public Class BootstrapDemoItemControlBuilder
        Inherits ControlBuilder

        Private privateDemoGroupKey As String
        Public Property DemoGroupKey() As String
            Get
                Return privateDemoGroupKey
            End Get
            Private Set(ByVal value As String)
                privateDemoGroupKey = value
            End Set
        End Property
        Private privateDemoKey As String
        Public Property DemoKey() As String
            Get
                Return privateDemoKey
            End Get
            Private Set(ByVal value As String)
                privateDemoKey = value
            End Set
        End Property
        Private privateDemoSectionKey As String
        Public Property DemoSectionKey() As String
            Get
                Return privateDemoSectionKey
            End Get
            Private Set(ByVal value As String)
                privateDemoSectionKey = value
            End Set
        End Property
        Public Overrides Sub Init(ByVal parser As TemplateParser, ByVal parentBuilder As ControlBuilder, ByVal type As Type, ByVal tagName As String, ByVal id As String, ByVal attribs As IDictionary)
            MyBase.Init(parser, parentBuilder, type, tagName, id, attribs)
            If Not String.IsNullOrEmpty(PageVirtualPath) Then
                Dim groupKey As String = ""

                Dim demoKey_Renamed As String = ""
                Utils.FindDemoKeysByVirtualPath(PageVirtualPath, groupKey, demoKey_Renamed)
                DemoGroupKey = groupKey
                DemoKey = demoKey_Renamed
                DemoSectionKey = DirectCast(attribs("ID"), String)
            End If
        End Sub
        Public Sub RegisterCode(ByVal code As String)
            Dim group = BootstrapDemosModel.Instance.FindGroup(DemoGroupKey)
            If group IsNot Nothing Then
                Dim demo = group.FindDemo(DemoKey)
                If demo IsNot Nothing Then
                    Dim section = demo.FindSection(DemoSectionKey)
                    If section IsNot Nothing Then
                        section.Code = code
                    End If
                End If
            End If
        End Sub
        Public Overrides Sub SetTagInnerText(ByVal text As String)
            MyBase.SetTagInnerText(text)
            RegisterCode(text)
        End Sub
        Public Overrides Function NeedsTagInnerText() As Boolean
            Return True
        End Function
    End Class
    Public Class BootstrapDemoContainerItem
        Inherits BootstrapDemoItemBase

        Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
            writer.RenderBeginTag(HtmlTextWriterTag.Div)
            writer.Write(BootstrapUtils.CurrentDemo.GetProcessedDescription())
            writer.RenderEndTag()
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "demo-container-item")
            writer.RenderBeginTag(HtmlTextWriterTag.Div)
            RenderContents(writer)
            writer.RenderEndTag()
        End Sub
    End Class
End Namespace
