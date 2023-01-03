using Plugin.Multilingual;
using Stuttering.Helper;
using Stuttering.Models;
using Stuttering.Resx;
using Stuttering.SQL;
using Stuttering.Views.Auth;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Stuttering.ViewModels
{
    public class ExerciseViewModel : BaseViewModel
    {
        public IAudio player;
        private Exercise exercise;
        private string moduleType;
        private Chapter chapter;
        private int currentLevel;
        private int totalLevels;
        string audioUrl;
        string downloadedFilePath;
        bool isDownloading;
        public bool IsDownloading
        {
            get
            {
                return this.isDownloading;
            }

            set
            {
                if (this.isDownloading == value)
                {
                    return;
                }

                SetProperty(ref isDownloading, value);
            }
        }
        public Exercise Exercise
        {
            get
            {
                return this.exercise;
            }

            set
            {
                if (this.exercise == value)
                {
                    return;
                }

                SetProperty(ref exercise, value);
            }
        }
        public string ModuleType
        {
            get
            {
                return this.moduleType;
            }

            set
            {
                if (this.moduleType == value)
                {
                    return;
                }

                SetProperty(ref moduleType, value);
            }
        }
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
        public int CurrentLevel
        {
            get
            {
                return this.currentLevel;
            }

            set
            {
                if (this.currentLevel == value)
                {
                    return;
                }

                SetProperty(ref currentLevel, value);
            }
        }
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
        public Command OnAppearingCommand { get; }
        public Command PlayCommand { get; }
        public Command PauseCommand { get; }
        public Command StopCommand { get; }
        INavigation navigation;
        bool isEngSelected { get; set; }
        public ExerciseViewModel(INavigation _navigation, Exercise _exercise)
        {
            audioUrl = "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-4.mp3";
            navigation = _navigation;
            Exercise = _exercise;
            OnAppearingCommand = new Command(OnAppearing);
            PlayCommand = new Command(Play);
            PauseCommand = new Command(Pause);
            StopCommand = new Command(Stop);
        }
        private async Task<string> DownloadAsync(string url)
        {
            var downloadedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "audio.mp3");

            var success = await DownloadFileAsync(url, downloadedFilePath);

            if (success)
            {
                return downloadedFilePath;
            }
            else
            {
                return null;
            }
        }
        private async Task<bool> DownloadFileAsync(string fileUrl, string downloadedFilePath)
        {
            try
            {
                var client = new HttpClient();
                var downloadStream = await client.GetStreamAsync(fileUrl);
                var fileStream = File.Create(downloadedFilePath);
                await downloadStream.CopyToAsync(fileStream);
                this.downloadedFilePath = downloadedFilePath;
                downloadStream.Close();
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                //TODO handle exception
                return false;
            }
        }
        private void OnAppearing(object obj)
        {
            IsBusy = true;
            player = DependencyService.Get<IAudio>();
            Chapter = SQLiteDbManager.GetChapter(Exercise.ChapterId);
            var list = SQLiteDbManager.GetChapterExercises(Chapter.Id);
            CurrentLevel = Exercise.Level;
            TotalLevels = list.Count;

            if (CurrentLevel != TotalLevels)
            {
                var nextExercise = SQLiteDbManager.GetExerciseByLevel(Exercise.Level + 1, (int)Exercise.ModuleType, (int)Exercise.ExerciseType);
                nextExercise.IsOpen = true;
                SQLiteDbManager.SaveExercise(nextExercise);
            }
            else
            {
                var chList = SQLiteDbManager.GetModuleChapters((ModuleType)Chapter.ModuleType);
                if (chList.Count != Chapter.Order)
                {
                    var meta = SQLiteDbManager.GetMetadata();
                    var chapter = SQLiteDbManager.GetChapterByOrder(Chapter.Order + 1, (int)Chapter.ModuleType);
                    chapter.IsOpen = true;
                    SQLiteDbManager.SaveChapter(chapter);
                    var type = (ModuleType)Chapter.ModuleType;
                    if (type == Models.ModuleType.Flexible)
                    {
                        meta.FlexibleChapter += 1;
                    }
                    else
                    {
                        meta.OnsetChapter += 1;
                    }
                    SQLiteDbManager.SaveMetadata(meta);
                }
            }
            var culture = LocalizationResourceManager.Instance.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower() == "en")
                isEngSelected = true;
            else
                isEngSelected = false;
            IsBusy = false;
        }
        private async void Play(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(downloadedFilePath))
                {

                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        IsDownloading = true;
                        string audioUrl;
                        if (isEngSelected)
                        {
                            audioUrl = exercise.Audio;
                        }
                        else
                        {
                            audioUrl = exercise.UrduAudio;
                        }

                        var path = await DownloadAsync(audioUrl);
                        if (!string.IsNullOrEmpty(path))
                        {
                            IsDownloading = false;
                            downloadedFilePath = path;
                            PlayAudio();
                        }
                        else
                        {
                            (Application.Current as App).MainPage.DisplayToastAsync("file could not be downloaded! please try again later");
                        }
                        IsDownloading = false;
                    }
                    else
                    {
                        (Application.Current as App).MainPage.DisplayToastAsync("please connect to the internet connection!");
                    }
                }
                else
                {
                    player.Play(downloadedFilePath);
                }
            }
            catch (Exception ex)
            {
                IsDownloading = false;
                (Application.Current as App).MainPage.DisplayToastAsync("something went wrong!");
            }
        }
        private void PlayAudio()
        {
            player.Play(downloadedFilePath);
        }
        private void Pause(object obj)
        {
            player.Pause();

        }
        private void Stop(object obj)
        {
            player.Stop();
        }
    }
}
