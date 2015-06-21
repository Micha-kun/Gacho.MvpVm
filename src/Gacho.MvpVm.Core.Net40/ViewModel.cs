using System;
using System.Collections.Generic;
using System.ComponentModel;
#if NET40
using System.Linq;
using System.Linq.Expressions;
#endif
using System.Reflection;
using System.Text;

namespace Gacho.MvpVm.Core
{
    public abstract class ViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

#if NET40
        protected void SetField<T>(ref T field, T value, Expression<Func<T>> propertyExpression)
        {
            var propertyName = this.ExtractPropertyName(propertyExpression);
            SetField<T>(ref field, value, propertyName);
        }

        private string ExtractPropertyName<T>(Expression<Func<T>> propertyExpresssion)
        {
            if (propertyExpresssion == null)
            {
                throw new ArgumentNullException("propertyExpresssion");
            }

            var memberExpression = propertyExpresssion.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpresssion");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("The member access expression does not access a property.", "propertyExpresssion");
            }

            if (!property.DeclaringType.IsInstanceOfType(this))
            {
                throw new ArgumentException("The referenced property belongs to a different type.", "propertyExpresssion");
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod == null)
            {
                // this shouldn't happen - the expression would reject the property before reaching this far
                throw new ArgumentException("The referenced property does not have a get method.", "propertyExpresssion");
            }

            if (getMethod.IsStatic)
            {
                throw new ArgumentException("The referenced property is a static property.", "propertyExpresssion");
            }

            return memberExpression.Member.Name;
        }
#endif
        protected void SetField<T>(ref T field, T value, string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            this.OnPropertyChanged(propertyName);
        }
    }
}
