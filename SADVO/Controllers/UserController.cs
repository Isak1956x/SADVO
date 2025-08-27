using Microsoft.AspNetCore.Mvc;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Enums;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IUserSession _userSession;
        private readonly IEleccionServices _eleccionServices;

        public UserController(IUserServices userServices, IUserSession userSession, IEleccionServices eleccionServices)
        {
            _userServices = userServices;
            _userSession = userSession;
            _eleccionServices = eleccionServices;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var ListElecciones = await _eleccionServices.GetAll();
            ViewBag.EleccionActiva = ListElecciones.Any(x => !x.EstaFinalizada);
            var ListEntities = await _userServices.GetAll();
            var ListViewModel = ListEntities.Select(x => new UserViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                EstaActivo = x.EstaActivo,
                Email = x.Email,
                Rol = x.Rol,
                NombreUsuario = x.NombreUsuario
            }).ToList();
            return View(ListViewModel);
        }

        public IActionResult Create()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            return View("Save",new CreateUserViewModel()
            {
                Nombre = "",
                Apellido = "",
                Email = "",
                NombreUsuario = "",
                Contrasena = "",
                ContrasenaConfirmacion = "",
                Rol = 1
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            if (!ModelState.IsValid)
            {
                return View("Save", vm);

            }
            var ListEntities = await _userServices.GetAll();
            if (ListEntities.Any(x => x.NombreUsuario == vm.NombreUsuario))
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya existe.");
                return View("Save", vm);
            }
            UserDto dto = new()
            {
                Id = 0,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Email = vm.Email,
                NombreUsuario = vm.NombreUsuario,
                ContrasenaHash = vm.Contrasena,
                Rol = vm.Rol,
                EstaActivo = true
            };
            var returnUser = await _userServices.AddAsync(dto);


            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
        
            var dto = await _userServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute("Index");
            }
            var vm = new EditUserViewModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                NombreUsuario = dto.NombreUsuario,
                Contrasena = "", 
                ContrasenaConfirmacion = ""
            };
           

            return View("Edit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            
            if (!ModelState.IsValid)
            {
                return View("Edit", vm);
            }
            var ListEntities = await _userServices.GetAll();
            if (ListEntities.Any(x => x.NombreUsuario == vm.NombreUsuario && x.Id != vm.Id))
            {
                ModelState.AddModelError("NombreUsuario", "El nombre de usuario ya existe.");
                return View("Edit", vm);
            }
            var existingUser = await _userServices.GetById(vm.Id);
            var dto = new UserDto()
            {
                Id = vm.Id,
                Apellido = vm.Apellido,
                Nombre = vm.Nombre,
                ContrasenaHash = vm.Contrasena,
                EstaActivo = existingUser.EstaActivo,
                Email = vm.Email,
                NombreUsuario = vm.NombreUsuario,
                Rol = existingUser.Rol
            };
            if(vm.Contrasena == null || vm.Contrasena == "")
            {
                if (existingUser != null)
                {
                    dto.ContrasenaHash = existingUser.ContrasenaHash; 
                }
            }
            else
            {
                dto.ContrasenaHash = vm.Contrasena;
            }
            var result = await _userServices.UpdateAsync(dto);
          

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


      
        public async Task<IActionResult> UpdateStatus(int id)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var dto = await _userServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute("Index");
            }
            var vm = new CambiarEstadoViewModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                EstaActivo = dto.EstaActivo
            };

            return View("UpdateStatus", vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(CambiarEstadoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
     
            UserDto? dto = await _userServices.GetById(vm.Id);
            dto.EstaActivo = !vm.EstaActivo;
            var result = await _userServices.UpdateAsync(dto);


            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


    }
}
