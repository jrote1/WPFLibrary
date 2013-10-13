using Microsoft.Practices.ServiceLocation;
using Ninject;
using NinjectAdapter;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof($rootnamespace$.App_Start.BootstrapperServiceLocator), "Start")]

namespace $rootnamespace$.App_Start
{
 public static class BootstrapperServiceLocator    
    {
        /// <summary>
        ///  Starts the application
        /// </summary>
        public static void Start()
        {
             var locator = new NinjectServiceLocator(Ninject.Kernel);
             ServiceLocator.SetLocatorProvider(() => locator);
        }
         
    }
}