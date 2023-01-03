using SQLite;
using Stuttering.Helper;
using System;
using System.Text.RegularExpressions;

namespace Stuttering.Models
{
    public class Rating
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Date { get; set; }
    }
}