using Microsoft.EntityFrameworkCore;
using MintLynk.Domain.Entities;
using MintLynk.Domain.Interfaces;
using MintLynk.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Infrastructure.Repositories
{
    public class LinkHistoryRepository : ILinkHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public LinkHistoryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }                                    

        public async Task<LinkHistory> CreateAsync(LinkHistory history)
        {
            _context.LinksHistory.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task DeleteAsync(Guid id)
        {
            var data = await _context.LinksHistory.FindAsync(id);
            if(data!= null)
            {
                _context.LinksHistory.Remove(data);
                await _context.SaveChangesAsync();
                return;
            }
            return;
        }

        public async Task<IEnumerable<LinkHistory>> GetAllAsync() => await _context.LinksHistory.ToListAsync();

        public async Task<LinkHistory?> GetAsync(Guid id) => await _context.LinksHistory.FindAsync(id);

        public async Task<IEnumerable<LinkHistory>> GetAllAsync(string linkId) => await _context.LinksHistory
            .Where(x => x.LinkId == linkId)
            .ToListAsync();

        public async Task<IEnumerable<LinkHistory>> GetAllAsync(List<string> linkIds) => await _context.LinksHistory
    .Where(x => linkIds.Contains(x.LinkId))
    .ToListAsync();

        public async Task<LinkHistory> UpdateAsync(LinkHistory history)
        {
            var data = _context.LinksHistory.Update(history);
            await _context.SaveChangesAsync();
            return data.Entity;
        }
    }
}
