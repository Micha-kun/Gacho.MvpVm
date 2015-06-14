using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Net45.Sample.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        private string text;

        public string Text 
        {
            get { return text; }
            set { SetField(ref text, value, () => this.Text); }
        }
    }
}