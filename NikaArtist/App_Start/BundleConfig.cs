using System.Web;
using System.Web.Optimization;

namespace NikaArtist
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/mainBundle").Include(
                        "~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
					    "~/Scripts/Site/common.js",
						"~/Scripts/masonry.js",
						"~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/adminBundle").Include(
                    "~/Scripts/bootbox.js",
                    "~/Scripts/Site/common.js",
					"~/Scripts/Site/eventHandlers.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
					  "~/Content/font-awesome.css",
					  "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/admin").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/font-awesome.css",
                      "~/Content/AdminPanel.css"));
        }
    }
}
