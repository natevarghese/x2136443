using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace x2136443.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value);
        }
        public INavigation NavService { get; set; }
        public bool IsVisible { get; set; }

        protected BaseViewModel() { }


        public virtual void ViewModelAppearing() { IsVisible = true; }
        public virtual void ViewModelDisappearing() { IsVisible = false; }

        protected virtual void SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "")
        {
            backingStore = value;
            RaisePropertyChanged(propertyName);
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

#if DEBUG
        ~BaseViewModel()
        {
            System.Diagnostics.Debug.WriteLine("Finalizer called: " + this.GetType().Name);
        }
#endif
    }
}
