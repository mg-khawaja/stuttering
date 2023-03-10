using Firebase.Storage;
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
using System.Runtime.InteropServices.ComTypes;
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
        private Exercise nextExercise;
        private bool isNextVisible;
        private string moduleType;
        private Chapter chapter;
        private int currentLevel;
        private int totalLevels;
        string audioUrl;
        //string downloadedFilePath;
        bool isDownloading;
        public bool IsNextVisible
        {
            get
            {
                return this.isNextVisible;
            }

            set
            {
                if (this.isNextVisible == value)
                {
                    return;
                }

                SetProperty(ref isNextVisible, value);
            }
        }
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
        public Command NextCommand { get; }
        INavigation navigation;
        bool isEngSelected { get; set; }
        public ExerciseViewModel(INavigation _navigation, Exercise _exercise)
        {
            //audioUrl = "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-4.mp3";
            navigation = _navigation;
            Exercise = _exercise;
            OnAppearingCommand = new Command(OnAppearing);
            NextCommand = new Command(NextClicked);
            PlayCommand = new Command(Play);
            PauseCommand = new Command(Pause);
            StopCommand = new Command(Stop);
        }
        private async Task<string> DownloadAsync(string url)
        {
            var downloadedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Exercise.Id + ".mp3");

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
                //this.downloadedFilePath = downloadedFilePath;
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
            if (navigation.NavigationStack[navigation.NavigationStack.Count -2] is Stuttering.Views.Management.Exercise)
            {
                this.navigation.RemovePage(this.navigation.NavigationStack[this.navigation.NavigationStack.Count - 2]);
            }
            player = DependencyService.Get<IAudio>();
            Chapter = SQLiteDbManager.GetChapter(Exercise.ChapterId);
            var list = SQLiteDbManager.GetChapterExercises(Chapter.Id);
            CurrentLevel = Exercise.Level;
            TotalLevels = list.Count;

            if (CurrentLevel != TotalLevels)
            {
                IsNextVisible = true;
                nextExercise = SQLiteDbManager.GetExerciseByLevel(Exercise.Level + 1, (int)Exercise.ModuleType, (int)Exercise.ExerciseType);
                nextExercise.IsOpen = true;
                SQLiteDbManager.SaveExercise(nextExercise);
            }
            else
            {
                IsNextVisible = false;
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
                    var nextExr = SQLiteDbManager.GetExerciseByLevel(1, Chapter.ModuleType, Exercise.ExerciseType + 1);
                    nextExr.IsOpen = true;
                    SQLiteDbManager.SaveExercise(nextExr);
                }
            }
            var culture = LocalizationResourceManager.Instance.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower() == "en")
                isEngSelected = true;
            else
                isEngSelected = false;
            IsBusy = false;
        }
        private async void NextClicked(object obj)
        {
            if (!IsNextVisible)
                return;
            IsBusy = true;
            //await navigation.PopAsync();
            player.Stop();
            await navigation.PushAsync(new Stuttering.Views.Management.Exercise(nextExercise));
            IsBusy = false;
        }
        private async void Play(object obj)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    if (string.IsNullOrEmpty(audioUrl))
                    {
                        //MediaoMediaPlayer oMediaPlayer = new MediaoMediaPlayer(); oMediaPlayer.SetAudioStreamType(Stream.Music); oMediaPlayer.SetDataSource("http://www.myownhosteddomain.com/songsapi/playlist"); oMediaPlayer.Prepare(); oMediaPlayer.Start();

                        IsDownloading = true;
                        FirebaseStorage firebaseStorage = new FirebaseStorage("stuttering-3337b.appspot.com");
                        var chap = ((ModuleType)Exercise.ModuleType).ToString() + "Chapters";
                        var exer = ((ExerciseType)Exercise.ExerciseType).ToString() + "s";
                        if (isEngSelected)
                        {
                            audioUrl = await firebaseStorage
                                .Child(chap)
                                .Child(exer)
                                .Child("English")
                                .Child(Exercise.Level + ".mp3")
                                .GetDownloadUrlAsync();
                        }
                        else
                        {
                            audioUrl = await firebaseStorage
                                .Child(chap)
                                .Child(exer)
                                .Child("Urdu")
                                .Child(Exercise.Level + ".mp3")
                                .GetDownloadUrlAsync();
                        }

                        //await MediaManager.CrossMediaManager.Current.Play(audioUrl);
                        PlayAudio(audioUrl);

                        //var path = await DownloadAsync(audioUrl);
                        //if (!string.IsNullOrEmpty(path))
                        //{
                        //    IsDownloading = false;
                        //    downloadedFilePath = path;
                        //    PlayAudio(audioUrl);
                        //}
                        //else
                        //{
                        //    (Application.Current as App).MainPage.DisplayToastAsync("file could not be downloaded! please try again later");
                        //}
                        IsDownloading = false;
                    }
                    else
                    {
                        player.Play(audioUrl);
                    }
                }
                else
                {
                    (Application.Current as App).MainPage.DisplayToastAsync("please connect to the internet connection!");
                }
            }
            catch (Exception ex)
            {
                IsDownloading = false;
                (Application.Current as App).MainPage.DisplayToastAsync("something went wrong!");
            }
        }
        private void PlayAudio(string url)
        {
            player.Play(url);
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
