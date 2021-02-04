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
    public class PostRepository : IRepository<Post>
    {
        private PhoneDirectoryContext db;

        public PostRepository(PhoneDirectoryContext context)
        {
            this.db = context;
        }
        public IEnumerable<Post> GetAll()
        {
            return db.Posts;
        }

        public Post Get(int id)
        {
            return db.Posts.Find(id);
        }

        public IEnumerable<Post> Find(Func<Post, bool> predicate)
        {
            return db.Posts.Where(predicate).ToList();
        }

        public void Create(Post item)
        {
            db.Posts.Add(item);
        }

        public void Update(Post item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Post item = db.Posts.Find(id);
            if (item != null) db.Posts.Remove(item);
        }
    }
}
