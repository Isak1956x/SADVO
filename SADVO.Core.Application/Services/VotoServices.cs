
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class VotoServices : IVotoServices
    {
        private readonly IVotoRepository _votoRepository;

        public VotoServices(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }

        public async Task<bool> AddAsync(VotoDto dto)
        {
            try
            {
                Voto Voto = new()
                {
                    Id = dto.Id,
                    CandidatoId = dto.CandidatoId,
                    CiudadanoId = dto.CiudadanoId,
                    EleccionId = dto.EleccionId,
                    PuestoElectivoId = dto.PuestoElectivoId,
                    FechaHora = dto.FechaHora
                };
                Voto? returnEntity = await _votoRepository.AddAsync(Voto);
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
                await _votoRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<List<VotoDto>> GetAll()
        {
            try
            {
                List<Voto> ListEntities = await _votoRepository.GetAllList();
                List<VotoDto> ListEntitiesDtos = ListEntities.Select(Voto => new VotoDto
                {
                    Id = Voto.Id,
                    CandidatoId = Voto.CandidatoId,
                    CiudadanoId = Voto.CiudadanoId,
                    EleccionId = Voto.EleccionId,
                    PuestoElectivoId = Voto.PuestoElectivoId,
                    FechaHora = Voto.FechaHora
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<VotoDto>();
            }
        }

        public async Task<List<VotoDto>> GetAllWithInclude()
        {
            try
            {
                var ListEntitiesQuery = _votoRepository.GetAllQueryWithInclude(["Candidato", "PuestoElectivo", "Eleccion", "Ciudadano"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new VotoDto
                {
                    Id = s.Id,
                    CandidatoId = s.CandidatoId,
                    CiudadanoId = s.CiudadanoId,
                    EleccionId = s.EleccionId,
                    PuestoElectivoId = s.PuestoElectivoId,
                    FechaHora = s.FechaHora,
                    Candidato = new CandidatoDto
                    {
                        Id = s.Candidato.Id,
                        Nombre = s.Candidato.Nombre,
                        PartidoPoliticoId = s.Candidato.PartidoPoliticoId,
                        EstaActivo = s.Candidato.EstaActivo,
                        Apellido = s.Candidato.Apellido,
                        FotoUrl = s.Candidato.FotoUrl
                    },
                    PuestoElectivo = new PuestoElectivoDto
                    {
                        Id = s.PuestoElectivo.Id,
                        Nombre = s.PuestoElectivo.Nombre,
                        Descripcion = s.PuestoElectivo.Descripcion,
                        EstaActivo = s.PuestoElectivo.EstaActivo
                    },
                    Eleccion = new EleccionDto
                    {
                        Id = s.Eleccion.Id,
                        Nombre = s.Eleccion.Nombre,
                        Fecha = s.Eleccion.Fecha,
                        EstaFinalizada = s.Eleccion.EstaFinalizada
                    },
                    Ciudadano = new CiudadanoDto
                    {
                        Id = s.Ciudadano.Id,
                        Nombre = s.Ciudadano.Nombre,
                        Apellido = s.Ciudadano.Apellido,
                        DocumentoIdentidad = s.Ciudadano.DocumentoIdentidad,
                        Email = s.Ciudadano.Email,
                        EstaActivo = s.Ciudadano.EstaActivo
                    }
                }).ToListAsync();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<VotoDto>();
            }
        }

        public async Task<VotoDto?> GetById(int id)
        {
            try
            {
                Voto? Voto = await _votoRepository.GetById(id);
                if (Voto == null)
                {
                    return null;
                }
                VotoDto VotoDto = new()
                {
                    Id = Voto.Id,
                    CandidatoId = Voto.CandidatoId,
                    CiudadanoId = Voto.CiudadanoId,
                    EleccionId = Voto.EleccionId,
                    PuestoElectivoId = Voto.PuestoElectivoId,
                    FechaHora = Voto.FechaHora
                }; ;

                return VotoDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdateAsync(VotoDto dto)
        {
            try
            {
                var entityDb = await _votoRepository.GetById(dto.Id);
                if (entityDb == null)
                {
                    return false;
                }
                Voto Voto = new()
                {
                    Id = dto.Id,
                    CandidatoId = dto.CandidatoId,
                    CiudadanoId = dto.CiudadanoId,
                    EleccionId = dto.EleccionId,
                    PuestoElectivoId = dto.PuestoElectivoId,
                    FechaHora = dto.FechaHora
                };
                Voto? returnEntity = await _votoRepository.UpdateAsync(Voto.Id, Voto);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
