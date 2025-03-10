using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventory_Management.Models
{
    public class Inventory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "BookID is required")]
        [ForeignKey("Book")]
        public int BookID { get; set; }

        //[ForeignKey("BookID")]
        //public Book Book { get; set; } // Navigation property

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "NotifyLimit is required")]
        [Range(1, int.MaxValue, ErrorMessage = "NotifyLimit must be at least 1")]
        public int NotifyLimit { get; set; }

        [JsonIgnore]
        public virtual Book Book { get; set; }
    }
}
