using AutoMapper;
using PhoneDirectory.BLL.DTO;
using PhoneDirectory.BLL.Infrastructure;
using PhoneDirectory.BLL.Interfaces;
using PhoneDirectory.DAL.Entities;
using PhoneDirectory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneDirectory.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        IUnitOfWork DB { get; set; }
        public EmployeeService()
        {
        }
        public void InitDB(IUnitOfWork unitOfWork)
        {
            DB = unitOfWork;
        }

        public UserDTO GetUser(int id)
        {
            User user = DB.Users.Get(id);
            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
                RoleId = user.RoleId,
                Role = user.Role.Name,
                Surname = user.Surname,
                PersonalNum = user.PersonalNum,
                PersonalNum1 = user.PersonalNum1,
                Name = user.Name,
                Patronymic = user.Patronymic,
                FullName = user.Surname + " " + user.Name + " " + user.Patronymic,
                StrucDivId = user.StrucDivId,
                NameStrucDiv = user.StructuralDivision.NameStrucDiv,
                PostId = user.PostId,
                NamePost = user.Post.NamePost,
                Email = user.Email
            };
        }
        public IEnumerable<UserDTO> GetUsers(int strucDivId)
        {
            List<UserDTO> list = new List<UserDTO>();
            StructuralDivision structuralDivision = DB.StructuralDivisions.Get(strucDivId);
            if (structuralDivision == null)
            {
                return null;
            }
            else
            {
                var users = DB.Users.Find(p => p.StrucDivId == strucDivId).ToList();
                foreach (var user in users)
                {
                    User user1 = DB.Users.Get(user.Id);
                    UserDTO item = new UserDTO
                    {
                        Id = user1.Id,
                        Login = user1.Login,
                        RoleId = user1.RoleId,
                        Role = user1.Role.Name,
                        PersonalNum = user1.PersonalNum,
                        PersonalNum1 = user1.PersonalNum1,
                        Surname = user1.Surname,
                        Name = user1.Name,
                        Patronymic = user1.Patronymic,
                        FullName = user1.Surname + " " + user1.Name + " " + user1.Patronymic,
                        StrucDivId = user1.StrucDivId,
                        NameStrucDiv = user.StructuralDivision.NameStrucDiv,
                        PostId = user1.PostId,
                        NamePost = user.Post.NamePost,
                        Email = user.Email
                    };
                    list.Add(item);
                }
            }
            return list.OrderBy(p => p.FullName);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            List<UserDTO> list = new List<UserDTO>();

            var users = DB.Users.GetAll().ToList();
            foreach (var user in users)
            {
                if (user.RoleId == 2)
                {
                    UserDTO item = new UserDTO
                    {
                        Id = user.Id,
                        Login = user.Login,
                        RoleId = user.RoleId,
                        Role = user.Role.Name,
                        Surname = user.Surname,
                        Name = user.Name,
                        Patronymic = user.Patronymic,
                        PersonalNum = user.PersonalNum,
                        PersonalNum1 = user.PersonalNum1,
                        FullName = user.Surname + " " + user.Name + " " + user.Patronymic,
                        StrucDivId = user.StrucDivId,
                        NameStrucDiv = user.StructuralDivision.NameStrucDiv,
                        PostId = user.PostId,
                        NamePost = user.Post.NamePost,
                        Email = user.Email
                    };
                    list.Add(item);
                }
                else if (user.RoleId == 1)
                {
                    UserDTO item = new UserDTO
                    {
                        Id = user.Id,
                        Login = user.Login,
                        RoleId = user.RoleId,
                        Role = user.Role.Name,
                        Surname = user.Surname,
                        Name = user.Name,
                        Patronymic = user.Patronymic,
                        PersonalNum = user.PersonalNum,
                        PersonalNum1 = user.PersonalNum1,
                        FullName = user.Surname + " " + user.Name + " " + user.Patronymic,
                        StrucDivId = user.StrucDivId,
                        NameStrucDiv = user.StructuralDivision.NameStrucDiv,
                        PostId = user.PostId,
                        NamePost = user.Post.NamePost,
                        Email = user.Email
                    };
                    list.Add(item);
                }
            }
            return list.OrderBy(p => p.FullName);
        }

        public IEnumerable<StructuralDivisionDTO> GetStructuralDivisions()
        {
            List<StructuralDivisionDTO> list = new List<StructuralDivisionDTO>();

            var structuralDivisions = DB.StructuralDivisions.GetAll().ToList();
            var posts = DB.Posts.GetAll().ToList();
            var divisionPosts = DB.DivisionPosts.GetAll().ToList();
            var departmentNumbers = DB.DepartmentNumbers.GetAll().ToList();
            var departmentmobNumbers = DB.DepartmentMobNumbers.GetAll().ToList();
            foreach (var divisionPost in divisionPosts)
            {
                StructuralDivisionDTO item = new StructuralDivisionDTO
                {
                    Id = divisionPost.StrucDivId,
                    StrucDivId = divisionPost.StrucDivId,
                    NameStrucDiv = "",
                    PostId = divisionPost.PostId,
                    NamePost = "",
                    NameDepartmentNumber = "",
                    NameDepartmentNumber1 = "",
                    NameDepartmentMobNumber = "",
                    NameDepartmentMobNumber1 = "",
                };
                item.Title = "Отдел " + item.Id;
                foreach (var structuralDivision in structuralDivisions.Where(p => p.Id == divisionPost.StrucDivId))
                {
                    item.NameStrucDiv = structuralDivision.NameStrucDiv;
                }
                foreach (var post in posts.Where(p => p.Id == divisionPost.PostId))
                {
                    item.NamePost = post.NamePost;
                }
                foreach (var departmentNumber in departmentNumbers.Where(p => p.StrucDivId == divisionPost.StrucDivId))
                {
                    item.NameDepartmentNumber = departmentNumber.StrucDivNum;
                    item.NameDepartmentNumber1 = departmentNumber.StrucDivNum1;
                }
                foreach (var departmentmobNumber in departmentmobNumbers.Where(p => p.StrucDivId == divisionPost.StrucDivId))
                {
                    item.NameDepartmentMobNumber = departmentmobNumber.StrucDivMobNum;
                    item.NameDepartmentMobNumber1 = departmentmobNumber.StrucDivMobNum1;
                }
                list.Add(item);
            }
            return list;
        }

        public IEnumerable<StructuralDivisionDTO> GetStructuralDivisions1()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivision, StructuralDivisionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<StructuralDivision>, List<StructuralDivisionDTO>>(DB.StructuralDivisions.GetAll());
        }

        public IEnumerable<StructuralDivisionDTO> GetStructuralDivisions(StructuralDivisionsFilterDTO filter)
        {
            List<StructuralDivisionDTO> list = new List<StructuralDivisionDTO>();
            var structuralDivisions = DB.StructuralDivisions.GetAll().ToList();
            var posts = DB.Posts.GetAll().ToList();
            var divisionPosts = DB.DivisionPosts.GetAll().ToList();
            if (filter.UseStructuralDivision)
            {
                divisionPosts = divisionPosts.Where(p => p.StrucDivId == filter.StrucDivId).ToList();
            }
            if (filter.UsePost)
            {
                divisionPosts = divisionPosts.Where(p => p.PostId == filter.PostId).ToList();
            }

            foreach (var divisionPost in divisionPosts)
            {
                StructuralDivisionDTO item = new StructuralDivisionDTO
                {
                    StrucDivId = divisionPost.StrucDivId,
                    PostId = divisionPost.PostId,
                    NameStrucDiv = divisionPost.StructuralDivision.NameStrucDiv,
                    NamePost = divisionPost.Post.NamePost,
                    NameDepartmentNumber = divisionPost.DepartmentNumber.StrucDivNum,
                    NameDepartmentNumber1 = divisionPost.DepartmentNumber.StrucDivNum1,
                    NameDepartmentMobNumber = divisionPost.DepartmentMobNumber.StrucDivMobNum,
                    NameDepartmentMobNumber1 = divisionPost.DepartmentMobNumber.StrucDivMobNum1
                };
                list.Add(item);
            }
            return list;
        }

        public StructuralDivisionDTO GetStructuralDivision(int strucDivId)
        {
            StructuralDivision structuralDivision = DB.StructuralDivisions.Get(strucDivId);
            if (structuralDivision == null) return null;
            return new StructuralDivisionDTO
            {
                Id = structuralDivision.Id,
                NameStrucDiv = structuralDivision.NameStrucDiv
            };
        }

        public IEnumerable<PostDTO> GetPosts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Post>, List<PostDTO>>(DB.Posts.GetAll());
        }

        public DivisionPostDTO GetDivisionPost(int id)
        {
            var divisionPost = DB.DivisionPosts.Get(id);
            if (divisionPost == null)
            {
                return null;
            }
            else
            {
                var mapperDivisionPost = new MapperConfiguration(cfg => cfg.CreateMap<DivisionPost, DivisionPostDTO>()).CreateMapper();
                var dtoDivisionPost = mapperDivisionPost.Map<DivisionPost, DivisionPostDTO>(divisionPost);
                return dtoDivisionPost;
            }
        }

        public void DeleteDivisionPost(int id)
        {
            var divisionPost = DB.DivisionPosts.Get(id);
            if (divisionPost == null)
            {
                throw new ValidationException("Отдел не найден!", "");
            }
            else
            {
                var divisionPosts = DB.DivisionPosts.Find(p => p.Id == id).ToList();
                foreach (var divisionPost1 in divisionPosts)
                {
                    DB.DivisionPosts.Delete(divisionPost1.StrucDivId, divisionPost1.PostId);
                }
                var posts = DB.Posts.Find(p => p.Id == id).ToList();
                foreach (var post in posts)
                {
                    DB.Posts.Delete(post.Id);
                }
                DB.Users.Delete(id);
                DB.Save();
            }
        }

    }
}
