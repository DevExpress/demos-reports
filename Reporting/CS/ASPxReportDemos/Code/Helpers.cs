using System;
using System.Linq;
using System.Web;
using DevExpress.Security;

namespace DevExpress.Web.Demos {
    public static class Helper {
        public static string AppData_Path = "";
        //
        public static string GetRelativePath(string name) {
            var request = HttpContext.Current.Request;
            string virtualPath = VirtualPathUtility.Combine("~/App_Data/", name);
            string appDataRoot = request.MapPath("~/App_Data");
            string appDataRelativePath = request.MapPath(virtualPath);
            if(!SafePath.IsRooted(appDataRoot, appDataRelativePath))
                throw new UnauthorizedAccessException("Access outside App_Data is not allowed.");
            return appDataRelativePath;
        }

        public static string GetDemoLocalUrl(DemoProductModel demoProductModel, string groupName, string demoName) {
            if(demoProductModel == null || string.IsNullOrEmpty(demoName) || string.IsNullOrEmpty(groupName))
                return null;
            var demo = demoProductModel.Groups.FirstOrDefault(x => x.Key == groupName)
                ?.Demos.FirstOrDefault(x => x.Key == demoName);
            if(demo == null)
                return null;
            return Utils.GenerateDemoUrl(demo);
        }
    }
}
