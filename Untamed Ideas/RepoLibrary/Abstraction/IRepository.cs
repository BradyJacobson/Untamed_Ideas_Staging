using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLibrary.Abstraction
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetMethod();
        IEnumerable<T> GetRelatedMethod(string current);
        T GetSpecificMethod(string current);
        IEnumerable<T> GetFoldersMethod(string current);
        void PostMethod(T current);
        void PutMethod(T current);
        void RemoveMethod(T current);
    }
}