using PhoneDirectory.BLL.DTO;
using PhoneDirectory.BLL.Infrastructure;
using PhoneDirectory.BLL.Interfaces;
using PhoneDirectory.DAL.Entities;
using PhoneDirectory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace PhoneDirectory.BLL.Services
{
    public class HomeService : IHomeService
    {
        IUnitOfWork DB { get; set; }
        public HomeService()
        {
        }
        public void InitDB(IUnitOfWork unitOfWork)
        {
            DB = unitOfWork;
        }

        public AccountDTO Login(string login, string password)
        {
            string pswrd = StringHelper.GetMD5(password);
            User user = DB.Users.Find(p => p.Login == login && p.Password == pswrd).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else
            {
                return new AccountDTO { Id = user.Id, Login = user.Login, FullName = user.Surname + " " + user.Name + " " + user.Patronymic, RoleId = user.RoleId, RoleDesign = user.Role.Design };
            }
        }
        public AccountDTO GetByLogin(string login)
        {
            User user = DB.Users.Find(p => p.Login == login).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else
            {
                return new AccountDTO { Id = user.Id, Login = user.Login, FullName = user.Surname + " " + user.Name + " " + user.Patronymic, RoleId = user.RoleId, RoleDesign = user.Role.Design };
            }
        }




        public void Register(RegisterDTO registerDTO)
        {
            User user = DB.Users.Find(p => p.Login == registerDTO.Login).FirstOrDefault();
            if (user == null)
            {

                DB.Users.Create(new User
                {
                    Login = registerDTO.Login,
                    Password = StringHelper.GetMD5(registerDTO.Password),
                    Role = DB.Roles.Get(2),
                    PersonalNum = registerDTO.PersonalNum,
                    PersonalNum1 = registerDTO.PersonalNum1,
                    Surname = registerDTO.Surname,
                    Name = registerDTO.Name,                    
                    Patronymic = registerDTO.Patronymic,
                    StrucDivId = registerDTO.StrucDivId,
                    PostId = registerDTO.PostId,
                    Email = registerDTO.Email
                });
                DB.Save();                
            }
            else
            {
                throw new ValidationException("Пользователь с таким логином уже зарегистрирован!", "");
            }
        }
        public IEnumerable<StructuralDivisionDTO> GetStructuralDivisions()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivision, StructuralDivisionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<StructuralDivision>, List<StructuralDivisionDTO>>(DB.StructuralDivisions.GetAll());
        }

        public IEnumerable<DepartmentNumberDTO> GetDepartmentNumbers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentNumber, DepartmentNumberDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<DepartmentNumber>, List<DepartmentNumberDTO>>(DB.DepartmentNumbers.GetAll());
        }

        public IEnumerable<PostDTO> GetPosts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Post>, List<PostDTO>>(DB.Posts.GetAll());
        }
        public IEnumerable<RoleDTO> GetRoles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Role>, List<RoleDTO>>(DB.Roles.GetAll());
        }
        public void Dispose()
        {
            DB.Dispose();
        }
    }
}


