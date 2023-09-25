namespace EcommerceAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Price { get; set; }

        public int StatusId { get; set; }

        public string UserEmail { get; set; }
        public string ProductName { get; set; }

        public TransactionStatus Status { get; set; } // Example: TransactionStatus status = TransactionStatus.Pending;
    }
}