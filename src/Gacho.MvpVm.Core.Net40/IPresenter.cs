using System;
using System.Collections.Generic;
using System.Text;
#if NET40
using System.Threading.Tasks;
#endif
namespace Gacho.MvpVm.Core
{
#if NET20
    public delegate void InitializeDelegate<TModel>(TModel model)
        where TModel : class, IViewModel;
#endif

    public interface IPresenter<in TModel> : IDisposable
        where TModel : class, IViewModel
    {
#if NET40
        Task InitializeAsync(TModel model);
#elif NET20
        void Initialize(TModel model);
#endif
    }
}
