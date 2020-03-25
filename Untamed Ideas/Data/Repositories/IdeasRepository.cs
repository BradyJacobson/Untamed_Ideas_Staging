using System;
using System.Collections.Generic;
using System.Text;
using RepoLibrary.Abstraction;
using Data.Models;
using System.Linq;

namespace Data.Repositories
{
    public class IdeasRepository : IRepository<Ideas>
    {
        UntamedIdeasDbContext uidb;
        public IdeasRepository()
        {
            uidb = new UntamedIdeasDbContext();
        }

        public IdeasRepository(UntamedIdeasDbContext uidb)
        {
            this.uidb = uidb ?? throw new ArgumentNullException(nameof(uidb));
        }

        public IEnumerable<Ideas> GetMethod()
        {
            var query = from e in uidb.Ideas select e;
            return query;
        }
        public IEnumerable<Ideas> GetRelatedMethod(string current)
        {
            var query = from e in uidb.Ideas where e.Username == current select e;
            return query;
        }

        public Ideas GetSpecificMethod(string current)
        {
            var query = from e in uidb.Ideas select e;
            var temp = query.FirstOrDefault<Ideas>(f => f.Id == Convert.ToInt32(current));
            return temp;
        }

        public void PostMethod(Ideas current)
        {
            if (uidb.Ideas.Any(e => e.Id == current.Id) || current == null)
                return;
            else
                uidb.Ideas.Add(current);
            uidb.SaveChanges();
            return;
        }

        public void PutMethod(Ideas current)
        {
            if(uidb.Ideas.Any(e => e.Id == current.Id))
            {
                var replace = uidb.Ideas.FirstOrDefault(f => f.Id == current.Id);
                replace.Ideaname = current.Ideaname;
                replace.Formating = current.Formating;
                replace.Username = current.Username;
                uidb.Ideas.Update(replace);
                uidb.SaveChanges();
            }
            return;
        }

        public void RemoveMethod(Ideas current)
        {
            if(uidb.Ideas.Any(e => e.Id == current.Id))
            {
                uidb.Remove(current);
                uidb.SaveChanges();
            }
            return;
        }
    }
}
