using System.IO;
using System.Linq;
using System.Web.Optimization;

namespace Site.Backend
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterJs(bundles);

            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterJs(BundleCollection bundles)
        {
            bundles.Add(BundleScrips("~/js/customerLog", "customerLog.js"));
            bundles.Add(BundleScrips("~/js/baiViet", "baiViet.js"));
            bundles.Add(BundleScrips("~/js/file", "file.js"));
            bundles.Add(BundleScrips("~/js/guiMail", "guiMail.js"));
            bundles.Add(BundleScrips("~/js/category", "category.js"));
            bundles.Add(BundleScrips("~/js/sanPham", "sanPham.js"));
            bundles.Add(BundleScrips("~/js/customer", "customer.js"));
            bundles.Add(BundleScrips("~/js/appSettingImage", "appSettingImage.js"));
            bundles.Add(BundleScrips("~/js/extensions", "extensions.js"));
            bundles.Add(BundleScrips("~/js/htmlContent", "htmlContent.js"));
            bundles.Add(BundleScrips("~/js/storeFrontSetup", "storeFrontSetup.js"));
            bundles.Add(BundleScrips("~/js/administrator", "administrator.js"));
            bundles.Add(BundleScrips("~/js/role", "role.js"));
            bundles.Add(BundleScrips("~/js/siteSetting", "siteSetting.js"));
            bundles.Add(BundleScrips("~/js/systemEmailTemplate", "systemEmailTemplate.js"));
            bundles.Add(BundleScrips("~/js/authen", "authen.js"));
            bundles.Add(BundleScrips("~/js/common", "common.js"));
            bundles.Add(BundleScrips("~/js/formComponent", "formComponent.js"));
            bundles.Add(BundleScrips("~/js/notify", "notify.js"));
            bundles.Add(BundleScrips("~/js/site", "site.js"));
        }

        private static Bundle BundleScrips(string bundleName, params string[] filePaths)
        {
            const string rootPath = "~/content/scripts/site";

            var paths = filePaths.Select(f => Path.Combine(rootPath, f)).ToArray();

            return new ScriptBundle(bundleName).Include(paths);
        }
    }
}