using PhoneDirectory.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }        
        IRepository<Role> Roles { get; }
        IRepository<StructuralDivision> StructuralDivisions { get; }
        IRepository<Post> Posts { get; }
        IRepositoryPU<DivisionPost> DivisionPosts { get; }
        IRepository<PersonalNumber> PersonalNumbers { get; }
        IRepository<DepartmentNumber> DepartmentNumbers { get; }
        IRepository<DepartmentMobNumber> DepartmentMobNumbers { get; }
        void Save();
    }
}
