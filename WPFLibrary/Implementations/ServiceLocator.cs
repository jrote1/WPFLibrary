using System;
using WPFLibrary.Interfaces;

namespace WPFLibrary.Implementations
{
    public class ServiceLocator : IServiceLocator
    {
        public T GetInstance<T>()
        {
            return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<T>();
        }

        public Object GetInstance(Type type)
        {
            return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance(type);
        }
    }
}
