using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Name field must not exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "The Description field must not exceed 255 characters.")]
        public string Description { get; set; }

        public string Quantity { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Price field must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Created Date field is required.")]
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public virtual User User { get; set; }
    }
}