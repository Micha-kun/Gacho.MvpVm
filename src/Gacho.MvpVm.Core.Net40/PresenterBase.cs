using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gacho.MvpVm.Core
{
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

        Task IPresenter<TModel>.InitializeAsync(TModel viewModel)
        {
            return this.InitializeAsync(viewModel);
        }

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
    }
}
