using PhoneDirectory.BLL.DTO;
using PhoneDirectory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.Interfaces
{
    public interface IEmployeeService
    {
        void InitDB(IUnitOfWork unitOfWork);

        UserDTO GetUser(int id);
        IEnumerable<UserDTO> GetUsers(int structuralDivisionId);
        IEnumerable<UserDTO> GetUsers();

        IEnumerable<StructuralDivisionDTO> GetStructuralDivisions();
        IEnumerable<StructuralDivisionDTO> GetStructuralDivisions1();
        IEnumerable<StructuralDivisionDTO> GetStructuralDivisions(StructuralDivisionsFilterDTO filter);
        StructuralDivisionDTO GetStructuralDivision(int id);

        IEnumerable<PostDTO> GetPosts();

        DivisionPostDTO GetDivisionPost(int id);        
        
    }
}
