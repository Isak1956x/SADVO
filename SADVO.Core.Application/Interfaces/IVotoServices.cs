using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface IVotoServices
    {
        Task<bool> AddAsync(VotoDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<VotoDto>> GetAll();
        Task<List<VotoDto>> GetAllWithInclude();
        Task<VotoDto?> GetById(int id);
        Task<bool> UpdateAsync(VotoDto dto);
    }
}