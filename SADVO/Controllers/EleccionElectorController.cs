using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Enums;
using SADVO.Core.Domain.Interfaces;
using SADVO.Helpers;
using SADVO.Middleware;

namespace SADVO.Controllers
{
    public class EleccionElectorController : Controller
    {
private readonly IPuestoElectivoServices _puestoElectivoServices;
        private readonly ICandidatoServices _candidatoServices;
        private readonly IVotoServices _votoServices;
        private readonly ICiudadanoSession _ciudadanoSession;
        private readonly IEleccionServices _eleccionServices;

        public EleccionElectorController(IPuestoElectivoServices puestoElectivoServices, ICandidatoServices candidatoServices, IVotoServices votoServices, ICiudadanoSession ciudadanoSession, IEleccionServices eleccionServices)
        {
            _puestoElectivoServices = puestoElectivoServices;
            _candidatoServices = candidatoServices;
            _votoServices = votoServices;
            _ciudadanoSession = ciudadanoSession;
            _eleccionServices = eleccionServices;
        }

        public async Task<IActionResult> Index()
        {
           var ciudadanoId = _ciudadanoSession.GetCiudadanoSession().Id;
            var votos = await _votoServices.GetAll();
            var votosDelCiudadano = votos.Where(v => v.CiudadanoId == ciudadanoId).ToList();
            var puestosElectivos = (await _puestoElectivoServices.GetAllWithInclude()).Where(a => a.EstaActivo == true);
            var puestosViewModel = puestosElectivos.Select(p => new PuestoElectivoViewModel
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                EstaActivo = p.EstaActivo,
                YaVotado = votosDelCiudadano.Any(v => v.PuestoElectivoId == p.Id),
                Asignaciones = p.Asignaciones.Select(a => new AsignacionPuestoViewModel
                {
                    Id = a.Id,
                    CandidatoId = a.CandidatoId,
                    PartidoPoliticoId = a.PartidoPoliticoId,
                    PuestoElectivoId = a.PuestoElectivoId
                }
                ).ToList()
            }).ToList();
    


            return View(puestosViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Seleccion(int id)
        {
            var puesto = await _puestoElectivoServices.GetById(id);
            var candidatos = (await _candidatoServices.GetAllWithInclude()).Where(a => a.EstaActivo == true && a.Asignaciones.Where(x => x.PuestoElectivoId == id).Any()).ToList();
            var candidatosVm = candidatos.Select(c => new CandidatoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                EstaActivo = c.EstaActivo,
                FotoUrl = c.FotoUrl,
                NombrePuesto = c.Asignaciones.FirstOrDefault(a => a.PuestoElectivoId == id)?.PuestoElectivo?.Nombre,
                PartidoPoliticoId = c.PartidoPoliticoId,
                Asignaciones = c.Asignaciones.Select(a => new AsignacionPuestoViewModel
                {
                    Id = a.Id,
                    CandidatoId = a.CandidatoId,
                    PartidoPoliticoId = a.PartidoPoliticoId,
                    PuestoElectivoId = a.PuestoElectivoId
                }).ToList(),
                PartidoPolitico = new PartidoPoliticoViewModel
                {
                    Id = c.PartidoPolitico.Id,
                    Nombre = c.PartidoPolitico.Nombre,
                    Descripcion = c.PartidoPolitico.Descripcion,
                    EstaActivo = c.PartidoPolitico.EstaActivo,
                    LogoUrl = c.PartidoPolitico.LogoUrl,
                    Siglas = c.PartidoPolitico.Siglas
                }
            }).ToList();
            SaveVotoViewModel vm = new()
            {
                CandidatoId = 0,
                CiudadanoId = 0,
                Id = 0,
                PuestoElectivoId = id,
                EleccionId = (await _eleccionServices.GetAll()).Where(a => a.EstaFinalizada == false).FirstOrDefault().Id,
                Candidatos = candidatosVm
            };
            return View("Seleccion", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Votar(SaveVotoViewModel vm)
        {
            var candidatos = (await _candidatoServices.GetAllWithInclude()).Where(a => a.EstaActivo == true && a.Asignaciones.Where(x => x.PuestoElectivoId == vm.PuestoElectivoId).Any()).ToList();
            vm.Candidatos = candidatos.Select(c => new CandidatoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                EstaActivo = c.EstaActivo,
                FotoUrl = c.FotoUrl,
                NombrePuesto = c.Asignaciones.FirstOrDefault(a => a.PuestoElectivoId == vm.PuestoElectivoId)?.PuestoElectivo?.Nombre ?? "Sin asignar",
                PartidoPoliticoId = c.PartidoPoliticoId,
                Asignaciones = c.Asignaciones.Select(a => new AsignacionPuestoViewModel
                {
                    Id = a.Id,
                    CandidatoId = a.CandidatoId,
                    PartidoPoliticoId = a.PartidoPoliticoId,
                    PuestoElectivoId = a.PuestoElectivoId
                }).ToList(),
                PartidoPolitico = new PartidoPoliticoViewModel
                {
                    Id = c.PartidoPolitico.Id,
                    Nombre = c.PartidoPolitico.Nombre,
                    Descripcion = c.PartidoPolitico.Descripcion,
                    EstaActivo = c.PartidoPolitico.EstaActivo,
                    LogoUrl = c.PartidoPolitico.LogoUrl,
                    Siglas = c.PartidoPolitico.Siglas
                }
            }).ToList();
            if (!ModelState.IsValid)
            {
                return View("Seleccion",vm);
            }
           if(vm.CandidatoId == 0)
            {
                vm.CandidatoId = 8;
            }
            var ciudadano = _ciudadanoSession.GetCiudadanoSession();
            var eleccionesActivas = await _eleccionServices.GetAll();
            var eleccionActiva = eleccionesActivas.FirstOrDefault(x => !x.EstaFinalizada);
            var candidato = (await _candidatoServices.GetAllWithInclude()).Where(a => a.Id == vm.CandidatoId).FirstOrDefault();

            VotoDto votoDto = new VotoDto
            {
                CandidatoId = vm.CandidatoId,
                CiudadanoId = ciudadano.Id,
                EleccionId = eleccionActiva.Id,
                PuestoElectivoId = vm.PuestoElectivoId,
                Id = 0
            };
            
           await _votoServices.AddAsync(votoDto);



            return RedirectToRoute(new { controller = "EleccionElector", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarVotacion()
        {
            var ciudadanoId = _ciudadanoSession.GetCiudadanoSession().Id;

            var puestos = (await _puestoElectivoServices.GetAllWithInclude()).Where(p => p.EstaActivo).ToList();
            var votos = (await _votoServices.GetAll()).Where(v => v.CiudadanoId == ciudadanoId).ToList();

            var puestosFaltantes = puestos
                .Where(p => !votos.Any(v => v.PuestoElectivoId == p.Id))
                .Select(p => p.Nombre)
                .ToList();

            if (puestosFaltantes.Any())
            {
                TempData["Error"] = "Debe votar en los siguientes puestos antes de finalizar: " + string.Join(", ", puestosFaltantes);
                return RedirectToAction("Index");
            }
            HttpContext.Session.Remove("Ciudadano");
            return View("FinEleccion"); 
        }


    }

}

