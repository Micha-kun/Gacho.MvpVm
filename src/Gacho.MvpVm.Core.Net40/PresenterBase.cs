using System;
using System.Collections.Generic;
using System.Text;
#if NET40
using System.Linq;
using System.Threading.Tasks;
#endif

namespace Gacho.MvpVm.Core
{
    internal delegate void ModelInitializer<TModel>(TModel model) where TModel : class, IViewModel;
    public abstract class Presenter<TModel> : IPresenter<TModel>
       where TModel : class, IViewModel
    {
        private TModel model;

        private NotifyPropertyChangedEventMapper notifyPropertyChangedEventMapper;

        ~Presenter()
        {
            this.Dispose(false);
        }

        protected TModel Model
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

#if NET40
        Task IPresenter<TModel>.InitializeAsync(TModel viewModel)
        {
            return this.InitializeAsync(viewModel);
        }
#elif NET20

        void IPresenter<TModel>.Initialize(TModel model)
        {
            Initialize(model);
        }

        protected abstract void Initialize(TModel model);
#endif

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.notifyPropertyChangedEventMapper != null)
                {
                    this.notifyPropertyChangedEventMapper.Dispose();
                    this.notifyPropertyChangedEventMapper = null;
                }
            }
        }

#if NET40

        protected virtual Task InitializeAsync(TModel viewModel)
        {
            if (viewModel == default(TModel))
            {
                throw new ArgumentNullException("viewModel", "ViewModel cannot be null.");
            }

            this.model = viewModel;
            this.notifyPropertyChangedEventMapper = new NotifyPropertyChangedEventMapper(this.model, this);
            return this.Initialize();
        }

        protected abstract Task Initialize();
#endif

    }
}
