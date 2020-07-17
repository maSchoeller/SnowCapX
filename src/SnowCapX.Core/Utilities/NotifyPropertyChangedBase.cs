using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SnowCapX.Core.Utilities
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void SetProperty<T>(ref T storage, T value, [CallerMemberName]string? name = null)
        {
            if (!(storage is null) && !storage.Equals(value))
            {
                storage = value;
                RaisePropertyChanged(name);
            }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string? name = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
