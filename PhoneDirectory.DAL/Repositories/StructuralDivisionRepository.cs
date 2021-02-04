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
    public class StructuralDivisionRepository : IRepository<StructuralDivision>
    {
        private PhoneDirectoryContext db;

        public StructuralDivisionRepository(PhoneDirectoryContext context)
        {
            this.db = context;
        }

        public IEnumerable<StructuralDivision> GetAll()
        {
            return db.StructuralDivisions;
        }

        public StructuralDivision Get(int id)
        {
            return db.StructuralDivisions.Find(id);
        }

        public IEnumerable<StructuralDivision> Find(Func<StructuralDivision, bool> predicate)
        {
            return db.StructuralDivisions.Where(predicate).ToList();
        }

        public void Create(StructuralDivision item)
        {
            db.StructuralDivisions.Add(item);
        }

        public void Update(StructuralDivision item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            StructuralDivision item = db.StructuralDivisions.Find(id);
            if (item != null) db.StructuralDivisions.Remove(item);
        }


    }
}
