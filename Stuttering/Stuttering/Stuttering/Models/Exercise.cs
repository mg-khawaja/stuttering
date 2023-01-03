using SQLite;
using Stuttering.Helper;

namespace Stuttering.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsOpen { get; set; }
        public bool IsLocked { get { return !IsOpen; } }
        public int Level { get; set; }
        public string Name { get; set; }
        public string DescriptionVisible
        {
            get
            {
                var culture = LocalizationResourceManager.Instance.CurrentCulture;
                if (culture.TwoLetterISOLanguageName.ToLower() == "ur")
                    return UrduDescription;
                else
                    return Description;
            }
        }
        public string UrduDescription { get; set; }
        public string Description { get; set; }
        public string Audio { get; set; }
        public string UrduAudio { get; set; }
        public string AudioVisible
        {
            get
            {
                var culture = LocalizationResourceManager.Instance.CurrentCulture;
                if (culture.TwoLetterISOLanguageName.ToLower() == "ur")
                    return UrduAudio;
                else
                    return Audio;
            }
        }
        public int ChapterId { get; set; }
        public int ExerciseType { get; set; }
        public int ModuleType { get; set; }
    }
}