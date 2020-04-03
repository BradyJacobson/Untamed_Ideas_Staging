using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Descriptions
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? Idea { get; set; }

        public virtual Ideas IdeaNavigation { get; set; }
    }
}
