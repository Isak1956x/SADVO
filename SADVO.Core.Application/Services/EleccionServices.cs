
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class EleccionServices : IEleccionServices
    {
        private readonly IEleccionRepository _eleccionRepository;

        public EleccionServices(IEleccionRepository eleccionRepository)
        {
            _eleccionRepository = eleccionRepository;
        }

        public async Task<bool> AddAsync(EleccionDto dto)
        {
            try
            {
                Eleccion Eleccion = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Fecha = dto.Fecha,
                    EstaFinalizada = dto.EstaFinalizada
                };
                Eleccion? returnEntity = await _eleccionRepository.AddAsync(Eleccion);
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
                await _eleccionRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<List<EleccionDto>> GetAll()
        {
            try
            {
                List<Eleccion> ListEntities = await _eleccionRepository.GetAllList();
                List<EleccionDto> ListEntitiesDtos = ListEntities.Select(Eleccion => new EleccionDto
                {
                    Id = Eleccion.Id,
                    Nombre = Eleccion.Nombre,
                    Fecha = Eleccion.Fecha,
                    EstaFinalizada = Eleccion.EstaFinalizada
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<EleccionDto>();
            }
        }

        public async Task<List<EleccionDto>> GetAllWithInclude()
        {
            try
            {
                var ListEntitiesQuery = _eleccionRepository.GetAllQueryWithInclude(["Votos"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new EleccionDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Fecha = s.Fecha,
                    EstaFinalizada = s.EstaFinalizada,
                    Votos = s.Votos.Select(a => new VotoDto
                    {
                        Id = a.Id,
                        EleccionId = a.EleccionId,
                        CandidatoId = a.CandidatoId,
                        PuestoElectivoId = a.PuestoElectivoId,
                        CiudadanoId = a.CiudadanoId
                    }).ToList()
                }).ToListAsync();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<EleccionDto>();
            }
        }

        public async Task<EleccionDto?> GetById(int id)
        {
            try
            {
                Eleccion? Eleccion = await _eleccionRepository.GetById(id);
                if (Eleccion == null)
                {
                    return null;
                }
                EleccionDto EleccionDto = new()
                {
                    Id = Eleccion.Id,
                    Nombre = Eleccion.Nombre,
                    Fecha = Eleccion.Fecha,
                    EstaFinalizada = Eleccion.EstaFinalizada
                }; ;
                return EleccionDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdateAsync(EleccionDto dto)
        {
            try
            {
                var entityDb = await _eleccionRepository.GetById(dto.Id);
                if (entityDb == null)
                {
                    return false;
                }
                Eleccion Eleccion = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Fecha = dto.Fecha,
                    EstaFinalizada = dto.EstaFinalizada
                };
                Eleccion? returnEntity = await _eleccionRepository.UpdateAsync(Eleccion.Id, Eleccion);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<int>> ObtenerAñosDisponibles()
        {
            try
            {
                var años = await _eleccionRepository.ObtenerAniosDisponiblesAsync();
                return años;
            }
            catch(Exception)
            {
                return new List<int>();
            }
        }
       
    }
}
