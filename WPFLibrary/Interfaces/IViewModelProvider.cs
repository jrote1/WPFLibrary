using System;

namespace WPFLibrary.Interfaces
{
    public interface IViewModelProvider<out TViewModel> where TViewModel : MarshalByRefObject, IInterceptorNotifiable, new()
    {
        TViewModel GetViewModel(object data = null);
    }
}
