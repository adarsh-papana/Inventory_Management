using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalBookstoreManagement.Models
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NotificationID { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
