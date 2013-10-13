using System.Reflection;
using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary.Interfaces
{
    public interface IViewModelManager
    {
        TViewModel GetViewModel<TViewModel>(Assembly assembly, object data = null) where TViewModel : ViewModel;
    }
}