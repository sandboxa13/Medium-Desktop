using DryIocAttributes;
using Medium.Core.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Services.Interfaces.Interfaces;

namespace Medium.Core.ViewModels.User
{
    [Reuse(ReuseType.Singleton)]
    [ExportEx(typeof(UserContentViewModel))]
    public class UserContentViewModel : BaseViewModel
    {
        private readonly IMediumApiService _mediumApiService;
        private readonly INavigationService _navigationService;

        public UserContentViewModel(
            IMediumApiService mediumApiService,
            INavigationService navigationService)   
        {
            _mediumApiService = mediumApiService;
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
