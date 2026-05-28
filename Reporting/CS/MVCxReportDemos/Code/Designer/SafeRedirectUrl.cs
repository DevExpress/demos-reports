using System.Linq;

namespace DevExpress.Web.Demos.Code.Designer {
    public class SafeRedirectUrl {
        public static string GetDemoLocalUrl(DemoProductModel demoProductModel, string groupName, string demoName) {
            if(demoProductModel == null || string.IsNullOrEmpty(demoName) || string.IsNullOrEmpty(groupName))
                return null;

            var demo = demoProductModel.Groups.FirstOrDefault(x => x.Key == groupName)
                ?.Demos.FirstOrDefault(x => x.Key == demoName);
            if(demo == null)
                return null;

            var demoUrl = Utils.GenerateDemoUrl(demo);
            return demoUrl;
        }
    }
}
