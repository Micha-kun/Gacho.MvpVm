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
        public static IViewModel FindModel(this Control control)
        {
            while (control != null)
            {
                var view = control as IView;
                if (view != null)
                {
                    return view.Model;
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
    }
}
