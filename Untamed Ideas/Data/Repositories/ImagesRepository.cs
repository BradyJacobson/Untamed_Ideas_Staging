using System;
using System.Collections.Generic;
using System.Text;
using RepoLibrary.Abstraction;
using Data.Models;
using System.Linq;

namespace Data.Repositories
{
    public class ImagesRepository : IRepository<Images>
    {
        UntamedIdeasDbContext uidb;
        public ImagesRepository()
        {
            uidb = new UntamedIdeasDbContext();
        }
        public ImagesRepository(UntamedIdeasDbContext uidb)
        {
            this.uidb = uidb ?? throw new ArgumentNullException(nameof(uidb));
        }

        public IEnumerable<Images> GetFoldersMethod(string current)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Images> GetMethod()
        {
            var query = from e in uidb.Images select e;
            return query;
        }

        public IEnumerable<Images> GetRelatedMethod(string current)
        {
            var query = from e in uidb.Images where e.Idea == Convert.ToInt32(current) select e;
            Images t;
            foreach(Images i in query)
            {
                t = i;
            }
            return query;
        }

        public Images GetSpecificMethod(string current)
        {
            var query = from e in uidb.Images select e;
            var temp = query.FirstOrDefault<Images>(f => f.Id == Convert.ToInt32(current));
            return temp;
        }

        public void PostMethod(Images current)
        {
            if (uidb.Images.Any(e => e.Id == current.Id) || current == null)
                return;
            else
                uidb.Images.Add(current);
            uidb.SaveChanges();
            return;
        }

        public void PutMethod(Images current)
        {
            if(uidb.Images.Any(e => e.Id == current.Id))
            {
                var replace = uidb.Images.FirstOrDefault(f => f.Id == current.Id);
                replace.Representation = current.Representation;
                replace.Idea = current.Idea;
                uidb.Images.Update(replace);
                uidb.SaveChanges();
            }
            return;
        }

        public void RemoveMethod(Images current)
        {
            if(uidb.Images.Any(e => e.Id == current.Id))
            {
                uidb.Remove(current);
                uidb.SaveChanges();
            }
            return;
        }
    }
}
