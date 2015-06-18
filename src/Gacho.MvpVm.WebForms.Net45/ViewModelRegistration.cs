using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public static class ViewModelRegistration
    {
        public static void RegisterToViewModel<TModel>(this IView<TModel> view)
            where TModel : class, INotifyPropertyChanged, new()
        {
            var key = view.UniqueID + "_NotifyPropertyChangedEventMapper";
            if (!HttpContext.Current.Items.Contains(key))
            {
                var notifyPropertyChangedEventMapper = new NotifyPropertyChangedEventMapper(view.Model, view);
                HttpContext.Current.Items[key] = notifyPropertyChangedEventMapper;
                ((Control)view).Unload += (e, args) => { notifyPropertyChangedEventMapper.Dispose(); };
            }
        }
    }
}
