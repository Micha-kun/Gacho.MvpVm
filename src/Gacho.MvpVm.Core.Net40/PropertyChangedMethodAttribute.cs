using System;
using System.Collections.Generic;
using System.Text;

namespace Gacho.MvpVm.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PropertyChangedMethodAttribute : Attribute
    {
        public PropertyChangedMethodAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
