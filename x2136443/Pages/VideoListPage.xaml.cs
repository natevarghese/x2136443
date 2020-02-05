
using x2136443.ViewModels;

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

            EmptyLabel.Text = x2136443.Resources.AppStrings.NoData;

            MyListView.ItemTapped += ViewModel.ListView_ItemTapped;

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MyListView.ItemTapped -= ViewModel.ListView_ItemTapped;

        }
    }
}
