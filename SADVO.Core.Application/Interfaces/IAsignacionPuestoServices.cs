using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface IAsignacionPuestoServices
    {
        Task<bool> AddAsync(AsignacionPuestoDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<AsignacionPuestoDto>> GetAll();
        Task<List<AsignacionPuestoDto>> GetAllWithInclude();
        Task<AsignacionPuestoDto?> GetById(int id);
        Task<bool> UpdateAsync(AsignacionPuestoDto dto);
    }
}