using System;
using System.Collections.Generic;
using System.Text;
using RepoLibrary.Abstraction;
using Data.Models;
using System.Linq;

namespace Data.Repositories
{
    public class PictureRepository : IRepository<Picture>
    {
        UntamedIdeasDbContext uidb;
        public PictureRepository()
        {
            uidb = new UntamedIdeasDbContext();
        }
        public PictureRepository(UntamedIdeasDbContext uidb)
        {
            this.uidb = uidb ?? throw new ArgumentNullException(nameof(uidb));
        }

        public IEnumerable<Picture> GetMethod()
        {
            var query = from e in uidb.Picture select e;
            return query;
        }

        public IEnumerable<Picture> GetRelatedMethod(string current)
        {
            var query = from e in uidb.Picture where e.Idea == Convert.ToInt32(current) select e;
            return query;
        }

        public Picture GetSpecificMethod(string current)
        {
            var query = from e in uidb.Picture select e;
            var temp = query.FirstOrDefault<Picture>(e => e.Id == Convert.ToInt32(current));
            return temp;
        }

        public void PostMethod(Picture current)
        { 
            if (uidb.Picture.Any<Picture>(e => e.Id == current.Id))
                return;
            else
                uidb.Picture.Add(current);
            uidb.SaveChanges();
            return;
        }

        public void PutMethod(Picture current)
        {
            if (uidb.Picture.Any(e => e.Id == current.Id))
            {
                var replace = uidb.Picture.FirstOrDefault(f => f.Id == current.Id);
                replace.Content1 = current.Content1;
                replace.Idea = current.Idea;
                uidb.Picture.Update(replace);
                uidb.SaveChanges();
            }
            return;
        }

        public void RemoveMethod(Picture current)
        {
            if (uidb.Picture.Any(e => e.Id == current.Id))
            {
                uidb.Remove(current);
                uidb.SaveChanges();
            }
            return;
        }
    }
}
