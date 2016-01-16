using System.Web.Optimization;

namespace Sciencecom
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/messi").Include(
                       "~/Scripts/Dialog/messi.js",
                       "~/Scripts/Dialog/jquery.bxslider.min.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/messi.css",
                      "~/Content/themes/flick/jquery-ui.css",
                      "~/Content/jquery.bxslider.css"));
            
            bundles.Add(new StyleBundle("~/Content/Map").Include(
            "~/Scripts/Autocomplete.js",
            "~/Scripts/AddPlacamerkToMap.js",
            "~/Scripts/MapSearch.js"
           ));
            //css для Grid
            bundles.Add(new StyleBundle("~/Content/jqCrid").Include(
            "~/Content/jqGrid/ui.jqgrid.css",
            "~/Content/jqGrid/searchFilter.css",
            "~/Content/jqGrid/ui.jqgrid-bootstarp.css",
            "~/Content/jqGrid/ui.multiselect.css",
            "~/Content/GridSystemForTable.css"
           ));
            //javascript для Grid
            bundles.Add(new StyleBundle("~/bundles/jqCrid").Include(
            "~/Scripts/jqGrid/i18n/grid.locale-en.js",
            "~/Scripts/jqGrid/jquery.jqGrid.js",
            "~/Scripts/jqGrid/plugins/grid.tbltogrid.js"
          
            ));

            //tooltip
            bundles.Add(new StyleBundle("~/bundles/tooltip").Include(
            "~/Scripts/tooltip/tooltipsy.source.js"
            ));

            bundles.Add(new StyleBundle("~/Content/Data").Include(
            "~/Scripts/AddPlacemarkEdit.js",
            "~/Scripts/AddPlacemark.js",
            "~/Scripts/MapSearch.js",
            "~/Scripts/Autocomplete.js"
           ));
        }
    }
}
