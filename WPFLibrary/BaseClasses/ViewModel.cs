using System.Reflection;
using System.Windows.Controls;
using WPFLibrary.Implementations;
using WPFLibrary.Interfaces;
using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary.BaseClasses
{
    public abstract class ViewModel<TView, TViewModelCommands> : ViewModel
        where TView : ContentControl
        where TViewModelCommands : ViewModelCommands
    {
       public TViewModelCommands Commands { get; private set; }

        protected ViewModel()
            : this(new ServiceLocator()) { }

        internal ViewModel(IServiceLocator serviceLocator)
        {
            Commands = serviceLocator.GetInstance<TViewModelCommands>();
            typeof(TViewModelCommands).GetProperty("ViewModel",BindingFlags.Instance | BindingFlags.NonPublic).SetValue(Commands, this);
        }
    }
}
