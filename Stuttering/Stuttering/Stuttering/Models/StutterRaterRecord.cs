using SQLite;
using Stuttering.Helper;
using System;
using System.Text.RegularExpressions;

namespace Stuttering.Models
{
    public class StutterRaterRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Syllables { get; set; }
        public int Stutters { get; set; }
        public double StutterPerSyllable { get; set; }
        public double SyllablePerMinute { get; set; }
        public string Duration { get; set; }
        public string Date { get; set; }
    }
}