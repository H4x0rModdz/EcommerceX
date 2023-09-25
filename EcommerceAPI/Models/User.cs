using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public bool IsSeller { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}