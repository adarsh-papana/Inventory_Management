﻿using DigitalBookstoreManagement.Models;

namespace DigitalBookstoreManagement.Repository
{
    public interface I_NotificationRepository
    {
        public Task AddNotificationAsync(string message);
        public Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        public Task AddorUpdateNotificationAsync(int bookId, string title, int inventoryId, int notifylimit);
    }
}
