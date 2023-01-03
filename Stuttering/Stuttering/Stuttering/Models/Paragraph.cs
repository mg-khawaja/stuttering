using SQLite;

namespace Stuttering.Models
{
    public class Paragraph
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsLockEnabled { get; set; }
        public string Pattern { get; set; }
        public string Pin { get; set; }
        public string LockType { get; set; }
        public string Language { get; set; }
    }
}