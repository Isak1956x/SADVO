
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class PuestoElectivoServices : IPuestoElectivoServices
    {
        private readonly IPuestoElectivoRepository _puestoElectivoService;

        public PuestoElectivoServices(IPuestoElectivoRepository userRepository)
        {
            _puestoElectivoService = userRepository;
        }

        public async Task<bool> AddAsync(PuestoElectivoDto dto)
        {
            try
            {
                PuestoElectivo puestoElectivo = new()
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    EstaActivo = dto.EstaActivo
                };
                PuestoElectivo? returnEntity = await _puestoElectivoService.AddAsync(puestoElectivo);
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
                await _puestoElectivoService.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<List<PuestoElectivoDto>> GetAll()
        {
            try
            {
                List<PuestoElectivo> ListEntities = await _puestoElectivoService.GetAllList();
                List<PuestoElectivoDto> ListEntitiesDtos = ListEntities.Select(puesto => new PuestoElectivoDto
                {
                    Id = puesto.Id,
                    Nombre = puesto.Nombre,
                    Descripcion = puesto.Descripcion,
                    EstaActivo = puesto.EstaActivo
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<PuestoElectivoDto>();
            }
        }

        public async Task<List<PuestoElectivoDto>> GetAllWithInclude()
        {
            try
            {
                var ListEntitiesQuery = _puestoElectivoService.GetAllQueryWithInclude(["Asignaciones"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new PuestoElectivoDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    EstaActivo = s.EstaActivo,
                    Asignaciones = s.Asignaciones.Select(a => new AsignacionPuestoDto
                    {
                        Id = a.Id,
                        CandidatoId = a.CandidatoId,
                        PuestoElectivoId = a.PuestoElectivoId,
                        PartidoPoliticoId = a.PartidoPoliticoId
                    }).ToList()
                }).ToListAsync();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<PuestoElectivoDto>();
            }
        }

        public async Task<PuestoElectivoDto?> GetById(int id)
        {
            try
            {
                PuestoElectivo? puesto = await _puestoElectivoService.GetById(id);
                if (puesto == null)
                {
                    return null;
                }
                PuestoElectivoDto puestoDto = new()
                {
                    Id = puesto.Id,
                    Nombre = puesto.Nombre,
                    Descripcion = puesto.Descripcion,
                    EstaActivo = puesto.EstaActivo
                }; ;
                return puestoDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdateAsync(PuestoElectivoDto dto)
        {
            try
            {
                var entityDb = await _puestoElectivoService.GetById(dto.Id);
                if (entityDb == null)
                {
                    return false;
                }
                PuestoElectivo puesto = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    EstaActivo = dto.EstaActivo
                };
                PuestoElectivo? returnEntity = await _puestoElectivoService.UpdateAsync(puesto.Id, puesto);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
