using System;
using System.Reflection;
using WPFLibrary.Interfaces;
using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary
{
    public interface IWPFLibraryCore
    {
        void Initialize(Assembly assembly);
        TViewModel GetViewModel<TViewModel>(object data = null) where TViewModel : MarshalByRefObject, IInterceptorNotifiable, new();
    }
}