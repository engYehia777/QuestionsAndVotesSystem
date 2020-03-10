using System.Web;
using System.Web.Optimization;

namespace QuestionsAndVotesSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/ProfileLibs").Include(

                     "~/Areas/Profile/bower_components/jquery/dist/jquery.js",
                     "~/Areas/Profile/bower_components/jquery-ui/jquery-ui.min.js",
                     "~/Scripts/jquery.unobtrusive-ajax", 
                     "~/Areas/Profile/bower_components/bootstrap/dist/js/bootstrap.min.js",
                     "~/Areas/Profile/dist/js/material.js",
                     "~/Areas/Profile/dist/js/ripples.js",
                     "~/Areas/Profile/bower_components/select2/dist/js/select2.full.min.js",
                     "~/Areas/Profile/bower_components/raphael/raphael.min.js",
                     "~/Areas/Profile/bower_components/morris.js/morris.min.js",
                     "~/Areas/Profile/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                     "~/Areas/Profile/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                     "~/Areas/Profile/bower_components/jquery-knob/dist/jquery.knob.min.js",
                     "~/Areas/Profile/bower_components/moment/min/moment.min.js",
                     "~/Areas/Profile/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                     "~/Areas/Profile/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                     "~/Areas/Profile/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                     "~/Areas/Profile/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                     "~/Areas/Profile/bower_components/fastclick/lib/fastclick.js",
                     "~/Areas/Profile/dist/js/adminlte.js",
                     "~/Areas/Profile/dist/js/pages/dashboard.js",
                     "~/Areas/Profile/dist/js/demo.js")
                     );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Profilecss").Include(
                     //Bootstrap 3.3.7
                     "~/Areas/Profile/bower_components/bootstrap/dist/css/bootstrap.min.css",
                     //Font Awesome
                     "~/Areas/Profile/bower_components/font-awesome/css/font-awesome.min.css",
                     //Ionicons
                     "~/Areas/Profile/bower_components/Ionicons/css/ionicons.min.css",
                     //Theme style
                     "~/Areas/Profile/dist/css/AdminLTE.css",
                     //Material Design
                     "~/Areas/Profile/dist/css/bootstrap-material-design.min.css",
                     "~/Areas/Profile/dist/css/ripples.min.css",
                     "~/Areas/Profile/dist/css/MaterialAdminLTE.css",
                      //AdminLTE Skins.Choose a skin from the css/ skins folder instead of downloading all of them to reduce the load
                     "~/Areas/Profile/dist/css/skins/all-md-skins.min.css",
                     //Morris chart
                     "~/Areas/Profile/bower_components/morris.js/morris.css",
                     //jvectormap
                     "~/Areas/Profile/bower_components/jvectormap/jquery-jvectormap.css",
                     //Date Picker
                     "~/Areas/Profile/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                     //Daterange picker
                     "~/Areas/Profile/bower_components/bootstrap-daterangepicker/daterangepicker.css",
                     // dropdown
                     "~/Areas/Profile/bower_components/select2/dist/css/select2.min.css",
                     //bootstrap wysihtml5 - text editor
                     "~/Areas/Profile/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css")
                     );

            // data tables
            bundles.Add(new StyleBundle("~/Content/DataTableCss").Include(
                      "~/Areas/Profile/bower_components/datatables.net-bs/css/dataTables.bootstrap.css"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/DataTableLibs").Include(
                    "~/Areas/Profile/bower_components/datatables.net/js/jquery.dataTables.min.js",
                    "~/Areas/Profile/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"));

            // toastr notification
            bundles.Add(new StyleBundle("~/Content/Toastr").Include(
                "~/Content/toastr.css"
                ));


            bundles.Add(new ScriptBundle("~/bundles/Toastr").Include(
                     "~/Scripts/toastr.js"
                    ));
        }
    }
}
