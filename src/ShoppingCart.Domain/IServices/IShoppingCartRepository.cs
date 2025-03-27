using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.IServices
{
    public interface IShoppingCartRepository
    {
        Task<List<CartItem>> GetCart(int userId);
        Task<CartItem> AddToCart(CartItem cartItem);
        Task<bool> RemoveFromCart(int userId, int productId);
        Task<bool> Checkout(int userId);
        Task<bool> UserExists(int userId);
        Task<bool> ProductExists(int productId);
    }
}
