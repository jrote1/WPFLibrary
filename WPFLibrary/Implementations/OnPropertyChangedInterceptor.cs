using System;
using WPFLibrary.Interfaces;

namespace WPFLibrary.Tools
{
    public class OnPropertyChangedInterceptor : IOnPropertyChangedInterceptor
    {
        public T Intercept<T>(T instance) where T : MarshalByRefObject, IInterceptorNotifiable, new()
        {
            var interceptor = new OnPropertyChangedInterceptorProxy<T>(instance);
            return (T)interceptor.GetTransparentProxy();
        }
    }
}
