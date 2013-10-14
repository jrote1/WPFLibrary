using System;
using System.Reflection;

namespace WPFLibrary.Interfaces
{
    public interface IViewModelManager
    {
        TViewModel GetViewModel<TViewModel>(Assembly assembly, object data = null) where TViewModel : MarshalByRefObject, IInterceptorNotifiable, new();
    }
}