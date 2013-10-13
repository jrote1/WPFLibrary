using System;

namespace WPFLibrary.Interfaces
{
    public interface IServiceLocator
    {
        T GetInstance<T>();
        Object GetInstance(Type type);
    }
}