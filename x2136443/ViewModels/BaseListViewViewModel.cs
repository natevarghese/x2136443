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

        ICommand _ItemTappedCommand;
        public ICommand ItemTappedCommand => _ItemTappedCommand ?? (_ItemTappedCommand = new Command(ListView_ItemTapped));


        IEnumerable<T> _TableItems;
        public IEnumerable<T> TableItems
        {
            get => _TableItems;
            set
            {
                SetProperty(ref _TableItems, value);
                ListViewEmpty = !value?.Any() ?? true;
            }
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

            var anyData = TableItems?.Any() ?? false;
            if (!anyData)
                await GetTableItems();
        }

        async Task GetTableItems()
        {
            IsBusy = true;

            TableItems = await Fetch();

            IsBusy = false;
        }

        public abstract Task<IEnumerable<T>> Fetch();
        public abstract void ListView_ItemTapped(object sender);
    }
}
