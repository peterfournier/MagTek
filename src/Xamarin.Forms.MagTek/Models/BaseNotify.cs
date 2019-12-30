using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.Forms.MagTek.Models
{
    internal abstract class BaseNotify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal bool SetPropertyChanged<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            return ComponentModelExtensions.SetProperty(PropertyChanged, this, ref currentValue, newValue, propertyName);
        }

        internal void SetPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class ComponentModelExtensions
    {
        //Just adding some new functionality to System.ComponentModel
        public static bool SetProperty<T>(this PropertyChangedEventHandler handler, object sender, ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                return false;

            currentValue = newValue;

            var dirty = sender as Xamarin.Forms.MagTek.Models.IDirty;

            if (dirty != null)
                dirty.IsDirty = true;

            if (handler == null)
                return true;

            handler.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}