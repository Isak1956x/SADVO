using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class AlianzasPoliticasServices : IAlianzaPoliticaServices
    {
        private readonly IAlianzaPoliticaRepository _alianzaPoliticaRepository;

        public AlianzasPoliticasServices(IAlianzaPoliticaRepository alianzaPoliticaRepository)
        {
            _alianzaPoliticaRepository = alianzaPoliticaRepository;
        }

        public async Task<bool> AddAsync(AlianzaPoliticaDto dto)
        {
           AlianzaPolitica alianzaPolitica = new AlianzaPolitica
           {
               PartidoSolicitanteId = dto.PartidoSolicitanteId,
               PartidoDestinoId = dto.PartidoDestinoId,
               FechaSolicitud = dto.FechaSolicitud,
               Estado = dto.Estado
           };
            var result = await _alianzaPoliticaRepository.AddAsync(alianzaPolitica);
            if(result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
           var alianza =await  _alianzaPoliticaRepository.GetById(id);
            if (alianza == null)
            {
                return false;
            }
            _alianzaPoliticaRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<AlianzaPoliticaDto>> GetAll()
        {
            var alianzas = await _alianzaPoliticaRepository.GetAllList();
            List<AlianzaPoliticaDto> alianzaDtos = alianzas.Select(a => new AlianzaPoliticaDto
            {
                Id = a.Id,
                PartidoSolicitanteId = a.PartidoSolicitanteId,
                PartidoDestinoId = a.PartidoDestinoId,
                FechaSolicitud = a.FechaSolicitud,
                Estado = a.Estado
            }).ToList();
            return alianzaDtos;
        }

        public async Task<List<AlianzaPoliticaDto>> GetAllWithInclude()
        {
            var alianzas = _alianzaPoliticaRepository.GetAllQueryWithInclude(["PartidoSolicitante", "PartidoDestino"]);
            var alianzaDtos = alianzas.Select(a => new AlianzaPoliticaDto
            {
                Id = a.Id,
                PartidoSolicitanteId = a.PartidoSolicitanteId,
                PartidoDestinoId = a.PartidoDestinoId,
                FechaSolicitud = a.FechaSolicitud,
                Estado = a.Estado,
                PartidoSolicitante = new PartidoPoliticoDto
                {
                    Id = a.PartidoSolicitante.Id,
                    Nombre = a.PartidoSolicitante.Nombre,
                    Siglas = a.PartidoSolicitante.Siglas,
                    LogoUrl = a.PartidoSolicitante.LogoUrl,
                    Descripcion = a.PartidoSolicitante.Descripcion,
                    EstaActivo = a.PartidoSolicitante.EstaActivo
                },
                PartidoDestino = new PartidoPoliticoDto
                {
                    Id = a.PartidoDestino.Id,
                    Nombre = a.PartidoDestino.Nombre,
                    Siglas = a.PartidoDestino.Siglas,
                    LogoUrl = a.PartidoDestino.LogoUrl,
                    EstaActivo = a.PartidoDestino.EstaActivo,
                    Descripcion = a.PartidoDestino.Descripcion
                }
            }).ToList();
            return alianzaDtos;
     
        }

        public async Task<AlianzaPoliticaDto?> GetById(int id)
        {
            var alianza = await _alianzaPoliticaRepository.GetById(id);
            if (alianza == null)
            {
                return null;
            }
            AlianzaPoliticaDto DTO = new AlianzaPoliticaDto
            {
                Id = alianza.Id,
                PartidoSolicitanteId = alianza.PartidoSolicitanteId,
                PartidoDestinoId = alianza.PartidoDestinoId,
                FechaSolicitud = alianza.FechaSolicitud,
                Estado = alianza.Estado,
                PartidoSolicitante = new PartidoPoliticoDto
                {
                    Id = alianza.PartidoSolicitante.Id,
                    Nombre = alianza.PartidoSolicitante.Nombre,
                    Siglas = alianza.PartidoSolicitante.Siglas,
                    LogoUrl = alianza.PartidoSolicitante.LogoUrl,
                    Descripcion = alianza.PartidoSolicitante.Descripcion,
                    EstaActivo = alianza.PartidoSolicitante.EstaActivo
                },
                PartidoDestino = new PartidoPoliticoDto
                {
                    Id = alianza.PartidoDestino.Id,
                    Nombre = alianza.PartidoDestino.Nombre,
                    Siglas = alianza.PartidoDestino.Siglas,
                    LogoUrl = alianza.PartidoDestino.LogoUrl,
                    EstaActivo = alianza.PartidoDestino.EstaActivo,
                    Descripcion = alianza.PartidoDestino.Descripcion
                }
            };
            return DTO;
        }

        public Task<bool> UpdateAsync(AlianzaPoliticaDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
