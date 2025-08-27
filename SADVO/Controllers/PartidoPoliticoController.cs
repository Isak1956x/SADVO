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
    public class PartidoPoliticoController : Controller
    {
        private readonly IPartidoPoliticoServices _partidoServices;
        private readonly IUserSession _userSession;
        private readonly IEleccionServices _eleccionServices;

        public PartidoPoliticoController(IPartidoPoliticoServices partidoServices, IUserSession userSession, IEleccionServices elccionServices)
        {
            _partidoServices = partidoServices;
            _userSession = userSession;
            _eleccionServices = elccionServices;
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
            var ListEntities = await _partidoServices.GetAll();
            var ListViewModel = ListEntities.Select(x => new PartidoPoliticoViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion,
                EstaActivo = x.EstaActivo,
                LogoUrl = x.LogoUrl,
                Siglas = x.Siglas
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
            return View("Save",new SavePartidoPoliticoViewModel()
            {
                Nombre = "",
                Descripcion = "",
                Siglas = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePartidoPoliticoViewModel vm)
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
            var ListEntities = await _partidoServices.GetAll();
            if (ListEntities.Any(x => x.Siglas == vm.Siglas))
            {
                ModelState.AddModelError("Siglas", "La Sigla del Partido ya existe.");
                return View("Save", vm);
            }
            PartidoPoliticoDto dto = new()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                EstaActivo = true,
                LogoUrl = "vm.LogoUrl",
                Siglas = vm.Siglas
            };
            var returnEntity = await _partidoServices.AddAsync(dto);
            if(returnEntity != null && returnEntity.Id != 0)
            {
                dto.Id = returnEntity.Id;
                dto.LogoUrl = FileManager.Upload(vm.LogoUrl, dto.Id, "PartidoPolitico");
                await _partidoServices.UpdateAsync(dto);
            }


            return RedirectToRoute(new { controller = "PartidoPolitico", action = "Index" });
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
            var dto = await _partidoServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute("Index");
            }
            var vm = new UpdatePartidoPoliticoViewModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Siglas = dto.Siglas
            };
          
            return View("Edit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePartidoPoliticoViewModel vm)
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
            var ListEntities = await _partidoServices.GetAll();
            if (ListEntities.Any(x => x.Siglas == vm.Siglas && x.Id != vm.Id))
            {
                ModelState.AddModelError("Siglas", "La Sigla del Partido ya existe.");
                return View("Edit", vm);
            }
            var dto = new PartidoPoliticoDto()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                LogoUrl = "vm.LogoUrl",
                Siglas = vm.Siglas,
                EstaActivo = true
            };
            var currentDto = await _partidoServices.GetById(vm.Id);
            string currenImagePath = "";
            if (currentDto != null)
            {
                currenImagePath = currentDto.LogoUrl;
            }
            dto.LogoUrl = FileManager.Upload(vm.LogoUrl, dto.Id, "PartidoPolitico",true, currenImagePath)!;
            var result = await _partidoServices.UpdateAsync(dto);
          

            return RedirectToRoute(new { controller = "PartidoPolitico", action = "Index" });
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
            var dto = await _partidoServices.GetById(id);
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
     
            PartidoPoliticoDto? dto = await _partidoServices.GetById(vm.Id);
            dto.EstaActivo = !vm.EstaActivo;
            var result = await _partidoServices.UpdateAsync(dto);


            return RedirectToRoute(new { controller = "PartidoPolitico", action = "Index" });
        }


    }
}
