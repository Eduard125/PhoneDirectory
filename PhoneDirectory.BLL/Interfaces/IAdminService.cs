using PhoneDirectory.BLL.DTO;
using PhoneDirectory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.BLL.Interfaces
{
    public interface IAdminService
    {
        void InitDB(IUnitOfWork unitOfWork);

        UserDTO GetUser(int id);        
        IEnumerable<UserDTO> GetUsers(int structuralDivisionId);
        IEnumerable<UserDTO> GetUsers();
        void CreateUser(UserDTO userDTO, string password);
        void UpdateUser(UserDTO userDTO);
        void DeleteUser(int id, string login);

        RoleDTO GetRole(int id);
        IEnumerable<StructuralDivisionDTO> GetStructuralDivisions();
        IEnumerable<StructuralDivisionDTO> GetStructuralDivisions1();

        IEnumerable<StructuralDivisionDTO> GetStructuralDivisions(StructuralDivisionsFilterDTO filter);
        IEnumerable<StructuralDivisionsAllowsDTO> GetStructuralDivisionsAllows();
        
        IEnumerable<PostDTO> GetPosts();

        StructuralDivisionDTO GetStructuralDivision(int id);
        void CreateStructuralDivision(StructuralDivisionDTO structuralDivisionDTO);
        void UpdateStructuralDivision(StructuralDivisionDTO structuralDivisionDTO);
        void DeleteStructuralDivision(int id);

        PostDTO GetPost(int id);
        void CreatePost(PostDTO postDTO);
        void UpdatePost(PostDTO postDTO);
        void DeletePost(int id);
        
        DepartmentNumberDTO GetDepartmentNumber(int id);
        IEnumerable<DepartmentNumberDTO> GetDepartmentNumbers();
        void CreateDepartmentNumber(DepartmentNumberDTO departmentNumberDTO);
        void UpdateDepartmentNumber(DepartmentNumberDTO departmentNumberDTO);
        void DeleteDepartmentNumber(int id);

        DepartmentMobNumberDTO GetDepartmentMobNumber(int id);
        void CreateDepartmentMobNumber(DepartmentMobNumberDTO departmentMobNumberDTO);
        void UpdateDepartmentMobNumber(DepartmentMobNumberDTO departmentMobNumberDTO);
        void DeleteDepartmentMobNumber(int id);

        DivisionPostDTO GetDivisionPost(int id);
        void UpdateDivisionPost(DivisionPostDTO divisionPostDTO);
        void DeleteDivisionPost(int id);
    }
}


