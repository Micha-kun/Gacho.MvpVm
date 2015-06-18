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
    public partial class SampleControlWithoutView : UserControlView<HomeViewModel>
    {
        [PropertyChangedMethod("Text")]
        public void TextChanged()
        {
            this.Test.Text = Model.Text;
        }
    }
}