using System.ComponentModel;
using Xamarin.Forms;
using x2136443.ViewModels;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace x2136443.Pages
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");


        }

        public virtual void IsDismissingModally() { }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PassDataToViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }


        public virtual bool CanGoBack() { IsDismissingModally(); return true; }
        public virtual void Popped() { }

        protected void ApplySafeAreaGridConstraints(Grid grid)
        {
            grid.Padding = GetSafeAreaInsets();
        }

        //android defaults to zeros.
        protected Thickness GetSafeAreaInsets()
        {
            return On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
        }

        protected virtual void PassDataToViewModel() { }

#if DEBUG
        ~BasePage()
        {
            System.Diagnostics.Debug.WriteLine("Finalizer called: " + GetType().Name);
        }
#endif
    }

    public class BasePage<TViewModel> : BasePage where TViewModel : BaseViewModel
    {
        public TViewModel ViewModel => BindingContext as TViewModel;

        public BasePage() { }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel != null)
            {
                ViewModel.NavService = Navigation;
                ViewModel.PropertyChanged += ViewModelPropertyChanged;
            }

            ViewModel?.ViewModelAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= ViewModelPropertyChanged;
            }

            ViewModel?.ViewModelDisappearing();
        }

        protected virtual void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}