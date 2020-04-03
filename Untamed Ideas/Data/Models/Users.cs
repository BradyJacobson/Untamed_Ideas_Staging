using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Users
    {
        public Users()
        {
            Ideas = new HashSet<Ideas>();
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Ideas> Ideas { get; set; }
    }
}
