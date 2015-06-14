using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebForms.Net45.Sample.ViewModels;

namespace WebForms.Net45.Sample.Presenters
{
    public class HomePresenter : Presenter<HomeViewModel>
    {
        protected override Task Initialize()
        {
            return Task.FromResult<object>(null);
        }
    }
}