Imports System
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports DevExpress.XtraPrinting

Namespace DevExpress.Web.Demos
    Public Class CustomPdfSignatureOptionsProviderAsync
        Implements DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync

        Private ReadOnly signatures As New Dictionary(Of String, PdfSignatureOptions)()
        Private ReadOnly Property DemoPassword() As String ' 123
            Get
                Return System.Configuration.ConfigurationManager.AppSettings("DemoPassword")
            End Get
        End Property
        Public Sub New()
            Dim signatureDictionaryPath = System.IO.Path.Combine(Helper.AppData_Path, "Signatures")
            signatures.Add(Guid.NewGuid().ToString(), New PdfSignatureOptions() With {.Certificate = New System.Security.Cryptography.X509Certificates.X509Certificate2(System.IO.Path.Combine(signatureDictionaryPath, "certificate.pfx"), DemoPassword), .ContactInfo = "John Smith", .Location = "Australia", .Reason = "I Agree", .ImageSource = DevExpress.XtraPrinting.Drawing.ImageSource.FromFile(System.IO.Path.Combine(signatureDictionaryPath, "John_Smith.png"))})
            signatures.Add(Guid.NewGuid().ToString(), New PdfSignatureOptions() With {.Certificate = New System.Security.Cryptography.X509Certificates.X509Certificate2(System.IO.Path.Combine(signatureDictionaryPath, "certificate.pfx"), DemoPassword), .ContactInfo = "Jane Cooper"})
        End Sub

        Private Function IPdfSignatureOptionsProviderAsync_GetAvailableOptionsAsync() As Task(Of Dictionary(Of String, PdfSignatureOptions)) Implements DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync.GetAvailableOptionsAsync
            Return Task.FromResult(signatures)
        End Function
    End Class
End Namespace
