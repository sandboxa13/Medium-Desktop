using DryIocAttributes;
using MediumDesktop.Core.MediumAPI;
using MediumDesktop.Core.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MediumDesktop.Core.ViewModels.User
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(UserContentViewModel))]
    public class UserContentViewModel : BaseViewModel
    {
        private readonly IApiController _apiController;
        private readonly INavigationService _navigationService;

        public UserContentViewModel(
            IApiController apiController,
            INavigationService navigationService)
        {
            _apiController = apiController;
            _navigationService = navigationService;

            ProfileCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await navigationService.NavigateAsync<UserProfileViewModel>();
            });

            ShowContentCommand = ReactiveCommand.Create(Show);
        }

        [Reactive] public bool IsVisible { get; set; }

        public ReactiveCommand BecomeMemberCommand { get; private set; }
        public ReactiveCommand NewStoryCommand { get; private set; }
        public ReactiveCommand StoriesCommand { get; private set; }
        public ReactiveCommand SeriesCommand { get; private set; }
        public ReactiveCommand MediumPartnerProgramCommand { get; private set; }
        public ReactiveCommand BookMarksCommand { get; private set; }
        public ReactiveCommand PublicationsCommand { get; private set; }
        public ReactiveCommand ProfileCommand { get; }
        public ReactiveCommand SettingsCommand { get; private set; }
        public ReactiveCommand HelpCommand { get; private set; }
        public ReactiveCommand SignOutCommand { get; private set; }
        public ReactiveCommand ShowContentCommand { get; }

        public void Show() => IsVisible = !IsVisible;
    }
}
