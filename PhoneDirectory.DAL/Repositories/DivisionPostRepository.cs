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
    public class DivisionPostRepository : IRepositoryPU<DivisionPost>
    {

        private PhoneDirectoryContext db;

        public DivisionPostRepository(PhoneDirectoryContext context)
        {
            this.db = context;
        }

        public IEnumerable<DivisionPost> GetAll()
        {
            return db.DivisionPosts.Include(o => o.StructuralDivision).Include(o => o.Post);
        }

        public DivisionPost Get(int id)
        {
            return db.DivisionPosts.Include(o => o.StructuralDivision).Include(o => o.Post).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<DivisionPost> Find(Func<DivisionPost, bool> predicate)
        {
            return db.DivisionPosts.Include(o => o.StructuralDivision).Include(o => o.Post).Where(predicate).ToList();
        }

        public void Create(DivisionPost item)
        {
            db.DivisionPosts.Add(item);
        }
        public void Update(DivisionPost item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int strucDivId, int postId)
        {
            DivisionPost item = db.DivisionPosts.FirstOrDefault(p => p.StrucDivId == strucDivId && p.PostId == postId);
            if (item != null) db.DivisionPosts.Remove(item);
        }
    }
}
