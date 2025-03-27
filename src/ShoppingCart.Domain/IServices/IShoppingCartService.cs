using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.IServices
{
    public interface IShoppingCartService
    {
        Task<List<CartItem>> GetCart(int userId);
        Task<CartItem> AddToCart(int userId, int productId, int quantity);
        Task<bool> RemoveFromCart(int userId, int productId);
        Task<bool> Checkout(int userId);
    }
}
