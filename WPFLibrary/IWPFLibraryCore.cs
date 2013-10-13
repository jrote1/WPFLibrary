using System.Reflection;
using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary
{
    public interface IWPFLibraryCore
    {
        void Initialize(Assembly assembly);
        TViewModel GetViewModel<TViewModel>(object data = null) where TViewModel : ViewModel;
    }
}