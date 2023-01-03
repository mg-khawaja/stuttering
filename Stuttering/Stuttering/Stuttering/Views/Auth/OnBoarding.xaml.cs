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
                (Application.Current as App).MainPage = new PinView(Helper.LockCheckType.Login);
            }
        }
    }
}