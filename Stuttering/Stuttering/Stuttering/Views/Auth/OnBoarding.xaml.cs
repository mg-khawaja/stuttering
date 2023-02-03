using Firebase.Storage;
using Plugin.CloudFirestore;
using Stuttering.Models;
using Stuttering.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stuttering.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnBoarding : ContentPage
    {
        string AudioUrl = "https://firebasestorage.googleapis.com/v0/b/stuttering-3337b.appspot.com/o/word%2062.mp3?alt=media&token=fa9d71f7-1ffd-4f5e-8de2-2668020a56ce";
        string UrduUrl = "https://firebasestorage.googleapis.com/v0/b/stuttering-3337b.appspot.com/o/urdu.mp3?alt=media&token=38336e8f-a9e8-48bc-8621-55e00d40deeb";
        public OnBoarding()
        {
            InitializeComponent();
            InitializeData();
        }

        private async void InitializeData()
        {
            try
            {
                //Delete complete data
                SQLiteDbManager.DeleteAllChapters();
                SQLiteDbManager.DeleteAllExercises();
                SQLiteDbManager.DeleteAllSelfEvaRecords();
                SQLiteDbManager.DeleteAllSelfRatingRecords();
                SQLiteDbManager.DeleteAllStutterRaterRecords();
                SQLiteDbManager.DeleteAllUsers();
                SQLiteDbManager.DeleteMetadata();

                //Firestore Exerices Creation

                /*
                foreach (int moduleType in Enum.GetValues(typeof(ModuleType)))
                {
                    foreach (int exerciseType in Enum.GetValues(typeof(ExerciseType)))
                    {
                        for (int i = 1; i <= 100; i++)
                        {
                            if ((exerciseType == 4 && i == 11) || (exerciseType == 3 && i == 51))
                                break;

                            var exer = new Exercise()
                            {
                                Id = Convert.ToInt32(moduleType + "" + exerciseType + i),
                                ChapterId = Convert.ToInt32(moduleType + "" + exerciseType),
                                ExerciseType = exerciseType,
                                ModuleType = moduleType,
                                IsOpen = false,
                                Level = i,
                                Name = "Level " + i,
                            };
                            await CrossCloudFirestore.Current
                                .Instance
                                .Collection(((ModuleType)moduleType).ToString())
                                .Document(((ExerciseType)exerciseType).ToString())
                                .Collection("Exercise")
                                .Document(((ModuleType)moduleType).ToString() + ((ExerciseType)exerciseType).ToString() + i)
                                .SetAsync(exer);
                        }
                    }
                }
                */

                //Firestore Data in SQL

                List<Chapter> chapters = new List<Chapter>();
                List<Exercise> exercises = new List<Exercise>();

                foreach (int moduleType in Enum.GetValues(typeof(ModuleType)))
                {
                    var documentChapters = await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection(((ModuleType)moduleType).ToString())
                                        .GetAsync();
                    var dataChapters = documentChapters.Documents.ToList();
                    foreach (var item in dataChapters)
                    {
                        chapters.Add(item.ToObject<Chapter>());
                    }
                    foreach (int exerciseType in Enum.GetValues(typeof(ExerciseType)))
                    {
                        var documentExercises = await CrossCloudFirestore.Current
                            .Instance
                            .Collection(((ModuleType)moduleType).ToString())
                            .Document(((ExerciseType)exerciseType).ToString())
                            .Collection("Exercise")
                            .GetAsync();
                        var dataExercises = documentExercises.Documents.ToList();
                        foreach (var item in dataExercises)
                        {
                            exercises.Add(item.ToObject<Exercise>());
                        }
                    }
                }
                SQLiteDbManager.InsertChapters(chapters);
                SQLiteDbManager.InsertExercises(exercises);
                

                //Demo Data in SQL

                /*
                //Flexible Chapters
                SQLiteDbManager.InsertChapters(new List<Models.Chapter>()
            {
                new Models.Chapter()
                {
                    Name = "Words",
                    UrduName = "الفاظ",
                    IsOpen = true,
                    ModuleType = Convert.ToInt32(ModuleType.Flexible),
                    Order = 1,
                },
                new Models.Chapter()
                {
                    Order = 2,
                    Name = "Phrases",
                    UrduName = "Phrases",
                    IsOpen = false,
                    ModuleType = Convert.ToInt32(ModuleType.Flexible)
                },
                new Models.Chapter()
                {
                    Order = 3,
                    Name = "Sentences",
                    UrduName = "Sentences",
                    IsOpen = false,
                    ModuleType = Convert.ToInt32(ModuleType.Flexible)
                },
                new Models.Chapter()
                {
                    Order = 4,
                    Name = "Paragraphs",
                    UrduName = "Paragraphs",
                    IsOpen = false,
                    ModuleType = Convert.ToInt32(ModuleType.Flexible)
                }
            });
                //Onset Chapters
                SQLiteDbManager.InsertChapters(new List<Models.Chapter>()
            {
                new Models.Chapter()
                {
                    Order = 1,
                    Name = "Words",
                    UrduName = "الفاظ",
                    IsOpen = true,
                    ModuleType = Convert.ToInt32(ModuleType.Onset)
                },
                new Models.Chapter()
                {
                    Order = 2,
                    Name = "Phrases",
                    UrduName = "Phrases",
                    IsOpen = false,
                    ModuleType = Convert.ToInt32(ModuleType.Onset)
                },
                new Models.Chapter()
                {
                    Order = 3,
                    Name = "Sentences",
                    UrduName = "Sentences",
                    IsOpen = false,
                    ModuleType = Convert.ToInt32(ModuleType.Onset)
                },
                new Models.Chapter()
                {
                    Order = 4,
                    Name = "Paragraphs",
                    UrduName = "Paragraphs",
                    IsOpen = false,
                    ModuleType = Convert.ToInt32(ModuleType.Onset)
                }
            });

                //Flexible Exercises
                var chapters = SQLiteDbManager.GetModuleChapters(ModuleType.Flexible);
                foreach (var item in chapters)
                {
                    if (item.Name == "Words")
                    {
                        SQLiteDbManager.InsertExercises(new List<Exercise>()
                    {
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Word),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = true,
                            Level = 1,
                            Name = "Level 1",
                            UrduDescription = "پہلا",
                            Description = "First",
                            Audio = AudioUrl,
                            UrduAudio = UrduUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Word),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = false,
                            Level = 2,
                            Name = "Level 2",
                            UrduDescription = "Second",
                            Description = "Second",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Word),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = false,
                            Level = 3,
                            Name = "Level 3",
                            UrduDescription = "Third",
                            Description = "Third",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        }
                    });
                    }
                    else if (item.Name == "Phrases")
                    {
                        SQLiteDbManager.InsertExercises(new List<Exercise>()
                    {
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Phrase),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = true,
                            Level = 1,
                            Name = "Level 1",
                            UrduDescription = "First",
                            Description = "First",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Phrase),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = false,
                            Level = 2,
                            Name = "Level 2",
                            UrduDescription = "Second",
                            Description = "Second",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Phrase),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = false,
                            Level = 3,
                            Name = "Level 3",
                            UrduDescription = "Third",
                            Description = "Third",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        }
                    });
                    }
                    else if (item.Name == "Sentences")
                    {
                        SQLiteDbManager.InsertExercises(new List<Exercise>()
                    {
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Sentence),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = true,
                            Level = 1,
                            Name = "Level 1",
                            UrduDescription = "First",
                            Description = "First",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Sentence),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = false,
                            Level = 2,
                            Name = "Level 2",
                            UrduDescription = "Second",
                            Description = "Second",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Sentence),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = false,
                            Level = 3,
                            Name = "Level 3",
                            UrduDescription = "Third",
                            Description = "Third",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        }
                    });
                    }
                    else if (item.Name == "Paragraphs")
                    {
                        SQLiteDbManager.InsertExercises(new List<Exercise>()
                    {
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Paragraph),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = true,
                            Level = 1,
                            Name = "Level 1",
                            UrduDescription = "First",
                            Description = "First",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Paragraph),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = false,
                            Level = 2,
                            Name = "Level 2",
                            UrduDescription = "Second",
                            Description = "Second",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {

                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Paragraph),
                            ModuleType = Convert.ToInt32(ModuleType.Flexible),
                            IsOpen = false,
                            Level = 3,
                            Name = "Level 3",
                            UrduDescription = "Third",
                            Description = "Third",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        }
                    });
                    }
                }

                //Onset Exercises
                chapters = SQLiteDbManager.GetModuleChapters(ModuleType.Onset);
                foreach (var item in chapters)
                {
                    if (item.Name == "Words")
                    {
                        SQLiteDbManager.InsertExercises(new List<Exercise>()
                    {
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Word),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = true,
                            Level = 1,
                            Name = "Level 1",
                            UrduDescription = "پہلا",
                            Description = "First",
                            Audio = AudioUrl,
                            UrduAudio = UrduUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Word),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = false,
                            Level = 2,
                            Name = "Level 2",
                            UrduDescription = "Second",
                            Description = "Second",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Word),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = false,
                            Level = 3,
                            Name = "Level 3",
                            UrduDescription = "Third",
                            Description = "Third",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        }
                    });
                    }
                    else if (item.Name == "Phrases")
                    {
                        SQLiteDbManager.InsertExercises(new List<Exercise>()
                    {
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Phrase),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = true,
                            Level = 1,
                            Name = "Level 1",
                            UrduDescription = "First",
                            Description = "First",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Phrase),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = false,
                            Level = 2,
                            Name = "Level 2",
                            UrduDescription = "Second",
                            Description = "Second",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Phrase),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = false,
                            Level = 3,
                            Name = "Level 3",
                            UrduDescription = "Third",
                            Description = "Third",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        }
                    });
                    }
                    else if (item.Name == "Sentences")
                    {
                        SQLiteDbManager.InsertExercises(new List<Exercise>()
                    {
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Sentence),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = true,
                            Level = 1,
                            Name = "Level 1",
                            UrduDescription = "First",
                            Description = "First",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Sentence),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = false,
                            Level = 2,
                            Name = "Level 2",
                            UrduDescription = "Second",
                            Description = "Second",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Sentence),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = false,
                            Level = 3,
                            Name = "Level 3",
                            UrduDescription = "Third",
                            Description = "Third",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        }
                    });
                    }
                    else if (item.Name == "Paragraphs")
                    {
                        SQLiteDbManager.InsertExercises(new List<Exercise>()
                    {
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Paragraph),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = true,
                            Level = 1,
                            Name = "Level 1",
                            UrduDescription = "First",
                            Description = "First",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Paragraph),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = false,
                            Level = 2,
                            Name = "Level 2",
                            UrduDescription = "Second",
                            Description = "Second",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        },
                        new Exercise()
                        {
                            ChapterId = item.Id,
                            ExerciseType = Convert.ToInt32(ExerciseType.Paragraph),
                            ModuleType = Convert.ToInt32(ModuleType.Onset),
                            IsOpen = false,
                            Level = 3,
                            Name = "Level 3",
                            UrduDescription = "Third",
                            Description = "Third",
                            Audio = AudioUrl,
                            UrduAudio = AudioUrl,
                        }
                    });
                    }
                }
                */

                SQLiteDbManager.SaveMetadata(new Metadata()
                {
                    ChNo_WhenAssessmentLocked = null,
                    FlexibleChapter = 1,
                    OnsetChapter = 1
                });

                await Task.Delay(2000);
                (Application.Current as App).MainPage = new PinView(Helper.LockCheckType.Login);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                (Application.Current as App).MainPage = new PinView(Helper.LockCheckType.Login);
            }
        }
    }
}