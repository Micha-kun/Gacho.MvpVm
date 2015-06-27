using Gacho.MvpVm.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Net45.Sample.Presenters;
using WebForms.Net45.Sample.ViewModels;

namespace WebForms.Net45.Sample.Views.Shared
{
    public partial class Site : MasterPageView<SiteViewModel, SitePresenter>
    {
        protected override SitePresenter BuildPresenter()
        {
            return new SitePresenter();
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }
    }
}