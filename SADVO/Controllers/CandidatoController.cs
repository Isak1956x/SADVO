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
    public class CandidatoController : Controller
    {
        private readonly ICandidatoServices _candidatoServices;
       private readonly IDirigentePoliticoServices _dirigentePoliticoServices;
        private readonly IAsignacionPuestoServices _asignacionPuestoServices;
        private readonly IPuestoElectivoServices _puestoElectivoServices;
        private readonly IEleccionServices _eleccionServices;

        private readonly IUserSession _userSession;

        public CandidatoController(ICandidatoServices candidatoServices, IDirigentePoliticoServices dirigentePoliticoServices, IAsignacionPuestoServices asignacionPuestoServices, IPuestoElectivoServices puestoElectivoServices, IEleccionServices eleccionServices, IUserSession userSession)
        {
            _candidatoServices = candidatoServices;
            _dirigentePoliticoServices = dirigentePoliticoServices;
            _asignacionPuestoServices = asignacionPuestoServices;
            _puestoElectivoServices = puestoElectivoServices;
            _eleccionServices = eleccionServices;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var ListElecciones = await _eleccionServices.GetAll();
            ViewBag.EleccionActiva = ListElecciones.Any(x => !x.EstaFinalizada);
            var ListAsignaciones = await _asignacionPuestoServices.GetAllWithInclude();
            var ListEntities = await _candidatoServices.GetAllWithInclude();
            var ListViewModel = ListEntities.Select(x => new CandidatoViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                EstaActivo = x.EstaActivo,
                FotoUrl = x.FotoUrl,
                PartidoPoliticoId = x.PartidoPoliticoId,
                NombrePuesto = ListAsignaciones
    .Where(a => a.CandidatoId == x.Id)
    .Select(a => a.PuestoElectivo.Nombre)
    .FirstOrDefault() ?? "Sin Puesto Asignado"
            }).ToList();

            return View(ListViewModel);
        }

        public IActionResult Create()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            return View("Save",new SaveCandidatoViewModel()
            {
                Nombre = "",
                Apellido = "",
                FotoUrl = default
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCandidatoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            if (!ModelState.IsValid)
            {
                return View("Save", vm);

            }
            var user = _userSession.GetUserSession();
            var dirigente = await _dirigentePoliticoServices.GetByUserId(user.Id);
            CandidatoDto dto = new()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                EstaActivo = true,
                PartidoPoliticoId = dirigente.PartidoPoliticoId,
                FotoUrl = "vm.FotoUrl"
            };
            var returnEntity = await _candidatoServices.AddAsync(dto);
            if(returnEntity != null && returnEntity.Id != 0)
            {
                dto.Id = returnEntity.Id;
                dto.FotoUrl = FileManager.Upload(vm.FotoUrl, dto.Id, "Candidato");
                await _candidatoServices.UpdateAsync(dto);
            }


            return RedirectToRoute(new { controller = "Candidato", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            ViewBag.EditMode = true;
            var dto = await _candidatoServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "Candidato", action = "Index" });
            }
            var vm = new UpdateCandidatoViewModel
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido
            };

            return View("Edit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCandidatoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            if (!ModelState.IsValid)
            {
                return View("Edit", vm);
            }
            var user = _userSession.GetUserSession();
            var dirigente = await _dirigentePoliticoServices.GetByUserId(user.Id);

            var dto = new CandidatoDto()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                EstaActivo = true,
                FotoUrl = default,
                PartidoPoliticoId = dirigente.PartidoPoliticoId
            };
            var currentDto = await _candidatoServices.GetById(vm.Id);
            string currenImagePath = "";
            if (currentDto != null)
            {
                currenImagePath = currentDto.FotoUrl;
            }
            dto.FotoUrl = FileManager.Upload(vm.FotoUrl, dto.Id, "Candidato", true, currenImagePath)!;
            var result = await _candidatoServices.UpdateAsync(dto);


            return RedirectToRoute(new { controller = "Candidato", action = "Index" });
        }



        public async Task<IActionResult> UpdateStatus(int id)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var dto = await _candidatoServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "Candidato", action = "Index" });
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

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
     
            CandidatoDto? dto = await _candidatoServices.GetById(vm.Id);
            dto.EstaActivo = !vm.EstaActivo;
            var result = await _candidatoServices.UpdateAsync(dto);


            return RedirectToRoute(new { controller = "Candidato", action = "Index" });
        }


    }
}
