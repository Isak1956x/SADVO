using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Application.Services.SADVO.Core.Application.Services;
using SADVO.Core.Application.ViewModels;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;
using SADVO.Helpers;
using SADVO.Models;
using SADVO.Core.Application.Helpers;

namespace SADVO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEleccionServices _eleccionServices;
        private readonly IUserSession _userSession;
        private readonly IVotoServices _votoServices;
        private readonly ICiudadanoServices _ciudadanoServices;

        public HomeController(ILogger<HomeController> logger, IEleccionServices eleccionServices, IUserSession userSession, IVotoServices votoServices, ICiudadanoServices ciudadanoServices)
        {
            _logger = logger;
            _eleccionServices = eleccionServices;
            _userSession = userSession;
            _votoServices = votoServices;
            _ciudadanoServices = ciudadanoServices;
        }

        public IActionResult Index()
        {
            return View();
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
        
        public async Task<IActionResult> ValidarIdentidad(validacionCedulaViewModel vm)
        {

            if(!ModelState.IsValid)
            {
                return View("Index", vm);
            }

            bool contieneCaracterNoValido = vm.Cedula.Any(c => !char.IsDigit(c));

            if (contieneCaracterNoValido)
            {
                ModelState.AddModelError("Cedula", "Ingrese una Cedula Valida sin guiones");
                return View("Index", vm);
            }

            var Ciudadano = (await _ciudadanoServices.GetAll()).Where(a => a.DocumentoIdentidad == vm.Cedula).FirstOrDefault();
            if (Ciudadano != null)
            {
                if (!Ciudadano.EstaActivo)
                {
                    ModelState.AddModelError("Cedula", "El Ciudadano con la Cedula Ingresada esta inactivo");
                    return View("Index", vm);
                }
            }
            else
            {
                ModelState.AddModelError("Cedula", "El Ciudadano no esta registrado");
                return View("Index", vm);
            }
                var eleccion = (await _eleccionServices.GetAllWithInclude()).Where(a => a.EstaFinalizada == false).FirstOrDefault();
            if (eleccion == null)
            {
                ModelState.AddModelError("Cedula", "No hay ningún proceso electoral en estos momentos");
                return View("Index", vm);
            }
            
           
                var votoExistente = (await _votoServices.GetAll()).Where(a => a.CiudadanoId == Ciudadano.Id && a.EleccionId == eleccion.Id).FirstOrDefault();
                if (votoExistente != null)
                {
                    ModelState.AddModelError("Cedula", "ya ha ejercido su derecho al voto");
                    return View("Index", vm);
                }
     


            return View(new ValidacionCiudadanoViewModel { Cedula = vm.Cedula, CedulaFile = default});
        }
        [HttpPost]
        public async Task<IActionResult> ValidarIdentidad(ValidacionCiudadanoViewModel vm)
        {

            string cedulaFormateada = $"{vm.Cedula.Substring(0, 3)}-{vm.Cedula.Substring(3, 7)}-{vm.Cedula.Substring(10, 1)}";



         
            var rutaTemp = FileManager.Upload(vm.CedulaFile, 3, "Validacion"); 
            
           
            var ocr = new OcrService(@"C:\Users\Isaac\source\repos\SADVO\SADVO.Core.Application\tessdata");
            var textoDetectado = ocr.LeerTextoDesdeImagen(@"C:\Users\Isaac\source\repos\SADVO\SADVO\wwwroot"+rutaTemp);

           
            var match = Regex.Match(textoDetectado, @"\d{3}-\d{7}-\d"); // patrón cédula 
            var cedula = match.Value;
            Console.WriteLine(cedula);
            if (!match.Success)
            {
                ModelState.AddModelError("CedulaFile", "No se detectó una cédula válida en la imagen.");
                return View("ValidarIdentidad", vm);
            }

           if(cedula == cedulaFormateada)
            {
                var ciudadano = (await _ciudadanoServices.GetAll()).Where(a => a.DocumentoIdentidad == vm.Cedula).FirstOrDefault();
              
                var ciudadanoVm = new CiudadanoViewModel
                {
                    Id = ciudadano.Id,
                    Nombre = ciudadano.Nombre,
                    Apellido = ciudadano.Apellido,
                    DocumentoIdentidad = ciudadano.DocumentoIdentidad,
                    EstaActivo = ciudadano.EstaActivo,
                    Email = ciudadano.Email
                };
                HttpContext.Session.Set<CiudadanoViewModel>("Ciudadano", ciudadanoVm);
                return RedirectToRoute(new { controller = "EleccionElector", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("CedulaFile", "La cédula ingresada no coincide con la detectada en la imagen.");
                return View("ValidarIdentidad", vm);
            }


        }

    }
}
