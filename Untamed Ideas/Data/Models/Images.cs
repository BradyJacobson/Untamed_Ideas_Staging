using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Images
    {
        public int Id { get; set; }
        public int? Representation { get; set; }
        public int? Idea { get; set; }

        public virtual Ideas IdeaNavigation { get; set; }
    }
}
