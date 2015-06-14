using Gacho.MvpVm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Net45.Sample.ViewModels
{
    public class SiteViewModel : ViewModel
    {
        private string title;

        public string Title
        {
            get { return this.title; }
            set { this.SetField(ref this.title, value, () => this.Title); }
        }
    }
}