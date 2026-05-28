using System;
using System.Linq;
using System.Web;

namespace DevExpress.Web.Demos {
    public static class ViewerSelectorState {
        public const string Key = "CurrentViewer";
        public const string Html5Viewer = "HTML5";
        public const string MobileViewer = "Mobile";
        public const string DefaultViewer = Html5Viewer;

        public static Item[] Items = {
            new Item(GenerateHtml5ViewerUrl) { Name = Html5Viewer, Text = "HTML5 Viewer", CssClass = "viewer-selector-html5" },
            new Item(GenerateMobileViewerUrl) { Name = MobileViewer, Text = "Mobile Viewer", CssClass = "viewer-selector-html5" }
        };

        public static Item GetByName(string name) {
            return Items.FirstOrDefault(x => x.Name == name);
        }

        static string GenerateHtml5ViewerUrl(Uri requestUrl) {
            var builder = new UriBuilder(requestUrl);
            builder.Query = "";
            return builder.ToString();
        }

        static string GenerateMobileViewerUrl(Uri requestUrl) {
            var builder = new UriBuilder(requestUrl);
            builder.Query = Key + "=" + MobileViewer;
            return builder.ToString();
        }

        public static string GetSafeCurrentViewerArgFromQuery(HttpRequestBase request) {
            string viewerFromQuery = request?.QueryString[Key];
            return GetSafeCurrentViewerArgFromString(viewerFromQuery);
        }

        public static string GetSafeCurrentViewerArgFromString(string viewerMode) {
            var currentViewer = Items
                    .Select(item => item.Name)
                    .Where(name => name == viewerMode)
                    .DefaultIfEmpty(DefaultViewer)
                    .First();
            return currentViewer == DefaultViewer ? null : currentViewer;
        }

        public class Item {
            readonly Func<Uri, string> generateUrl;

            public Item(Func<Uri, string> generateUrl) {
                this.generateUrl = generateUrl;
            }

            public string GenerateUrl(Uri requestUri) {
                return generateUrl(requestUri);
            }

            public string Name { get; set; }
            public string Text { get; set; }
            public string CssClass { get; set; }
        }
    }
}
