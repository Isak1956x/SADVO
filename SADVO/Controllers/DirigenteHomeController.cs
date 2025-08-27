using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Enums;
using SADVO.Models;

namespace SADVO.Controllers
{
    public class DirigenteHomeController : Controller
    {
        private readonly ILogger<DirigenteHomeController> _logger;
        private readonly IUserSession _userSession;
        private readonly IPartidoPoliticoServices _partidoServices;
        private readonly IAsignacionPuestoServices _asignacionPuestoServices;
        private readonly ICandidatoServices _candidatoServices;
        private readonly IDirigentePoliticoServices _dirigentePoliticoServices;
        private readonly IAlianzaPoliticaServices _alianzasPoliticaServices;

        public DirigenteHomeController(ILogger<DirigenteHomeController> logger, IUserSession userSession, IPartidoPoliticoServices partidoServices, IAsignacionPuestoServices asignacionPuestoServices, ICandidatoServices candidatoServices, IDirigentePoliticoServices dirigentePoliticoServices, IAlianzaPoliticaServices alianzasPoliticaServices)
        {
            _logger = logger;
            _userSession = userSession;
            _partidoServices = partidoServices;
            _asignacionPuestoServices = asignacionPuestoServices;
            _candidatoServices = candidatoServices;
            _dirigentePoliticoServices = dirigentePoliticoServices;
            _alianzasPoliticaServices = alianzasPoliticaServices;
        }

        public async Task< IActionResult> Index()
        {
            if(!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var user = _userSession.GetUserSession();
            var dirigente = await _dirigentePoliticoServices.GetByUserId(user.Id);
            var partidoPolitico = await _partidoServices.GetById(dirigente.PartidoPoliticoId);
            var Candidatos = await _candidatoServices.GetAll();
            var CandidatosAcvtivos = Candidatos.Where(x => x.EstaActivo == true).Count();
            var CandidatosInactivos = Candidatos.Where(x => x.EstaActivo == false).Count();
            var totalAsignaciones = (await _asignacionPuestoServices.GetAll()).Count();
            var alianzas = await _alianzasPoliticaServices.GetAllWithInclude();
            var totalSolicitudPendiente = alianzas
    .Count(a => a.Estado == (int)AlianzaEstado.Pendiente);
            var totalAlianzas = alianzas.Count(a => a.Estado == (int)AlianzaEstado.Aceptada);
            DirigenteHomeViewModel vm = new()
            {
                CandidatosActivos = CandidatosAcvtivos,
                CandidatosInactivos = CandidatosInactivos,
                PartidoPolitico = new PartidoPoliticoViewModel()
                {
                    Nombre = partidoPolitico.Nombre,
                    Siglas = partidoPolitico.Siglas,
                    Descripcion = partidoPolitico.Descripcion,
                    EstaActivo = partidoPolitico.EstaActivo,
                    LogoUrl = partidoPolitico.LogoUrl
                },
                CandidatosAsignados = totalAsignaciones,
                CantidadAlianzas = totalAlianzas,
                CantidaSolicitudesAlianzas = totalSolicitudPendiente
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
