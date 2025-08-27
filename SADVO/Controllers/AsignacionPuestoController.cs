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
    public class AsignacionPuestoController : Controller
    {
   
        private readonly IAsignacionPuestoServices _asignacionPuestoServices;
        private readonly IDirigentePoliticoServices _dirigentePoliticoServices;
        private readonly ICandidatoServices _candidatoServices;
        private readonly IPuestoElectivoServices _puestoElectivoServices;
        private readonly IUserSession _userSession;
        private readonly IEleccionServices _eleccionServices;

        public AsignacionPuestoController(IAsignacionPuestoServices asignacionPuestoServices, IDirigentePoliticoServices dirigentePoliticoServices, ICandidatoServices candidatoServices, IPuestoElectivoServices puestoElectivoServices, IUserSession userSession, IEleccionServices eleccionServices)
        {
            _asignacionPuestoServices = asignacionPuestoServices;
            _dirigentePoliticoServices = dirigentePoliticoServices;
            _candidatoServices = candidatoServices;
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

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var ListElecciones = await _eleccionServices.GetAll();
            ViewBag.EleccionActiva = ListElecciones.Any(x => !x.EstaFinalizada);

            var ListEntities = await _asignacionPuestoServices.GetAllWithInclude();
            var ListViewModel = ListEntities.Select(x => new AsignacionPuestoViewModel
            {
                Id = x.Id,
                CandidatoId = x.CandidatoId,
                PartidoPoliticoId = x.PartidoPoliticoId,
                PuestoElectivoId = x.PuestoElectivoId,
                Candidato = new CandidatoViewModel
                {
                    Id = x.Candidato.Id,
                    Nombre = x.Candidato.Nombre,
                    Apellido = x.Candidato.Apellido,
                    EstaActivo = x.Candidato.EstaActivo,
                    FotoUrl = x.Candidato.FotoUrl,
                    PartidoPoliticoId = x.Candidato.PartidoPoliticoId
                },
                PartidoPolitico = new PartidoPoliticoViewModel
                {
                    Id = x.PartidoPolitico.Id,
                    Nombre = x.PartidoPolitico.Nombre,
                    Siglas = x.PartidoPolitico.Siglas,
                    EstaActivo = x.PartidoPolitico.EstaActivo,
                    Descripcion = x.PartidoPolitico.Descripcion,
                    LogoUrl = x.PartidoPolitico.LogoUrl
                },
                PuestoElectivo = new PuestoElectivoViewModel
                {
                    Id = x.PuestoElectivo.Id,
                    Nombre = x.PuestoElectivo.Nombre,
                    Descripcion = x.PuestoElectivo.Descripcion,
                    EstaActivo = x.PuestoElectivo.EstaActivo
                }
            }).ToList();


            return View(ListViewModel);
        }

        public async Task<IActionResult> Create()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var user = _userSession.GetUserSession();
            var dirigente = await _dirigentePoliticoServices.GetByUserId(user.Id);
            var partidoId = dirigente.PartidoPoliticoId;

       
            var todosCandidatos = await _candidatoServices.GetAll();
            var candidatosActivos = todosCandidatos.Where(c => c.EstaActivo).ToList();


            var asignaciones = await _asignacionPuestoServices.GetAll(); 


            var candidatosAsignadosMismoPartido = asignaciones
                .Where(a => a.PartidoPoliticoId == partidoId)
                .Select(a => a.CandidatoId)
                .Distinct()
                .ToHashSet();

            var candidatosValidos = candidatosActivos
                .Where(c => c.PartidoPoliticoId == partidoId && !candidatosAsignadosMismoPartido.Contains(c.Id))
                .ToList();

            ViewBag.Candidatos = candidatosValidos;

            var Candidatos = await _candidatoServices.GetAll();
            var puestos = await _puestoElectivoServices.GetAll();
            var asignacionesPartido = asignaciones.Where(a => a.PartidoPoliticoId == dirigente.PartidoPoliticoId);
            var puestosConAsignado = asignacionesPartido.Select(a => a.PuestoElectivoId).ToList();

  
            var puestosDisponibles = puestos
                .Where(p => p.EstaActivo && !puestosConAsignado.Contains(p.Id))
                .ToList();

            ViewBag.PuestoElectivo = puestosDisponibles;
            return View("Save",new SaveAsignacionPuestoViewModel()
            {
                CandidatoId = 0,
                PuestoElectivoId = 0

            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAsignacionPuestoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var user = _userSession.GetUserSession();
            var dirigente = await _dirigentePoliticoServices.GetByUserId(user.Id);
            if (!ModelState.IsValid)
            {
                
                var partidoId = dirigente.PartidoPoliticoId;


                var todosCandidatos = await _candidatoServices.GetAll();
                var candidatosActivos = todosCandidatos.Where(c => c.EstaActivo).ToList();


                var asignaciones = await _asignacionPuestoServices.GetAll();


                var candidatosAsignadosMismoPartido = asignaciones
                    .Where(a => a.PartidoPoliticoId == partidoId)
                    .Select(a => a.CandidatoId)
                    .Distinct()
                    .ToHashSet();

                var candidatosValidos = candidatosActivos
                    .Where(c => c.PartidoPoliticoId == partidoId && !candidatosAsignadosMismoPartido.Contains(c.Id))
                    .ToList();

                ViewBag.Candidatos = candidatosValidos;

                var Candidatos = await _candidatoServices.GetAll();
                var puestos = await _puestoElectivoServices.GetAll();
                var asignacionesPartido = asignaciones.Where(a => a.PartidoPoliticoId == dirigente.PartidoPoliticoId);
                var puestosConAsignado = asignacionesPartido.Select(a => a.PuestoElectivoId).ToList();


                var puestosDisponibles = puestos
                    .Where(p => p.EstaActivo && !puestosConAsignado.Contains(p.Id))
                    .ToList();

                ViewBag.PuestoElectivo = puestosDisponibles;
                return View("Save", vm);

            }
          
            AsignacionPuestoDto dto = new()
            {
                Id = vm.Id,
                CandidatoId = vm.CandidatoId,
                PuestoElectivoId = vm.PuestoElectivoId,
                PartidoPoliticoId = dirigente.PartidoPoliticoId
            };
            var returnEntity = await _asignacionPuestoServices.AddAsync(dto);
       


            return RedirectToRoute(new { controller = "AsignacionPuesto", action = "Index" });
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var dto = await _asignacionPuestoServices.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "AsignacionPuesto", action = "Index" });
            }
            var vm = new DeleteAsignacionPuestoViewModel()
            {
                Id = dto.Id
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteAsignacionPuestoViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            await _asignacionPuestoServices.DeleteAsync(vm.Id);

            return RedirectToRoute(new { controller = "AsignacionPuesto", action = "Index" });
        }



    }
}
