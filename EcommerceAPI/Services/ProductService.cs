using EcommerceAPI.Models;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task AddProductAsync(Product product)
        {
            var productToAdd = await _productRepository.GetProductByIdAsync(product.Id);

            productToAdd.Name = product.Name;
            productToAdd.Description = product.Description;
            productToAdd.Price = product.Price;
            productToAdd.CreatedDate = DateTime.Now;
            productToAdd.IsAvailable = product.IsAvailable;
            productToAdd.Quantity = product.Quantity;
            productToAdd.User = product.User;
            productToAdd.UserEmail = product.UserEmail;

            await _productRepository.AddProductAsync(productToAdd);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await FindProductByIdAsync(product.Id);

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.IsAvailable = product.IsAvailable;
            existingProduct.Quantity = product.Quantity;
            existingProduct.User = product.User;
            existingProduct.UserEmail = product.UserEmail;

            await _productRepository.UpdateProductAsync(existingProduct);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var existingProduct = await FindProductByIdAsync(productId);

            await _productRepository.DeleteProductAsync(existingProduct.Id);
        }

        private async Task<Product> FindProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            return product;
        }
    }
}