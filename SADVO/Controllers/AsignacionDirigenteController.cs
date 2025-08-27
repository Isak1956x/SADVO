using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Enums;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Controllers
{
    public class AsignacionDirigenteController : Controller
    {
       private readonly IUserServices _userServices;
        private readonly IPartidoPoliticoServices _partidoServices;
        private readonly IDirigentePoliticoServices _dirigenteServices;
        private readonly IUserSession _userSession;
        private IEleccionServices _eleccionServices;


        public AsignacionDirigenteController(IUserServices userServices, IPartidoPoliticoServices partidoServices, IDirigentePoliticoServices dirigenteServices, IUserSession userSession, IEleccionServices eleccionServices)
        {
            _userServices = userServices;
            _partidoServices = partidoServices;
            _dirigenteServices = dirigenteServices;
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
            var ListEntities = await _dirigenteServices.GetAllWithInclude();
            var ListViewModel = ListEntities.Select(x => new DirigentePoliticoViewModel
            {
                Id = x.Id,
                PartidoPoliticoId = x.PartidoPoliticoId,
                UsuarioId = x.UsuarioId,
                PartidoPolitico  = new PartidoPoliticoViewModel
                {
                    Id = x.PartidoPolitico.Id,
                    Nombre = x.PartidoPolitico.Nombre,
                    Descripcion = x.PartidoPolitico.Descripcion,
                    LogoUrl = x.PartidoPolitico.LogoUrl,
                    Siglas = x.PartidoPolitico.Siglas,
                    EstaActivo = x.PartidoPolitico.EstaActivo
                } ,
                Usuario = new UserViewModel
                {
                    Id = x.Usuario.Id,
                    Nombre = x.Usuario.Nombre,
                    Apellido = x.Usuario.Apellido,
                    Email = x.Usuario.Email,
                    EstaActivo = x.Usuario.EstaActivo,
                    Rol = x.Usuario.Rol,
                    NombreUsuario = x.Usuario.NombreUsuario
                },
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
            var usuarios = _userServices.GetAll();
            var usuariosVm = usuarios.Result.Select(x => new UserViewModel()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Email = x.Email,
                EstaActivo = x.EstaActivo,
                Rol = x.Rol,
                NombreUsuario = x.NombreUsuario
            });
            ViewBag.Usuarios = usuariosVm.Where(x => x.EstaActivo && x.Rol == (int)Role.Dirigente);
            ViewBag.Partidos = _partidoServices.GetAll().Result.Where(x => x.EstaActivo);
            return View("Save", new SaveDirigentePoliticoViewModel()
            {
                PartidoPoliticoId = 0,
                UsuarioId = 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveDirigentePoliticoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var usuarios = _userServices.GetAll();
            var usuariosVm = usuarios.Result.Select(x => new UserViewModel()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Email = x.Email,
                EstaActivo = x.EstaActivo,
                Rol = x.Rol,
                NombreUsuario = x.NombreUsuario
            });
            ViewBag.Usuarios = usuariosVm.Where(x => x.EstaActivo && x.Rol == (int)Role.Dirigente);
            ViewBag.Partidos = _partidoServices.GetAll().Result.Where(x => x.EstaActivo);
            if (!ModelState.IsValid)
            {
             
                return View("Save", vm);

            }
            var dirigentesRelacionados = _dirigenteServices.GetAllWithInclude().Result.Select(x => x.Usuario.Id);
            if (dirigentesRelacionados.Contains(vm.UsuarioId))
            {
                ModelState.AddModelError("UsuarioId", "Este dirigente ya está relacionado con otro partido político");
                return View("Save", vm);
            }
            DirigentePoliticoDto dto = new()
            {
                Id = 0,
                PartidoPoliticoId = vm.PartidoPoliticoId,
                UsuarioId = vm.UsuarioId,
            };
            var returnUser = await _dirigenteServices.AddAsync(dto);


            return RedirectToRoute(new { controller = "AsignacionDirigente", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var dto = await _dirigenteServices.GetById(id);
            var usuario = await _userServices.GetById(dto.UsuarioId);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
            }
            var vm = new DeleteDirigentePoliticoViewModel()
            {
                Id = dto.Id,
                NombreUsuario = usuario.NombreUsuario
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDirigentePoliticoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            await _dirigenteServices.DeleteAsync(vm.Id);

            return RedirectToRoute(new { controller = "AsignacionDirigente", action = "Index" });
        }
    }
}
