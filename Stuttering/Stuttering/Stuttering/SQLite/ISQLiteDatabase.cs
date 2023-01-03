using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stuttering.SQL
{
    public interface ISQLiteDatabase
    {
        SQLiteConnection GetConnection();
    }
}
