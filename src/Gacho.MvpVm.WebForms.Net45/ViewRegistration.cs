using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Gacho.MvpVm.WebForms
{
    public static class ViewRegistration
    {
        public static TModel FindModel<TModel>(this Control control)
            where TModel : class, IViewModel
        {
            while (control != null)
            {
                var view = control as IView;
                if (view != null && view.ViewModel is TModel)
                {
                    return (TModel)view.ViewModel;
                }

                control = control.NamingContainer;
            }

            return null;
        }

        public static IViewModel FindModel(this Control control)
        {
            while (control != null)
            {
                var view = control as IView;
                if (view != null)
                {
                    return view.ViewModel;
                }

                control = control.NamingContainer;
            }

            return null;
        }

        public static void RegisterToViewModel(this Control control)
        {
            var key = control.UniqueID + "_NotifyPropertyChangedEventMapper";
            if (!HttpContext.Current.Items.Contains(key))
            {
                var model = control.FindModel();
                if (model != null)
                {
                    var notifyPropertyChangedEventMapper = new NotifyPropertyChangedEventMapper(model, control);
                    HttpContext.Current.Items[key] = notifyPropertyChangedEventMapper;
                }
            }
        }
        public static void RegisterToViewModel<TModel>(this Control control)
            where TModel : class, IViewModel
        {
            var key = control.UniqueID + "_NotifyPropertyChangedEventMapper";
            if (!HttpContext.Current.Items.Contains(key))
            {
                var model = control.FindModel<TModel>();
                if (model != null)
                {
                    var notifyPropertyChangedEventMapper = new NotifyPropertyChangedEventMapper(model, control);
                    HttpContext.Current.Items[key] = notifyPropertyChangedEventMapper;
                }
            }
        }
    }
}
