using System;
using System.ComponentModel;
using x2136443.ViewModels;

namespace x2136443.Pages
{
    public class BaseListViewPage<TViewModel> : BasePage<TViewModel> where TViewModel : BaseViewModel
    {
        protected override void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ViewModelPropertyChanged(sender, e);

            if (e.PropertyName == "TableItems")
            {
                ReloadUI();
            }
        }

        public virtual void ReloadUI() { }
    }
}