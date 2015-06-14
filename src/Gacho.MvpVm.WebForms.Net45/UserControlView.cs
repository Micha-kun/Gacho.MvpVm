using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public abstract class UserControlView<TModel, TPresenter> : UserControl, IView<TModel, TPresenter>
        where TModel : class, IViewModel, new()
        where TPresenter : class, IPresenter<TModel>
    {
        private TModel model;

        private TPresenter presenter;

        protected virtual TModel Model
        {
            get
            {
                if (this.model == null)
                {
                    throw new InvalidOperationException("ViewModel cannot be null.");
                }

                return this.model;
            }
        }

        IViewModel IView.ViewModel { get { return this.Model; } }

        TModel IView<TModel, TPresenter>.Model { get { return this.Model; } }

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

        protected override void FrameworkInitialize()
        {
            base.FrameworkInitialize();
            this.model = this.CreateViewModelInstance();
            if (this.model == null)
            {
                throw new InvalidOperationException("ViewModel cannot be null.");
            }
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

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.IsPageRedirecting())
            {
                base.Render(writer);
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.Presenter.Dispose();
        }

        protected virtual TModel CreateViewModelInstance()
        {
            return new TModel();
        }
    }
}
