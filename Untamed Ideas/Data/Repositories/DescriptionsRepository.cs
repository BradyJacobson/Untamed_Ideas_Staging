using System;
using System.Collections.Generic;
using System.Text;
using RepoLibrary.Abstraction;
using Data.Models;
using System.Linq;

namespace Data.Repositories
{
    public class DescriptionsRepository : IRepository<Descriptions>
    {
        UntamedIdeasDbContext uidb;
        public DescriptionsRepository()
        {
            uidb = new UntamedIdeasDbContext();
        }

        public DescriptionsRepository(UntamedIdeasDbContext uidb)
        {
            this.uidb = uidb ?? throw new ArgumentNullException(nameof(uidb));
        }

        public IEnumerable<Descriptions> GetFoldersMethod(string current)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Descriptions> GetMethod()
        {
            var query = from e in uidb.Descriptions select e;
            return query;
        }

        public IEnumerable<Descriptions> GetRelatedMethod(string current)
        {
            var query = from e in uidb.Descriptions where e.Idea == Convert.ToInt32(current) select e;
            return query;
        }

        public Descriptions GetSpecificMethod(string current)
        {
            var query = from e in uidb.Descriptions select e;
            var temp = query.FirstOrDefault<Descriptions>(f => f.Id == Convert.ToInt32(current));
            return temp;
        }

        public void PostMethod(Descriptions current)
        {
            if (uidb.Descriptions.Any(e => e.Id == current.Id) || current == null)
                return;
            else
                uidb.Descriptions.Add(current);
            uidb.SaveChanges();
            return;
        }

        public void PutMethod(Descriptions current)
        {
            if(uidb.Descriptions.Any(e => e.Id == current.Id))
            {
                var replace = uidb.Descriptions.FirstOrDefault(f => f.Id == current.Id);
                replace.Content = current.Content;
                replace.Idea = current.Idea;
                uidb.Descriptions.Update(replace);
                uidb.SaveChanges();
            }
            return;
        }

        public void RemoveMethod(Descriptions current)
        {
            if(uidb.Descriptions.Any(e => e.Id == current.Id))
            {
                uidb.Remove(current);
                uidb.SaveChanges();
            }
            return;
        }
    }
}
