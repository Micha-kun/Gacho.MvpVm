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
        protected FormView(TPresenter presenter)
        {
            this.Presenter = presenter;
        }

        public TPresenter Presenter
        {
            get;
            private set;
        }

        public string UniqueID
        {
            get { return this.GetHashCode().ToString(); }
        }
        
        private TModel model;

#if NET40

        public virtual TModel Model
        {
            get
            {
                if (this.model == null)
                {
                    throw new InvalidOperationException("Model cannot be null.");
                }

                return this.model;
            }

            set
            {
                this.model = value;
                this.RegisterToViewModel();
            }
        }
#elif NET20
        public virtual TModel Model
        {
            get
            {
                if (this.model == null)
                {
                    throw new InvalidOperationException("Model cannot be null.");
                }

                return this.model;
            }

            set
            {
                this.model = value;
                ViewModelRegistration.RegisterToViewModel(this);
            }
        }
#endif

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Presenter.InitializeAsync(this.Model).Start();
        }
    }
}
