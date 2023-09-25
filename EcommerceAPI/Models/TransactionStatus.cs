namespace EcommerceAPI.Models
{
    public class TransactionStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static TransactionStatus Pending { get; } = new TransactionStatus(1, "Pending");
        public static TransactionStatus Completed { get; } = new TransactionStatus(2, "Completed");
        public static TransactionStatus Canceled { get; } = new TransactionStatus(3, "Canceled");
        public static TransactionStatus Refunded { get; } = new TransactionStatus(4, "Refunded");

        public TransactionStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}