using SADVO.Core.Application.Dtos;
using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface IEleccionServices
    {
        Task<bool> AddAsync(EleccionDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<EleccionDto>> GetAll();
        Task<List<EleccionDto>> GetAllWithInclude();
        Task<EleccionDto?> GetById(int id);
        Task<bool> UpdateAsync(EleccionDto dto);
        Task<List<int>> ObtenerAñosDisponibles();



    }
}