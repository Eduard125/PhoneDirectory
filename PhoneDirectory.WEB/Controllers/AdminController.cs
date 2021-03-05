using AutoMapper;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneDirectory.BLL.DTO;
using PhoneDirectory.BLL.Infrastructure;
using PhoneDirectory.BLL.Interfaces;
using PhoneDirectory.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IAdminService adminService;

        public AdminController(IAdminService service)
        {
            // Сервис, внедренный через зависимость
            adminService = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllNumbers()
        {
            IEnumerable<UserDTO> dtosUsers = adminService.GetUsers();
            var mapperUsers = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            var users = mapperUsers.Map<IEnumerable<UserDTO>, List<UserViewModel>>(dtosUsers);
            return View(users);
        }


        public IActionResult Users(int? id)
        {
            if (id == null)
            {
                IEnumerable<UserDTO> dtosUsers = adminService.GetUsers();
                if (dtosUsers == null) return NotFound();
                var mapperUsers = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
                var model = mapperUsers.Map<IEnumerable<UserDTO>, List<UserViewModel>>(dtosUsers);
                return View("Users", model);
            }
            else
            {
                StructuralDivisionDTO dtostructuralDivision = adminService.GetStructuralDivision((int)id);
                if (dtostructuralDivision == null) return NotFound();
                IEnumerable<UserDTO> dtosUsers = adminService.GetUsers(dtostructuralDivision.Id);
                if (dtosUsers == null) return NotFound();
                var mapperGroup = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionDTO, StructuralDivisionViewModel>()).CreateMapper();
                var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
                StructuralDivisionUsersViewModel model = new StructuralDivisionUsersViewModel
                {
                    StructuralDivision = mapperGroup.Map<StructuralDivisionDTO, StructuralDivisionViewModel>(dtostructuralDivision),
                    Users = mapperUser.Map<IEnumerable<UserDTO>, List<UserViewModel>>(dtosUsers)
                };
                return View("StructuralDivisionUsers", model);
            }
        }

        public IActionResult DepStaff(int? id)
        {
            if (id == null)
            {
                IEnumerable<UserDTO> dtosUsers = adminService.GetUsers();
                if (dtosUsers == null) return NotFound();
                var mapperStaff = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
                var model = mapperStaff.Map<IEnumerable<UserDTO>, List<UserViewModel>>(dtosUsers);
                return View("DepStaff", model);
            }
            else
            {
                StructuralDivisionDTO dtostructuralDivision = adminService.GetStructuralDivision((int)id);
                if (dtostructuralDivision == null) return NotFound();
                IEnumerable<UserDTO> dtosUsers = adminService.GetUsers(dtostructuralDivision.Id);
                if (dtosUsers == null) return NotFound();
                var mapperStructuralDivision = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionDTO, StructuralDivisionViewModel>()).CreateMapper();
                var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
                StructuralDivisionUsersViewModel model = new StructuralDivisionUsersViewModel
                {
                    StructuralDivision = mapperStructuralDivision.Map<StructuralDivisionDTO, StructuralDivisionViewModel>(dtostructuralDivision),
                    Users = mapperUser.Map<IEnumerable<UserDTO>, List<UserViewModel>>(dtosUsers)
                };
                return View("StructuralDivisionUsers", model);
            }
        }

        
        public IActionResult StructuralDivisions()
        {
            StructuralDivisionsFilterViewModel filter = new StructuralDivisionsFilterViewModel { StrucDivId = 1, PostId = 1, UseStructuralDivision = false, UsePost= false };            
            var dtosStructuralDivisions = adminService.GetStructuralDivisions();
            var mapperStructuralDivision = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionDTO, StructuralDivisionViewModel>()).CreateMapper();
            ViewData["StructuralDivisions"] = mapperStructuralDivision.Map<IEnumerable<StructuralDivisionDTO>, List<StructuralDivisionViewModel>>(dtosStructuralDivisions);
            ViewData["StrucDivId"] = new SelectList(adminService.GetStructuralDivisions1(), "Id", "NameStrucDiv", filter.StrucDivId);            
            ViewData["PostId"] = new SelectList(adminService.GetPosts(), "Id", "NamePost", filter.PostId);           
            return View(filter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StructuralDivisions(StructuralDivisionsFilterViewModel filter)
        {           
            var mapperFilter = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionsFilterViewModel, StructuralDivisionsFilterDTO>()).CreateMapper();
            var dtosStructuralDivisions = adminService.GetStructuralDivisions(mapperFilter.Map<StructuralDivisionsFilterViewModel, StructuralDivisionsFilterDTO>(filter));            
            var mapperGroup = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionDTO, StructuralDivisionViewModel>()).CreateMapper();
            ViewData["StructuralDivisions"] = mapperGroup.Map<IEnumerable<StructuralDivisionDTO>, List<StructuralDivisionViewModel>>(dtosStructuralDivisions);
            ViewData["StrucDivId"] = new SelectList(adminService.GetStructuralDivisions(), "Id", "NameStrucDiv", filter.StrucDivId);            
            ViewData["PostId"] = new SelectList(adminService.GetPosts(), "Id", "NamePost", filter.PostId);          
            return View(filter);
        }

        [HttpGet]
        public ActionResult StructuralDivisionEdit(int? id)
        {
            if (id == null) return NotFound();
            var dtoDivisionPost = adminService.GetDivisionPost((int)id);
            var mapperDivisionPost = new MapperConfiguration(cfg => cfg.CreateMap<DivisionPostDTO, StructuralDivisionCreateViewModel>()).CreateMapper();            
            StructuralDivisionCreateViewModel model = new StructuralDivisionCreateViewModel { StrucDivId = 1 };
            ViewData["StrucDivId"] = new SelectList(adminService.GetStructuralDivisions1(), "Id", "NameStrucDiv", model.StrucDivId);            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StructuralDivisionEdit(StructuralDivisionCreateViewModel model)
        {            
            if (ModelState.IsValid)
            {
                try
                {
                    var mapperDivisionPost = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionCreateViewModel, DivisionPostDTO>()).CreateMapper();
                    var dtoDivisionPost = mapperDivisionPost.Map<StructuralDivisionCreateViewModel, DivisionPostDTO>(model);
                    ViewData["StrucDivId"] = new SelectList(adminService.GetStructuralDivisions1(), "Id", "NameStrucDiv", model.StrucDivId);                    
                    adminService.UpdateDivisionPost(dtoDivisionPost);
                    return RedirectToAction("StructuralDivisions");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }            
            return View(model);
        }

        [HttpGet]
        public IActionResult StructuralDivisionDelete(int? id)
        {
            if (id == null) return NotFound();
            DivisionPostDTO dtodivisionPost = adminService.GetDivisionPost((int)id);
            if (dtodivisionPost == null) return NotFound();
            var mapperGroup = new MapperConfiguration(cfg => cfg.CreateMap<DivisionPostDTO, StructuralDivisionViewModel>()).CreateMapper();
            var model = mapperGroup.Map<DivisionPostDTO, StructuralDivisionViewModel>(dtodivisionPost);
            return View(model);
        }

        [HttpPost, ActionName("StructuralDivisionDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult StructuralDivisionDeleteConfirmed(int id)
        {
            try
            {
                adminService.DeleteDivisionPost(id);
                return RedirectToAction("StructuralDivisions");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            DivisionPostDTO dtoDivisionPost = adminService.GetDivisionPost(id);
            if (dtoDivisionPost == null) return NotFound();
            var mapperGroup = new MapperConfiguration(cfg => cfg.CreateMap<DivisionPostDTO, StructuralDivisionViewModel>()).CreateMapper();
            var model = mapperGroup.Map<DivisionPostDTO, StructuralDivisionViewModel>(dtoDivisionPost);
            return View(model);
        }

        [HttpGet]
        public IActionResult UserCreate(int? id)
        {
            if (id == null || id < 1 || id > 2) return NotFound();
            var dtoRole = adminService.GetRole((int)id);
            ViewData["Role"] = dtoRole.Name;
            RegisterViewModel model = new RegisterViewModel { StrucDivId = 1, PostId = 1 };
            ViewData["StrucDivId"] = new SelectList(adminService.GetStructuralDivisions(), "Id", "NameStrucDiv", model.StrucDivId);
            ViewData["PostId"] = new SelectList(adminService.GetPosts(), "Id", "NamePost", model.PostId);
            return View(model);
        }

        [HttpPost, ActionName("UserCreate")]
        [ValidateAntiForgeryToken]
        public IActionResult UserCreateConfirmed(RegisterViewModel model, int id)
        {
            if (id < 1 || id > 2) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    adminService.CreateUser(new UserDTO
                    {
                        Login = model.Login,
                        RoleId = id,
                        PersonalNum = model.PersonalNum,
                        PersonalNum1 = model.PersonalNum1,
                        Name = model.Name,
                        Surname = model.Surname,
                        Patronymic = model.Patronymic,
                        StrucDivId = model.StrucDivId,
                        PostId = model.PostId,
                        Email = model.Email
                    }, model.Password);
                    if (id == 2)
                    {
                        return RedirectToAction("AllNumbers");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            var dtoRole = adminService.GetRole(id);
            ViewData["Role"] = dtoRole.Name;
            ViewData["StrucDivId"] = new SelectList(adminService.GetStructuralDivisions(), "Id", "NameStrucDiv", model.StrucDivId);
            ViewData["PostId"] = new SelectList(adminService.GetPosts(), "Id", "NamePost", model.PostId);
            return View(model);
        }

        [HttpGet]
        public IActionResult UserEdit(int? id)
        {
            if (id == null) return NotFound();
            UserDTO dtoUser = adminService.GetUser((int)id);
            if (dtoUser == null) return NotFound();
            var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserEditViewModel>()).CreateMapper();
            var model = mapperUser.Map<UserDTO, UserEditViewModel>(dtoUser);            
            ViewData["StrucDivId"] = new SelectList(adminService.GetStructuralDivisions(), "Id", "NameStrucDiv", model.StrucDivId);
            ViewData["PostId"] = new SelectList(adminService.GetPosts(), "Id", "NamePost", model.PostId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserEdit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<UserEditViewModel, UserDTO>()).CreateMapper();
                    var dtoUser = mapperUser.Map<UserEditViewModel, UserDTO>(model);
                    adminService.UpdateUser(dtoUser);
                    if (model.RoleId == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("AllNumbers");
                    }
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            ViewData["StrucDivId"] = new SelectList(adminService.GetStructuralDivisions(), "Id", "NameStrucDiv", model.StrucDivId);
            ViewData["PostId"] = new SelectList(adminService.GetPosts(), "Id", "NamePost", model.PostId);
            return View(model);
        }

        [HttpGet]
        public IActionResult UserDelete(int? id)
        {
            if (id == null) return NotFound();
            UserDTO dtoUser = adminService.GetUser((int)id);
            if (dtoUser == null) return NotFound();
            var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            var model = mapperUser.Map<UserDTO, UserViewModel>(dtoUser);
            return View(model);
        }

        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public IActionResult UserDeleteConfirmed(int id)
        {
            UserDTO dtoUser = adminService.GetUser(id);
            try
            {
                adminService.DeleteUser(id, User.Identity.Name);
                if (dtoUser.RoleId == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AllNumbers");
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            if (dtoUser == null) return NotFound();
            var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            var model = mapperUser.Map<UserDTO, UserViewModel>(dtoUser);
            return View(model);
        }
    }
}
