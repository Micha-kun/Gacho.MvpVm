using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebForms.Net45.Sample
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                         "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/styles/bootstrap").Include(
                         "~/Content/bootstrap.css", "~/Content/bootstrap-theme.css"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr-respond").Include(
                         "~/Scripts/modernizr-{version}.js", "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/styles/site").Include("~/Content/Shared/Site.css"));

            // Code removed for clarity.
            BundleTable.EnableOptimizations = true;
        }
    }
}