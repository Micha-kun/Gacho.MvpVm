using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gacho.MvpVm.Core
{
    public static class AttributeUsageHelper
    {
        public static bool IsAttributedWith<TAttribute>(this object instance, bool inherit = false) where TAttribute : Attribute
        {
            return instance.FindAttributesOfType<TAttribute>(inherit).Any();
        }

        public static bool IsAttributedWith<TAttribute>(this MemberInfo propertyInfo, bool inherit = false) where TAttribute : Attribute
        {
            return propertyInfo.FindAttributesOfType<TAttribute>(inherit).Any();
        }

        public static IEnumerable<TAttribute> FindAttributesOfType<TAttribute>(this object instance, bool inherit = false)
            where TAttribute : Attribute
        {
            return instance.GetType().FindAttributesOfType<TAttribute>(inherit);
        }

        public static IEnumerable<TAttribute> FindAttributesOfType<TAttribute>(this Type type, bool inherit = false) where TAttribute : Attribute
        {
            return type.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }

        public static IEnumerable<TAttribute> FindAttributesOfType<TAttribute>(this MemberInfo propertyInfo, bool inherit = false)
            where TAttribute : Attribute
        {
            return propertyInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }
    }
}
