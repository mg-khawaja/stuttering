using System;
using System.Collections.Generic;
using System.Text;

namespace Stuttering.Models
{
    public class StutterReadItem
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public StutterreadItemType Type { get; set; }
    }
    public enum StutterreadItemType
    {
        Text,
        Image,
        ImageText
    }
}
