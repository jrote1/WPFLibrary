using System.ComponentModel;

namespace WPFLibrary.InternalBaseClasses
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected internal void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
