using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface IDirigentePoliticoServices
    {
        Task<bool> AddAsync(DirigentePoliticoDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<DirigentePoliticoDto>> GetAll();
        Task<List<DirigentePoliticoDto>> GetAllWithInclude();
        Task<DirigentePoliticoDto?> GetById(int id);
        Task<bool> UpdateAsync(DirigentePoliticoDto dto);
        Task<DirigentePoliticoDto?> GetByUserId(int id);
    }
}