'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Web
Imports DevExpress.Web
Namespace DevExpress.Web.Demos
    Public NotInheritable Class RichEditDemoUtils

        Private Sub New()
        End Sub

        Public Shared Sub HideFileTab(ByVal richedit As ASPxRichEdit.ASPxRichEdit)
            richedit.CreateDefaultRibbonTabs(True)
            Dim fileTab As RibbonTab = richedit.RibbonTabs(0)
            richedit.RibbonTabs.Remove(fileTab)
        End Sub
    End Class
End Namespace
