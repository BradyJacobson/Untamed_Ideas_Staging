using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Ideas
    {
        public Ideas()
        {
            Descriptions = new HashSet<Descriptions>();
            Images = new HashSet<Images>();
            Supplies = new HashSet<Supplies>();
            Picture = new HashSet<Picture>();
        }

        public int Id { get; set; }
        public string Ideaname { get; set; }
        public int? Formating { get; set; }
        public string Username { get; set; }

        public virtual Users UsernameNavigation { get; set; }
        public virtual ICollection<Descriptions> Descriptions { get; set; }
        public virtual ICollection<Images> Images { get; set; }
        public virtual ICollection<Supplies> Supplies { get; set; }
        public virtual ICollection<Picture> Picture { get; set; }
    }
}
