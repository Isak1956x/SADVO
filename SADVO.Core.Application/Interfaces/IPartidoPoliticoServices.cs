using SADVO.Core.Domain.Entities;

namespace SADVO.Core.Application.Interfaces
{
    public interface IPartidoPoliticoServices
    {
        Task<PartidoPoliticoDto> AddAsync(PartidoPoliticoDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<PartidoPoliticoDto>> GetAll();
        Task<List<PartidoPoliticoDto>> GetAllWithInclude();
        Task<PartidoPoliticoDto?> GetById(int id);
        Task<PartidoPoliticoDto> UpdateAsync(PartidoPoliticoDto dto);
    }
}