using PhoneDirectory.BLL.DTO;
using PhoneDirectory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.Interfaces
{
    public interface IHomeService
    {
        void InitDB(IUnitOfWork unitOfWork);
        AccountDTO Login(string login, string password);
        AccountDTO GetByLogin(string login);
        void Register(RegisterDTO registerDTO);
        IEnumerable<StructuralDivisionDTO> GetStructuralDivisions();
        IEnumerable<DepartmentNumberDTO> GetDepartmentNumbers();
        IEnumerable<PostDTO> GetPosts();
        IEnumerable<RoleDTO> GetRoles();
        void Dispose();
    }
}


