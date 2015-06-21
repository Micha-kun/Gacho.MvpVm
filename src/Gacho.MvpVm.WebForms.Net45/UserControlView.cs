using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
#if NET45 || NET40
using System.Linq;
using System.Threading.Tasks;
#endif
using System.Web.UI;
using System.Web;

namespace Gacho.MvpVm.WebForms
{
    public abstract class UserControlView<TModel> : UserControl, IView<TModel>
         where TModel : class, INotifyPropertyChanged, new()
    {
        private TModel model;

#if NET45 || NET40

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
    }

    public abstract class UserControlView<TModel, TPresenter> : UserControlView<TModel>, IView<TModel, TPresenter>, INamingContainer
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

#if NET45
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
#elif NET40
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Page.IsAsync)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(new BeginEventHandler(this.BeginInitializeModel), new EndEventHandler(this.EndInitializeModel), new EndEventHandler(this.TimeoutInitializeModel), null, true));
            }
            else
            {
                this.Presenter.InitializeAsync(this.Model).Wait();
            }
        }

        private IAsyncResult BeginInitializeModel(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            return this.Presenter.InitializeAsync(this.Model);
        }

        private void EndInitializeModel(IAsyncResult ar)
        {
            ((Task)ar).Wait();
        }

        private void TimeoutInitializeModel(IAsyncResult ar)
        {
        }
#else
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Page.IsAsync)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(new BeginEventHandler(this.BeginInitializeModel), new EndEventHandler(this.EndInitializeModel), new EndEventHandler(this.TimeoutInitializeModel), null, true));
            }
            else
            {
                this.Presenter.Initialize(this.Model);
            }
        }

        private IAsyncResult BeginInitializeModel(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            return this.Presenter.BeginInitialize(this.Model);
        }

        private void EndInitializeModel(IAsyncResult ar)
        {
        }

        private void TimeoutInitializeModel(IAsyncResult ar)
        {
        }
#endif

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.Presenter.Dispose();
        }
    }
}
