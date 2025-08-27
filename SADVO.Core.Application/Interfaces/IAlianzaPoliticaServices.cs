using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface IAlianzaPoliticaServices
    {
        Task<bool> AddAsync(AlianzaPoliticaDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<AlianzaPoliticaDto>> GetAll();
        Task<List<AlianzaPoliticaDto>> GetAllWithInclude();
        Task<AlianzaPoliticaDto?> GetById(int id);
        Task<bool> UpdateAsync(AlianzaPoliticaDto dto);
    }
}