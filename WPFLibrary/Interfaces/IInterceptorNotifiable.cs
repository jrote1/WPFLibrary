using System.ComponentModel;

namespace WPFLibrary.Interfaces
{
    public interface IInterceptorNotifiable : INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }
}