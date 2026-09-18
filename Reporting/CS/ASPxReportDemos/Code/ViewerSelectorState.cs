using System;
using System.Linq;
using System.Web;

namespace DevExpress.Web.Demos {
    public static class ViewerSelectorState {
        internal const string Key = "CurrentViewer";
        public const string ModernViewer = "HTML5";
        public const string DefaultViewer = ModernViewer;

        public static readonly Item[] Items;

        static ViewerSelectorState() {
            var generateHtml5ViewerUrlDelegate = new Func<Uri, string>(GenerateHtml5ViewerUrl);
            Items = new Item[] {
                new Item(generateHtml5ViewerUrlDelegate) { Name = ModernViewer, Text = "HTML5 Viewer", CssClass = "viewer-selector-html5" },
            };
        }

        public static string GetCurrentViewer() {
            return HttpContext.Current == null
                ? DefaultViewer
                : Items
                    .Select(item => item.Name)
                    .Where(name => name == HttpContext.Current.Request.Params[Key])
                    .DefaultIfEmpty(DefaultViewer)
                    .First();
        }

        public static Item GetByName(string name) {
            return Items.FirstOrDefault(x => x.Name == name);
        }

        static string GenerateHtml5ViewerUrl(Uri requestUrl) {
            var builder = new UriBuilder(requestUrl);
            builder.Query = "";
            return builder.ToString();
        }

        public class Item {
            readonly Func<Uri, string> _generateUrl;

            public Item(Func<Uri, string> generateUrl) {
                this._generateUrl = generateUrl;
            }

            public string GenerateUrl(Uri requestUri) {
                return _generateUrl(requestUri);
            }

            public string Name { get; set; }
            public string Text { get; set; }
            public string CssClass { get; set; }
        }
    }
}
