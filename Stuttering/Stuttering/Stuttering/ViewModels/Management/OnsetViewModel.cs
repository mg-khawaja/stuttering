using Plugin.Multilingual;
using Stuttering.Helper;
using Stuttering.Models;
using Stuttering.Resx;
using Stuttering.SQL;
using Stuttering.Views.Auth;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Stuttering.ViewModels
{
    public class OnsetViewModel : BaseViewModel
    {
        private List<Chapter> chapters;
        public List<Chapter> Chapters
        {
            get
            {
                return this.chapters;
            }

            set
            {
                if (this.chapters == value)
                {
                    return;
                }

                SetProperty(ref chapters, value);
            }
        }
        public Command OnAppearingCommand { get; }
        public Command GoToExerciseCommand { get; }
        INavigation navigation;
        public OnsetViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            OnAppearingCommand = new Command(OnAppearing);
            GoToExerciseCommand = new Command(GoToExercise);
            OnAppearingCommand.Execute(null);
        }
        private void OnAppearing(object obj)
        {
            IsBusy = true;
            Chapters = SQLiteDbManager.GetModuleChapters(ModuleType.Onset);
            IsBusy = false;
        }
        private void GoToExercise(object obj)
        {
            IsBusy = true;
            var ch = obj as Chapter;
            if(ch != null)
            {
                navigation.PushAsync(new Views.Management.ExercisesList(ch));
            }
            Chapters = SQLiteDbManager.GetModuleChapters(ModuleType.Onset);
            IsBusy = false;
        }
    }
}
