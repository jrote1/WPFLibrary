using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using WPFLibrary.InternalBaseClasses;

namespace WPFLibrary.Tools
{
    public abstract class ViewModelCommandsTest<TViewModel, TViewModelCommands>
        where TViewModel : ViewModel
        where TViewModelCommands : ViewModelCommands
    {
        protected TViewModel ViewModel { get; private set; }
        protected TViewModelCommands ViewModelCommands { get; private set; }
        protected Dictionary<Type, Object> Parameters { get; private set; }

        protected ViewModelCommandsTest()
        {
            ViewModel = Activator.CreateInstance<TViewModel>();

            Parameters = new Dictionary<Type, object>();

            var constructor = (from constructorInfo in typeof(TViewModelCommands).GetConstructors()
                               where constructorInfo.GetParameters().All(parameter => parameter.ParameterType.IsInterface)
                               orderby constructorInfo.GetParameters().Count() descending
                               select constructorInfo).First();


            foreach (var parameterInfo in constructor.GetParameters())
                Parameters.Add(parameterInfo.ParameterType, MockRepository.GenerateMock(parameterInfo.ParameterType, new Type[0]));


            if (Parameters.Count == 0)
                ViewModelCommands = Activator.CreateInstance<TViewModelCommands>();
            else
                ViewModelCommands = (TViewModelCommands)Activator.CreateInstance(typeof(TViewModelCommands), Parameters.Select(x => x.Value).ToArray());

            typeof(TViewModelCommands).BaseType.GetProperty("ViewModel", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ViewModelCommands, ViewModel);

        }
    }
}
