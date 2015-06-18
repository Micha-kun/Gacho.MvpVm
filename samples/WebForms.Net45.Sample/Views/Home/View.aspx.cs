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
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.Model.Text = this.Element.Text;
        }

        protected override HomePresenter BuildPresenter()
        {
            return new HomePresenter();
        }

        protected void NoViewCtrl_Init(object sender, EventArgs e)
        {
            NoViewCtrl.Model = this.Model;
        }
    }
}