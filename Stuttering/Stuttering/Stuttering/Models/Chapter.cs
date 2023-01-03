using SQLite;
using Stuttering.Helper;
using System.Text.RegularExpressions;

namespace Stuttering.Models
{
    public class Chapter
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Order { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get { return !IsOpen; } }
        public string NameVisible
        {
            get
            {
                var culture = LocalizationResourceManager.Instance.CurrentCulture;
                if (culture.TwoLetterISOLanguageName.ToLower() == "ur")
                    return UrduName;
                else
                    return Name;
            }
        }
        public string Name { get; set; }
        public string UrduName { get; set; }
        public string Description { get; set; }
        public int ModuleType { get; set; }
    }
}