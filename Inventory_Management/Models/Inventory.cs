using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalBookstoreManagement.Models
{
    public class Inventory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "BookID is required")]
        //[ForeignKey("Book")]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "NotifyLimit is required")]
        [Range(1, int.MaxValue, ErrorMessage = "NotifyLimit must be at least 1")]
        public int NotifyLimit { get; set; }

        //[JsonIgnore]
        //public virtual BookManagement BookManagement { get; set; }

        [ForeignKey("BookID")]
        public BookManagement BookManagement { get; set; } // Navigation property
    }
}
