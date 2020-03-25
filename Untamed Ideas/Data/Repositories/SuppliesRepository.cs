using System;
using System.Collections.Generic;
using System.Text;
using RepoLibrary.Abstraction;
using Data.Models;
using System.Linq;

namespace Data.Repositories
{
    public class SuppliesRepository : IRepository<Supplies>
    {
        UntamedIdeasDbContext uidb;
        public SuppliesRepository()
        {
            uidb = new UntamedIdeasDbContext();
        }

        public SuppliesRepository(UntamedIdeasDbContext uidb)
        {
            this.uidb = uidb ?? throw new ArgumentNullException(nameof(uidb));
        }

        public IEnumerable<Supplies> GetMethod()
        {
            var query = from e in uidb.Supplies select e;
            return query;
        }

        public IEnumerable<Supplies> GetRelatedMethod(string current)
        {
            var query = from e in uidb.Supplies where e.Idea == Convert.ToInt32(current) select e;
            return query;
        }

        public Supplies GetSpecificMethod(string current)
        {
            var query = from e in uidb.Supplies select e;
            var temp = query.FirstOrDefault<Supplies>(e => e.Id == Convert.ToInt32(current));
            return temp;
        }

        public void PostMethod(Supplies current)
        {
            if (uidb.Supplies.Any<Supplies>(e => e.Id == current.Id))
                return;
            else
                uidb.Supplies.Add(current);
            uidb.SaveChanges();
            return;
        }

        public void PutMethod(Supplies current)
        {
            if(uidb.Supplies.Any(e => e.Id == current.Id))
            {
                var replace = uidb.Supplies.FirstOrDefault(f => f.Id == current.Id);
                replace.Supplies1 = current.Supplies1;
                replace.Idea = current.Idea;
                uidb.Supplies.Update(replace);
                uidb.SaveChanges();
            }
            return;
        }

        public void RemoveMethod(Supplies current)
        {
            if (uidb.Supplies.Any(e => e.Id == current.Id))
            {
                uidb.Remove(current);
                uidb.SaveChanges();
            }
            return;
        }
    }
}
