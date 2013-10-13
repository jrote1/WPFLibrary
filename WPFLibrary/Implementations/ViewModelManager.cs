using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Castle.DynamicProxy;
using WPFLibrary.Interfaces;
using WPFLibrary.InternalBaseClasses;

[assembly: InternalsVisibleTo("WPFLibraryTests")]
namespace WPFLibrary.Implementations
{
    internal class ViewModelManager : IViewModelManager
    {
        private readonly IServiceLocator _serviceLocator;

        public ViewModelManager()
        {
            _serviceLocator = new ServiceLocator();
        }

        public ViewModelManager(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public TViewModel GetViewModel<TViewModel>(Assembly assembly, object data = null) where TViewModel : ViewModel
        {
            var types = (from type in assembly.GetTypes()
                         where typeof(IViewModelProvider<TViewModel>).IsAssignableFrom(type)
                         select type).ToList();

            if (types.Count != 1)
                throw new NotImplementedException("ViewModel Provider Is Not Implemented Correctly");

            var viewModelProvider = _serviceLocator.GetInstance(types[0]) as IViewModelProvider<TViewModel>;

            var viewModel = viewModelProvider.GetViewModel(data);

            return viewModel;
        }
    }
}
