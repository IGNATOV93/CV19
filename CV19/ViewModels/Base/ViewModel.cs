using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CV19.ViewModels.Base
{
    internal abstract class ViewModel:INotifyPropertyChanged,IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool Set<T>(ref T field,T value, [CallerMemberName] string PropertyName=null)
        {

            if((Equals(field,value))) return false;
            OnPropertyChanged(PropertyName); return true;
        }
       public void Dispose()
    {
        Dispose(true);
    }
    private bool _Disposed;
    protected virtual void Dispose(bool Disposing)
    {
        if (!Disposing || _Disposed) return;
        _Disposed = true;
        //освобождение управляемых ресурсов
    }
    }
}
