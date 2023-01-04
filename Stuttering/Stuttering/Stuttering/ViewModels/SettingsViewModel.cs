using Plugin.Multilingual;
using Rg.Plugins.Popup.Services;
using Stuttering.Helper;
using Stuttering.Models;
using Stuttering.Resx;
using Stuttering.SQL;
using Stuttering.Views;
using Stuttering.Views.Auth;
using Stuttering.Views.Popup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Stuttering.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private Metadata metadate;
        private string breathDate;
        private string ratingDate;
        private string evaDate;
        private string breathTime;
        private string ratingTime;
        private string evaTime;
        private bool isLockEnabled;
        private bool isStartup = true;
        private bool isPatternLockEnabled;
        private string selectedLockType;
        private int flexibleChapters;
        private int flexibleLevels;
        private int onsetChapters;
        private int onsetLevels;
        private AppLanguage selectedLanguage;
        private List<string> lockTypesList;
        private List<AppLanguage> languagesList;


        public int FlexibleChapters
        {
            get
            {
                return this.flexibleChapters;
            }
            set
            {
                if (this.flexibleChapters == value)
                {
                    return;
                }
                SetProperty(ref flexibleChapters, value);
            }
        }
        public int FlexibleLevels
        {
            get
            {
                return this.flexibleLevels;
            }
            set
            {
                if (this.flexibleLevels == value)
                {
                    return;
                }
                SetProperty(ref flexibleLevels, value);
            }
        }
        public int OnsetChapters
        {
            get
            {
                return this.onsetChapters;
            }
            set
            {
                if (this.onsetChapters == value)
                {
                    return;
                }
                SetProperty(ref onsetChapters, value);
            }
        }
        public int OnsetLevels
        {
            get
            {
                return this.onsetLevels;
            }
            set
            {
                if (this.onsetLevels == value)
                {
                    return;
                }
                SetProperty(ref onsetLevels, value);
            }
        }
        public string BreathDate
        {
            get
            {
                return this.breathDate;
            }

            set
            {
                if (this.breathDate == value)
                {
                    return;
                }

                SetProperty(ref breathDate, value);
            }
        }
        public string RatingDate
        {
            get
            {
                return this.ratingDate;
            }

            set
            {
                if (this.ratingDate == value)
                {
                    return;
                }

                SetProperty(ref ratingDate, value);
            }
        }
        public string EvaDate
        {
            get
            {
                return this.evaDate;
            }

            set
            {
                if (this.evaDate == value)
                {
                    return;
                }

                SetProperty(ref evaDate, value);
            }
        }
        public string BreathTime
        {
            get
            {
                return this.breathTime;
            }

            set
            {
                if (this.breathTime == value)
                {
                    return;
                }

                SetProperty(ref breathTime, value);
            }
        }
        public string RatingTime
        {
            get
            {
                return this.ratingTime;
            }

            set
            {
                if (this.ratingTime == value)
                {
                    return;
                }

                SetProperty(ref ratingTime, value);
            }
        }
        public string EvaTime
        {
            get
            {
                return this.evaTime;
            }

            set
            {
                if (this.evaTime == value)
                {
                    return;
                }

                SetProperty(ref evaTime, value);
            }
        }
        public bool IsLockEnabled
        {
            get
            {
                return this.isLockEnabled;
            }

            set
            {
                if (this.isLockEnabled == value)
                {
                    return;
                }

                SetProperty(ref isLockEnabled, value);
            }
        }
        public bool IsPatternLockEnabled
        {
            get
            {
                return this.isPatternLockEnabled;
            }

            set
            {
                if (this.isPatternLockEnabled == value)
                {
                    return;
                }

                SetProperty(ref isPatternLockEnabled, value);
            }
        }
        public string SelectedLockType
        {
            get
            {
                return this.selectedLockType;
            }

            set
            {
                if (this.selectedLockType == value)
                {
                    return;
                }
                SetProperty(ref selectedLockType, value);
                ChangeLockType();
            }
        }
        public AppLanguage SelectedLanguage
        {
            get
            {
                return this.selectedLanguage;
            }

            set
            {
                if (this.selectedLanguage == value)
                {
                    return;
                }
                SetProperty(ref selectedLanguage, value);
                if (!isStartup)
                    ChangeLanguage();
                else
                    isStartup = false;
            }
        }
        public List<string> LockTypesList
        {
            get
            {
                return this.lockTypesList;
            }

            set
            {
                if (this.lockTypesList == value)
                {
                    return;
                }

                SetProperty(ref lockTypesList, value);
            }
        }
        public List<AppLanguage> LanguagesList
        {
            get
            {
                return this.languagesList;
            }

            set
            {
                if (this.languagesList == value)
                {
                    return;
                }

                SetProperty(ref languagesList, value);
            }
        }
        public Command SwitchCommand { get; }
        public Command ChangePatternCommand { get; }
        public Command ChangeLanguageCommand { get; }
        public Command ChangePinCommand { get; }
        public Command OnAppearingCommand { get; }
        public Command RaterStatsCommand { get; }
        public Command EvaStatsCommand { get; }
        public Command RatingStatsCommand { get; }
        INavigation navigation;
        public SettingsViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            var list = new List<string>();
            var enums = Enum.GetValues(typeof(LockType)).Cast<LockType>();
            foreach (var item in enums)
            {
                list.Add(item.ToString());
            }
            LockTypesList = list;
            LanguagesList = new List<AppLanguage>()
            {
                new AppLanguage(){ DisplayName = StutteringResources.english, ShortName = "en"},
                new AppLanguage(){ DisplayName = StutteringResources.urdu, ShortName = "ur"}
            };
            SelectedLanguage = LanguagesList.Where(l => l.ShortName == GlobalSettings.CurrentUser.Language).FirstOrDefault();
            SwitchCommand = new Command(OnSwitchClicked);
            ChangePatternCommand = new Command(ChangePatternClicked);
            ChangeLanguageCommand = new Command(ChangeLanguage);
            ChangePinCommand = new Command(ChangePinClicked);
            RaterStatsCommand = new Command(GoToRaterStats);
            EvaStatsCommand = new Command(GoToSelfEvaStats);
            RatingStatsCommand = new Command(GoToSelfRatingStats);
            OnAppearingCommand = new Command(OnAppearing);
        }

        private async void OnSwitchClicked(object obj)
        {
            try
            {
                if (isLockEnabled)
                {
                    await navigation.PushAsync(new PinView(LockCheckType.DisableLock));
                    //await Shell.Current.GoToAsync($"{nameof(PatternView)}?{nameof(PatternViewModel.checkType)}={PatternCheckType.DisablePattern}");
                }
                else
                {
                    await navigation.PushAsync(new PinView(LockCheckType.EnableLock));
                    //await Shell.Current.GoToAsync($"{nameof(PatternView)}?{nameof(PatternViewModel.checkType)}={PatternCheckType.EnablePattern}");
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void ChangeLockType()
        {
            IsBusy = true;
            await Task.Delay(10);
            if (SelectedLockType == LockType.Pattern.ToString() && string.IsNullOrEmpty(GlobalSettings.CurrentUser.Pattern))
            {
                await navigation.PushAsync(new PatternView(LockCheckType.EnableLock));
                if (string.IsNullOrEmpty(GlobalSettings.CurrentUser.Pattern))
                {
                    SelectedLockType = LockType.Pin.ToString();
                }
            }
            else
            {
                GlobalSettings.CurrentUser.LockType = SelectedLockType;
                SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
            }
            if (IsLockEnabled && SelectedLockType == LockType.Pattern.ToString())
                IsPatternLockEnabled = true;
            else
                IsPatternLockEnabled = false;
            IsBusy = false;
        }
        private async void ChangeLanguage()
        {
            IsBusy = true;
            await Task.Delay(10);
            GlobalSettings.CurrentUser.Language = SelectedLanguage.ShortName;
            SQLiteDbManager.SaveUser(GlobalSettings.CurrentUser);
            var culture = new CultureInfo(GlobalSettings.CurrentUser.Language);
            LocalizationResourceManager.Instance.SetCulture(culture);
            //StutteringResources.Culture = culture;
            //CrossMultilingual.Current.CurrentCultureInfo = culture;

            (Application.Current as App).MainPage = new NavigationPage(new Views.MasterDetailPage());

            IsBusy = false;
        }
        private async void ChangePatternClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await navigation.PushAsync(new PatternView(LockCheckType.ChangeLock));
        }
        private async void ChangePinClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await navigation.PushAsync(new PinView(LockCheckType.ChangeLock));
        }
        private async void GoToRaterStats()
        {
            IsBusy = true;
            await Task.Delay(10);
            await PopupNavigation.Instance.PushAsync(new RaterStatsPopup());
            IsBusy = false;
        }
        private async void GoToSelfEvaStats()
        {
            IsBusy = true;
            await Task.Delay(10);
            await PopupNavigation.Instance.PushAsync(new SelfEvaStatsPopup());
            IsBusy = false;
        }
        private async void GoToSelfRatingStats()
        {
            IsBusy = true;
            await Task.Delay(10);
            await PopupNavigation.Instance.PushAsync(new SelfRatingStatsPopup());
            IsBusy = false;
        }
        private async void OnAppearing(object obj)
        {
            IsBusy = true;
            await Task.Delay(10);
            metadate = SQLiteDbManager.GetMetadata();

            var flChapters = SQLiteDbManager.GetModuleChapters(ModuleType.Flexible);
            var onChapters = SQLiteDbManager.GetModuleChapters(ModuleType.Onset);

            FlexibleChapters = flChapters.Where(ch => ch.IsLocked == false).Count();
            var flCh = flChapters.Where(ch => ch.IsLocked == false).LastOrDefault();
            var flLevels = SQLiteDbManager.GetChapterExercises(flCh.Id);
            FlexibleLevels = flLevels.Where(le => le.IsLocked == false).Count();

            OnsetChapters = onChapters.Where(ch => ch.IsLocked == false).Count();
            var onCh = onChapters.Where(ch => ch.IsLocked == false).LastOrDefault();
            var onLevels = SQLiteDbManager.GetChapterExercises(onCh.Id);
            OnsetLevels = onLevels.Where(le => le.IsLocked == false).Count();

            if (metadate != null)
            {
                if (metadate.BreathDateTime.Year != 1)
                {
                    BreathDate = metadate.BreathDateTime.ToString("dd/MM/yyyy");
                    BreathTime = metadate.BreathDateTime.ToString("HH:mm");
                }
                if (metadate.SelfRateDateTime.Year != 1)
                {
                    RatingDate = metadate.SelfRateDateTime.ToString("dd/MM/yyyy");
                    RatingTime = metadate.SelfRateDateTime.ToString("HH:mm");
                }
                if (metadate.SelfEvalDateTime.Year != 1)
                {
                    EvaDate = metadate.SelfEvalDateTime.ToString("dd/MM/yyyy");
                    EvaTime = metadate.SelfEvalDateTime.ToString("HH:mm");
                }
            }
            IsLockEnabled = GlobalSettings.CurrentUser.IsLockEnabled;
            SelectedLockType = GlobalSettings.CurrentUser.LockType;
            if (IsLockEnabled && SelectedLockType == LockType.Pattern.ToString())
                IsPatternLockEnabled = true;
            else
                IsPatternLockEnabled = false;
            IsBusy = false;
        }
    }
}
