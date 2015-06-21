using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gacho.MvpVm.WinForms
{
    public abstract class FormView<TModel, TPresenter> : Form, IView<TModel,TPresenter>
        where TModel : class, IViewModel, new()
        where TPresenter : class, IPresenter<TModel>
    {
        public TPresenter Presenter
        {
            get { throw new NotImplementedException(); }
        }

        public string UniqueID
        {
            get { throw new NotImplementedException(); }
        }

        public TModel Model
        {
            get { throw new NotImplementedException(); }
        }
    }
}
