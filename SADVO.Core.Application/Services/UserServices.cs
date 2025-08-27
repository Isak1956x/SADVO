
using Microsoft.EntityFrameworkCore;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Application.Helpers;
using SADVO.Core.Application.Interfaces;
using SADVO.Core.Domain.Entities;
using SADVO.Core.Domain.Interfaces;

namespace SADVO.Core.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _partidoPoliticoService;

        public UserServices(IUserRepository userRepository)
        {
            _partidoPoliticoService = userRepository;
        }
        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            User? user = await _partidoPoliticoService.LoginAsync(dto.Username, dto.Password);
            if (user == null)
            {
               return null;
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                NombreUsuario = user.NombreUsuario,
                ContrasenaHash = user.ContrasenaHash,
                Rol = user.Rol,
                EstaActivo = user.EstaActivo
            };
            return userDto;

        }
        public async Task<bool> AddAsync(UserDto dto)
        {
            try
            {
                User user = new()
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Email = dto.Email,
                    NombreUsuario = dto.NombreUsuario,
                    ContrasenaHash = PasswordEncriptation.ComputeSha256Hash(dto.ContrasenaHash),
                    Rol = dto.Rol,
                    EstaActivo = dto.EstaActivo
                };
                User? returnEntity = await _partidoPoliticoService.AddAsync(user);
                if (returnEntity == null)
                {
                    return false;
                }
                return true;

            }
            catch(Exception)
            {
                return false;
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
        public async Task<List<UserDto>> GetAll()
        {
            try
            {
                List<User> ListEntities = await _partidoPoliticoService.GetAllList();
                List<UserDto> ListEntitiesDtos = ListEntities.Select(user => new UserDto
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Email = user.Email,
                    NombreUsuario = user.NombreUsuario,
                    ContrasenaHash = user.ContrasenaHash,
                    Rol = user.Rol,
                    EstaActivo = user.EstaActivo
                }).ToList();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<UserDto>();
            }
        }

        public async Task<List<UserDto>> GetAllWithInclude()
        {
           try
            {
                var ListEntitiesQuery =  _partidoPoliticoService.GetAllQueryWithInclude(["DirigentePolitico"]);
                var ListEntitiesDtos = await ListEntitiesQuery.Select(s => new UserDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Email = s.Email,
                    NombreUsuario = s.NombreUsuario,
                    ContrasenaHash = s.ContrasenaHash,
                    Rol = s.Rol,
                    EstaActivo = s.EstaActivo
                }).ToListAsync();
                return ListEntitiesDtos;
            }
            catch (Exception)
            {
                return new List<UserDto>();
            }
        }

        public async Task<UserDto?> GetById(int id)
        {
            try
            {
                User? user = await _partidoPoliticoService.GetById(id);
                if (user == null)
                {
                    return null;
                }
                UserDto userDto = new()
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Email = user.Email,
                    NombreUsuario = user.NombreUsuario,
                    ContrasenaHash = user.ContrasenaHash,
                    Rol = user.Rol,
                    EstaActivo = user.EstaActivo
                };
                return userDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdateAsync(UserDto dto)
        {
            try
            {
                var entituDb = await _partidoPoliticoService.GetById(dto.Id);
                if(entituDb == null)
                {
                    return false;
                }
                User user = new()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Email = dto.Email,
                    NombreUsuario = dto.NombreUsuario,
                    ContrasenaHash = PasswordEncriptation.ComputeSha256Hash(dto.ContrasenaHash),
                    Rol = dto.Rol,
                    EstaActivo = dto.EstaActivo
                };
                User? returnEntity = await _partidoPoliticoService.UpdateAsync(user.Id, user);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
