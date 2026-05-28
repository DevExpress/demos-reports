using DevExpress.Web.Mvc;
using System.Web.Mvc;
using System.Web.UI;

namespace DevExpress.Web.Demos {
    public abstract class DemoController: Controller {
        public const string ViewTitleSuffix = "DevExpress ASP.NET MVC Extensions";

        public abstract string Name { get; }

        public ActionResult DemoView(string actionName) {
            return DemoView(actionName, actionName, null);
        }
        public ActionResult DemoView(string actionName, object model) {
            return DemoView(actionName, actionName, model);
        }
        public ActionResult DemoView(string actionName, string viewName) {
            return DemoView(actionName, viewName, null);
        }
        public ActionResult DemoView(string actionName, string viewName, object model) {
            Utils.RegisterCurrentMvcDemo(Name, actionName);
            return (model != null) ? View(viewName, model) : View(viewName);
        }

        // Specific views
        public ActionResult ClientSideEventsDemoView(string[] events) {
            return ClientSideEventsDemoView(events, null);
        }
        public ActionResult ClientSideEventsDemoView(string[] events, object model) {
            return ClientSideEventsDemoView(events, true, model);
        }
        public ActionResult ClientSideEventsDemoView(string[] events, bool showEventListPanel, object model) {
            ViewData["ClientSideEvents"] = events;
            ViewData["ShowEventListPanel"] = showEventListPanel;
            return DemoView("ClientSideEvents", model);
        }
        protected internal string MapPath(string path) {
            return System.Web.HttpContext.Current.Request.MapPath(path);
        }

#if OfficeDemo
        protected override void OnActionExecuted(ActionExecutedContext filterContext) {
            DirectoryManagmentUtils.PurgeOldUserDirectories();
            base.OnActionExecuted(filterContext);
        }
#endif
    }

    public class DemosHelper {
        public static string GetFieldText(object data, string fieldName) {
            object text = DataBinder.Eval(data, fieldName);
            if (text == null || text.ToString() == string.Empty)
                return "&nbsp";
            return text.ToString();
        }
    }
    public static class DemoHelperExtension {
        public static void PrepareControlOptions<T>(this DemoHelper demoHelper, FormLayoutSettings<T> settings, MvcControlOptionsSettings options) {
            settings.Theme = DemoHelper.Instance.Theme;
            settings.RequiredMarkDisplayMode = RequiredMarkMode.None;
            var controlOptions = options.GetOptions();
            if(DemoHelper.Instance.ControlOptionsRightBlockWidth != null) {
                controlOptions.RightBlockWidth = (int)DemoHelper.Instance.ControlOptionsRightBlockWidth.Value;
            }
            settings.PreRender = (s, e) => {
                DemoHelper.Instance.PrepareControlOptions((ASPxFormLayout)s, controlOptions);
            };
        }
        public static void PrepareControlLayout<T>(this DemoHelper demoHelper, FormLayoutSettings<T> settings, MvcControlOptionsSettings options) {
            var controlOptions = options.GetOptions();
            if(DemoHelper.Instance.ControlOptionsRightBlockWidth != null) {
                controlOptions.RightBlockWidth = (int)DemoHelper.Instance.ControlOptionsRightBlockWidth.Value;
            }
            settings.PreRender = (s, e) => {
                DemoHelper.Instance.PrepareControlLayout((ASPxFormLayout)s, controlOptions);
            };
        }
    }
    public class MvcControlOptionsSettings {
        private ControlOptionsSettings settings;
        public MvcControlOptionsSettings() {
            settings = new ControlOptionsSettings();
        }
        public RecalculateColumnCountMode ColumnCountMode {
            get { return settings.ColumnCountMode; }
            set { settings.ColumnCountMode = value; }
        }
        public int ColumnMinWidth {
            get { return settings.ColumnMinWidth; }
            set { settings.ColumnMinWidth = value; }
        }
        public ControlOptionsSettings GetOptions() {
            return settings;
        }
    }

    public static class ExtensionsMethods {
        public static string GetUrlContentWithVersionSuffix(this WebViewPage view, string url) {
            return string.Format("{0}?v={1}", view.Url.Content(url), Utils.GetVersionSuffix());
        }
    }
}
