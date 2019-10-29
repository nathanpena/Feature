using System.Web;
using System.Web.Optimization;

namespace Features
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            var rootContext = "~/Scripts/";
            if (!HttpContext.Current.IsDebuggingEnabled) {
                rootContext = "~/../../common/AngularDotNet/";
            }

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        rootContext + "jquery-{version}.js"));

            var angularBundle = new ScriptBundle("~/bundles/angularjs");
            angularBundle.Include(rootContext + "angular.js")
                .Include(rootContext + "jquery.signalR-2.2.1.js")
                .Include(rootContext + "angular-resource.js")
                .Include(rootContext + "angular-cookies.js")
                .Include(rootContext + "angular-ui/ui-bootstrap.js")
                .Include(rootContext + "angular-ui/ui-bootstrap-tpls.js")
                .Include(rootContext + "spin.min.js")
                .Include(rootContext + "angular-spinner.js")
                .Include(rootContext + "angular-ui-router.js")
                .Include(rootContext + "lodash.core.min.js")
                .Include(rootContext + "dirPagination.js")
                .Include(rootContext + "ng-file-upload.js")
                .Include(rootContext + "rangy/rangy-core.js")
                .Include(rootContext + "rangy/rangy-selectionsaverestore.js")
                .Include(rootContext + "textAngular/dist/textAngular-sanitize.min.js")
                .Include(rootContext + "textAngular/dist/textAngular.min.js")
                .Include(rootContext + "textAngular/dist/textAngular-rangy.min.js");

            bundles.Add(angularBundle);

            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory(
                        "~/App", "*.js", true));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        rootContext + "modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      rootContext + "bootstrap.js",
                      rootContext + "respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      rootContext + "textAngular/dist/textAngular.css",
                      "~/Content/font-awesome.min.css"));
            //BundleTable.EnableOptimizations = true;

        }
    }
}
