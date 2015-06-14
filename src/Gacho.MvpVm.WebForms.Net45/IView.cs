using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public interface IView<out TModel> : INamingContainer
        where TModel : class, IViewModel
    {
        TModel Model { get; }
    }
}
