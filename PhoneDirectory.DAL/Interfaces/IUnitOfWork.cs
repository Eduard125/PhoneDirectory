using PhoneDirectory.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Post> Posts { get; }
        IRepository<StructuralDivision> StructuralDivisions { get; }
        IRepository<Role> Roles { get; }        
        IRepository<DepartmentNumber> DepartmentNumbers { get; }
        IRepositoryPU<DivisionPost> DivisionPosts { get; }
        IRepository<DepartmentMobNumber> DepartmentMobNumbers { get; }

        void Save();
    }
}
