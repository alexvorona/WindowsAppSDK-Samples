using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhotoEditor.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // Event to notify property changes
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies when a property changes.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets a property and raises the PropertyChanged event if the value has changed.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to the backing field.</param>
        /// <param name="value">New value for the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if the value was changed, otherwise false.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
