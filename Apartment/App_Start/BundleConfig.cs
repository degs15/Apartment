using System.Web;
using System.Web.Optimization;

namespace Apartment
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js")
                        .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                        );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                "~/Scripts/DataTables/DataTables-1.10.20/js/dataTables*",
                "~/Scripts/DataTables/DataTables-1.10.20/js/jquery.dataTables*",
                "~/Scripts/DataTables/datatables*"));

            bundles.Add(new ScriptBundle("~/bundles/Buttons").Include(
        "~/Scripts/DataTables/Buttons-1.6.1/js/dataTables*",
        "~/Scripts/DataTables/Buttons-1.6.1/js/buttons*"));

            bundles.Add(new ScriptBundle("~/bundles/Apartment").Include(
                "~/Scripts/CustomScripts/apartment*"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/Content/bootstrap.css",
               "~/Content/site.css")
               );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
          "~/Scripts/DataTables/DataTables-1.10.20/css/dataTables*",
          "~/Scripts/DataTables/DataTables-1.10.20/css/jquery.dataTables*"
          ));

            bundles.Add(new StyleBundle("~/Content/Buttons").Include(
  "~/Scripts/DataTables/Buttons-1.6.1/css/buttons*",
  "~/Scripts/DataTables/Buttons-1.6.1/css/common*",
   "~/Scripts/DataTables/Buttons-1.6.1/css/mixins*"
   ));
        }
    }
}
