using MediatR;
using System.Threading.Tasks;


namespace MintLynk.Domain.Interfaces
{
    public interface INotificationCenterRepository
    {

        Task<NotificationCenter?> GetAsync(Guid id);

        Task<NotificationCenter> CreateAsync(NotificationCenter notification);

        Task<NotificationCenter> UpdateAsync(NotificationCenter notification);

        Task<bool> DeleteAsync(Guid id);

        Task<IEnumerable<NotificationCenter>> GetAllAsync();

        Task<IEnumerable<NotificationCenter>> GetAllAsync(string receiverId);

        List<NotificationCenter> GetImmediateMails(int check);

        List<NotificationCenter> GetScheduledMails();
    }
}