using System;
using System.Collections.Generic;

namespace x2136443.DataModels
{
    public class OutlineResult
    {
        public IList<Section> Sections { get; set; }
    }

    public class Video
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Section
    {
        public string Name { get; set; }
        public IList<Video> Videos { get; set; }
        public bool IsExpanded { get; set; }
    }
}
