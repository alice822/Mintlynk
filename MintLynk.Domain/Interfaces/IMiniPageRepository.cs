using MediatR;
using System.Threading.Tasks;


namespace MintLynk.Domain.Interfaces
{
    public interface IMiniPageRepository
    {

        Task<MiniPage?> GetAsync(Guid id);

        Task<MiniPage?> GetAsync(string alias);

        Task<MiniPage> CreateAsync(MiniPage page);

        Task<MiniPage> UpdateAsync(MiniPage page);

        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<MiniPage>> GetAllAsync();

        Task<IEnumerable<MiniPage>> GetAllAsync(string userId);
    }
}