using SQLite;
using Stuttering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Stuttering.SQL
{
    public static class SQLiteDbManager
    {
        public static SQLiteConnection Connection;
        public static void Initialise()
        {
            Connection = DependencyService.Get<ISQLiteDatabase>().GetConnection();
            Connection.CreateTable<UserModel>();
            Connection.CreateTable<Exercise>();
            Connection.CreateTable<Chapter>();
            Connection.CreateTable<Rating>();
            Connection.CreateTable<Evaluation>();
            Connection.CreateTable<StutterRaterRecord>();
            Connection.CreateTable<Metadata>();
        }
        public static UserModel GetUser()
        {
            return Connection.Table<UserModel>().FirstOrDefault();
        }
        public static int SaveUser(UserModel item)
        {
            Connection.DeleteAll<UserModel>();
            return Connection.Insert(item);
        }
        public static int DeleteAllUsers()
        {
            return Connection.DeleteAll<UserModel>();
        }
        public static Exercise GetExercise(int id)
        {
            return Connection.Table<Exercise>().Where(ex => ex.Id == id).FirstOrDefault();
        }
        public static List<Exercise> GetChapterExercises(int id)
        {
            return Connection.Table<Exercise>().Where(ex => ex.ChapterId == id).OrderBy(e => e.Level).ToList();
        }
        public static int InsertExercises(List<Exercise> items)
        {
            return Connection.InsertAll(items);
        }
        public static int SaveExercise(Exercise item)
        {
            if (item.Id == 0)
            {
                return Connection.Insert(item);
            }
            return Connection.Update(item);
        }
        public static int SaveExercises(List<Exercise> items)
        {
            return Connection.UpdateAll(items);
        }
        public static int DeleteAllExercises()
        {
            return Connection.DeleteAll<Exercise>();
        }
        public static Chapter GetChapter(int id)
        {
            return Connection.Table<Chapter>().Where(ex => ex.Id == id).FirstOrDefault();
        }
        public static List<Chapter> GetModuleChapters(ModuleType module)
        {
            return Connection.Table<Chapter>().Where(ex => ex.ModuleType == (int)module).OrderBy(e=> e.Order).ToList();
        }
        public static Exercise GetToDoExercise(int chapterId)
        {
            return Connection.Table<Exercise>().Where(ex => ex.ChapterId == chapterId && ex.IsOpen == true).LastOrDefault();
        }
        public static Exercise GetNextToDoExercise(int chapterId)
        {
            return Connection.Table<Exercise>().Where(ex => ex.ChapterId == chapterId && ex.IsOpen == false).FirstOrDefault();
        }
        public static Exercise GetExerciseByLevel(int level,int moduleType, int exerType)
        {
            return Connection.Table<Exercise>().Where(ex => ex.Level == level && 
            ex.ExerciseType == exerType & ex.ModuleType == moduleType).FirstOrDefault();
        }
        public static Chapter GetChapterByOrder(int order, int moduleType)
        {
            return Connection.Table<Chapter>().Where(ex => ex.Order == order && ex.ModuleType == moduleType).FirstOrDefault();
        }
        public static Chapter GetCurrentChapter(ModuleType module)
        {
            return Connection.Table<Chapter>().Where(ex => ex.ModuleType == (int)module && ex.IsOpen == true).OrderByDescending(o => o.Id).FirstOrDefault();
        }
        public static int InsertChapters(List<Chapter> items)
        {
            return Connection.InsertAll(items);
        }
        public static int SaveChapter(Chapter item)
        {
            if (item.Id == 0)
            {
                return Connection.Insert(item);
            }
            return Connection.Update(item);
        }
        public static int SaveChapters(List<Chapter> items)
        {
            return Connection.UpdateAll(items);
        }
        public static int DeleteAllChapters()
        {
            return Connection.DeleteAll<Chapter>();
        }

        #region rating
        public static int DeleteAllRatings()
        {
            return Connection.DeleteAll<Rating>();
        }
        public static List<Rating> GetAllRatings()
        {
            return Connection.Table<Rating>().ToList();
        }
        public static int SaveRating(Rating item)
        {
            if (item.Id == 0)
            {
                return Connection.Insert(item);
            }
            return Connection.Update(item);
        }
        #endregion

        #region evaluation
        public static int DeleteAllEvaluation()
        {
            return Connection.DeleteAll<Evaluation>();
        }
        public static List<Evaluation> GetAllEvaluations()
        {
            return Connection.Table<Evaluation>().ToList();
        }
        public static int SaveEvaluation(Evaluation item)
        {
            if (item.Id == 0)
            {
                return Connection.Insert(item);
            }
            return Connection.Update(item);
        }
        #endregion

        #region stutter rater
        public static int DeleteAllStutterRaterRecords()
        {
            return Connection.DeleteAll<StutterRaterRecord>();
        }
        public static List<StutterRaterRecord> GetAllStutterRaterRecords()
        {
            return Connection.Table<StutterRaterRecord>().ToList();
        }
        public static int SaveStutterRaterRecord(StutterRaterRecord item)
        {
            if (item.Id == 0)
            {
                return Connection.Insert(item);
            }
            return Connection.Update(item);
        }
        #endregion

        #region self evaluation
        public static int DeleteAllSelfEvaRecords()
        {
            return Connection.DeleteAll<Evaluation>();
        }
        public static List<Evaluation> GetAllSelfEvaRecords()
        {
            return Connection.Table<Evaluation>().ToList();
        }
        public static int SaveStutterSelfEva(Evaluation item)
        {
            if (item.Id == 0)
            {
                return Connection.Insert(item);
            }
            return Connection.Update(item);
        }
        #endregion

        #region self rating
        public static int DeleteAllSelfRatingRecords()
        {
            return Connection.DeleteAll<Rating>();
        }
        public static List<Rating> GetAllSelfRatingRecords()
        {
            return Connection.Table<Rating>().ToList();
        }
        public static int SaveStutterSelfRating(Rating item)
        {
            if (item.Id == 0)
            {
                return Connection.Insert(item);
            }
            return Connection.Update(item);
        }
        #endregion

        #region Metadata
        public static Metadata GetMetadata()
        {
            return Connection.Table<Metadata>().FirstOrDefault();
        }
        public static int SaveMetadata(Metadata item)
        {
            if (item.Id == 0)
            {
                return Connection.Insert(item);
            }
            return Connection.Update(item);
        }
        public static int DeleteMetadata()
        {
            return Connection.DeleteAll<Metadata>();
        }
        #endregion

    }
}
