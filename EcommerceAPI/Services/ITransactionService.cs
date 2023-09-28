using EcommerceAPI.Models;

namespace EcommerceAPI.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

        Task<Transaction> GetTransactionByIdAsync(int transactionId);

        Task AddTransactionAsync(Transaction transaction);

        Task UpdateTransactionAsync(Transaction transaction);

        Task DeleteTransactionAsync(int transactionId);
    }
}