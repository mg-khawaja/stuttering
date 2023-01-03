﻿using Stuttering.Helper;
using Stuttering.SQL;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Stuttering.ViewModels
{
    public class PatternViewModel : BaseViewModel
    {
        private string patternLabel;
        private string newPattern;
        private LockStatus status;
        public string PatternLabel
        {
            get
            {
                return this.patternLabel;
            }

            set
            {
                if (this.patternLabel == value)
                {
                    return;
                }

                SetProperty(ref patternLabel, value);
            }
        }
        public LockCheckType checkType { get; set; }
        public Command OnAppearingCommand { get; }
        INavigation navigation;

        public PatternViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            OnAppearingCommand = new Command(OnAppearing);
        }

        public async Task OnPatternCompleted(string pattern)
        {
            IsBusy = true;
            await Task.Delay(10);
            switch (checkType)
            {
                case LockCheckType.EnableLock:
                    switch (status)
                    {
                        case LockStatus.EnterCurrent:
                            if (GlobalSettings.CurrentUser.Pattern == pattern)
                            {
                                GlobalSettings.CurrentUser.IsLockEnabled = true;
                                GlobalSettings.CurrentUser.LockType = LockType.Pattern.ToString();
                                SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
                                await navigation.PopAsync();
                            }
                            else
                            {
                                await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pattern is incorrect!", "OK");
                            }
                            break;
                        case LockStatus.EnterNew:
                            PatternLabel = "Draw again";
                            newPattern = pattern;
                            status = LockStatus.EnterAgain;
                            break;
                        case LockStatus.EnterAgain:
                            if (newPattern == pattern)
                            {
                                GlobalSettings.CurrentUser.Pattern = pattern;
                                GlobalSettings.CurrentUser.IsLockEnabled = true;
                                GlobalSettings.CurrentUser.LockType = LockType.Pattern.ToString();
                                SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
                                await navigation.PopAsync();
                            }
                            else
                            {
                                await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pattern is incorrect!", "OK");
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                case LockCheckType.DisableLock:
                    if (GlobalSettings.CurrentUser.Pattern == pattern)
                    {
                        GlobalSettings.CurrentUser.IsLockEnabled = false;
                        SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
                        await navigation.PopAsync();
                    }
                    else
                    {
                        await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pattern is incorrect!", "OK");
                    }
                    break;
                case LockCheckType.ChangeLock:
                    switch (status)
                    {
                        case LockStatus.EnterCurrent:
                            if (GlobalSettings.CurrentUser.Pattern == pattern)
                            {
                                status = LockStatus.EnterNew;
                                PatternLabel = "Draw new Pattern";
                            }
                            else
                            {
                                await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pattern is incorrect!", "OK");
                            }
                            break;
                        case LockStatus.EnterNew:
                            newPattern = pattern;
                            PatternLabel = "Draw again";
                            status = LockStatus.EnterAgain;
                            break;
                        case LockStatus.EnterAgain:
                            if (newPattern == pattern)
                            {
                                GlobalSettings.CurrentUser.Pattern = pattern;
                                GlobalSettings.CurrentUser.IsLockEnabled = true;
                                SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
                                await navigation.PopAsync();
                            }
                            else
                            {
                                await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pattern is incorrect!", "OK");
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case LockCheckType.Login:
                    if (GlobalSettings.CurrentUser.Pattern == pattern)
                    {
                        (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
                    }
                    else
                    {
                        await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pattern is incorrect!", "OK");
                    }
                    break;
                default:
                    break;
            }
            IsBusy = false;
        }
        private async void OnAppearing(object obj)
        {
            IsBusy = true;
            await Task.Delay(10);
            switch (checkType)
            {
                case LockCheckType.EnableLock:
                    if (string.IsNullOrEmpty(GlobalSettings.CurrentUser.Pattern))
                    {
                        PatternLabel = "Draw new Pattern";
                        status = LockStatus.EnterNew;
                    }
                    else
                    {
                        PatternLabel = "Draw Pattern to enable";
                        status = LockStatus.EnterCurrent;
                    }
                    break;
                case LockCheckType.DisableLock:
                    PatternLabel = "Draw Pattern to disable";
                    status = LockStatus.EnterCurrent;
                    break;
                case LockCheckType.ChangeLock:
                    PatternLabel = "Draw Current Pattern";
                    status = LockStatus.EnterCurrent;
                    break;
                case LockCheckType.Login:
                    PatternLabel = "Draw Pattern";
                    var user = SQLiteDbManager.GetUser();
                    if (user != null)
                    {
                        GlobalSettings.CurrentUser = user;
                        if (!user.IsLockEnabled)
                            (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
                    }
                    else
                    {
                        user = new Models.UserModel()
                        {
                            IsLockEnabled = false,
                            Pattern = "",
                            Language = "en"
                        };
                        SQLiteDbManager.SaveUser(user);
                        GlobalSettings.CurrentUser = SQLiteDbManager.GetUser();
                        (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
                    }
                    break;
                default:
                    break;
            }
            IsBusy = false;
        }
    }
}
