using SQLite;
using Stuttering.iOS.SQLite;
using Stuttering.SQL;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteIOS))]
namespace Stuttering.iOS.SQLite
{
    public class SQLiteIOS : ISQLiteDatabase
    {
        public SQLiteConnection GetConnection()
        {
            string dbPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "stuttering.sqlite");
            //string PersonalFolder = GetFolderPath(SpecialFolder.Personal);
            //var libraryFolder = Path.Combine(PersonalFolder, "", "Library");
            //var path = Path.Combine(libraryFolder, "mydatabase.sqlite");
            return new SQLiteConnection(dbPath);
        }
    }
}