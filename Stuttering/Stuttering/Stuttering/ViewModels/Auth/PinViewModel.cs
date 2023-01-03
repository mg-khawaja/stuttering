using Plugin.Multilingual;
using Stuttering.Helper;
using Stuttering.Resx;
using Stuttering.SQL;
using Stuttering.Views.Auth;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Stuttering.ViewModels
{
    public class PinViewModel : BaseViewModel
    {
        private string pinLabel;
        private bool isSecLabelVisible;
        private string newPin;
        private LockStatus status;
        public string PinLabel
        {
            get
            {
                return this.pinLabel;
            }

            set
            {
                if (this.pinLabel == value)
                {
                    return;
                }

                SetProperty(ref pinLabel, value);
            }
        }
        public bool IsSecLabelVisible
        {
            get
            {
                return this.isSecLabelVisible;
            }

            set
            {
                if (this.isSecLabelVisible == value)
                {
                    return;
                }

                SetProperty(ref isSecLabelVisible, value);
            }
        }
        public LockCheckType checkType { get; set; }
        public Command OnAppearingCommand { get; }
        INavigation navigation;
        public PinViewModel(INavigation _navigation)
        {
            IsSecLabelVisible = true;
            navigation = _navigation;
            OnAppearingCommand = new Command(OnAppearing);
        }
        public async Task OnPinCompleted(string pin)
        {
            IsBusy = true;
            await Task.Delay(10);
            switch (checkType)
            {
                case LockCheckType.EnableLock:
                    switch (status)
                    {
                        case LockStatus.EnterCurrent:
                            if (GlobalSettings.CurrentUser.Pin == pin)
                            {
                                GlobalSettings.CurrentUser.IsLockEnabled = true;
                                GlobalSettings.CurrentUser.LockType = LockType.Pin.ToString();
                                SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
                                await navigation.PopAsync();
                            }
                            else
                            {
                                await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pin is incorrect!", "OK");
                            }
                            break;
                        case LockStatus.EnterNew:
                            PinLabel = "Enter again";
                            newPin = pin;
                            status = LockStatus.EnterAgain;
                            break;
                        case LockStatus.EnterAgain:
                            if (newPin == pin)
                            {
                                GlobalSettings.CurrentUser.Pin = pin;
                                GlobalSettings.CurrentUser.IsLockEnabled = true;
                                GlobalSettings.CurrentUser.LockType = LockType.Pin.ToString();
                                SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
                                await navigation.PopAsync();
                            }
                            else
                            {
                                await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pin is incorrect!", "OK");
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                case LockCheckType.DisableLock:
                    if (GlobalSettings.CurrentUser.Pin == pin)
                    {
                        GlobalSettings.CurrentUser.IsLockEnabled = false;
                        GlobalSettings.CurrentUser.LockType = LockType.Pin.ToString();
                        SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
                        await navigation.PopAsync();
                    }
                    else
                    {
                        await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pin is incorrect!", "OK");
                    }
                    break;
                case LockCheckType.ChangeLock:
                    switch (status)
                    {
                        case LockStatus.EnterCurrent:
                            if (string.IsNullOrEmpty(GlobalSettings.CurrentUser.Pin) || GlobalSettings.CurrentUser.Pin == pin)
                            {
                                status = LockStatus.EnterNew;
                                PinLabel = "Enter new Pin";
                                IsSecLabelVisible = true;
                            }
                            else
                            {
                                await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pin is incorrect!", "OK");
                            }
                            break;
                        case LockStatus.EnterNew:
                            newPin = pin;
                            PinLabel = "Enter again";
                            status = LockStatus.EnterAgain;
                            break;
                        case LockStatus.EnterAgain:
                            if (newPin == pin)
                            {
                                GlobalSettings.CurrentUser.Pin = pin;
                                GlobalSettings.CurrentUser.IsLockEnabled = true;
                                GlobalSettings.CurrentUser.LockType = LockType.Pin.ToString();
                                SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
                                await navigation.PopAsync();
                            }
                            else
                            {
                                await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pin is incorrect!", "OK");
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case LockCheckType.Login:
                    if (GlobalSettings.CurrentUser.Pin == pin)
                    {
                        (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
                    }
                    else
                    {
                        await (Application.Current as App).MainPage.DisplayAlert("Mismatch", "Pin is incorrect!", "OK");
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
                    if (string.IsNullOrEmpty(GlobalSettings.CurrentUser.Pin))
                    {
                        PinLabel = "Enter new Pin";
                        status = LockStatus.EnterNew;
                    }
                    else
                    {
                        PinLabel = "Enter Pin to enable";
                        status = LockStatus.EnterCurrent;
                        IsSecLabelVisible = false;
                    }
                    break;
                case LockCheckType.DisableLock:
                    PinLabel = "Enter Pin to disable";
                    status = LockStatus.EnterCurrent;
                    IsSecLabelVisible = false;
                    break;
                case LockCheckType.ChangeLock:
                    PinLabel = "Enter Current Pin";
                    status = LockStatus.EnterCurrent;
                    IsSecLabelVisible = false;
                    break;
                case LockCheckType.Login:
                    IsSecLabelVisible = false;
                    PinLabel = "Enter Pin";
                    var user = SQLiteDbManager.GetUser();
                    if (user != null)
                    {
                        GlobalSettings.CurrentUser = user;
                        //var culture = new CultureInfo(user.Language);
                        //Thread.CurrentThread.CurrentUICulture = culture;
                        //StutteringResources.Culture = culture;
                        //CrossMultilingual.Current.CurrentCultureInfo = culture;

                        //var enCulture = new CultureInfo(lang.ShortName);
                        //TamweelyResources.Culture = enCulture;
                        //CrossMultilingual.Current.CurrentCultureInfo = enCulture;

                        var culture = new CultureInfo(GlobalSettings.CurrentUser.Language);
                        LocalizationResourceManager.Instance.SetCulture(culture);

                        //CultureInfo.DefaultThreadCurrentCulture = culture;
                        //StutteringResources.Culture = culture;
                        //CrossMultilingual.Current.CurrentCultureInfo = culture;

                        if (!user.IsLockEnabled)
                            (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());
                        else if(user.LockType == LockType.Pattern.ToString())
                        {
                            (Application.Current as App).MainPage = new PatternView(Helper.LockCheckType.Login);
                        }
                    }
                    else
                    {
                        user = new Models.UserModel()
                        {
                            IsLockEnabled = false,
                            Pin = "",
                            Pattern = "",
                            LockType = LockType.Pin.ToString(),
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
