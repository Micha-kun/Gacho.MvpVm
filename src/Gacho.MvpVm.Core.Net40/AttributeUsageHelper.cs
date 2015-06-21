using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
#if NET40
using System.Linq;
using System.Threading.Tasks;
#endif

namespace Gacho.MvpVm.Core
{
    public static class AttributeUsageHelper
    {
#if NET40
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
#elif NET20
        public static bool IsAttributedWith<TAttribute>(object instance, bool inherit = false) where TAttribute : Attribute
        {
            foreach (var item in FindAttributesOfType<TAttribute>(instance, inherit))
            {
                return true;
            }

            return false;
        }

        public static bool IsAttributedWith<TAttribute>(MemberInfo propertyInfo, bool inherit = false) where TAttribute : Attribute
        {
            foreach (var item in FindAttributesOfType<TAttribute>(propertyInfo,inherit))
            {
                return true;
            }

            return false;
        }

        public static IEnumerable<TAttribute> FindAttributesOfType<TAttribute>(object instance, bool inherit = false)
            where TAttribute : Attribute
        {
            return FindAttributesOfType<TAttribute>(instance.GetType(), inherit);
        }

        public static IEnumerable<TAttribute> FindAttributesOfType<TAttribute>(Type type, bool inherit = false) where TAttribute : Attribute
        {
            foreach (var attr in type.GetCustomAttributes(typeof(TAttribute), inherit))
            {
                if (attr is TAttribute)
                {
                    yield return (TAttribute)attr;
                }
            }
        }

        public static IEnumerable<TAttribute> FindAttributesOfType<TAttribute>(MemberInfo propertyInfo, bool inherit = false)
            where TAttribute : Attribute
        {
            foreach (var attr in propertyInfo.GetCustomAttributes(typeof(TAttribute), inherit))
            {
                if (attr is TAttribute)
                {
                    yield return (TAttribute)attr;
                }
            }
        }
#endif
    }
}
