using System.Web;
using System.Web.Optimization;

namespace SignalR
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js")
                      .Include("~/Assets/js/jquery/jquery-2.0.3.js")
                .Include("~/Assets/js/jquery/jquery-ui-1.10.3.js")
                .Include("~/Assets/js/plugins/jquery.cookie.js")
                .Include("~/Assets/js/modernizr-2.6.2.js")
                .Include("~/Assets/js/main.js")
                .Include("~/Assets/js/plugins.js")
                .Include("~/Assets/js/plugins/transition.js")
                .Include("~/Assets/js/plugins/tab.js")
                .Include("~/Assets/js/infragistics.core.js")
                .Include("~/Assets/js/infragistics.lob.js")
                .Include("~/Assets/js/bootstrap-multiselect.js")
                .Include("~/Assets/js/plugins/tioltip.js")
                
                );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css").Include("~/Assets/css/spinner.css")
                .Include("~/Assets/css/infragistics.theme.custom.css")
                .Include("~/Assets/css/infragistics.structure.css")
                .Include("~/Assets/css/fonts.css")/*Fonts*/
                .Include("~/Assets/css/custom.css")
                 .Include("~/Assets/css/bootstrap-multiselect.css")); 
        }
    }
}
