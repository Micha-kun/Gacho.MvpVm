using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public interface IView : INamingContainer
    {
        IViewModel ViewModel { get; }
    }

    public interface IView<TModel, TPresenter> : IView
        where TModel : class, IViewModel
        where TPresenter : class, IPresenter<TModel>
    {
        TModel Model { get; }

        TPresenter Presenter { get; }
    }
}
