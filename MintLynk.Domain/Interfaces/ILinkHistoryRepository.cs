using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Domain.Interfaces
{
    public interface ILinkHistoryRepository
    {
        Task<LinkHistory?> GetAsync(Guid id);

        Task<LinkHistory> CreateAsync(LinkHistory history);

        Task<LinkHistory> UpdateAsync(LinkHistory history);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<LinkHistory>> GetAllAsync();

        Task<IEnumerable<LinkHistory>> GetAllAsync(string linkId);

        Task<IEnumerable<LinkHistory>> GetAllAsync(List<string> linkIds);
    }
}
