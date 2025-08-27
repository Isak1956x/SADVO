using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Enums;
using SADVO.Core.Domain.Interfaces;
using SADVO.Helpers;

namespace SADVO.Controllers
{
    public class EleccionesController : Controller
    {
        private readonly IEleccionServices _eleccionServices;
        private readonly IUserSession _userSession;
        private readonly IPartidoPoliticoServices _partidoPoliticoServices;
        private readonly IPuestoElectivoServices _puestoElectivoServices;

        public EleccionesController(IEleccionServices eleccionServices, IUserSession userSession, IPartidoPoliticoServices partidoPoliticoServices, IPuestoElectivoServices puestoElectivoServices)
        {
            _eleccionServices = eleccionServices;
            _userSession = userSession;
            _partidoPoliticoServices = partidoPoliticoServices;
            _puestoElectivoServices = puestoElectivoServices;
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
        
         
            var elecciones = await _eleccionServices.GetAllWithInclude();
            var eleccionesViewModel = elecciones
    .OrderByDescending(x => x.Fecha)
    .Select(x => new EleccionViewModel
    {
        Id = x.Id,
        Nombre = x.Nombre,
        Fecha = x.Fecha,
        EstaFinalizada = x.EstaFinalizada
    })
    .ToList();

            var ListEntities = await _eleccionServices.GetAll();
            ViewBag.EleccionActiva = ListEntities.Any(x => !x.EstaFinalizada);
            var ListViewModel = ListEntities
        .OrderBy(x => x.EstaFinalizada) 
        .ThenByDescending(x => x.Fecha) 
        .Select(x => new EleccionViewModel
        {
            Id = x.Id,
            Nombre = x.Nombre,
            Fecha = x.Fecha,
            EstaFinalizada = x.EstaFinalizada
        })
        .ToList();
            return View(ListViewModel);
        }

        public IActionResult Seleccion()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            return View("Save",new SaveEleccionViewModel()
            {
                Nombre = "",
                Fecha = DateTime.UtcNow
            });
        }
        public  IActionResult Create()
        {
            var vm = new SaveEleccionViewModel
            {
                Fecha = DateTime.UtcNow,
                Nombre = ""
            };
            return View("Save", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveEleccionViewModel vm)
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

            var puestosActivos = (await _puestoElectivoServices.GetAll()).Where(a => a.EstaActivo).ToList();

            if (!puestosActivos.Any())
            {
                ModelState.AddModelError("Nombre", "Debe haber al menos un puesto electivo activo para crear una elección.");
                return View("Save", vm);
            }
            var partidosActivos = (await _partidoPoliticoServices.GetAllWithInclude())
        .Where(p => p.EstaActivo)
        .ToList();
            if (partidosActivos.Count < 2)
            {
                   ModelState.AddModelError("Nombre", "No hay suficientes partidos políticos para realizar una elección.");
                return View("Save", vm);
                
            }
            foreach (var partido in partidosActivos)
            {
                var candidatosActivos = partido.Candidatos.Where(c => c.EstaActivo).ToList();

                var puestosSinCandidato = puestosActivos
                    .Where(p => !candidatosActivos.Any(c => c.Puesto.Id == p.Id))
                    .Select(p => p.Nombre)
                    .ToList();

                if (puestosSinCandidato.Any())
                {
                    ModelState.AddModelError("Nombre", $"El partido político {partido.Nombre} ({partido.Siglas}) no tiene candidatos registrados para los siguientes puestos electivos: {string.Join(", ", puestosSinCandidato)}.");
                    return View("Save", vm);

                }
            }
            var dto = new EleccionDto()
            {
                Nombre = vm.Nombre,
                Fecha = vm.Fecha,
                EstaFinalizada = false
            };

            var result = await _eleccionServices.AddAsync(dto);
            return RedirectToRoute(new { controller = "Elecciones", action = "Index" });
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
            var dto = await _eleccionServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute("Index");
            }
            var vm = new CambiarEstadoViewModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                EstaActivo = dto.EstaFinalizada
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
     
            EleccionDto? dto = await _eleccionServices.GetById(vm.Id);
            dto.EstaFinalizada = !vm.EstaActivo;
            var result = await _eleccionServices.UpdateAsync(dto);


            return RedirectToRoute(new { controller = "Elecciones", action = "Index" });
        }


    }
}
