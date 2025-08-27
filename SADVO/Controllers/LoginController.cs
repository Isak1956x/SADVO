using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Enums;

namespace SADVO.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserServices userServices;
        private readonly IUserSession _userSession;
        private readonly IDirigentePoliticoServices _dirigentePoliticoServices;

        public LoginController(IUserServices userServices, IUserSession userSession, IDirigentePoliticoServices dirigentePoliticoServices)
        {
            this.userServices = userServices;
            _userSession = userSession;
            _dirigentePoliticoServices = dirigentePoliticoServices;
        }

        public IActionResult Index()
        {
            if(_userSession.HasUser())
            {
                UserViewModel? userSession = _userSession.GetUserSession();
                if (userSession != null)
                {
                    return userSession.Rol switch
                    {
                        (int)Role.ADMIN => RedirectToRoute(new { controller = "AdminHome", action = "Index" }),
                        (int)Role.Dirigente => RedirectToRoute(new { controller = "DirigenteHome", action = "Index" }),
                        _ => RedirectToRoute(new { controller = "Login", action = "Index" }),
                    };
                }
            }
            return View( new LoginViewModel() {Username = "", Password = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
             if(!ModelState.IsValid)
            {
                vm.Password = "";
                return View(vm);
            }
            UserDto? user = await userServices.LoginAsync(new LoginDto()
            {
                Password = vm.Password,
                Username = vm.Username
            });
            if(user != null)
            { if(!user.EstaActivo)
                {
                    ModelState.AddModelError("Password", "Su cuenta esta inactiva porfavor contacte un admin");
                    return View(vm);
                }
          
                UserViewModel userVm = new()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    NombreUsuario = user.NombreUsuario,
                    Rol = user.Rol
                };
                if (userVm.Rol == (int)Role.Dirigente)
                {
                    var dirigente = await _dirigentePoliticoServices.GetByUserId(user.Id);
                    if (dirigente == null)
                    {
                        ModelState.AddModelError("Password", "Usted no esta asociado con Ningun Partido");
                        return View(vm);
                    }
                }
                HttpContext.Session.Set<UserViewModel>("User", userVm);
                if(userVm.Rol == (int)Role.ADMIN)
                {
                    return RedirectToRoute(new { controller = "AdminHome", action = "Index" });
                }
                else if (userVm.Rol == (int)Role.Dirigente)
                {
                    return RedirectToRoute(new { controller = "DirigenteHome", action = "Index" });
                }
                
            }
            else
            {
                ModelState.AddModelError("userValidation", "Usuario o contraseña incorrectos");
            }
            vm.Password = "";
            return View(vm);
        }

 

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            if (_userSession.HasUser())
            {
                return View();
            }

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}
