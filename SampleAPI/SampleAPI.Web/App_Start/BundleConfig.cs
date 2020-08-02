using System.Web;
using System.Web.Optimization;

namespace SampleAPI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/lib/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/lib/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/lib/font-awesome/js/all.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/lib/font-awesome/css/all.min.css",
                "~/lib/bootstrap/dist/css/bootstrap.min.css"
            ));
        }
    }
}
