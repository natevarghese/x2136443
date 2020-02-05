using System;
using System.Collections.Generic;
using System.Linq;
using x2136443.ViewModels;
using Xamarin.Forms;

namespace x2136443.Pages
{
    public partial class VideoListPage : BaseListViewPage<VideoListViewModel>
    {
        public VideoListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ApplySafeAreaGridConstraints(ParentGrid);

            MyListView.ItemSelected += ViewModel.ListView_ItemSelected;

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MyListView.ItemSelected -= ViewModel.ListView_ItemSelected;

        }
    }
}
