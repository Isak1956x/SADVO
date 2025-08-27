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
    public class AlianzasPoliticasController : Controller
    {
   private readonly IAlianzaPoliticaServices _alianzasPoliticasServices;
        private readonly IDirigentePoliticoServices _dirigentePoliticoServices;
        private readonly IUserSession _userSession;
        private readonly IPartidoPoliticoServices _partidoServices;
        private readonly IEleccionServices _eleccionServices;

        public AlianzasPoliticasController(IAlianzaPoliticaServices alianzasPoliticasServices, IDirigentePoliticoServices dirigentePoliticoServices, IUserSession userSession, IPartidoPoliticoServices partidoServices, IEleccionServices eleccionServices)
        {
            _alianzasPoliticasServices = alianzasPoliticasServices;
            _dirigentePoliticoServices = dirigentePoliticoServices;
            _userSession = userSession;
            _partidoServices = partidoServices;
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
            var user = _userSession.GetUserSession();
            var dirigente = await _dirigentePoliticoServices.GetByUserId(user.Id);
            var alianzasPoliticas = await _alianzasPoliticasServices.GetAllWithInclude();
            var solicitudesPendientes = alianzasPoliticas
       .Where(a =>
           a.PartidoDestinoId == dirigente.PartidoPoliticoId &&
           a.Estado == (int)AlianzaEstado.Pendiente)
       .Select(a => new AlianzaPoliticaViewModel
       {
           Id = a.Id,
           Estado = a.Estado,
           PartidoDestinoId = a.PartidoDestinoId,
           FechaSolicitud = a.FechaSolicitud,
           PartidoSolicitanteId = a.PartidoSolicitanteId,
           PartidoDestino = new PartidoPoliticoViewModel
           {
               Id = a.PartidoDestino.Id,
               Nombre = a.PartidoDestino.Nombre,
               Descripcion = a.PartidoDestino.Descripcion,
               LogoUrl = a.PartidoDestino.LogoUrl,
               Siglas = a.PartidoDestino.Siglas,
               EstaActivo = a.PartidoDestino.EstaActivo
           },
           PartidoSolicitante = new PartidoPoliticoViewModel
           {
               Id = a.PartidoSolicitante.Id,
               Nombre = a.PartidoSolicitante.Nombre,
               Descripcion = a.PartidoSolicitante.Descripcion,
               LogoUrl = a.PartidoSolicitante.LogoUrl,
               Siglas = a.PartidoSolicitante.Siglas,
               EstaActivo = a.PartidoSolicitante.EstaActivo
           }
       })
       .ToList();


            var solicitudesEnviadas = alianzasPoliticas
    .Where(a => a.PartidoSolicitanteId == dirigente.PartidoPoliticoId && a.Estado == (int)AlianzaEstado.Pendiente || a.Estado == (int)AlianzaEstado.Rechazada)
    .Select(a => new AlianzaPoliticaViewModel
    {
        Id = a.Id,
        PartidoSolicitanteId = a.PartidoSolicitanteId,
        PartidoDestinoId = a.PartidoDestinoId,
        Estado = a.Estado,
        FechaSolicitud = a.FechaSolicitud,
        PartidoSolicitante = new PartidoPoliticoViewModel
        {
            Id = a.PartidoSolicitante.Id,
            Nombre = a.PartidoSolicitante.Nombre,
            Descripcion = a.PartidoSolicitante.Descripcion,
            LogoUrl = a.PartidoSolicitante.LogoUrl,
            Siglas = a.PartidoSolicitante.Siglas,
            EstaActivo = a.PartidoSolicitante.EstaActivo
        },
        PartidoDestino = new PartidoPoliticoViewModel
        {
            Id = a.PartidoDestino.Id,
            Nombre = a.PartidoDestino.Nombre,
            Descripcion = a.PartidoDestino.Descripcion,
            LogoUrl = a.PartidoDestino.LogoUrl,
            Siglas = a.PartidoDestino.Siglas,
            EstaActivo = a.PartidoDestino.EstaActivo
        }

    })
    .ToList();

            var alianzasVigentes = alianzasPoliticas
    .Where(a =>
        a.Estado == (int)AlianzaEstado.Aceptada &&
        (a.PartidoSolicitanteId == dirigente.PartidoPoliticoId || a.PartidoDestinoId == dirigente.PartidoPoliticoId))
    .Select(a => new AlianzaPoliticaViewModel
    {
       Estado = a.Estado,
        PartidoDestinoId = a.PartidoDestinoId,
        FechaSolicitud = a.FechaSolicitud,
        Id = a.Id,
        PartidoDestino = new PartidoPoliticoViewModel
        {
            Id = a.PartidoDestino.Id,
            Nombre = a.PartidoDestino.Nombre,
            Descripcion = a.PartidoDestino.Descripcion,
            LogoUrl = a.PartidoDestino.LogoUrl,
            Siglas = a.PartidoDestino.Siglas,
            EstaActivo = a.PartidoDestino.EstaActivo
        },
        PartidoSolicitanteId = a.PartidoSolicitanteId,
        PartidoSolicitante = new PartidoPoliticoViewModel
        {
            Id = a.PartidoSolicitante.Id,
            Nombre = a.PartidoSolicitante.Nombre,
            Descripcion = a.PartidoSolicitante.Descripcion,
            LogoUrl = a.PartidoSolicitante.LogoUrl,
            Siglas = a.PartidoSolicitante.Siglas,
            EstaActivo = a.PartidoSolicitante.EstaActivo
        }
    })
    .ToList();
            var ListViewModel = new AlianzaPoliticaIndexViewModel
            {
                AlianzasAceptadas = alianzasVigentes,
                AlianzasEnviadas = solicitudesEnviadas,
                AlianzasPendientes = solicitudesPendientes
            };
            var ListElecciones = await _eleccionServices.GetAll();
            ViewBag.EleccionActiva = ListElecciones.Any(x => !x.EstaFinalizada);

            return View(ListViewModel);
        }



        public async Task<IActionResult> UpdateStatus(int id, int Estado)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var dto = (await _alianzasPoliticasServices.GetAllWithInclude())
       .FirstOrDefault(a => a.Id == id);
            if (dto == null)
            {
                return RedirectToRoute("Index");
            }
            var vm = new CambiarEstadoAlianzaViewModel
            {
                Id = dto.Id,
                Nombre = dto.PartidoSolicitante.Nombre,
                Estado = Estado,
                Siglas = dto.PartidoSolicitante.Siglas,
            };

            return View("UpdateStatus", vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(CambiarEstadoAlianzaViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            AlianzaPoliticaDto? dto = await _alianzasPoliticasServices.GetById(vm.Id);
            dto.Estado = vm.Estado;

            var result = await _alianzasPoliticasServices.UpdateAsync(dto);


            return RedirectToRoute(new { controller = "AlianzasPoliticas", action = "Index" });
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
            var relaciones = (await _alianzasPoliticasServices.GetAllWithInclude())
      .Where(a => a.PartidoSolicitanteId == dirigente.PartidoPoliticoId)
      .Select(a => a.PartidoDestinoId) 
      .ToList();

            var partidos = (await _partidoServices.GetAll())
           .Where(p =>
               p.EstaActivo &&
               p.Id != dirigente.PartidoPoliticoId &&
               !relaciones.Contains(p.Id))
           .ToList();

            ViewBag.Partidos = partidos.Select(p => new PartidoPoliticoViewModel
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                LogoUrl = p.LogoUrl,
                Siglas = p.Siglas,
                EstaActivo = p.EstaActivo
            });
            return View("Save", new SaveAlianzaPoliticaViewModel()
            {
                Id = 0,
                PartidoSolicitanteId = dirigente.PartidoPoliticoId,
                PartidoDestinoId = 0,
                Estado = (int)AlianzaEstado.Pendiente
            });
        }
 
        

        [HttpPost]
        public async Task<IActionResult> Create(SaveAlianzaPoliticaViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            if(!ModelState.IsValid)
            {

                var relaciones = (await _alianzasPoliticasServices.GetAllWithInclude())
          .Where(a => a.PartidoSolicitanteId == vm.PartidoSolicitanteId)
          .Select(a => a.PartidoDestinoId)
          .ToList();

                var partidos = (await _partidoServices.GetAll())
               .Where(p =>
                   p.EstaActivo &&
                   p.Id != vm.PartidoSolicitanteId &&
                   !relaciones.Contains(p.Id))
               .ToList();

                ViewBag.Partidos = partidos.Select(p => new PartidoPoliticoViewModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    LogoUrl = p.LogoUrl,
                    Siglas = p.Siglas,
                    EstaActivo = p.EstaActivo
                });
                return View("Save",vm);
            }
            AlianzaPoliticaDto dto = new AlianzaPoliticaDto
            {
                Id = 0,
                PartidoSolicitanteId = vm.PartidoSolicitanteId,
                PartidoDestinoId = vm.PartidoDestinoId,
                Estado = vm.Estado,
                FechaSolicitud = DateTime.Now
            };
            var result = await _alianzasPoliticasServices.AddAsync(dto);

            return RedirectToRoute(new { controller = "AlianzasPoliticas", action = "Index" });
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
            var dto = (await _alianzasPoliticasServices.GetAllWithInclude())
       .FirstOrDefault(a => a.Id == id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "Ciudadano", action = "Index" });
            }
            var vm = new DeleteAlianzaViewModel()
            {
                Id = dto.Id,
                Nombre = dto.PartidoDestino.Nombre,
                Siglas = dto.PartidoDestino.Siglas
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteAlianzaViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (_userSession.isAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            await _alianzasPoliticasServices.DeleteAsync(vm.Id);

            return RedirectToRoute(new { controller = "AlianzasPoliticas", action = "Index" });
        }
    }

}

