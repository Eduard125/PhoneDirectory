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
    public class DepartmentMobNumberRepository : IRepository<DepartmentMobNumber>
    {
        private PhoneDirectoryContext db;

        public DepartmentMobNumberRepository(PhoneDirectoryContext context)
        {
            this.db = context;
        }
        public IEnumerable<DepartmentMobNumber> GetAll()
        {
            return db.DepartmentMobNumbers;
        }

        public DepartmentMobNumber Get(int id)
        {
            return db.DepartmentMobNumbers.Find(id);
        }

        public IEnumerable<DepartmentMobNumber> Find(Func<DepartmentMobNumber, bool> predicate)
        {
            return db.DepartmentMobNumbers.Where(predicate).ToList();
        }

        public void Create(DepartmentMobNumber item)
        {
            db.DepartmentMobNumbers.Add(item);
        }

        public void Update(DepartmentMobNumber item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DepartmentMobNumber item = db.DepartmentMobNumbers.Find(id);
            if (item != null) db.DepartmentMobNumbers.Remove(item);
        }
    }
}
