using Plugin.CloudFirestore.Attributes;
using SQLite;
using Stuttering.Helper;
using System.Text.RegularExpressions;

namespace Stuttering.Models
{
    public class Chapter
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Order { get; set; }
        public bool IsOpen { get; set; }
        [Ignored]
        public bool IsLocked { get { return !IsOpen; } }
        [Ignored]
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
        [Ignored]
        public string Description { get; set; }
        public int ModuleType { get; set; }
        public int ExerciseType { get; set; }
    }
}