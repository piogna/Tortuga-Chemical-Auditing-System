using System.Web;
using System.Web.Optimization;

namespace TMNT
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //tabs custom bundle
            bundles.Add(new ScriptBundle("~/bundles/section-tabs").Include(
                        "~/Scripts/modernizr-custom.js",
                        "~/Scripts/js-tabs.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        //required fields bundle
                        "~/Scripts/CustomMaxxamJS/finalize-create-data-table.js",
                        "~/Scripts/CustomMaxxamJS/form-validation.js",
                        "~/Scripts/CustomMaxxamJS/file-validation.js",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-sidemenu").Include(
                        "~/Scripts/jquery-sidemenu.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/sb-admin-2.js",
                      "~/Scripts/metisMenu.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", 
                      "~/Content/sections.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/metisMenu.css")
                      .Include("~/Content/font-awesome/css/font-awesome-4.4.0.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/maxxam-forms").Include(
                "~/Content/maxxam-forms.css"
            ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
