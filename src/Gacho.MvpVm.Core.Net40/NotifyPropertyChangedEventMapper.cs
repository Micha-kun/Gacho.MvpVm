using System;
using System.Collections.Generic;
using System.ComponentModel;
#if NET40
using System.Linq;
#endif
using System.Reflection;
using System.Text;

namespace Gacho.MvpVm.Core
{
    public class NotifyPropertyChangedEventMapper : IDisposable
    {
        private readonly INotifyPropertyChanged observable;

        private readonly object observer;

        private readonly IDictionary<string, ICollection<MethodInfo>> trackedObserverPropertyMethodsDictionary = new Dictionary<string, ICollection<MethodInfo>>();

        public NotifyPropertyChangedEventMapper(INotifyPropertyChanged observable, object observer)
        {
            this.observable = observable;
            this.observer = observer;
            this.observable.PropertyChanged += this.HandlePropertyChanged;
            this.PrepareObserverPropertyHandlers();
        }

        ~NotifyPropertyChangedEventMapper()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

#if NET40
        private void PrepareObserverPropertyHandlers()
        {
            var observerPropertyHandlerMethods = this.observer.GetType().GetMethods().Where(info => info.IsAttributedWith<PropertyChangedMethodAttribute>());
            foreach (var observerPropertyHandlerMethod in observerPropertyHandlerMethods)
            {
                foreach (var propertyChangedMethodAttribute in observerPropertyHandlerMethod.FindAttributesOfType<PropertyChangedMethodAttribute>())
                {
                    if (!this.trackedObserverPropertyMethodsDictionary.ContainsKey(propertyChangedMethodAttribute.PropertyName))
                    {
                        this.trackedObserverPropertyMethodsDictionary.Add(
                            propertyChangedMethodAttribute.PropertyName,
                            new List<MethodInfo> { observerPropertyHandlerMethod });
                    }
                    else
                    {
                        this.trackedObserverPropertyMethodsDictionary[propertyChangedMethodAttribute.PropertyName].Add(observerPropertyHandlerMethod);
                    }
                }
            }
        }
#elif NET20
        private void PrepareObserverPropertyHandlers()
        {
            foreach (var method in this.observer.GetType().GetMethods())
            {
                if (AttributeUsageHelper.IsAttributedWith<PropertyChangedMethodAttribute>(method))
                {
                    foreach (var propertyChangedMethodAttribute in AttributeUsageHelper.FindAttributesOfType<PropertyChangedMethodAttribute>(method))
                    {
                        if (!this.trackedObserverPropertyMethodsDictionary.ContainsKey(propertyChangedMethodAttribute.PropertyName))
                        {
                            this.trackedObserverPropertyMethodsDictionary.Add(
                                propertyChangedMethodAttribute.PropertyName,
                                new List<MethodInfo> { method });
                        }
                        else
                        {
                            this.trackedObserverPropertyMethodsDictionary[propertyChangedMethodAttribute.PropertyName].Add(method);
                        }
                    }
                }
            }
        }
#endif

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.trackedObserverPropertyMethodsDictionary.ContainsKey(e.PropertyName))
            {
                foreach (var methodInfo in this.trackedObserverPropertyMethodsDictionary[e.PropertyName])
                {
                    methodInfo.Invoke(this.observer, new object[0]);
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.observable.PropertyChanged -= this.HandlePropertyChanged;
            }
        }
    }
}
