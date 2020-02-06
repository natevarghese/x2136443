
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
        }
    }
}
