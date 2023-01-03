using SQLite;
using Stuttering.Helper;
using System;
using System.Text.RegularExpressions;

namespace Stuttering.Models
{
    public class Evaluation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Q1Rate { get; set; }
        public int Q2Rate { get; set; }
        public int Q3Rate { get; set; }
        public int Q4Rate { get; set; }
        public int Q5Rate { get; set; }
        public int Q6Rate { get; set; }
        public int Q7Rate { get; set; }
        public string Date { get; set; }
    }
}