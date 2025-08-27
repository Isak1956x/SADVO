
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class DirigentePoliticoServices : IDirigentePoliticoServices
    {
        private readonly IDirgentePoliticoRepository _dirigentePoliticoService;

        public DirigentePoliticoServices(IDirgentePoliticoRepository dirigentePoliticoService)
        {
            _dirigentePoliticoService = dirigentePoliticoService;
        }

        public async Task<bool> AddAsync(DirigentePoliticoDto dto)
        {
            try
            {
                DirigentePolitico entity = new()
                {
                    Id = dto.Id,
                    PartidoPoliticoId = dto.PartidoPoliticoId,
                    UsuarioId = dto.UsuarioId
                };
                DirigentePolitico? returnEntity = await _dirigentePoliticoService.AddAsync(entity);
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
                await _dirigentePoliticoService.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<List<DirigentePoliticoDto>> GetAll()
        {
            try
            {
                List<DirigentePolitico> ListEntities = await _dirigentePoliticoService.GetAllList();
                List<DirigentePoliticoDto> ListEntitiesDtos = ListEntities.Select(partido => new DirigentePoliticoDto
                {
                    Id = partido.Id,
                    PartidoPoliticoId = partido.PartidoPoliticoId,
                    UsuarioId = partido.UsuarioId
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<DirigentePoliticoDto>();
            }
        }

        public async Task<List<DirigentePoliticoDto>> GetAllWithInclude()
        {
            try
            {
                var ListEntitiesQuery = _dirigentePoliticoService.GetAllQueryWithInclude(["Usuario", "PartidoPolitico"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new DirigentePoliticoDto
                {
                    Id = s.Id,
                    PartidoPoliticoId = s.PartidoPoliticoId,
                    UsuarioId = s.UsuarioId,
                    Usuario = new UserDto
                    {
                        Id = s.Usuario.Id,
                        Nombre = s.Usuario.Nombre,
                        Apellido = s.Usuario.Apellido,
                        Email = s.Usuario.Email,
                        NombreUsuario = s.Usuario.NombreUsuario,
                        ContrasenaHash = s.Usuario.ContrasenaHash,
                        Rol = s.Usuario.Rol,
                        EstaActivo = s.Usuario.EstaActivo
                    },
                    PartidoPolitico = new PartidoPoliticoDto
                    {
                        Id = s.PartidoPolitico.Id,
                        Nombre = s.PartidoPolitico.Nombre,
                        Descripcion = s.PartidoPolitico.Descripcion,
                        EstaActivo = s.PartidoPolitico.EstaActivo,
                        LogoUrl = s.PartidoPolitico.LogoUrl,
                        Siglas = s.PartidoPolitico.Siglas
                    }
                }).ToListAsync();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<DirigentePoliticoDto>();
            }
        }

        public async Task<DirigentePoliticoDto?> GetById(int id)
        {
            try
            {
                var query = _dirigentePoliticoService
            .GetAllQuery()
            .Include(dp => dp.PartidoPolitico);
                var puesto = await query.FirstOrDefaultAsync(dp => dp.Id == id);
                if (puesto == null)
                {
                    return null;
                }
                DirigentePoliticoDto dirigenteDto = new()
                {
                    Id = puesto.Id,
                    PartidoPoliticoId = puesto.PartidoPoliticoId,
                    UsuarioId = puesto.UsuarioId,
                    PartidoPolitico = new PartidoPoliticoDto
                    {
                        Id = puesto.PartidoPolitico.Id,
                        Nombre = puesto.PartidoPolitico.Nombre,
                        Descripcion = puesto.PartidoPolitico.Descripcion,
                        EstaActivo = puesto.PartidoPolitico.EstaActivo,
                        LogoUrl = puesto.PartidoPolitico.LogoUrl,
                        Siglas = puesto.PartidoPolitico.Siglas
                    },
                }; ;
                return dirigenteDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<DirigentePoliticoDto?> GetByUserId(int id)
        {
            try
            {
                var query = _dirigentePoliticoService
            .GetAllQuery()
            .Include(dp => dp.PartidoPolitico);
                var puesto = await query.FirstOrDefaultAsync(dp => dp.UsuarioId == id);
                if (puesto == null)
                {
                    return null;
                }
                DirigentePoliticoDto dirigenteDto = new()
                {
                    Id = puesto.Id,
                    PartidoPoliticoId = puesto.PartidoPoliticoId,
                    UsuarioId = puesto.UsuarioId,
                    PartidoPolitico = new PartidoPoliticoDto
                    {
                        Id = puesto.PartidoPolitico.Id,
                        Nombre = puesto.PartidoPolitico.Nombre,
                        Descripcion = puesto.PartidoPolitico.Descripcion,
                        EstaActivo = puesto.PartidoPolitico.EstaActivo,
                        LogoUrl = puesto.PartidoPolitico.LogoUrl,
                        Siglas = puesto.PartidoPolitico.Siglas
                    },
                }; ;
                return dirigenteDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdateAsync(DirigentePoliticoDto dto)
        {
            try
            {
                var entityDb = await _dirigentePoliticoService.GetById(dto.Id);
                if (entityDb == null)
                {
                    return false;
                }
                DirigentePolitico partido = new()
                {
                    Id = dto.Id,
                    PartidoPoliticoId = dto.PartidoPoliticoId,
                    UsuarioId = dto.UsuarioId
                };
                DirigentePolitico? returnEntity = await _dirigentePoliticoService.UpdateAsync(partido.Id, partido);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
