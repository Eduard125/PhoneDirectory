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
                    Role = DB.Roles.Get(3),
                    Name = registerDTO.Name,
                    Surname = registerDTO.Surname,
                    Patronymic = registerDTO.Patronymic,
                    PostId = registerDTO.PostId,
                    StrucDivId = registerDTO.StrucDivId,
                    Email = registerDTO.Email
                });
                DB.Save();
                DB.PersonalNumbers.Create(new PersonalNumber
                {
                    UserId = user.Id,
                    PersonalNum = registerDTO.PersonalNum
                });
                DB.Save();
                DB.DepartmentNumbers.Create(new DepartmentNumber
                {
                    StrucDivId = registerDTO.StrucDivId,
                    StrucDivNum = registerDTO.StrucDivNum
                });
                DB.Save();
                DB.DepartmentMobNumbers.Create(new DepartmentMobNumber
                {
                    StrucDivId = registerDTO.StrucDivId,
                    StrucDivMobNum = registerDTO.StrucDivMobNum
                });
                DB.Save();
            }
            else
            {
                throw new ValidationException("Пользователь с таким логином уже зарегистрирован!", "");
            }
        }

        public IEnumerable<StructuralDivisionDTO> GetStrucDivId()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivision, StructuralDivisionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<StructuralDivision>, List<StructuralDivisionDTO>>(DB.StructuralDivisions.GetAll());
        }

        //public IEnumerable<UserDTO> GetUsers()
        //{
        //    List<UserDTO> list = new List<UserDTO>();
        //    var users = DB.Users.GetAll().ToList();
        //    var posts = DB.Posts.GetAll().ToList();
        //    var structuralDivision = DB.StructuralDivisions.GetAll().ToList();
        //    foreach (var party in parties)
        //    {
        //        GroupDTO item = new GroupDTO
        //        {
        //            Id = party.Id,
        //            Discipline = party.Discipline.Name,
        //            Hall = party.Hall.Name,
        //            Coach = party.User.Surname + " " + party.User.Name + " " + party.User.Patronymic,
        //            Schedule = ""
        //        };
        //        item.IsSingle = party.MaxPupilsCnt == 1;
        //        item.PlaceCnt = party.MaxPupilsCnt - partyUsers.Count(p => p.PartyId == party.Id);
        //        item.Statement = StringHelper.GetStatement(item.PlaceCnt, item.IsSingle);
        //        item.Title = item.IsSingle ? "Индивидуальное занятие" : "Группа " + item.Id;
        //        foreach (var schedule in schedules.Where(p => p.PartyId == party.Id))
        //        {
        //            item.Schedule += schedule.Weekday.Design + " " + schedule.Period.Name + " ";
        //        }
        //        list.Add(item);
        //    }
        //    return list;
        //}
        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
