
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using x2136443.DataModels;
using x2136443.Services;
using Xamarin.Forms;

namespace x2136443.ViewModels
{
    public class VideoListViewModel : BaseListViewViewModel<object>
    {
        IList<Section> OriginalData;

        async public override Task<IEnumerable<object>> Fetch()
        {
            OriginalData = await App.SANSService.GetOutline();
            return ProcessItems();
        }

        IEnumerable<object> ProcessItems()
        {
            var items = new List<object>();
            foreach (var section in OriginalData ?? new List<Section>())
            {
                items.Add(section);

                //if (section.IsExpanded)
                //{
                if (section.Videos?.Any() ?? false)
                    items.AddRange(section.Videos);
                //}
            }
            return items;
        }

        async public override void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem;
            if (item is Section section)
            {

                return;
            }

            else if (item is Video video)
            {
                var fileManagerService = ServiceLocator.Instance.Resolve<IFileManager>();
                var videoPlayerService = ServiceLocator.Instance.Resolve<IVideoPlayer>();

                //The result payload should should really have an id but for now
                //create unique identifier from the name property
                var name = video.Name.Replace(' ', '_') + ".mp4";

                //if not downloaded, download it and save it for later
                if (!fileManagerService.FileExists(name))
                {
                    using (App.DialogService.Loading())
                    {
                        var bytes = await App.SANSService.DownloadVideoAtUrl(video.Url);
                        if (bytes?.Any() ?? false)
                            fileManagerService.SaveFile(name, bytes);
                    }
                }

                //ensure exist and play
                if (fileManagerService.FileExists(name))
                {
                    var path = fileManagerService.GetPathForFile(name);
                    var lastPlaybackSeconds = App.PlaybackManager.GetPlaybackLastTimeForVideo(name);

                    videoPlayerService.PlayVideo(path, lastPlaybackSeconds);
                }
                return;
            }
        }
    }
}
