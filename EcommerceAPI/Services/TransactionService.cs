using EcommerceAPI.Models;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int transactionId)
        {
            return await _transactionRepository.GetTransactionByIdAsync(transactionId);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            var transactionToAdd = new Transaction
            {
                Quantity = transaction.Quantity,
                UserEmail = transaction.UserEmail, // relationship user
                Price = transaction.Price,
                ProductName = transaction.ProductName, // relationship product
                Status = transaction.Status,
                TransactionDate = DateTime.Now,
            };

            transactionToAdd.User = await _userRepository.GetByEmailAsync(transaction.UserEmail);

            transactionToAdd.Product = await _productRepository.GetProductByNameAsync(transaction.ProductName);

            await _transactionRepository.AddTransactionAsync(transactionToAdd);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            var existingTransaction = await FindTransactionByIdAsync(transaction.Id);

            existingTransaction.Quantity = transaction.Quantity;
            existingTransaction.Price = transaction.Price;
            existingTransaction.Status = transaction.Status;

            await _transactionRepository.UpdateTransactionAsync(existingTransaction);
        }

        public async Task DeleteTransactionAsync(int transactionId)
        {
            var existingTransaction = await FindTransactionByIdAsync(transactionId);

            await _transactionRepository.DeleteTransactionAsync(existingTransaction.Id);
        }

        private async Task<Transaction> FindTransactionByIdAsync(int transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);

            return transaction;
        }
    }
}