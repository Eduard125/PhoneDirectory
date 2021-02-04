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
        //IEnumerable<UserDTO> GetUsers();    
        IEnumerable<StructuralDivisionDTO> GetStrucDivId();
        void Dispose();
    }
}
