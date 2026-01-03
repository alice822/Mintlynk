using MintLynk.Application.Common.Models;
using MintLynk.Application.Dtos;
using MintLynk.Domain.Entities;
using System.Threading.Tasks;


namespace MintLynk.Application.Interfaces
{
    public interface ICommunicationService
    {
        Task<NotificationCenterDto?> GetAsync(Guid id);

        Task<NotificationCenterDto> CreateAsync(NotificationCenterDto notification);

        Task<NotificationCenterDto> UpdateAsync(NotificationCenterDto notification);

        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<NotificationCenterDto>> GetAllAsync();

        Task<IEnumerable<NotificationCenterDto>> GetAllAsync(string receiverId);

        List<NotificationCenterDto> GetImmediateMails(int check);

        List<NotificationCenterDto> GetScheduledMails();

        Task<bool> SendMail(MailModel mail);
    }
}