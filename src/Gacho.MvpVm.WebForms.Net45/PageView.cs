using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public abstract class PageView<TModel> : Page, IView<TModel>
        where TModel : class, IViewModel, new()
    {
        private NotifyPropertyChangedEventMapper notifyPropertyChangedEventMapper;

        private TModel model;

        public virtual TModel Model
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

        public abstract IPresenter<TModel> Presenter { get; }

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

            this.notifyPropertyChangedEventMapper = new NotifyPropertyChangedEventMapper(this.model, this);
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
                    this.Presenter.InitializeAsync(this.Model).RunSynchronously();
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
            if (this.notifyPropertyChangedEventMapper != null)
            {
                this.notifyPropertyChangedEventMapper.Dispose();
            }

            this.Presenter.Dispose();
        }
    }
}
