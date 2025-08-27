using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface ICiudadanoServices
    {
        Task<bool> AddAsync(CiudadanoDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<CiudadanoDto>> GetAll();
        Task<List<CiudadanoDto>> GetAllWithInclude();
        Task<CiudadanoDto?> GetById(int id);
        Task<bool> UpdateAsync(CiudadanoDto dto);
    }
}