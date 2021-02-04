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
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                FullName = user.Surname + " " + user.Name + " " + user.Patronymic,
                PostId = user.PostId,
                StrucDivId = user.StrucDivId
            };
        }
        public void CreateUser(UserDTO userDTO, string password)
        {
            User user = DB.Users.Find(p => p.Login == userDTO.Login).FirstOrDefault();
            if (user == null)
            {
                DB.Users.Create(new User
                {
                    Id = userDTO.Id,
                    Login = userDTO.Login,
                    Password = StringHelper.GetMD5(password),
                    RoleId = userDTO.RoleId,
                    Surname = userDTO.Surname,
                    Name = userDTO.Name,
                    Patronymic = userDTO.Patronymic,
                    PostId = userDTO.PostId,
                    StrucDivId = userDTO.StrucDivId,
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
                user.Surname = userDTO.Surname;
                user.Name = userDTO.Name;
                user.Patronymic = userDTO.Patronymic;
                user.PostId = userDTO.PostId;
                user.StrucDivId = userDTO.StrucDivId;
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
            else if (user.RoleId == 2)
            {
                var structuralDivisions = DB.StructuralDivisions.Find(p => p.Id == id).ToList();
                foreach (var structuralDivision in structuralDivisions)
                {
                    var divisionPosts = DB.DivisionPosts.Find(p => p.StrucDivId == structuralDivision.Id).ToList();
                    foreach (var divisionPost in divisionPosts)
                    {
                        DB.DivisionPosts.Delete(divisionPost.StrucDivId, divisionPost.PostId);
                    }
                    var posts = DB.Posts.Find(p => p.Id == structuralDivision.Id).ToList();
                    foreach (var post in posts)
                    {
                        DB.Posts.Delete(post.Id);
                    }

                    DB.StructuralDivisions.Delete(structuralDivision.Id);
                }
                DB.Users.Delete(id);
                DB.Save();
            }
            else
            {
                DB.Users.Delete(id);
                DB.Save();
            }
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

        public StructuralDivisionDTO GetStructuralDivision(int id)
        {
            StructuralDivision structuralDivision = DB.StructuralDivisions.Get(id);
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


        public PersonalNumberDTO GetPersonalNumber(int id)
        {
            PersonalNumber personalNumber = DB.PersonalNumbers.Get(id);
            if (personalNumber == null) return null;
            return new PersonalNumberDTO
            {
                Id = personalNumber.Id,
                UserId = personalNumber.UserId,
                PersonalNum = personalNumber.PersonalNum
            };
        }
        public void CreatePersonalNumber(PersonalNumberDTO personalNumberDTO)
        {
            PersonalNumber personalNumber = DB.PersonalNumbers.Find(p => p.PersonalNum == personalNumberDTO.PersonalNum).FirstOrDefault();
            if (personalNumber == null)
            {
                DB.PersonalNumbers.Create(new PersonalNumber
                {
                    Id = personalNumberDTO.Id,
                    UserId = personalNumberDTO.UserId,
                    PersonalNum = personalNumberDTO.PersonalNum
                });
                DB.Save();
            }
            else
            {
                throw new ValidationException("Такой личный номер уже существует!", "");
            }
        }
        public void UpdatePersonalNumber(PersonalNumberDTO personalNumberDTO)
        {
            PersonalNumber personalNumber = DB.PersonalNumbers.Get(personalNumberDTO.Id);
            if (personalNumber == null)
            {
                throw new ValidationException("Личный номер не найден!", "");
            }
            else
            {
                personalNumber.Id = personalNumberDTO.Id;
                personalNumber.UserId = personalNumberDTO.UserId;
                DB.PersonalNumbers.Update(personalNumber);
                DB.Save();
            }
        }
        public void DeletePersonalNumber(int id)
        {
            PersonalNumber personalNumber = DB.PersonalNumbers.Get(id);
            if (personalNumber == null)
            {
                throw new ValidationException("Личный номер не найден!", "");
            }
            else
            {
                DB.PersonalNumbers.Delete(id);
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
                StrucDivNum = departmentNumber.StrucDivNum
            };
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
                    StrucDivNum = departmentNumberDTO.StrucDivNum
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
                StrucDivMobNum = departmentMobNumber.StrucDivMobNum
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
                    StrucDivMobNum = departmentMobNumberDTO.StrucDivMobNum
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
    }
}
