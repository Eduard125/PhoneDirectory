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
    public class RoleRepository : IRepository<Role>
    {
        private PhoneDirectoryContext db;

        public RoleRepository(PhoneDirectoryContext context)
        {
            this.db = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }
        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate).ToList();
        }

        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        public void Update(Role item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Role item = db.Roles.Find(id);
            if (item != null) db.Roles.Remove(item);
        }
    }
}
