using EcommerceAPI.Models;

namespace EcommerceAPI.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

        Task<Transaction> GetTransactionByIdAsync(int transactionId);

        Task AddTransactionAsync(Transaction transaction);

        Task UpdateTransactionAsync(Transaction transaction);

        Task DeleteTransactionAsync(int transactionId);
    }
}