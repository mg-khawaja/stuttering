using Plugin.CloudFirestore.Attributes;
using SQLite;
using Stuttering.Helper;
using System.Text.RegularExpressions;

namespace Stuttering.Models
{
    public class ChapterFirebase
    {
        public string Id { get; set; }
        public int Order { get; set; }
        public bool IsOpen { get; set; }
        public string Name { get; set; }
        public string UrduName { get; set; }
        public int ModuleType { get; set; }
    }
}