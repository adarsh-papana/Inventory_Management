﻿using DigitalBookstoreManagement.Data;
using DigitalBookstoreManagement.Models;
using DigitalBookstoreManagement.Repository;
using DigitalBookstoreManagement.Service;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookstoreManagement.Repository
{
    public class InventoryRepository : I_InventoryRepository
    {
        private readonly InventoryDbContext _context;
        private readonly I_NotificationRepository _notificationRepository;
        //private readonly EmailService _emailService;


        public InventoryRepository(InventoryDbContext context, I_NotificationRepository notificationRepository)
        {
            _context = context;
            _notificationRepository = notificationRepository;
            //_emailService = emailService;
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            return await _context.Inventories.FirstOrDefaultAsync(i => i.InventoryID == id);
        }

        public async Task<Inventory> GetInventoryByBookIdAsync(int bookId)
        {
            return await _context.Inventories.FirstOrDefaultAsync(i => i.BookID == bookId);
        }

        public async Task<Inventory> AddInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task DeleteInventoryAsync(int inventoryId)
        {
            var inventory = await _context.Inventories.FindAsync(inventoryId);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsStockAvailableAsync(int bookId, int quantity)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.BookID == bookId);
            return inventory != null && inventory.Quantity >= quantity;
        }

        public async Task UpdateStockOnOrderAsync(int bookId, int orderedQuantity)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.BookID == bookId);
            if (inventory != null)
            {
                inventory.Quantity -= orderedQuantity;
                await _context.SaveChangesAsync();

                // Check stock and notify admin if needed
                await CheckStockAndNotifyAdminAsync(bookId);
            }
        }

        public async Task CheckStockAndNotifyAdminAsync(int bookId)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.BookID == bookId);
            if (inventory != null)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.BookID == bookId);
                if (book != null)
                {
                    if (inventory.Quantity == 0)
                        book.StockQuantity = "Not Available";
                    else if (inventory.Quantity <= 5)
                        book.StockQuantity = "Only few books are left";
                    else
                        book.StockQuantity = "Available";

                    _context.Books.Update(book);
                    await _context.SaveChangesAsync();

                    // 🔔 Send Email Notification if stock is below NotifyLimit
                    if (inventory.Quantity < inventory.NotifyLimit)
                    {
                        string message = $"The book containing in Inventory {inventory.InventoryID} with BookID {book.BookID} of '{book.Title}' is less than the notify limit. Kindly re-stock the book.";
                        await _notificationRepository.AddNotification(message);
                        Console.WriteLine($"ALERT: {message}");
                        // TODO: Implement Email Notification
                        //Console.WriteLine($"ALERT: Inventory {inventory.InventoryID}, BookID {book.BookID}, Book {book.Title} is running low on stock! Current Quantity {inventory.Quantity}");
                    }
                }
            }
        }
    }
}
