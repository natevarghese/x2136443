using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace x2136443.Services
{
    public interface IDialogService
    {
        IProgressDialog Loading();
        Task<string> ActionSheetAsync(string title, string cancel, string destructive, CancellationToken cancelToken, string[] buttons);
    }

    public class DialogService : IDialogService
    {
        public IProgressDialog Loading()
        {
            return UserDialogs.Instance.Loading(x2136443.Resources.AppStrings.Loading, null, null, true, MaskType.Gradient);
        }
        async public Task<string> ActionSheetAsync(string title, string cancel, string destructive, CancellationToken cancelToken, string[] buttons)
        {
            return await UserDialogs.Instance.ActionSheetAsync(title, cancel, destructive, cancelToken, buttons);
        }
    }
}
