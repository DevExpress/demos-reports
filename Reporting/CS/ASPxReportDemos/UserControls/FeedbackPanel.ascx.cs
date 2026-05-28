using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevExpress.Web.Demos {
    public partial class UserControls_FeedbackPanel : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void linkSupportCenter_Init(object sender, EventArgs e) {
            var link = (ASPxHyperLink)sender;
            link.Text = "on the DevExpress Support Forum.";
            link.Target = "_blank";

            var currentDemo = DevExpress.Web.Demos.Utils.CurrentDemo;
            if(currentDemo != null && currentDemo.Product != null) {
                string currentProductID = GetProductPlatformId(currentDemo.Product.Key);
                string currentProductFilter = "filters=%5B%7B%22filterName%22:%22PlatformedProductId%22,%22selectedValues%22:%5B%22" + currentProductID + "%22%5D%7D%5D";

                string demoTitle = currentDemo.GetSeoTitle();
                string demoGroup = "";

                if(currentDemo.Product.Key.Contains("ASPxperience")) {
                    DevExpress.Web.Demos.DemoPageModel pageModel = currentDemo as DevExpress.Web.Demos.DemoPageModel;
                    if(pageModel != null && pageModel.Group != null) {
                        if(pageModel.Group.Key.Contains("Accessibility"))
                            demoGroup = currentDemo.Product.NavItemTitle;
                        else if(!pageModel.Group.Key.Contains("Cloud Storage"))
                            demoGroup = pageModel.Group.Title;
                    }
                }
                if(!String.IsNullOrEmpty(demoGroup) && demoTitle != "Icon Library Explorer")
                    demoTitle = demoGroup + " " + demoTitle;

                demoTitle = demoTitle.Replace("/", " ").Replace("Support for", "");
                link.NavigateUrl = String.Format("https://supportcenter.devexpress.com/ticket/list?{0}&searchString={1}&sorting=Relevance", currentProductFilter, HttpUtility.UrlEncode(demoTitle));
            }
            else {
                link.NavigateUrl = "https://supportcenter.devexpress.com/ticket/list";
            }
        }
        static object instanceLocker = new object();
        private static Dictionary<string, string> _productPlatformCache;
        private static Dictionary<string, string> ProductPlatformCache {
            get {
                if(_productPlatformCache == null) {
                    lock(instanceLocker) {
                        if(_productPlatformCache == null) {
                            _productPlatformCache = new Dictionary<string, string>();
                            _productPlatformCache.Add("ASPxCardView", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:9d99daa7-fe83-11e6-82d9-54271e19360e");
                            _productPlatformCache.Add("Dashboard", "35e0b143-40ca-11e2-b3e9-c0cb38ffce8d:e6bb47fe-cc0b-11e6-82d1-54271e19360e");
                            _productPlatformCache.Add("ASPxDiagram", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:e2102f3e-c99c-48ad-a83f-c2086aa6b006");
                            _productPlatformCache.Add("ASPxEditors", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:7b499dd4-c4bd-4fd0-bc84-b78f207758d0");
                            _productPlatformCache.Add("ASPxGantt", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:6199c828-a8b6-45d1-8736-0c80ee9b5e62");
                            _productPlatformCache.Add("ASPxGauges", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:eee44732-43af-44e3-afc6-9464fc076599");
                            _productPlatformCache.Add("ASPxGridView", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:22713479-a995-45d4-9ed4-72ffa096d83d");
                            _productPlatformCache.Add("ASPxHtmlEditor", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:3a1ffe09-446f-4b93-9c49-3730b33e839e");
                            _productPlatformCache.Add("ASPxperience.DockAndPopups", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed");
                            _productPlatformCache.Add("ASPxperience.FileManagerAndUpload", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed");
                            _productPlatformCache.Add("ASPxperience.ImageAndDataNavigation", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed");
                            _productPlatformCache.Add("ASPxperience.MultiUseControls", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed");
                            _productPlatformCache.Add("ASPxperience.NavigationAndLayout", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:725f3678-7b8c-476a-9bae-d68df69f01ed");
                            _productPlatformCache.Add("ASPxPivotGrid", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:48b469a5-cd05-48aa-b992-ed28e84dd7d7");
                            _productPlatformCache.Add("XtraReports", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:e9f1506a-471c-4b72-82df-151effca6b0d");
                            _productPlatformCache.Add("ASPxRichEdit", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:4a9d252d-6e4e-11e4-b1b2-5442496457d0");
                            _productPlatformCache.Add("ASPxScheduler", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:f8aad72b-2600-4fe2-9047-07c820fadd48");
                            _productPlatformCache.Add("ASPxSpellChecker", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:c0d297f4-d920-452b-89c3-f348b4f4deb4");
                            _productPlatformCache.Add("ASPxSpreadsheet", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:0ac3bc5b-4e0a-11e3-8230-5442496457d0");
                            _productPlatformCache.Add("ASPxTreeList", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:a6a6028e-e0cd-418e-a4c9-31e297bb0d69");
                            _productPlatformCache.Add("ASPxVerticalGrid", "21fcf869-bfcd-4f53-b199-016b3c49d2fc:9d99da9a-fe83-11e6-82d9-54271e19360e");
                        }
                    }
                }            
                return _productPlatformCache;
            }
        }

        private string GetProductPlatformId(string demoNodeName) {
            return ProductPlatformCache.ContainsKey(demoNodeName) ? ProductPlatformCache[demoNodeName] : "";
        }
    }
}
