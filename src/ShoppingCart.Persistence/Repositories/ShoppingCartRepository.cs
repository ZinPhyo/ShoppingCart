using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingCart.Domain.IServices;
using ShoppingCart.Domain.Models;
using ShoppingCart.Persistence.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Persistence.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShoppingCartDbContext _context;
        private readonly ILogger<ShoppingCartRepository> _logger;

        public ShoppingCartRepository(ShoppingCartDbContext context, ILogger<ShoppingCartRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CartItem>> GetCart(int userId)
        {
            _logger.LogInformation("Fetching cart from database for user {UserId}", userId);
            return await _context.CartItems.Where(ci => ci.UserId == userId)
                .Include(ci => ci.Product).ToListAsync();
        }

        public async Task<CartItem> AddToCart(CartItem cartItem)
        {
            _logger.LogInformation("Adding item to cart in database: {CartItem}", cartItem);
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<bool> RemoveFromCart(int userId, int productId)
        {
            _logger.LogInformation("Removing product {ProductId} from cart of user {UserId} in database", productId, userId);
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
            if (cartItem == null) return false;
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Checkout(int userId)
        {
            _logger.LogInformation("Checking out cart for user {UserId} in database", userId);
            var cartItems = await _context.CartItems.Where(ci => ci.UserId == userId).ToListAsync();
            if (!cartItems.Any())
            {
                _logger.LogWarning("Checkout failed for user {UserId} as the cart was empty", userId);
                return false;
            }
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> ProductExists(int productId)
        {
            return await _context.Products.AnyAsync(p => p.Id == productId);
        }
    }
}
