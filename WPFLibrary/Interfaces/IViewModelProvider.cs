using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary.Interfaces
{
    public interface IViewModelProvider<out TViewModel> where TViewModel : ViewModel
    {
        TViewModel GetViewModel(object data = null);
    }
}
