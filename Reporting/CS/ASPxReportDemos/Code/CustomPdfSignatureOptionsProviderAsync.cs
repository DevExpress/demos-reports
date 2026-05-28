using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExpress.XtraPrinting;

namespace DevExpress.Web.Demos {
    public class CustomPdfSignatureOptionsProviderAsync : DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync {
        readonly Dictionary<string, PdfSignatureOptions> signatures = new Dictionary<string, PdfSignatureOptions>();
        string DemoPassword { get { return System.Configuration.ConfigurationManager.AppSettings["DemoPassword"]; } } // 123
        public CustomPdfSignatureOptionsProviderAsync() {
            var signatureDictionaryPath = System.IO.Path.Combine(Helper.AppData_Path, "Signatures");
            signatures.Add(Guid.NewGuid().ToString(), new PdfSignatureOptions() {
                Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(System.IO.Path.Combine(signatureDictionaryPath, "certificate.pfx"), DemoPassword),
                ContactInfo = "John Smith",
                Location = "Australia",
                Reason = "I Agree",
                ImageSource = DevExpress.XtraPrinting.Drawing.ImageSource.FromFile(System.IO.Path.Combine(signatureDictionaryPath, "John_Smith.png"))
            });
            signatures.Add(Guid.NewGuid().ToString(), new PdfSignatureOptions() {
                Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(System.IO.Path.Combine(signatureDictionaryPath, "certificate.pfx"), DemoPassword),
                ContactInfo = "Jane Cooper",
            });
        }

        Task<Dictionary<string, PdfSignatureOptions>> DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync.GetAvailableOptionsAsync() {
            return Task.FromResult(signatures);
        }
    }
}
