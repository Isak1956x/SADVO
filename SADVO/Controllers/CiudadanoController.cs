using Microsoft.AspNetCore.Mvc;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Controllers
{
    public class CiudadanoController : Controller
    {
        private readonly ICiudadanoServices _ciudadanoServices;
        private readonly IUserSession _userSession;
        private readonly IEleccionServices _eleccionServices;

        public CiudadanoController(ICiudadanoServices ciudadanoServices, IUserSession userSession, IEleccionServices eleccionServices)
        {
            _ciudadanoServices = ciudadanoServices;
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
            var ListEntities = await _ciudadanoServices.GetAll();
            var ListViewModel = ListEntities.Select(x => new CiudadanoViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                DocumentoIdentidad = x.DocumentoIdentidad,
                EstaActivo = x.EstaActivo,
                Email = x.Email
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
            return View("Save", new SaveCiudadanoViewModel() { Nombre = "", Apellido = "", DocumentoIdentidad = "", Email = "", EstaActivo = true});
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCiudadanoViewModel vm)
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
            var cedulaDuplicada = _ciudadanoServices.GetAll().Result.Select(x => x.DocumentoIdentidad).Contains(vm.DocumentoIdentidad);
            if (cedulaDuplicada)
            {
                ModelState.AddModelError("DocumentoIdentidad", "Esta cedula ya esta registrada con otro Ciudadano");
                return View("Save", vm);
            }
            var dto = new CiudadanoDto()
            {
                Id = 0,
              Apellido= vm.Apellido,
                Nombre = vm.Nombre,
                DocumentoIdentidad = vm.DocumentoIdentidad,
                EstaActivo = true,
                Email = vm.Email
            };
            var result = await _ciudadanoServices.AddAsync(dto);

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
            var dto = await _ciudadanoServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute("Index");
            }
            var vm = new SaveCiudadanoViewModel
            {
                Id = dto.Id,
                Apellido = dto.Apellido,
                Nombre = dto.Nombre,
                DocumentoIdentidad = dto.DocumentoIdentidad,
                EstaActivo = dto.EstaActivo,
                Email = dto.Email
            };

            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCiudadanoViewModel vm)
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
            var cedulaDuplicada = _ciudadanoServices.GetAll().Result.Any(x => x.DocumentoIdentidad == vm.DocumentoIdentidad && x.Id != vm.Id);
            if (cedulaDuplicada)
            {
                ModelState.AddModelError("DocumentoIdentidad", "Esta cedula ya esta registrada con otro Ciudadano");
                return View("Save", vm);
            }
            var dto = new CiudadanoDto()
            {
                Id = vm.Id,
                Apellido = vm.Apellido,
                Nombre = vm.Nombre,
                DocumentoIdentidad = vm.DocumentoIdentidad,
                EstaActivo = true,
                Email = vm.Email
            };
            var result = await _ciudadanoServices.UpdateAsync(dto);
          

            return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
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
            var dto = await _ciudadanoServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
            }
            var vm = new DeleteCiudadanoViewModel()
            {
                Id = dto.Id,
                Nombre = dto.Nombre
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCiudadanoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            await _ciudadanoServices.DeleteAsync(vm.Id);

            return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
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
            var dto = await _ciudadanoServices.GetById(id);
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
     
            CiudadanoDto? dto = await _ciudadanoServices.GetById(vm.Id);
            dto.EstaActivo = !vm.EstaActivo;
            var result = await _ciudadanoServices.UpdateAsync(dto);


            return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
        }


    }
}
