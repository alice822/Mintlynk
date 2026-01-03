using MintLynk.Application.Dtos;
using MintLynk.Application.Extensions;
using MintLynk.Application.Helper;
using MintLynk.Application.Interfaces;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Interfaces;
using MintLynk.Domain.Models;

namespace MintLynk.Application.Services
{
    public class LinkStatService : ILinkStatsService
    {
        private readonly ILinkHistoryRepository _historyRepository;

        public LinkStatService(ILinkHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<UrlVisitDto> CreateAsync(UrlVisitDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = dto.ToSmartLinkHistory();
            var result  = await _historyRepository.CreateAsync(entity);
            return result.ToUrlVisitDto();
        }

        public void Delete(Guid id)
        {
            _historyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UrlVisitDto>> GetAllAsync(string linkId)
        {
            var data = await _historyRepository.GetAllAsync(linkId);
            if (data != null)
            {
                return data.Select(x => x.ToUrlVisitDto());
            }
            return Enumerable.Empty<UrlVisitDto>();
        }

        public async Task<IEnumerable<UrlVisitDto>> GetAllAsync(List<string> linkIds)
        {
            var data = await _historyRepository.GetAllAsync(linkIds);
            if (data != null)
            {
                return data.Select(x => x.ToUrlVisitDto());
            }
            return Enumerable.Empty<UrlVisitDto>();
        }

        public async Task<UrlVisitDto?> GetAsync(Guid id)
        {
            var data = await _historyRepository.GetAsync(id);

            if (data == null)
            {
                return null;
            }
            var dto = data.ToUrlVisitDto();
            return dto;
        }

        public async Task<UrlVisitDto> UpdateAsync(UrlVisitDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = dto.ToSmartLinkHistory();
            var result = await _historyRepository.UpdateAsync(entity);
            return result.ToUrlVisitDto();
        }
    }
}
