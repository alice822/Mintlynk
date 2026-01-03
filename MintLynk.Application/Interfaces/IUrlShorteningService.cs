using MintLynk.Domain.Entities.SmartLink;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Interfaces;
using MintLynk.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Interfaces
{
    public interface IUrlShorteningService
    {
        Task<SmartLinkDto?> GetAsync(long id, string firstChar);

        Task<SmartLinkDto> CreateAsync(SmartLinkDto dto);

        Task<SmartLinkDto> QuickShortUrl(SmartLinkDto dto);

        Task<SmartLinkDto> UpdateAsync(SmartLinkDto dto);

        Task<bool> Delete(string entityId);

        Task<IEnumerable<SmartLinkDto>> GetAllAsync(string firstChar);

        Task<IEnumerable<SmartLinkDto>> GetAllAsync(Guid userId);

        Task<SmartLinkDto?> GetAsync(string value, SmartLinkLookupType lookupType);

        Task<IEnumerable<SmartLinkDto>> GetAllAsync(char firstChar, int pageNumber, int pageSize);
    }
}
