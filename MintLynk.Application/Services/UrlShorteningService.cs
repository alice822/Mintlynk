using MintLynk.Application.Helper;
using MintLynk.Application.Interfaces;
using MintLynk.Domain.Entities.SmartLink;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Interfaces;
using MintLynk.Domain.Models;
using QRCoder;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Color = SixLabors.ImageSharp.Color;
using Image = SixLabors.ImageSharp.Image;
using Point = SixLabors.ImageSharp.Point;
using RectangleF = SixLabors.ImageSharp.RectangleF;
using PointF = SixLabors.ImageSharp.PointF;
using SixLabors.Fonts;
using SystemFonts = SixLabors.Fonts.SystemFonts;
using FontStyle = SixLabors.Fonts.FontStyle;
using Font = SixLabors.Fonts.Font;
using Rectangle = SixLabors.ImageSharp.Rectangle;

namespace MintLynk.Application.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        private readonly ISmartLinkRepository _urlRepository;

        public UrlShorteningService(ISmartLinkRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<SmartLinkDto?> GetAsync(long id, string firstChar)
        {
            var data = await _urlRepository.GetAsync(id, firstChar);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        public async Task<SmartLinkDto> CreateAsync(SmartLinkDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.DestinationUrl == null) throw new ArgumentNullException(nameof(dto.DestinationUrl));
            if (!UrlHelper.IsValidUrl(dto.DestinationUrl)) throw new ArgumentException("Invalid URL format", nameof(dto.DestinationUrl));

            var nextId = await _urlRepository.GetNextIdAsync();
            var shortUrl = UrlHelper.GenerateShortUrl();
            var entityId = shortUrl.Substring(0, 1).ToUpper() + nextId.ToString("D9");

            dto.ShortUrl = shortUrl;
            dto.EntityId = entityId;
            dto.Status = (int)Status.Active;

            var existingLink = await _urlRepository.GetByShortUrlAsync(dto.ShortUrl);
            if (existingLink == null)
            {
                return _urlRepository.CreateAsync(dto);
            }
            else
            {
                throw new InvalidOperationException("Short URL already exists.");
            }
        }
        public async Task<SmartLinkDto> UpdateAsync(SmartLinkDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.DestinationUrl == null) throw new ArgumentNullException(nameof(dto.DestinationUrl));
            if (!UrlHelper.IsValidUrl(dto.DestinationUrl)) throw new ArgumentException("Invalid URL format", nameof(dto.DestinationUrl));
            return await _urlRepository.UpdateAsync(dto);

        }

        public Task<bool> Delete(string entityId)
        {
            return _urlRepository.DeleteAsync(entityId);
        }

        public async Task<IEnumerable<SmartLinkDto>> GetAllAsync(string firstChar)
        {
            var data = await _urlRepository.GetAllAsync(firstChar);
            if (data != null)
            {
                return data;
            }
            return Enumerable.Empty<SmartLinkDto>();
        }

        public Task<SmartLinkDto?> GetAsync(string value, SmartLinkLookupType lookupType)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (lookupType == SmartLinkLookupType.ShortUrl)
            {
                return _urlRepository.GetByShortUrlAsync(value);
            }
            else if (lookupType == SmartLinkLookupType.Entity)
            {
                return _urlRepository.GetByEntityIdAsync(value);
            }
            else
            {
                throw new ArgumentException("Invalid lookup type", nameof(lookupType));
            }
        }

        public async Task<IEnumerable<SmartLinkDto>> GetAllAsync(char firstChar, int pageNumber, int pageSize)
        {
            var data = await _urlRepository.GetAllWithPagingAsync(firstChar, pageNumber, pageSize);
            if (data != null)
            {
                return data;
            }
            return Enumerable.Empty<SmartLinkDto>();
        }

        public async Task<SmartLinkDto> QuickShortUrl(SmartLinkDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (!UrlHelper.IsValidUrl(dto.DestinationUrl)) throw new ArgumentException("Invalid URL format", nameof(dto.DestinationUrl));

            var nextId = await _urlRepository.GetNextIdAsync();
            var shortUrl = UrlHelper.GenerateShortUrl();
            var entityId = shortUrl.Substring(0, 1).ToUpper() + nextId;

            dto.ShortUrl = shortUrl;
            dto.EntityId = entityId;
            dto.Status = (int)Status.Active;
            dto.Title= entityId;

            var existingLink = await _urlRepository.GetByShortUrlAsync(dto.ShortUrl);
            if (existingLink == null)
            {
                return _urlRepository.CreateAsync(dto);
            }
            else
            {
                throw new InvalidOperationException("Short URL already exists.");
            }
        }

        public async Task<IEnumerable<SmartLinkDto>> GetAllAsync(Guid userId)
        {
            var data = await _urlRepository.GetAllAsync(userId);
            if (data != null)
            {
                return data;
            }
            return Enumerable.Empty<SmartLinkDto>();
        }
    }
}
