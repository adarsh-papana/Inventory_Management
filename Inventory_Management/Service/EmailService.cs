using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace DigitalBookstoreManagement.Service
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // SMTP server (Gmail, Outlook, etc.)
        private readonly int _smtpPort = 587; // Port for TLS
        private readonly string _senderEmail = "your-email@gmail.com"; // Your email
        private readonly string _senderPassword = "your-email-password"; // Your email password
        private readonly string _adminEmail = "admin-email@example.com"; // Admin email to receive alerts

        public async Task SendLowStockNotification(int inventoryId, int bookId, string bookTitle, int quantity)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Digital Bookstore", _senderEmail));
                message.To.Add(new MailboxAddress("Admin", _adminEmail));
                message.Subject = "Low Stock Alert";

                message.Body = new TextPart("plain")
                {
                    Text = $"ALERT: Book '{bookTitle}' (BookID: {bookId}, InventoryID: {inventoryId}) has low stock! " +
                           $"Only {quantity} books are left."
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_senderEmail, _senderPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                Console.WriteLine("✅ Low stock email sent to Admin.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error sending email: {ex.Message}");
            }
        }
    }
}
