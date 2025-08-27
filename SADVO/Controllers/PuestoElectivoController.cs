using Microsoft.AspNetCore.Mvc;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Controllers
{
    public class PuestoElectivoController : Controller
    {
        private readonly IPuestoElectivoServices _puestoElectivoServices;
        private readonly IUserSession _userSession;
        private readonly IEleccionServices _eleccionServices;

        public PuestoElectivoController(IPuestoElectivoServices puestoElectivoServices, IUserSession userSession, IEleccionServices eleccionServices)
        {
            _puestoElectivoServices = puestoElectivoServices;
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

            var ListEntities = await _puestoElectivoServices.GetAll();
            var ListViewModel = ListEntities.Select(x => new PuestoElectivoViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                EstaActivo = x.EstaActivo
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
            return View("Save", new SavePuestoElectivoViewModel() { Nombre = "", Descripcion = ""});
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePuestoElectivoViewModel vm)
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
            var dto = new PuestoElectivoDto()
            {
                Id = 0,
              Descripcion= vm.Descripcion,
                Nombre = vm.Nombre,
                EstaActivo = true
            };
            var result = await _puestoElectivoServices.AddAsync(dto);

            return RedirectToAction("Index");
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
            ViewBag.EditMode = true;
            var dto = await _puestoElectivoServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute("Index");
            }
            var vm = new SavePuestoElectivoViewModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
               Descripcion = dto.Descripcion
            };

            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePuestoElectivoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            ViewBag.EditMode = true;
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }
            var dto = new PuestoElectivoDto
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                EstaActivo = true
            };
            var result = await _puestoElectivoServices.UpdateAsync(dto);
          

            return RedirectToRoute(new { controller = "PuestoElectivo", action = "Index" });
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
            var dto = await _puestoElectivoServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "PuestoElectivo", action = "Index" });
            }
            var vm = new DeletePuestoElectivoViewModel()
            {
                Id = dto.Id,
                Nombre = dto.Nombre
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePuestoElectivoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            await _puestoElectivoServices.DeleteAsync(vm.Id);

            return RedirectToRoute(new { controller = "PuestoElectivo", action = "Index" });
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
            var dto = await _puestoElectivoServices.GetById(id);
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

            PuestoElectivoDto? dto = await _puestoElectivoServices.GetById(vm.Id);
            dto.EstaActivo = !vm.EstaActivo;
            var result = await _puestoElectivoServices.UpdateAsync(dto);


            return RedirectToRoute(new { controller = "PuestoElectivo", action = "Index" });
        }
    }
}
