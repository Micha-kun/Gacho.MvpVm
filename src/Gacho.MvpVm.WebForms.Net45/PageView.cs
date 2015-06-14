using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public abstract class PageView<TModel, TPresenter> : Page, IView<TModel, TPresenter>
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

        TModel IView<TModel,TPresenter>.Model { get { return this.Model; } }

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

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (!this.IsPageRedirecting())
            {
                base.RaisePostBackEvent(sourceControl, eventArgument);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.IsPageRedirecting())
            {
                base.Render(writer);
            }
        }

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
            if (!this.IsPageRedirecting())
            {
                if (this.Page.IsAsync)
                {
                    this.RegisterAsyncTask(new PageAsyncTask(() => this.Presenter.InitializeAsync(this.Model)));
                }
                else
                {
                    this.Presenter.InitializeAsync(this.Model).Wait();
                }
            }
        }

        protected virtual TModel CreateViewModelInstance()
        {
            return new TModel();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.Presenter.Dispose();
        }
    }
}
