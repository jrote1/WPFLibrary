using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary.BaseClasses
{
    public abstract class ViewModelCommands<TViewModel> : ViewModelCommands
        where TViewModel : ViewModel
    {
        protected TViewModel ViewModel { get; set; }
    }
}
