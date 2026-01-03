using MintLynk.Application.Dtos;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Interfaces
{
    public interface ILinkStatsService
    {
        Task<UrlVisitDto?> GetAsync(Guid id);

        Task<UrlVisitDto> CreateAsync(UrlVisitDto dto);

        Task<UrlVisitDto> UpdateAsync(UrlVisitDto dto);

        void Delete(Guid id);

        Task<IEnumerable<UrlVisitDto>> GetAllAsync(string linkId);

        Task<IEnumerable<UrlVisitDto>> GetAllAsync(List<string> linkIds);
    }
}
