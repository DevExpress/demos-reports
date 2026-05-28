using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DevExpress.Web.Demos;
using DevExpress.XtraPrinting;

public class CustomPdfSignatureOptionsProviderAsync : DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync {
    readonly Dictionary<string, PdfSignatureOptions> signatures = new Dictionary<string, PdfSignatureOptions>();
    public CustomPdfSignatureOptionsProviderAsync() {
        var password = System.Configuration.ConfigurationManager.AppSettings["SignatureDemoPassword"];
        var signatureDictionaryPath = Path.Combine(MvcApplication.AppData_Path, "Signatures");
        signatures.Add(Guid.NewGuid().ToString(), new PdfSignatureOptions() {
            Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(Path.Combine(signatureDictionaryPath, "certificate.pfx"), password),
            ContactInfo = "John Smith",
            Location = "Australia",
            Reason = "I Agree",
            ImageSource = DevExpress.XtraPrinting.Drawing.ImageSource.FromFile(Path.Combine(signatureDictionaryPath, "John_Smith.png"))
        });
        signatures.Add(Guid.NewGuid().ToString(), new PdfSignatureOptions() {
            Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(Path.Combine(signatureDictionaryPath, "certificate.pfx"), password),
            ContactInfo = "Jane Cooper",
        });
    }

    public Task<Dictionary<string, PdfSignatureOptions>> GetAvailableOptionsAsync() {
        return Task.FromResult(signatures);
    }
}
