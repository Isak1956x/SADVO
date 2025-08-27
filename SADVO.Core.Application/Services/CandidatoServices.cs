
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class CandidatoServices : ICandidatoServices
    {
        private readonly ICandidatoRepository _candidatoServices;

        public CandidatoServices(ICandidatoRepository candidatoServices)
        {
            _candidatoServices = candidatoServices;
        }

        public async Task<CandidatoDto?> AddAsync(CandidatoDto dto)
        {
            try
            {
                Candidato entity = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    FotoUrl = dto.FotoUrl,
                    Apellido = dto.Apellido,
                    EstaActivo = dto.EstaActivo,
                    PartidoPoliticoId = dto.PartidoPoliticoId
                };
                Candidato? returnEntity = await _candidatoServices.AddAsync(entity);
                CandidatoDto Dto = new()
                {
                    Id = returnEntity.Id,
                    Nombre = returnEntity.Nombre,
                    Apellido = returnEntity.Apellido,
                    FotoUrl = returnEntity.FotoUrl,
                    EstaActivo = returnEntity.EstaActivo,
                    PartidoPoliticoId = returnEntity.PartidoPoliticoId
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
                await _candidatoServices.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<List<CandidatoDto>> GetAll()
        {
            try
            {
                List<Candidato> ListEntities = await _candidatoServices.GetAllList();
                List<CandidatoDto> ListEntitiesDtos = ListEntities.Select(candidato => new CandidatoDto
                {
                    Id = candidato.Id,
                    Apellido = candidato.Apellido,
                    EstaActivo = candidato.EstaActivo,
                    FotoUrl = candidato.FotoUrl,
                    Nombre = candidato.Nombre,
                    PartidoPoliticoId = candidato.PartidoPoliticoId
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<CandidatoDto>();
            }
        }

        public async Task<List<CandidatoDto>> GetAllWithInclude()
        {
            try
            {
                var ListEntitiesQuery = _candidatoServices.GetAllQueryWithInclude(["PartidoPolitico", "Votos", "Asignaciones"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new CandidatoDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    EstaActivo = s.EstaActivo,
                    FotoUrl = s.FotoUrl,
                    PartidoPoliticoId = s.PartidoPoliticoId,

                    PartidoPolitico = new PartidoPoliticoDto()
                    {
                        Id = s.PartidoPolitico.Id,
                        Nombre = s.PartidoPolitico.Nombre,
                        Descripcion = s.PartidoPolitico.Descripcion,
                        EstaActivo = s.PartidoPolitico.EstaActivo,
                        LogoUrl = s.PartidoPolitico.LogoUrl,
                        Siglas = s.PartidoPolitico.Siglas
                    },


                    Votos = s.Votos.Select(a => new VotoDto
                    {
                        CandidatoId = a.CandidatoId,
                        Id = a.Id,
                        CiudadanoId = a.CiudadanoId,
                        EleccionId = a.EleccionId,
                        PuestoElectivoId = a.PuestoElectivoId,
                    }).ToList(),
                    Asignaciones = s.Asignaciones.Select(asig => new AsignacionPuestoDto()
                    {
                        CandidatoId = asig.CandidatoId,
                        Id = asig.Id,
                        PuestoElectivoId = asig.PuestoElectivoId,
                        PartidoPoliticoId = asig.PartidoPoliticoId
                    }).ToList()
                }).ToListAsync();

                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<CandidatoDto>();
            }
        }

        public async Task<CandidatoDto?> GetById(int id)
        {
            try
            {
                Candidato? candidato = await _candidatoServices.GetById(id);
                if (candidato == null)
                {
                    return null;
                }
                CandidatoDto candidatoDto = new()
                {
                    Nombre = candidato.Nombre,
                    Apellido = candidato.Apellido,
                    Id = candidato.Id,
                    EstaActivo = candidato.EstaActivo,
                    FotoUrl = candidato.FotoUrl,
                    PartidoPoliticoId = candidato.PartidoPoliticoId
                }; ;
                return candidatoDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<CandidatoDto?> UpdateAsync(CandidatoDto dto)
        {
            try
            {
                var entityDb = await _candidatoServices.GetById(dto.Id);
                if (entityDb == null)
                {
                    return null;
                }
                Candidato candidato = new()
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Id = dto.Id,
                    EstaActivo = dto.EstaActivo,
                    FotoUrl = dto.FotoUrl,
                    PartidoPoliticoId = dto.PartidoPoliticoId
                };
                Candidato? returnEntity = await _candidatoServices.UpdateAsync(candidato.Id, candidato);
                CandidatoDto candidatoDto = new()
                {
                    Nombre = returnEntity.Nombre,
                    Apellido = returnEntity.Apellido,
                    Id = returnEntity.Id,
                    EstaActivo = returnEntity.EstaActivo,
                    FotoUrl = returnEntity.FotoUrl,
                    PartidoPoliticoId = returnEntity.PartidoPoliticoId
                }; ;
                return candidatoDto;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
