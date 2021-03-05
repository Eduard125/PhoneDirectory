using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneDirectory.BLL.DTO;
using PhoneDirectory.BLL.Interfaces;
using PhoneDirectory.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB.Controllers
{
    [Authorize(Roles = "employee")]
    public class EmployeeController : Controller
    {
        private IEmployeeService employeeService;

        public EmployeeController(IEmployeeService service)
        {
            // Сервис, внедренный через зависимость
            employeeService = service;
        }
        public IActionResult Index()
        {
            IEnumerable<UserDTO> dtosUsers = employeeService.GetUsers();
            var mapperUsers = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            var users = mapperUsers.Map<IEnumerable<UserDTO>, List<UserViewModel>>(dtosUsers);
            return View(users);
        }       

        public IActionResult StructuralDivisions()
        {
            StructuralDivisionsFilterViewModel filter = new StructuralDivisionsFilterViewModel { StrucDivId = 1, PostId = 1, UseStructuralDivision = false, UsePost = false };
            var dtosStructuralDivisions = employeeService.GetStructuralDivisions();
            var mapperStructuralDivision = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionDTO, StructuralDivisionViewModel>()).CreateMapper();
            ViewData["StructuralDivisions"] = mapperStructuralDivision.Map<IEnumerable<StructuralDivisionDTO>, List<StructuralDivisionViewModel>>(dtosStructuralDivisions);
            ViewData["StrucDivId"] = new SelectList(employeeService.GetStructuralDivisions1(), "Id", "NameStrucDiv", filter.StrucDivId);
            ViewData["PostId"] = new SelectList(employeeService.GetPosts(), "Id", "NamePost", filter.PostId);
            return View(filter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StructuralDivisions(StructuralDivisionsFilterViewModel filter)
        {
            var mapperFilter = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionsFilterViewModel, StructuralDivisionsFilterDTO>()).CreateMapper();
            var dtosStructuralDivisions = employeeService.GetStructuralDivisions(mapperFilter.Map<StructuralDivisionsFilterViewModel, StructuralDivisionsFilterDTO>(filter));
            var mapperGroup = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionDTO, StructuralDivisionViewModel>()).CreateMapper();
            ViewData["StructuralDivisions"] = mapperGroup.Map<IEnumerable<StructuralDivisionDTO>, List<StructuralDivisionViewModel>>(dtosStructuralDivisions);
            ViewData["StrucDivId"] = new SelectList(employeeService.GetStructuralDivisions(), "Id", "NameStrucDiv", filter.StrucDivId);
            ViewData["PostId"] = new SelectList(employeeService.GetPosts(), "Id", "NamePost", filter.PostId);
            return View(filter);
        }       

        public IActionResult DepStaff(int? id)
        {
            if (id == null)
            {
                IEnumerable<UserDTO> dtosUsers = employeeService.GetUsers();
                if (dtosUsers == null) return NotFound();
                var mapperStaff = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
                var model = mapperStaff.Map<IEnumerable<UserDTO>, List<UserViewModel>>(dtosUsers);
                return View("DepStaff", model);
            }
            else
            {
                StructuralDivisionDTO dtostructuralDivision = employeeService.GetStructuralDivision((int)id);
                if (dtostructuralDivision == null) return NotFound();
                IEnumerable<UserDTO> dtosUsers = employeeService.GetUsers(dtostructuralDivision.Id);
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
    }
}
