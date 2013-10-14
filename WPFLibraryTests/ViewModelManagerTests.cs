using System;
using System.Reflection;
using System.Windows.Controls;
using NUnit.Framework;
using Rhino.Mocks;
using WPFLibrary.BaseClasses;
using WPFLibrary.Implementations;
using WPFLibrary.Interfaces;

namespace WPFLibraryTests
{
    [TestFixture]
    public class ViewModelManagerTests
    {
        private IServiceLocator _serviceLocator;
        private ViewModelManager _viewModelManager;

        [SetUp]
        public void SetUp()
        {
            _serviceLocator = MockRepository.GenerateMock<IServiceLocator>();
            _viewModelManager = new ViewModelManager(_serviceLocator);
        }

        [Test]
        [ExpectedException(typeof (NotImplementedException))]
        public void ThrowsExceptionIfViewModelProviderDosentExist()
        {
            _viewModelManager.GetViewModel<TestViewModel2>(Assembly.GetAssembly(typeof(TestViewModel1)));
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void ThrowsExceptionIfThereIsMoreThanOneViewModelProviderForViewModel()
        {
            _viewModelManager.GetViewModel<TestViewModel3>(Assembly.GetAssembly(typeof(TestViewModel1)));
        }

        [Test]
        public void CallsGetInstanceCorrectly()
        {
            _serviceLocator.Stub(x => x.GetInstance(typeof (TestViewModelProvider))).Return(new TestViewModelProvider(_serviceLocator));
            
            _viewModelManager.GetViewModel<TestViewModel1>(Assembly.GetAssembly(typeof(TestViewModel1)));
            
            _serviceLocator.AssertWasCalled(x => x.GetInstance(typeof(TestViewModelProvider)));
        }

        [Test]
        public void CallsViewModelProviderCorrectly()
        {
            const String data = "data";

            _serviceLocator.Stub(x => x.GetInstance(typeof(TestViewModelProvider))).Return(new TestViewModelProvider(_serviceLocator));

            var viewModel = _viewModelManager.GetViewModel<TestViewModel1>(Assembly.GetAssembly(typeof(TestViewModel1)),data);
            
            Assert.AreEqual(data,viewModel.Test);
        }
    }

    public class TestViewModelProvider : IViewModelProvider<TestViewModel1>
    {
        private readonly IServiceLocator _serviceLocator;

        public TestViewModelProvider(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public TestViewModel1 GetViewModel(object data = null)
        {
            return new TestViewModel1(_serviceLocator)
            {
                Test = data as String
            };
        }
    }

    public class TestViewModel1 : ViewModel<TestView1, TestViewModelCommands1>
    {
        public TestViewModel1()
        {

        }

        public TestViewModel1(IServiceLocator serviceLocator)
            : base(serviceLocator)
        {

        }
        public String Test { get; set; }
    }
    public class TestViewModelCommands1 : ViewModelCommands<TestViewModel1> { }
    public class TestView1 : ContentControl { }

    public class TestViewModel2 : ViewModel<TestView2, TestViewModelCommands2> { }
    public class TestViewModelCommands2 : ViewModelCommands<TestViewModel2> { }
    public class TestView2 : ContentControl { }

    public class TestViewModelProvider3 : IViewModelProvider<TestViewModel3>
    {
        public TestViewModel3 GetViewModel(object data = null)
        {
            throw new NotImplementedException();
        }
    }
    public class TestViewModelProvider31 : IViewModelProvider<TestViewModel3>
    {
        public TestViewModel3 GetViewModel(object data = null)
        {
            throw new NotImplementedException();
        }
    }
    public class TestViewModel3 : ViewModel<TestView3, TestViewModelCommands3> { }
    public class TestViewModelCommands3 : ViewModelCommands<TestViewModel3> { }
    public class TestView3 : ContentControl { }
}