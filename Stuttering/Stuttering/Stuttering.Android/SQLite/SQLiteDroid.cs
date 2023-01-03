using SQLite;
using Stuttering.Droid.SQLite;
using Stuttering.SQL;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDroid))]
namespace Stuttering.Droid.SQLite
{
    public class SQLiteDroid : ISQLiteDatabase
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "stuttering.sqlite";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(dbPath, dbName);
            var conn = new SQLiteConnection(path);
            return conn;

        }
    }
}