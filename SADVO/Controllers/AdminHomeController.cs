using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.ViewModels;
using SADVO.Models;

namespace SADVO.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly ILogger<AdminHomeController> _logger;
        private readonly IUserSession _userSession;
        private readonly IEleccionServices _eleccionServices;

        public AdminHomeController(ILogger<AdminHomeController> logger, IUserSession userSession, IEleccionServices eleccionServices)
        {
            _logger = logger;
            _userSession = userSession;
            _eleccionServices = eleccionServices;
        }

        public async Task< IActionResult> Index(int? anio)
        {
            if(!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var años = await _eleccionServices.ObtenerAñosDisponibles();
            var añoSeleccionado = anio ?? años.FirstOrDefault();
            if(añoSeleccionado == 0)
            {
                return View(new AdminHomeViewModel());
            }
            var elecciones = (await _eleccionServices.GetAllWithInclude())
            .Where(a => a.Fecha.Year == añoSeleccionado)
            .ToList();

            var adminHomeVm = new AdminHomeViewModel()
            {
                Años = años,
                añoSelecionado = añoSeleccionado,
                Elecciones = elecciones.Select(a => new EleccionViewModel
                {
                    Id = a.Id,
                    EstaFinalizada = a.EstaFinalizada,
                    Fecha = a.Fecha,
                    Nombre = a.Nombre,
                    Votos = a.Votos.Select(v => new VotoViewModel
                    {
                        CandidatoId = v.Id,
                        CiudadanoId = v.CiudadanoId,
                        EleccionId = v.EleccionId,
                        Id = v.Id,
                        PuestoElectivoId = v.PuestoElectivoId
                    }).ToList()
                }).ToList()
            };

          

            return View(adminHomeVm);
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
