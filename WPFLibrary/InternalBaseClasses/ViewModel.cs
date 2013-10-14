using System;
using System.ComponentModel;
using WPFLibrary.Interfaces;

namespace WPFLibrary.InternalBaseClasses
{
    public class ViewModel : MarshalByRefObject, IInterceptorNotifiable
    {
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
