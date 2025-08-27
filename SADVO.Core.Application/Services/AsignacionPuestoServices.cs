
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class AsignacionPuestoServices : IAsignacionPuestoServices
    {
        private readonly IAsignacionPuestoRepository _asignacionPuestoRepository;

        public AsignacionPuestoServices(IAsignacionPuestoRepository asignacionPuestoRepository)
        {
            _asignacionPuestoRepository = asignacionPuestoRepository;
        }

        public async Task<bool> AddAsync(AsignacionPuestoDto dto)
        {
            try
            {
                AsignacionPuesto entity = new()
                {
                    Id = dto.Id,
                    PartidoPoliticoId = dto.PartidoPoliticoId,
                    CandidatoId = dto.CandidatoId,
                    PuestoElectivoId = dto.PuestoElectivoId
                };
                AsignacionPuesto? returnEntity = await _asignacionPuestoRepository.AddAsync(entity);
                if (returnEntity == null)
                {
                    return false;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _asignacionPuestoRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<List<AsignacionPuestoDto>> GetAll()
        {
            try
            {
                List<AsignacionPuesto> ListEntities = await _asignacionPuestoRepository.GetAllList();
                List<AsignacionPuestoDto> ListEntitiesDtos = ListEntities.Select(partido => new AsignacionPuestoDto
                {
                    Id = partido.Id,
                    PartidoPoliticoId = partido.PartidoPoliticoId,
                    CandidatoId = partido.CandidatoId,
                    PuestoElectivoId = partido.PuestoElectivoId
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<AsignacionPuestoDto>();
            }
        }

        public async Task<List<AsignacionPuestoDto>> GetAllWithInclude()
        {
            try
            {
                var ListEntitiesQuery = _asignacionPuestoRepository.GetAllQueryWithInclude(["Candidato", "PartidoPolitico", "PuestoElectivo"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new AsignacionPuestoDto
                {
                    Id = s.Id,
                    PartidoPoliticoId = s.PartidoPoliticoId,
                    CandidatoId = s.CandidatoId,
                    Candidato = new CandidatoDto
                    {
                        Id = s.Candidato.Id,
                        Nombre = s.Candidato.Nombre,
                        Apellido = s.Candidato.Apellido,
                        FotoUrl = s.Candidato.FotoUrl,
                        EstaActivo = s.Candidato.EstaActivo,
                        PartidoPoliticoId = s.Candidato.PartidoPoliticoId
                    },
                    PartidoPolitico = new PartidoPoliticoDto
                    {
                        Id = s.PartidoPolitico.Id,
                        Nombre = s.PartidoPolitico.Nombre,
                        Descripcion = s.PartidoPolitico.Descripcion,
                        EstaActivo = s.PartidoPolitico.EstaActivo,
                        LogoUrl = s.PartidoPolitico.LogoUrl,
                        Siglas = s.PartidoPolitico.Siglas
                    },
                    PuestoElectivoId = s.PuestoElectivoId,
                    PuestoElectivo = new PuestoElectivoDto
                    {
                        Id = s.PuestoElectivo.Id,
                        Nombre = s.PuestoElectivo.Nombre,
                        Descripcion = s.PuestoElectivo.Descripcion,
                        EstaActivo = s.PuestoElectivo.EstaActivo
                    }
                }).ToListAsync();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<AsignacionPuestoDto>();
            }
        }

        public async Task<AsignacionPuestoDto?> GetById(int id)
        {
            try
            {
                var asignacion = await _asignacionPuestoRepository.GetById(id);
                if (asignacion == null)
                {
                    return null;
                }
                AsignacionPuestoDto dto = new()
                {
                    Id = asignacion.Id,
                    PartidoPoliticoId = asignacion.PartidoPoliticoId,
                    CandidatoId = asignacion.CandidatoId,
                    PuestoElectivoId = asignacion.PuestoElectivoId
                }; ;
                return dto;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<bool> UpdateAsync(AsignacionPuestoDto dto)
        {
            try
            {
                var entityDb = await _asignacionPuestoRepository.GetById(dto.Id);
                if (entityDb == null)
                {
                    return false;
                }
                AsignacionPuesto asignacion = new()
                {
                    Id = dto.Id,
                    PartidoPoliticoId = dto.PartidoPoliticoId,
                    CandidatoId = dto.CandidatoId,
                    PuestoElectivoId = dto.PuestoElectivoId
                };
                AsignacionPuesto? returnEntity = await _asignacionPuestoRepository.UpdateAsync(asignacion.Id, asignacion);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
