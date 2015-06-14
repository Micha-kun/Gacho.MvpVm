using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebForms.Net45.Sample.ViewModels;

namespace WebForms.Net45.Sample.Presenters
{
    public class SitePresenter : Presenter<SiteViewModel>
    {
        protected override Task Initialize()
        {
            Model.Title = "Test Of Our Presenter";
            return Task.FromResult<object>(null);
        }
    }
}