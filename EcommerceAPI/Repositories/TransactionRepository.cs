using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int transactionId)
        {
            return await FindTransactionAsync(transactionId);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            var existingTransaction = await FindTransactionAsync(transaction.Id);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int transactionId)
        {
            var transactionToRemove = await FindTransactionAsync(transactionId);

            _context.Transactions.Remove(transactionToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> FindTransactionAsync(int transactionId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
        }
    }
}