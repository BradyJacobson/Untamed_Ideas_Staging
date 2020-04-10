using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Supplies
    {
        public int Id { get; set; }
        public string Supplies1 { get; set; }
        public int? Idea { get; set; }
        public int? TaskDone { get; set; }

        public virtual Ideas IdeaNavigation { get; set; }
    }
}
