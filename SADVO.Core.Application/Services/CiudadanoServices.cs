
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class CiudadanoServices : ICiudadanoServices
    {
        private readonly ICiudadanoRepository _ciudadanoRepository;

        public CiudadanoServices(ICiudadanoRepository ciudadanoRepository)
        {
            _ciudadanoRepository = ciudadanoRepository;
        }

        public async Task<bool> AddAsync(CiudadanoDto dto)
        {
            try
            {
                Ciudadano Ciudadano = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    EstaActivo = dto.EstaActivo,
                    Apellido = dto.Apellido,
                    DocumentoIdentidad = dto.DocumentoIdentidad,
                    Email = dto.Email
                };
                Ciudadano? returnEntity = await _ciudadanoRepository.AddAsync(Ciudadano);
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
                await _ciudadanoRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<List<CiudadanoDto>> GetAll()
        {
            try
            {
                List<Ciudadano> ListEntities = await _ciudadanoRepository.GetAllList();
                List<CiudadanoDto> ListEntitiesDtos = ListEntities.Select(ciudadano => new CiudadanoDto
                {
                    Id = ciudadano.Id,
                    Nombre = ciudadano.Nombre,
                    EstaActivo = ciudadano.EstaActivo,
                    Apellido = ciudadano.Apellido,
                    DocumentoIdentidad = ciudadano.DocumentoIdentidad,
                    Email = ciudadano.Email
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<CiudadanoDto>();
            }
        }

        public async Task<List<CiudadanoDto>> GetAllWithInclude()
        {
            try
            {
                var ListEntitiesQuery = _ciudadanoRepository.GetAllQueryWithInclude(["Voto"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new CiudadanoDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    EstaActivo = s.EstaActivo,
                    Apellido = s.Apellido,
                    DocumentoIdentidad = s.DocumentoIdentidad,
                    Email = s.Email,
                    Votos = s.Votos.Select(a => new VotoDto
                    {
                        Id = a.Id,
                        CiudadanoId = a.CiudadanoId,
                        CandidatoId = a.CandidatoId,
                        EleccionId = a.EleccionId,
                        PuestoElectivoId = a.PuestoElectivoId,
                    }).ToList()
                }).ToListAsync();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<CiudadanoDto>();
            }
        }

        public async Task<CiudadanoDto?> GetById(int id)
        {
            try
            {
                Ciudadano? ciudadano = await _ciudadanoRepository.GetById(id);
                if (ciudadano == null)
                {
                    return null;
                }
                CiudadanoDto ciudadanoDto = new()
                {
                    Id = ciudadano.Id,
                    Nombre = ciudadano.Nombre,
                    EstaActivo = ciudadano.EstaActivo,
                    Apellido = ciudadano.Apellido,
                    DocumentoIdentidad = ciudadano.DocumentoIdentidad,
                    Email = ciudadano.Email
                }; ;
                return ciudadanoDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdateAsync(CiudadanoDto dto)
        {
            try
            {
                var entityDb = await _ciudadanoRepository.GetById(dto.Id);
                if (entityDb == null)
                {
                    return false;
                }
                Ciudadano ciudadano = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Email = dto.Email,
                    DocumentoIdentidad = dto.DocumentoIdentidad,
                    EstaActivo = dto.EstaActivo
                };
                Ciudadano? returnEntity = await _ciudadanoRepository.UpdateAsync(ciudadano.Id, ciudadano);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
