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

namespace MintLynk.Infrastructure.Repositories
{
    public class NotificationCenterRepository : INotificationCenterRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationCenterRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<NotificationCenter> CreateAsync(NotificationCenter notification)
        {
            notification.Status = (int)NotificationStatusEnum.Active;
            _context.NotificationCenters.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = GetAsync(id).Result;
            if (data != null)
            {
                data.Status = (int)NotificationStatusEnum.Deleted;

                var updatedData = _context.NotificationCenters.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<IEnumerable<NotificationCenter>> GetAllAsync() => await _context.NotificationCenters.ToListAsync();


        public async Task<IEnumerable<NotificationCenter>> GetAllAsync(string receiverId) => await _context.NotificationCenters
            .Where(x => x.ReceiverId == Guid.Parse(receiverId)).ToListAsync();

        public async Task<NotificationCenter?> GetAsync(Guid id) => await _context.NotificationCenters.FindAsync(id);

        public List<NotificationCenter> GetImmediateMails(int check)
        {
            var result = _context.NotificationCenters.Where(x =>
               x.ChannelType == (int)ChannelTypeEnum.Email && x.Status == (int)NotificationStatusEnum.Active && x.SendImmediately == true).ToList();
            return result;
        }

        public List<NotificationCenter> GetScheduledMails()
        {
            var result = _context.NotificationCenters.Where(x =>
                x.ChannelType == (int)ChannelTypeEnum.Email && x.Status == (int)NotificationStatusEnum.Active && DateTime.Now >= x.ReminderDuration).ToList();
            return result;
        }

        public async Task<NotificationCenter> UpdateAsync(NotificationCenter notification)
        {
            var data = _context.NotificationCenters.Update(notification);
            await _context.SaveChangesAsync();
            return data.Entity;
        }
    }
}