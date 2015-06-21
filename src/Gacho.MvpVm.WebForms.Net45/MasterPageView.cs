using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public abstract class MasterPageView<TModel> : MasterPage, IView<TModel>
         where TModel : class, INotifyPropertyChanged, new()
    {
        private TModel model;

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
    }

    public abstract class MasterPageView<TModel, TPresenter> : MasterPageView<TModel>, IView<TModel, TPresenter>, INamingContainer
        where TModel : class, IViewModel, new()
        where TPresenter : class, IPresenter<TModel>
    {
        private TPresenter presenter;

        public TPresenter Presenter
        {
            get
            {
                if (this.presenter == null)
                {
                    this.presenter = BuildPresenter();
                }

                return this.presenter;
            }
        }

        protected abstract TPresenter BuildPresenter();

        protected virtual TModel CreateViewModelInstance()
        {
            return new TModel();
        }

        protected override void FrameworkInitialize()
        {
            base.FrameworkInitialize();
            this.Model = this.CreateViewModelInstance();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Page.IsAsync)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(() => this.Presenter.InitializeAsync(this.Model)));
            }
            else
            {
                this.Presenter.InitializeAsync(this.Model).Wait();
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.Presenter.Dispose();
        }
    }
}
