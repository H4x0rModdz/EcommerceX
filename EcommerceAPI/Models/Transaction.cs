using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }

        public DateTime TransactionDate { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }


        public string UserEmail { get; set; }
        public string ProductName { get; set; }

        public enum TransactionStatus
        {
            Pending = 1,
            Completed = 2,
            Canceled = 3,
            Refunded = 4
        }

        public TransactionStatus Status { get; set; } // Example: TransactionStatus status = TransactionStatus.Pending;
    }
}