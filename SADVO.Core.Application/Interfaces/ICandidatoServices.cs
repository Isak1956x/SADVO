using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface ICandidatoServices
    {
        Task<CandidatoDto?> AddAsync(CandidatoDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<CandidatoDto>> GetAll();
        Task<List<CandidatoDto>> GetAllWithInclude();
        Task<CandidatoDto?> GetById(int id);
        Task<CandidatoDto?> UpdateAsync(CandidatoDto dto);
    }
}