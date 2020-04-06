using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Picture
    {
        public int Id { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public int? Idea { get; set; }

        public virtual Ideas IdeaNavigation { get; set; }
    }
}
