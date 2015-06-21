using System;
using System.Collections.Generic;
using System.Text;
#if NET40
using System.Threading.Tasks;
#endif
namespace Gacho.MvpVm.Core
{
    public interface IPresenter<in TModel> : IDisposable
        where TModel : class, IViewModel
    {
#if NET40
        Task InitializeAsync(TModel model);
#elif NET20
        void Initialize(TModel model);

        IAsyncResult BeginInitialize(TModel model);

        void EndInitialize(IAsyncResult result);
#endif
    }
}
