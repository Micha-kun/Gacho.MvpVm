using Gacho.MvpVm.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Net45.Sample.Presenters;
using WebForms.Net45.Sample.ViewModels;

namespace WebForms.Net45.Sample.Views.Home
{
    public partial class View : PageView<HomeViewModel,HomePresenter>
    {
        protected override HomePresenter BuildPresenter()
        {
            return new HomePresenter();
        }
    }
}