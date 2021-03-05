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
    public class AdminService : IAdminService
    {
        IUnitOfWork DB { get; set; }
        public AdminService()
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

        public void CreateUser(UserDTO userDTO, string password)
        {
            User user = DB.Users.Find(p => p.Login == userDTO.Login).FirstOrDefault();
            if (user == null)
            {
                DB.Users.Create(new User
                {                    
                    Login = userDTO.Login,
                    Password = StringHelper.GetMD5(password),
                    RoleId = userDTO.RoleId,
                    PersonalNum = userDTO.PersonalNum,
                    PersonalNum1 = userDTO.PersonalNum1,
                    Surname = userDTO.Surname,
                    Name = userDTO.Name,
                    Patronymic = userDTO.Patronymic,
                    StrucDivId = userDTO.StrucDivId,
                    PostId = userDTO.PostId,                    
                    Email = userDTO.Email
                });
                DB.Save();
            }
            else
            {
                throw new ValidationException("Пользователь с таким именем уже существует!", "");
            }
        }

        public void UpdateUser(UserDTO userDTO)
        {
            User user = DB.Users.Get(userDTO.Id);
            if (user == null)
            {
                throw new ValidationException("Пользователь не найден!", "");
            }
            else
            {
                user.PersonalNum = userDTO.PersonalNum;
                user.PersonalNum1 = userDTO.PersonalNum1;
                user.Surname = userDTO.Surname;
                user.Name = userDTO.Name;
                user.Patronymic = userDTO.Patronymic;
                user.StrucDivId = userDTO.StrucDivId;
                user.PostId = userDTO.PostId;
                user.Email = userDTO.Email;
                DB.Users.Update(user);
                DB.Save();
            }
        }

        public void DeleteUser(int id, string login)
        {
            User user = DB.Users.Get(id);
            if (user == null)
            {
                throw new ValidationException("Пользователь не найден!", "");
            }
            else if (user.Login == login)
            {
                throw new ValidationException("Вы не можете удалить собственную запись!", "");
            }            
            else
            {
                DB.Users.Delete(id);
                DB.Save();
            }
        }

        public RoleDTO GetRole(int id)
        {
            if (id < 1 || id > 2) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<Role, RoleDTO>(DB.Roles.Get(id));
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

        public IEnumerable<PostDTO> GetPosts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Post>, List<PostDTO>>(DB.Posts.GetAll());
        }

        public PostDTO GetPost(int id)
        {
            Post post = DB.Posts.Get(id);
            if (post == null) return null;
            return new PostDTO
            {
                Id = post.Id,
                NamePost = post.NamePost
            };
        }
        public void CreatePost(PostDTO postDTO)
        {
            Post post = DB.Posts.Find(p => p.NamePost == postDTO.NamePost).FirstOrDefault();
            if (post == null)
            {
                DB.Posts.Create(new Post
                {
                    Id = postDTO.Id,
                    NamePost = postDTO.NamePost
                });
                DB.Save();
            }
            else
            {
                throw new ValidationException("Должность с таким названием уже существует!", "");
            }
        }
        public void UpdatePost(PostDTO postDTO)
        {
            Post post = DB.Posts.Get(postDTO.Id);
            if (post == null)
            {
                throw new ValidationException("Должность не найдена!", "");
            }
            else
            {
                post.Id = postDTO.Id;
                post.NamePost = postDTO.NamePost;
                DB.Posts.Update(post);
                DB.Save();
            }
        }
        public void DeletePost(int id)
        {
            Post post = DB.Posts.Get(id);
            if (post == null)
            {
                throw new ValidationException("Должность не найдена!", "");
            }
            else
            {
                DB.Posts.Delete(id);
                DB.Save();
            }
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

        public void CreateStructuralDivision(StructuralDivisionDTO structuralDivisionDTO)
        {
            StructuralDivision structurDivision = DB.StructuralDivisions.Find(p => p.NameStrucDiv == structuralDivisionDTO.NameStrucDiv).FirstOrDefault();
            if (structurDivision == null)
            {
                DB.StructuralDivisions.Create(new StructuralDivision
                {
                    Id = structuralDivisionDTO.Id,
                    NameStrucDiv = structuralDivisionDTO.NameStrucDiv
                });
                DB.Save();
            }
            else
            {
                throw new ValidationException("Структурное подразделение с таким названием уже существует!", "");
            }
        }
        public void UpdateStructuralDivision(StructuralDivisionDTO structuralDivisionDTO)
        {
            StructuralDivision structurDivision = DB.StructuralDivisions.Get(structuralDivisionDTO.Id);
            if (structurDivision == null)
            {
                throw new ValidationException("Структурное подразделение не найдено!", "");
            }
            else
            {
                structurDivision.Id = structuralDivisionDTO.Id;
                structurDivision.NameStrucDiv = structuralDivisionDTO.NameStrucDiv;
                DB.StructuralDivisions.Update(structurDivision);
                DB.Save();
            }
        }
        public void DeleteStructuralDivision(int id)
        {
            StructuralDivision structurDivision = DB.StructuralDivisions.Get(id);
            if (structurDivision == null)
            {
                throw new ValidationException("Структурное подразделение не найдено!", "");
            }
            else
            {
                DB.StructuralDivisions.Delete(id);
                DB.Save();
            }
        }        

        public DepartmentNumberDTO GetDepartmentNumber(int id)
        {
            DepartmentNumber departmentNumber = DB.DepartmentNumbers.Get(id);
            if (departmentNumber == null) return null;
            return new DepartmentNumberDTO
            {
                Id = departmentNumber.Id,
                StrucDivId = departmentNumber.StrucDivId,
                StrucDivNum = departmentNumber.StrucDivNum,
                StrucDivNum1 = departmentNumber.StrucDivNum1
            };
        }

        public IEnumerable<DepartmentNumberDTO> GetDepartmentNumbers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentNumber, DepartmentNumberDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<DepartmentNumber>, List<DepartmentNumberDTO>>(DB.DepartmentNumbers.GetAll());
        }
        public void CreateDepartmentNumber(DepartmentNumberDTO departmentNumberDTO)
        {
            DepartmentNumber departmentNumber = DB.DepartmentNumbers.Find(p => p.StrucDivNum == departmentNumberDTO.StrucDivNum).FirstOrDefault();
            if (departmentNumber == null)
            {
                DB.DepartmentNumbers.Create(new DepartmentNumber
                {
                    Id = departmentNumberDTO.Id,
                    StrucDivId = departmentNumberDTO.StrucDivId,
                    StrucDivNum = departmentNumberDTO.StrucDivNum,
                    StrucDivNum1 = departmentNumberDTO.StrucDivNum1
                });
                DB.Save();
            }
            else
            {
                throw new ValidationException("Такой служебный номер уже существует!", "");
            }
        }
        public void UpdateDepartmentNumber(DepartmentNumberDTO departmentNumberDTO)
        {
            DepartmentNumber departmentNumber = DB.DepartmentNumbers.Get(departmentNumberDTO.Id);
            if (departmentNumber == null)
            {
                throw new ValidationException("Cлужебный номер не найден!", "");
            }
            else
            {
                departmentNumber.Id = departmentNumberDTO.Id;
                departmentNumber.StrucDivId = departmentNumberDTO.StrucDivId;
                departmentNumber.StrucDivNum = departmentNumberDTO.StrucDivNum;
                departmentNumber.StrucDivNum1 = departmentNumberDTO.StrucDivNum1;
                DB.DepartmentNumbers.Update(departmentNumber);
                DB.Save();
            }
        }
        public void DeleteDepartmentNumber(int id)
        {
            DepartmentNumber departmentNumber = DB.DepartmentNumbers.Get(id);
            if (departmentNumber == null)
            {
                throw new ValidationException("Cлужебный номер не найден!", "");
            }
            else
            {
                DB.DepartmentNumbers.Delete(id);
                DB.Save();
            }
        }

        public DepartmentMobNumberDTO GetDepartmentMobNumber(int id)
        {
            DepartmentMobNumber departmentMobNumber = DB.DepartmentMobNumbers.Get(id);
            if (departmentMobNumber == null) return null;
            return new DepartmentMobNumberDTO
            {
                Id = departmentMobNumber.Id,
                StrucDivId = departmentMobNumber.StrucDivId,
                StrucDivMobNum = departmentMobNumber.StrucDivMobNum,
                StrucDivMobNum1 = departmentMobNumber.StrucDivMobNum1
            };
        }
        public void CreateDepartmentMobNumber(DepartmentMobNumberDTO departmentMobNumberDTO)
        {
            DepartmentMobNumber departmentMobNumber = DB.DepartmentMobNumbers.Find(p => p.StrucDivMobNum == departmentMobNumberDTO.StrucDivMobNum).FirstOrDefault();
            if (departmentMobNumber == null)
            {
                DB.DepartmentMobNumbers.Create(new DepartmentMobNumber
                {
                    Id = departmentMobNumberDTO.Id,
                    StrucDivId = departmentMobNumberDTO.StrucDivId,
                    StrucDivMobNum = departmentMobNumberDTO.StrucDivMobNum,
                    StrucDivMobNum1 = departmentMobNumberDTO.StrucDivMobNum1
                });
                DB.Save();
            }
            else
            {
                throw new ValidationException("Такой служебный моб. номер уже существует!", "");
            }
        }
        public void UpdateDepartmentMobNumber(DepartmentMobNumberDTO departmentMobNumberDTO)
        {
            DepartmentMobNumber departmentMobNumber = DB.DepartmentMobNumbers.Get(departmentMobNumberDTO.Id);
            if (departmentMobNumber == null)
            {
                throw new ValidationException("Cлужебный моб. номер не найден!", "");
            }
            else
            {
                departmentMobNumber.Id = departmentMobNumberDTO.Id;
                departmentMobNumber.StrucDivId = departmentMobNumberDTO.StrucDivId;
                departmentMobNumber.StrucDivMobNum = departmentMobNumberDTO.StrucDivMobNum;
                departmentMobNumber.StrucDivMobNum1 = departmentMobNumberDTO.StrucDivMobNum1;
                DB.DepartmentMobNumbers.Update(departmentMobNumber);
                DB.Save();
            }
        }
        public void DeleteDepartmentMobNumber(int id)
        {
            DepartmentMobNumber departmentMobNumber = DB.DepartmentMobNumbers.Get(id);
            if (departmentMobNumber == null)
            {
                throw new ValidationException("Cлужебный моб. номер не найден!", "");
            }
            else
            {
                DB.DepartmentMobNumbers.Delete(id);
                DB.Save();
            }
        }

        public IEnumerable<StructuralDivisionsAllowsDTO> GetStructuralDivisionsAllows()
        {
            var list = new List<StructuralDivisionsAllowsDTO>();
            foreach (var divisionPost in DB.DivisionPosts.GetAll().ToList())
            {
                var item = new StructuralDivisionsAllowsDTO
                {
                    StrucDivId = divisionPost.StrucDivId,
                    NameStrucDiv = divisionPost.StructuralDivision.NameStrucDiv,
                    PostId = divisionPost.PostId,
                    NamePost = divisionPost.Post.NamePost                    
                };
                list.Add(item);
            }
            return list;
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

        public void UpdateDivisionPost(DivisionPostDTO divisionPostDTO)
        {
            var divisionPost = DB.DivisionPosts.Get(divisionPostDTO.Id);
            var structuralDivisions = DB.StructuralDivisions.Get(divisionPostDTO.StrucDivId);
            var posts = DB.Posts.Get(divisionPostDTO.PostId);            
            var departmentNumber = DB.DepartmentNumbers.Get(divisionPostDTO.StrucDivId);            
            if (divisionPost == null)
            {
                throw new ValidationException("Отдел не найден!", "");
            }
            else
            {
                if (DB.DivisionPosts.Find(p => p.StrucDivId == divisionPostDTO.StrucDivId).Count() > 0)
                {                   
                    departmentNumber.StrucDivNum = divisionPostDTO.StrucDivNum;
                    departmentNumber.StrucDivNum1 = divisionPostDTO.StrucDivNum1;                   
                    DB.DepartmentNumbers.Update(departmentNumber);
                    DB.Save();
                }                
                else
                {
                    throw new ValidationException("Отдел и должность недоступны!", "");
                }                
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

