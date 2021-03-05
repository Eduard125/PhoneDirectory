using PhoneDirectory.DAL.EF;
using PhoneDirectory.DAL.Entities;
using PhoneDirectory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private PhoneDirectoryContext db;
        private UserRepository userRepository;
        private StructuralDivisionRepository structuralDivisionRepository;
        private RoleRepository roleRepository;
        private PostRepository postRepository;
        private DivisionPostRepository divisionPostRepository;              
        private DepartmentNumberRepository departmentNumberRepository;
        private DepartmentMobNumberRepository departmentMobNumberRepository;

        private bool disposed = false;
        public EFUnitOfWork(string connectionString)
        {
            db = new PhoneDirectoryContext(connectionString);
        }
       
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null) userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null) roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public IRepository<StructuralDivision> StructuralDivisions
        {
            get
            {
                if (structuralDivisionRepository == null) structuralDivisionRepository = new StructuralDivisionRepository(db);
                return structuralDivisionRepository;
            }
        }

        public IRepositoryPU<DivisionPost> DivisionPosts
        {
            get
            {
                if (divisionPostRepository == null) divisionPostRepository = new DivisionPostRepository(db);
                return divisionPostRepository;
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                if (postRepository == null) postRepository = new PostRepository(db);
                return postRepository;
            }
        }  
        
        public IRepository<DepartmentNumber> DepartmentNumbers
        {
            get
            {
                if (departmentNumberRepository == null) departmentNumberRepository = new DepartmentNumberRepository(db);
                return departmentNumberRepository;
            }
        }

        public IRepository<DepartmentMobNumber> DepartmentMobNumbers
        {
            get
            {
                if (departmentMobNumberRepository == null) departmentMobNumberRepository = new DepartmentMobNumberRepository(db);
                return departmentMobNumberRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
