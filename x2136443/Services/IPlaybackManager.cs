using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using x2136443.DataModels;

namespace x2136443.Services
{
    public interface IPlaybackManager
    {
        Task Start();
        Task Save();
        Task Clear();
        void UploadTimeForVideo(string name, int seconds);
        int GetPlaybackLastTimeForVideo(string name);
    }

    public class PlaybackManager : IPlaybackManager
    {
        static List<DownloadedVideo> Videos;
        static string Key = AppConstants.PersistantStorageKeyVideoPlaybackLog;

        async public Task Clear()
        {
            Videos?.Clear();
            await Save();
        }


        async public Task Save()
        {
            await App.PersistantStorageService.Set(Key, Videos);
        }

        async public Task Start()
        {
            if (!Videos?.Any() ?? true)
                Videos = await App.PersistantStorageService.Get<List<DownloadedVideo>>(Key) ?? new List<DownloadedVideo>();
        }

        public void UploadTimeForVideo(string name, int seconds)
        {
            var target = Videos.FirstOrDefault(c => c.Name == name);
            if (target == null)
            {
                target = new DownloadedVideo { Name = name, CurrentTime = seconds };
                Videos.Add(target);
            }
            else
            {
                target.CurrentTime = seconds;
            }
        }

        public int GetPlaybackLastTimeForVideo(string name)
        {
            return Videos?.FirstOrDefault(c => c.Name == name)?.CurrentTime ?? 0;
        }

    }
}
