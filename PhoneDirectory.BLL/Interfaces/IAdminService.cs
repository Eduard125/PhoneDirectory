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
        void CreateUser(UserDTO userDTO, string password);
        void UpdateUser(UserDTO userDTO);
        void DeleteUser(int id, string login);

        StructuralDivisionDTO GetStructuralDivision(int id);
        void CreateStructuralDivision(StructuralDivisionDTO structuralDivisionDTO);
        void UpdateStructuralDivision(StructuralDivisionDTO structuralDivisionDTO);
        void DeleteStructuralDivision(int id);

        PostDTO GetPost(int id);
        void CreatePost(PostDTO postDTO);
        void UpdatePost(PostDTO postDTO);
        void DeletePost(int id);

        PersonalNumberDTO GetPersonalNumber(int id);
        void CreatePersonalNumber(PersonalNumberDTO personalNumberDTO);
        void UpdatePersonalNumber(PersonalNumberDTO personalNumberDTO);
        void DeletePersonalNumber(int id);

        DepartmentMobNumberDTO GetDepartmentMobNumber(int id);
        void CreateDepartmentMobNumber(DepartmentMobNumberDTO departmentMobNumberDTO);
        void UpdateDepartmentMobNumber(DepartmentMobNumberDTO departmentMobNumberDTO);
        void DeleteDepartmentMobNumber(int id);

        DepartmentNumberDTO GetDepartmentNumber(int id);
        void CreateDepartmentNumber(DepartmentNumberDTO departmentNumberDTO);
        void UpdateDepartmentNumber(DepartmentNumberDTO departmentNumberDTO);
        void DeleteDepartmentNumber(int id);
    }
}
