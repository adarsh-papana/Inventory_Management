using DigitalBookstoreManagement.Models;

namespace DigitalBookstoreManagement.Repository
{
    public interface I_NotificationRepository
    {
        public Task AddNotification(string message);
        public Task<IEnumerable<Notification>> GetAllNotifications();
    }
}
