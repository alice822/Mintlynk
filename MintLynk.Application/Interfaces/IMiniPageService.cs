using MediatR;
using MintLynk.Application.Dtos;
using System.Threading.Tasks;


namespace MintLynk.Application.Interfaces
{
    public interface IMiniPageService
    {

        Task<MiniPageDto?> GetAsync(Guid id);

        Task<MiniPageDto?> GetAsync(string alias);

        Task<MiniPageDto> CreateAsync(MiniPageDto page);

        Task<MiniPageDto> UpdateAsync(MiniPageDto page);

        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<MiniPageDto>> GetAllAsync();

        Task<IEnumerable<MiniPageDto>> GetAllAsync(string userId);

        Task<bool> AliasExistsAsync(string alias);
    }
}