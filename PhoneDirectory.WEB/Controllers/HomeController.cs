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

        [HttpGet]
        public IActionResult Login()
        {
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
                    return RedirectToAction("Groups", "Home");
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
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel { StrucDivId = 1, PostId = 1 };
            ViewData["StrucDivId"] = new SelectList(homeService.GetStrucDivId(), "Id", "Name", model.StrucDivId);
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
                        StrucDivNum = model.StrucDivNum,
                        StrucDivMobNum = model.StrucDivMobNum,
                        Email = model.Email
                    });
                    return View("RegisterMsg");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            ViewData["StrucDivId"] = new SelectList(homeService.GetStrucDivId(), "Id", "StrucDivId", model.StrucDivId);
            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
