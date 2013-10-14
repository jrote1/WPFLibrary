using System;
using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary.Interfaces
{
    public interface IViewModelProvider<out TViewModel> where TViewModel : MarshalByRefObject, IInterceptorNotifiable, new()
    {
        TViewModel GetViewModel(object data = null);
    }
}
