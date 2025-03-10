using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalBookstoreManagement.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "AuthorID is required")]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "CategoryID is required")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be between 0.01 and 99,999.99.")]
        [Column(TypeName = "decimal(18,2)")]
        //[Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000")]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "StockQuantity is required")]
        //[Range(0, int.MaxValue, ErrorMessage = "StockQuantity cannot be negative")]
        //public int StockQuantity { get; set; }

        [Required]
        public string StockQuantity { get; set; } = "Available";

        [JsonIgnore] // Avoid circular reference issue
        public virtual Inventory Inventory { get; set; }
    }
}
