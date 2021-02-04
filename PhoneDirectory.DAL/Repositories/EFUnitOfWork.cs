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
        public EFUnitOfWork(string connectionString)
        {
            db = new PhoneDirectoryContext(connectionString);
        }
        public IRepository<User> Users => throw new NotImplementedException();

        public IRepository<Role> Roles => throw new NotImplementedException();

        public IRepository<StructuralDivision> StructuralDivisions => throw new NotImplementedException();

        public IRepository<Post> Posts => throw new NotImplementedException();

        public IRepositoryPU<DivisionPost> DivisionPosts => throw new NotImplementedException();

        public IRepository<PersonalNumber> PersonalNumbers => throw new NotImplementedException();

        public IRepository<DepartmentNumber> DepartmentNumbers => throw new NotImplementedException();

        public IRepository<DepartmentMobNumber> DepartmentMobNumbers => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
