using System;
using Ninject;
using Ninject.Web.Common;
using WebActivator;

[assembly: ApplicationShutdownMethod(typeof ($rootnamespace$.App_Start.Ninject), "Stop")]

namespace $rootnamespace$.App_Start
{
    public static class Ninject
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static IKernel Kernel
        {
            get
            {
                if (Bootstrapper.Kernel == null)
                    Bootstrapper.Initialize(CreateKernel);
                return Bootstrapper.Kernel;
            }
        }


        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }


        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        private static void RegisterServices(IKernel kernel)
        {
            //Example
            // kernel.Bind<ITest>().To<Test>().InRequestScope();
        }
    }
}
