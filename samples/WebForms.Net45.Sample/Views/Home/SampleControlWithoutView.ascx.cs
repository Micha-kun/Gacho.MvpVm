using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gacho.MvpVm.WebForms;
using Gacho.MvpVm.Core;
using WebForms.Net45.Sample.ViewModels;

namespace WebForms.Net45.Sample.Views.Home
{
    public partial class SampleControlWithoutView : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.RegisterToViewModel<HomeViewModel>();
        }

        protected HomeViewModel Model { get { return this.FindModel<HomeViewModel>(); } }

        [PropertyChangedMethod("Text")]
        public void TextChanged()
        {
            this.Test.Text = Model.Text;
        }
    }
}