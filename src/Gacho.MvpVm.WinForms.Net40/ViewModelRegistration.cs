using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Gacho.MvpVm.WinForms
{
    public static class ViewModelRegistration
    {
#if NET40 || NET45
        public static void RegisterToViewModel<TModel>(this IView<TModel> view)
            where TModel : class, INotifyPropertyChanged, new()
        {
            var key = view.UniqueID + "_NotifyPropertyChangedEventMapper";
            if (CallContext.GetData(key) == null)
            {
                var notifyPropertyChangedEventMapper = new NotifyPropertyChangedEventMapper(view.Model, view);
                CallContext.SetData(key, notifyPropertyChangedEventMapper);
                ((Control)view).Disposed += (e, args) => { notifyPropertyChangedEventMapper.Dispose(); };
            }
        }
#elif NET20
        public static void RegisterToViewModel<TModel>(IView<TModel> view)
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
#endif
    }
}
