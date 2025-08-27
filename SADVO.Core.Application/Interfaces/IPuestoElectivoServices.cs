using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface IPuestoElectivoServices
    {
        Task<bool> AddAsync(PuestoElectivoDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<PuestoElectivoDto>> GetAll();
        Task<List<PuestoElectivoDto>> GetAllWithInclude();
        Task<PuestoElectivoDto?> GetById(int id);
        Task<bool> UpdateAsync(PuestoElectivoDto dto);
    }
}