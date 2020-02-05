using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace x2136443.ViewModels
{
    public abstract class BaseListViewViewModel<T> : BaseViewModel where T : new()
    {
        ICommand _RefreshCommand;
        public ICommand RefreshCommand => _RefreshCommand ?? (_RefreshCommand = new Command(async () => await GetTableItems()));

        IEnumerable<T> _TableItems;
        public IEnumerable<T> TableItems
        {
            get => _TableItems;
            set => SetProperty(ref _TableItems, value);
        }

        bool _ListViewEmpty;
        public bool ListViewEmpty
        {
            get => _ListViewEmpty;
            set => SetProperty(ref _ListViewEmpty, value);
        }




        async public override void ViewModelAppearing()
        {
            base.ViewModelAppearing();

            await GetTableItems();
        }

        async Task GetTableItems()
        {
            IsBusy = true;
            var items = await Fetch();
            IsBusy = false;

            TableItems = items;
            ListViewEmpty = !TableItems?.Any() ?? true;
        }

        public abstract Task<IEnumerable<T>> Fetch();
        public abstract void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e);
    }
}
