using System;
using System.Reflection;
using WPFLibrary.Implementations;
using WPFLibrary.Interfaces;
using WPFLibrary.InternalBaseClasses;
using WPFLibrary.Tools;

namespace WPFLibrary
{
    public class WPFLibraryCore : IWPFLibraryCore
    {
        private readonly IViewModelManager _viewModelManager;
        private static Assembly _assembly;
        private static DataTemplateMapper _dataTemplateMapper;


        public WPFLibraryCore()
        {
            _viewModelManager = new ViewModelManager();
        }

        internal WPFLibraryCore(IViewModelManager viewModelManager)
        {
            _viewModelManager = viewModelManager;
        }

        /// <summary>
        /// Call Initialize After Setting Service Locator To Setup WPFLibraryCore
        /// </summary>
        public void Initialize(Assembly assembly)
        {
            if (Microsoft.Practices.ServiceLocation.ServiceLocator.Current == null)
                throw new NotImplementedException("You must set service locator before initilizing");

            _dataTemplateMapper = new DataTemplateMapper();
            _dataTemplateMapper.MapViewModels(assembly);

            _assembly = assembly;
        }

        public TViewModel GetViewModel<TViewModel>(object data = null) where TViewModel : ViewModel
        {
           return  _viewModelManager.GetViewModel<TViewModel>(_assembly, data);
        }
    }
}
