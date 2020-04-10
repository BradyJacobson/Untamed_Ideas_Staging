using System;
using System.Collections.Generic;
using RepoLibrary.Abstraction;
using System.Linq;
using Data.Models;

namespace Data.Repositories
{
    public class UsersRepository : IRepository<Users>
    {

        UntamedIdeasDbContext uidb;
        public UsersRepository()
        {
            uidb = new UntamedIdeasDbContext();
        }

        public UsersRepository(UntamedIdeasDbContext uidb)
        {
            this.uidb = uidb ?? throw new ArgumentNullException(nameof(uidb));
        }

        public IEnumerable<Users> GetFoldersMethod(string current)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> GetMethod()
        {
            var query = from e in uidb.Users select e;
            return query;
        }

        public IEnumerable<Users> GetRelatedMethod(string current)
        {
            throw new NotImplementedException();
        }

        public Users GetSpecificMethod(string current)
        {
            var query = from e in uidb.Users select e;
            var temp = query.FirstOrDefault<Users>(f => f.Username == current);
            return temp;

        }

        public void PostMethod(Users current)
        {
            if (uidb.Users.Any(e => e.Username == current.Username) || current.Username == null)
                return;
            else
                uidb.Users.Add(current);
            uidb.SaveChanges();
            return;
        }

        public void PutMethod(Users current)
        {
            if(uidb.Users.Any(e => e.Username == current.Username))
            {
                var replace = uidb.Users.FirstOrDefault(f => f.Username == current.Username);
                replace.Password = current.Password;
                uidb.Users.Update(replace);
                uidb.SaveChanges();
            }
            return;
        }

        public void RemoveMethod(Users current)
        {
            if(uidb.Users.Any(e => e.Username == current.Username))
            {
                uidb.Remove(current);
                uidb.SaveChanges();
            }
            return;
        }
    }
}
