using System;
using WPFLibrary.Interfaces;

namespace WPFLibrary.Tools
{
    public interface IOnPropertyChangedInterceptor
    {
        T Intercept<T>(T instance) where T : MarshalByRefObject, IInterceptorNotifiable, new();
    }
}