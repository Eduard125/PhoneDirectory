using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneDirectory.BLL.DTO;
using PhoneDirectory.BLL.Interfaces;
using PhoneDirectory.WEB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneDirectory.BLL.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using PhoneDirectory.DAL.Entities;

namespace PhoneDirectory.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IHomeService homeService;

        public HomeController(IHomeService service)
        {
            // Сервис, внедренный через зависимость
            homeService = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult StructuralDivisions()
        {
            IEnumerable<StructuralDivisionDTO> dtos = homeService.GetStructuralDivisions();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StructuralDivisionDTO, StructuralDivisionViewModel>()).CreateMapper();
            var list = mapper.Map<IEnumerable<StructuralDivisionDTO>, List<StructuralDivisionViewModel>>(dtos);
            return View(list);
        }

        [HttpGet]
        public IActionResult Login()
        {
            RegisterViewModel model = new RegisterViewModel { StrucDivId = 1, PostId = 1 };
            ViewData["StrucDivId"] = new SelectList(homeService.GetStructuralDivisions(), "Id", "NameStrucDiv", model.StrucDivId);
            ViewData["PostId"] = new SelectList(homeService.GetPosts(), "Id", "NamePost", model.PostId);
            ViewData["RoleId"] = new SelectList(homeService.GetRoles(), "Id", "Name", model.RoleId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AccountDTO account = homeService.Login(model.Login, model.Password);
                if (account != null)
                {
                    await Authenticate(account.Login, account.RoleDesign, account.FullName);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Неверные логин и (или) пароль");
            }
            return View(model);
        }



        private async Task Authenticate(string userLogin, string userRole, string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole),
                new Claim("FullName", userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Register(User user)
        {
            RegisterViewModel model = new RegisterViewModel { StrucDivId = 1, PostId = 1 };
            ViewData["StrucDivId"] = new SelectList(homeService.GetStructuralDivisions(), "Id", "NameStrucDiv", model.StrucDivId);
            ViewData["PostId"] = new SelectList(homeService.GetPosts(), "Id", "NamePost", model.PostId);            
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    homeService.Register(new RegisterDTO
                    {
                        Login = model.Login,
                        Password = model.Password,
                        Surname = model.Surname,
                        Name = model.Name,
                        Patronymic = model.Patronymic,
                        StrucDivId = model.StrucDivId,
                        PostId = model.PostId,
                        PersonalNum = model.PersonalNum,
                        PersonalNum1 = model.PersonalNum1,
                        Email = model.Email
                    });
                    return View("RegisterMsg");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            ViewData["StrucDivId"] = new SelectList(homeService.GetStructuralDivisions(), "Id", "StrucDivId", model.StrucDivId);
            ViewData["PostId"] = new SelectList(homeService.GetPosts(), "Id", "PostId", model.PostId);            
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
