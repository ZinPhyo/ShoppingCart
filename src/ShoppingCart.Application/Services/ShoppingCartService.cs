using Microsoft.Extensions.Logging;
using ShoppingCart.Domain.IServices;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _repository;
        private readonly ILogger<ShoppingCartService> _logger;

        public ShoppingCartService(IShoppingCartRepository repository, ILogger<ShoppingCartService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<CartItem>> GetCart(int userId)
        {
            if (!await _repository.UserExists(userId))
            {
                _logger.LogWarning("User {UserId} does not exist", userId);
                return null;
            }

            _logger.LogInformation("Retrieving cart for user {UserId}", userId);
            return await _repository.GetCart(userId);
        }

        public async Task<CartItem?> AddToCart(int userId, int productId, int quantity)
        {
            _logger.LogInformation("Validating user {UserId} and product {ProductId} before adding to cart", userId, productId);

            // Check if user exists
            var userExists = await _repository.UserExists(userId);
            if (!userExists)
            {
                _logger.LogWarning("User {UserId} does not exist", userId);
                return null;  // Return null to indicate failure
            }

            // Check if product exists
            var productExists = await _repository.ProductExists(productId);
            if (!productExists)
            {
                _logger.LogWarning("Product {ProductId} does not exist", productId);
                return null;  // Return null to indicate failure
            }

            _logger.LogInformation("Adding product {ProductId} to user {UserId}'s cart", productId, userId);
            var cartItem = new CartItem { UserId = userId, ProductId = productId, Quantity = quantity };
            return await _repository.AddToCart(cartItem);
        }

        public async Task<bool> RemoveFromCart(int userId, int productId)
        {
            if (!await _repository.UserExists(userId))
            {
                _logger.LogWarning("User {UserId} does not exist", userId);
                return false;
            }

            _logger.LogInformation("Removing product {ProductId} from user {UserId}'s cart", productId, userId);
            return await _repository.RemoveFromCart(userId, productId);
        }

        public async Task<bool> Checkout(int userId)
        {
            if (!await _repository.UserExists(userId))
            {
                _logger.LogWarning("User {UserId} does not exist", userId);
                return false;
            }

            _logger.LogInformation("Processing checkout for user {UserId}", userId);
            return await _repository.Checkout(userId);
        }
    }
}
