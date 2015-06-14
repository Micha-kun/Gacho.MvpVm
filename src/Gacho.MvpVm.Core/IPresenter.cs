using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gacho.MvpVm.Core
{
    public interface IPresenter<in TModel> : IDisposable
        where TModel : class, IViewModel
    {
        Task InitializeAsync(TModel model);
    }
}
