
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class PartidoPoliticoServices : IPartidoPoliticoServices
    {
        private readonly IPartidoPoliticoRepository _partidoPoliticoService;

        public PartidoPoliticoServices(IPartidoPoliticoRepository partidoPoliticoRepository)
        {
            _partidoPoliticoService = partidoPoliticoRepository;
        }

        public async Task<PartidoPoliticoDto?> AddAsync(PartidoPoliticoDto dto)
        {
            try
            {
                PartidoPolitico entity = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Siglas = dto.Siglas,
                    Descripcion = dto.Descripcion,
                    LogoUrl = dto.LogoUrl,
                    EstaActivo = dto.EstaActivo
                };
                PartidoPolitico? returnEntity = await _partidoPoliticoService.AddAsync(entity);
                PartidoPoliticoDto Dto = new()
                {
                    Id = returnEntity.Id,
                    Nombre = returnEntity.Nombre,
                    Siglas = returnEntity.Siglas,
                    Descripcion = returnEntity.Descripcion,
                    LogoUrl = returnEntity.LogoUrl,
                    EstaActivo = returnEntity.EstaActivo
                };
                if (returnEntity == null)
                {
                    return null;
                }
                return Dto;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _partidoPoliticoService.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<List<PartidoPoliticoDto>> GetAll()
        {
            try
            {
                List<PartidoPolitico> ListEntities = await _partidoPoliticoService.GetAllList();
                List<PartidoPoliticoDto> ListEntitiesDtos = ListEntities.Select(partido => new PartidoPoliticoDto
                {
                    Id = partido.Id,
                    Nombre = partido.Nombre,
                    Descripcion = partido.Descripcion,
                    EstaActivo = partido.EstaActivo,
                    LogoUrl = partido.LogoUrl,
                    Siglas = partido.Siglas
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<PartidoPoliticoDto>();
            }
        }

        public async Task<List<PartidoPoliticoDto>> GetAllWithInclude()
        {
            try
            {
                var ListEntitiesQuery = _partidoPoliticoService.GetAllQueryWithInclude(["Candidatos", "Alianzas"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new PartidoPoliticoDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    EstaActivo = s.EstaActivo,
                    Siglas = s.Siglas,
                    LogoUrl = s.LogoUrl,
                    Candidatos = s.Candidatos.Select(a => new CandidatoDto
                    {
                        Id = a.Id,
                        PartidoPoliticoId = a.PartidoPoliticoId,
                        Nombre = a.Nombre,
                        Apellido = a.Apellido,
                        FotoUrl = a.FotoUrl,
                        EstaActivo = a.EstaActivo
                    }).ToList()
                }).ToListAsync();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<PartidoPoliticoDto>();
            }
        }

        public async Task<PartidoPoliticoDto?> GetById(int id)
        {
            try
            {
                PartidoPolitico? puesto = await _partidoPoliticoService.GetById(id);
                if (puesto == null)
                {
                    return null;
                }
                PartidoPoliticoDto partidoDto = new()
                {
                    Nombre = puesto.Nombre,
                    Descripcion = puesto.Descripcion,
                    EstaActivo = puesto.EstaActivo,
                    LogoUrl = puesto.LogoUrl,
                    Siglas = puesto.Siglas,
                    Id = id
                }; ;
                return partidoDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<PartidoPoliticoDto?> UpdateAsync(PartidoPoliticoDto dto)
        {
            try
            {
                var entityDb = await _partidoPoliticoService.GetById(dto.Id);
                if (entityDb == null)
                {
                    return null;
                }
                PartidoPolitico partido = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    EstaActivo = dto.EstaActivo,
                    Siglas = dto.Siglas,
                    LogoUrl = dto.LogoUrl
                };
                PartidoPolitico? returnEntity = await _partidoPoliticoService.UpdateAsync(partido.Id, partido);
                PartidoPoliticoDto Dto = new()
                {
                    Id = returnEntity.Id,
                    Nombre = returnEntity.Nombre,
                    Siglas = returnEntity.Siglas,
                    Descripcion = returnEntity.Descripcion,
                    LogoUrl = returnEntity.LogoUrl,
                    EstaActivo = returnEntity.EstaActivo
                };
                return Dto;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
