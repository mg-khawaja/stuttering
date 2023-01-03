using SQLite;
using Stuttering.Helper;
using System;
using System.Text.RegularExpressions;

namespace Stuttering.Models
{
    public class Metadata
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime BreathDateTime { get; set; }
        public DateTime SelfRateDateTime { get; set; }
        public DateTime SelfEvalDateTime { get; set; }
        public int? ChNo_WhenAssessmentLocked { get; set; }
        public int FlexibleChapter { get; set; }
        public int OnsetChapter { get; set; }
    }
}