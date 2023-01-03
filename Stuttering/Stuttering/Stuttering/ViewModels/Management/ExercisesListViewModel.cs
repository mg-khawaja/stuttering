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
    public class ExercisesListViewModel : BaseViewModel
    {
        private int totalLevels;
        public int TotalLevels
        {
            get
            {
                return this.totalLevels;
            }

            set
            {
                if (this.totalLevels == value)
                {
                    return;
                }

                SetProperty(ref totalLevels, value);
            }
        }
        private List<Exercise> exercises;
        public List<Exercise> Exercises
        {
            get
            {
                return this.exercises;
            }

            set
            {
                if (this.exercises == value)
                {
                    return;
                }

                SetProperty(ref exercises, value);
            }
        }
        private Chapter chapter;
        public Chapter Chapter
        {
            get
            {
                return this.chapter;
            }

            set
            {
                if (this.chapter == value)
                {
                    return;
                }

                SetProperty(ref chapter, value);
            }
        }
        public Command OnAppearingCommand { get; }
        public Command GoToExerciseCommand { get; }
        INavigation navigation;
        public ExercisesListViewModel(INavigation _navigation,Chapter chapter)
        {
            navigation = _navigation;
            Chapter = chapter;
            OnAppearingCommand = new Command(OnAppearing);
            GoToExerciseCommand = new Command(GoToExercise);
        }
        private void OnAppearing(object obj)
        {
            IsBusy = true;
            Exercises = SQLiteDbManager.GetChapterExercises(Chapter.Id);
            IsBusy = false;
        }
        private void GoToExercise(object obj)
        {
            IsBusy = true;
            var exercise = obj as Exercise;
            if(exercise != null)
            {
                navigation.PushAsync(new Views.Management.Exercise(exercise));
            }
            IsBusy = false;
        }
    }
}
