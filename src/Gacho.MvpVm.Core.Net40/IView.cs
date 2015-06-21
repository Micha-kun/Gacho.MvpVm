using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gacho.MvpVm.Core
{
    public interface IView<TModel>
        where TModel : class, INotifyPropertyChanged
    {
        string UniqueID { get; }

        TModel Model { get; }
    }

    public interface IView<TModel, TPresenter> : IView<TModel>
        where TModel : class, IViewModel
        where TPresenter : class, IPresenter<TModel>
    {
        TPresenter Presenter { get; }
    }
}
