using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public interface IView<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        string UniqueID { get; }

        TModel Model { get; }
    }

    public interface IView<TModel, TPresenter> : IView<TModel>, INamingContainer
        where TModel : class, IViewModel
        where TPresenter : class, IPresenter<TModel>
    {
        TPresenter Presenter { get; }
    }
}
