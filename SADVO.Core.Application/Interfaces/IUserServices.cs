using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SADVO.Core.Application.Dtos;
using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface IUserServices
    {
        Task<bool> AddAsync(UserDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<UserDto>> GetAll();
        Task<List<UserDto>> GetAllWithInclude();
        Task<UserDto> GetById(int id);
        Task<bool> UpdateAsync(UserDto dto);
        Task<UserDto?> LoginAsync(LoginDto dto);
    }
}
