using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using WPFLibrary.Interfaces;
using WPFLibrary.Tools;

[assembly: InternalsVisibleTo("WPFLibraryTests")]
namespace WPFLibrary.Implementations
{
    internal class ViewModelManager : IViewModelManager
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IOnPropertyChangedInterceptor _interceptor;

        public ViewModelManager()
        {
            _serviceLocator = new ServiceLocator();
            _interceptor = new OnPropertyChangedInterceptor();
        }

        public ViewModelManager(IServiceLocator serviceLocator, IOnPropertyChangedInterceptor interceptor)
        {
            _serviceLocator = serviceLocator;
            _interceptor = interceptor;
        }

        public TViewModel GetViewModel<TViewModel>(Assembly assembly, object data = null) where TViewModel : MarshalByRefObject, IInterceptorNotifiable, new()
        {
            var types = (from type in assembly.GetTypes()
                         where typeof(IViewModelProvider<TViewModel>).IsAssignableFrom(type)
                         select type).ToList();

            if (types.Count != 1)
                throw new NotImplementedException("ViewModel Provider Is Not Implemented Correctly");

            var viewModelProvider = _serviceLocator.GetInstance(types[0]) as IViewModelProvider<TViewModel>;

            var viewModel = viewModelProvider.GetViewModel(data);

            return _interceptor.Intercept(viewModel);
        }
    }
}
