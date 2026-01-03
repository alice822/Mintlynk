using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MintLynk.Domain.Entities;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Interfaces;
using MintLynk.Infrastructure.Data;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MintLynk.Infrastructure.Repositories
{
    public class MiniPageRepository : IMiniPageRepository
    {
        private readonly ApplicationDbContext _context;

        public MiniPageRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<MiniPage> CreateAsync(MiniPage page)
        {
            page.Status = (int)MiniPageStatus.Draft;
            _context.MiniPages.Add(page);
            await _context.SaveChangesAsync();
            return page;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = GetAsync(id).Result;
            if (data != null)
            {
                data.Status = (int)MiniPageStatus.Deleted;

                var updatedData = _context.MiniPages.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<IEnumerable<MiniPage>> GetAllAsync() => await _context.MiniPages.Where(x => x.Status != (int)MiniPageStatus.Deleted).ToListAsync();


        public async Task<IEnumerable<MiniPage>> GetAllAsync(string userId) => await _context.MiniPages
            .Where(x => x.CreatedBy == userId && x.Status != (int)MiniPageStatus.Deleted).ToListAsync();

        public async Task<MiniPage?> GetAsync(Guid id) => await _context.MiniPages.FindAsync(id);

        public async Task<MiniPage?> GetAsync(string alias)
        {
            var data = await _context.MiniPages
                .FirstOrDefaultAsync(x => x.Alias == alias && x.Status != (int)MiniPageStatus.Deleted);
            return data;
        }

        public async Task<MiniPage> UpdateAsync(MiniPage page)
        {
            var tracked = _context.MiniPages.Local.FirstOrDefault(e => e.Id == page.Id);
            if (tracked == null)
            {
                var data =_context.MiniPages.Update(page);
                await _context.SaveChangesAsync();
                return data.Entity;
            }
            else
            {
                _context.Entry(tracked).CurrentValues.SetValues(page);
                await _context.SaveChangesAsync();
                return tracked;
            }
        }
    }
}