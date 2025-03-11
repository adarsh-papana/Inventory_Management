using DigitalBookstoreManagement.Data;
using DigitalBookstoreManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookstoreManagement.Repository
{
    public class NotificationRepository : I_NotificationRepository
    {
        private readonly InventoryDbContext _context;
        public NotificationRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddNotification(string message)
        {
            var notification = new Notification { Message = message };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }
    }
}
