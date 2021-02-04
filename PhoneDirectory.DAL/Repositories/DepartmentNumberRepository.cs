using Microsoft.EntityFrameworkCore;
using PhoneDirectory.DAL.EF;
using PhoneDirectory.DAL.Entities;
using PhoneDirectory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneDirectory.DAL.Repositories
{
    public class DepartmentNumberRepository : IRepository<DepartmentNumber>
    {
        private PhoneDirectoryContext db;

        public DepartmentNumberRepository(PhoneDirectoryContext context)
        {
            this.db = context;
        }
        public IEnumerable<DepartmentNumber> GetAll()
        {
            return db.DepartmentNumbers;
        }

        public DepartmentNumber Get(int id)
        {
            return db.DepartmentNumbers.Find(id);
        }
        public IEnumerable<DepartmentNumber> Find(Func<DepartmentNumber, bool> predicate)
        {
            return db.DepartmentNumbers.Where(predicate).ToList();
        }

        public void Create(DepartmentNumber item)
        {
            db.DepartmentNumbers.Add(item);
        }
        public void Update(DepartmentNumber item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DepartmentNumber item = db.DepartmentNumbers.Find(id);
            if (item != null) db.DepartmentNumbers.Remove(item);
        }
    }
}
