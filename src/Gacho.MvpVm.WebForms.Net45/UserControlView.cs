using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public abstract class UserControlView<TModel> : UserControl, IView<TModel>
        where TModel : class, IViewModel, new()
    {
        private TModel model;

        private NotifyPropertyChangedEventMapper notifyPropertyChangedEventMapper;

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
            if (this.Page.IsAsync)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(() => this.Presenter.InitializeAsync(this.Model)));
            }
            else
            {
                this.Presenter.InitializeAsync(this.Model).RunSynchronously();
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
            if (this.notifyPropertyChangedEventMapper != null)
            {
                this.notifyPropertyChangedEventMapper.Dispose();
            }
        }

        protected virtual TModel CreateViewModelInstance()
        {
            return new TModel();
        }
    }
}
