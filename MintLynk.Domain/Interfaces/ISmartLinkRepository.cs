using MintLynk.Domain.Models;

namespace MintLynk.Domain.Interfaces
{
    public interface ISmartLinkRepository
    {
        Task<SmartLinkDto?> GetAsync(long id, string firstChar);

        Task<SmartLinkDto?> GetByShortUrlAsync(string shortCode);

        SmartLinkDto CreateAsync(SmartLinkDto url);

        Task<SmartLinkDto> UpdateAsync(SmartLinkDto url);

        Task<bool> DeleteAsync(string entityId);

        Task<IEnumerable<SmartLinkDto>> GetAllAsync(string firstChar);

        Task<IEnumerable<SmartLinkDto>> GetAllAsync(Guid userId);

        Task<SmartLinkDto?> GetByEntityIdAsync(string entityId);

        Task<IEnumerable<SmartLinkDto>> GetAllWithPagingAsync(char firstChar, int pageNumber, int pageSize);

        Task<long> GetNextIdAsync();
    }
}
