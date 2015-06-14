using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public static class PageRedirectingExtensions
    {
        public static bool IsPageRedirecting(this Control control) {
            return HttpContext.Current.Items.Contains("IsPageRedirecting");
        }

        public static Page GetPage(this Control control)
        {
            return control.Page == null ? (Page)control : control.Page;
        }

        public static void Redirect(this Control control, Uri url, bool isPermanent = false)
        {
            control.GetPage().Redirect(url, isPermanent);
        }

        public static void Redirect(this Page page, Uri url, bool isPermanent = false)
        {
            if (isPermanent)
            {
                page.Response.RedirectPermanent(url.AbsoluteUri, true);
            }
            else
            {
                page.Response.Redirect(url.AbsoluteUri, true);
            }

            CompleteRedirect();
        }

        public static void RedirectToRoute(this Control control, string routeName, RouteValueDictionary routeValues, bool isPermanent = false)
        {
            control.GetPage().RedirectToRoute(routeName, routeValues, isPermanent);
        }

        public static void RedirectToRoute(this Page page, string routeName, RouteValueDictionary routeValues, bool isPermanent = false)
        {
            if (isPermanent)
            {
                page.Response.RedirectToRoutePermanent(routeName, routeValues);
            }
            else
            {
                page.Response.RedirectToRoute(routeName, routeValues);
            }

            CompleteRedirect();
        }

        private static void CompleteRedirect()
        {
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            HttpContext.Current.Items["IsPageRedirecting"] = true;
        }
    }
}
