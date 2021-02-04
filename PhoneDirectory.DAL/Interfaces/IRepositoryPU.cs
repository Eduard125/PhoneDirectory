using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.DAL.Interfaces
{
    public interface IRepositoryPU<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int strucDivId, int postId);
    }
}
