using Microsoft.EntityFrameworkCore;
using MintLynk.Domain.Interfaces;
using MintLynk.Domain.Models;
using MintLynk.Infrastructure.Data;

namespace MintLynk.Infrastructure.Repositories
{
    public class SmartLinkRepository : ISmartLinkRepository
    {
        private readonly ApplicationDbContext _context;
        public SmartLinkRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<SmartLinkDto?> GetAsync(long id, string firstChar)
        {
            var result = await _context.SmartLinkDtos
                .FromSqlInterpolated($@"
            EXEC GetSmartLinkByIdDynamic @Id={id}, @FirstChar={firstChar}
        ")
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public SmartLinkDto CreateAsync(SmartLinkDto dto)
        {
            try
            {
                var result = _context.SmartLinkDtos.FromSqlInterpolated($@"
                EXEC InsertSmartLink 
                    @EntityId={dto.EntityId}, 
                    @Title={dto.Title ?? dto.EntityId}, 
                    @Description={dto.Description ?? string.Empty}, 
                    @DestinationUrl={dto.DestinationUrl}, 
                    @ShortUrl={dto.ShortUrl}, 
                    @Tags={dto.Tags ?? string.Empty}, 
                    @UtmParameters={dto.UtmParameters ?? string.Empty}, 
                    @LinkType={dto.LinkType}, 
                    @HasExpirationDate={dto.HasExpirationDate}, 
                    @ExpirationDate={dto.ExpirationDate}, 
                    @Status={dto.Status},
	                @Created ={DateTime.UtcNow},
	                @LastModified={DateTime.UtcNow},
	                @CreatedBy={dto.CreatedBy},
	                @LastModifiedBy ={dto.LastModifiedBy}
            ").ToListAsync();

                return result.Result.FirstOrDefault() ?? throw new InvalidOperationException("Failed to create link.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the link.", ex);
            }
        }

        public async Task<SmartLinkDto> UpdateAsync(SmartLinkDto dto)
        {
            var result = await _context.SmartLinkDtos
        .FromSqlInterpolated($@"
            EXEC UpdateSmartLink 
                @EntityId={dto.EntityId}, 
                @Title={dto.Title}, 
                @Description={dto.Description}, 
                @DestinationUrl={dto.DestinationUrl}, 
                @ShortUrl={dto.ShortUrl}, 
                @Tags={dto.Tags}, 
                @UtmParameters={dto.UtmParameters}, 
                @LinkType={dto.LinkType}, 
                @HasExpirationDate={dto.HasExpirationDate}, 
                @ExpirationDate={dto.ExpirationDate}, 
                @Status={dto.Status},
                @Created = {dto.Created},
                @CreatedBy = {dto.CreatedBy},
                @LastModified={dto.LastModified},
                @LastModifiedBy={dto.LastModifiedBy}
        ")
        .ToListAsync();

            return result.FirstOrDefault() ?? throw new InvalidOperationException("Failed to update link.");
        }

        public async Task<bool> DeleteAsync(string entityId)
        {
            try
            {
                var affectedRows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC DeleteSmartLink @EntityId={entityId}
        ");
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<SmartLinkDto?> GetByEntityIdAsync(string entityId)
        {
            var result = await _context.SmartLinkDtos
                .FromSqlInterpolated($@"
                EXEC GetSmartLinkByEntityId @EntityId={entityId}
            ")
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<SmartLinkDto?> GetByShortUrlAsync(string shortUrl)
        {
            var result = await _context.SmartLinkDtos
                .FromSqlInterpolated($@"
                EXEC GetSmartLinkByShortUrl @ShortUrl={shortUrl}
            ")
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<SmartLinkDto>> GetAllWithPagingAsync(char firstChar, int pageNumber, int pageSize)
        {
            return await _context.SmartLinkDtos
                .FromSqlInterpolated($@"
                EXEC GetAllSmartLinksWithPaging 
                    @FirstChar={firstChar}, 
                    @PageNumber={pageNumber}, 
                    @PageSize={pageSize}
            ")
                .ToListAsync();
        }

        public async Task<IEnumerable<SmartLinkDto>> GetAllAsync(string firstChar)
        {
            var result = await _context.SmartLinkDtos
    .FromSqlInterpolated($@"
                EXEC GetAllSmartLinks @FirstChar={firstChar}
            ")
    .ToListAsync();

            return result;
        }

        public async Task<long> GetNextIdAsync()
        {
            var result = await _context.Set<NextIdDto>().FromSqlRaw("EXEC GetNextSmartLinkId").ToListAsync();
            return result.FirstOrDefault()?.NextId ?? 0;
        }

        public async Task<IEnumerable<SmartLinkDto>> GetAllAsync(Guid userId)
        {
            var result = await _context.SmartLinkDtos
     .FromSqlInterpolated($@"
                EXEC GetAllSmartLinksByUser @CreatedBy={userId.ToString()}
            ")
     .ToListAsync();

            return result;
        }
    }
}
