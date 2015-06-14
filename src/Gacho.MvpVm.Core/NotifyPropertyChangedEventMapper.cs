using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
