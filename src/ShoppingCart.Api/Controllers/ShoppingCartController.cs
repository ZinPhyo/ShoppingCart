using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Domain.Dto;
using ShoppingCart.Domain.IServices;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;
    private readonly ILogger<ShoppingCartController> _logger;
    public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<ShoppingCartController> logger)
    {
        _shoppingCartService = shoppingCartService; 
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart([FromRoute] int userId)
    {
        if (userId <= 0)
        {
            _logger.LogWarning("Invalid user ID provided: {UserId}", userId);
            return BadRequest("Invalid user ID.");
        }

        _logger.LogInformation("Fetching cart for user {UserId}", userId);
        var cart = await _shoppingCartService.GetCart(userId);

        if (cart == null)
        {
            return NotFound("User does not exist.");
        }

        return Ok(cart);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for AddToCartRequest: {Request}", request);
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Adding product {ProductId} to user {UserId}'s cart", request.ProductId, request.UserId);
        var cartItem = await _shoppingCartService.AddToCart(request.UserId, request.ProductId, request.Quantity);

        if (cartItem == null)
        {
            return NotFound("User or Product does not exist.");
        }

        return Ok(cartItem);
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for RemoveFromCartRequest: {Request}", request);
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Removing product {ProductId} from user {UserId}'s cart", request.ProductId, request.UserId);
        var success = await _shoppingCartService.RemoveFromCart(request.UserId, request.ProductId);

        if (!success)
        {
            return NotFound("User does not exist or product is not in the cart.");
        }

        return Ok();
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for CheckoutRequest: {Request}", request);
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Processing checkout for user {UserId}", request.UserId);
        var success = await _shoppingCartService.Checkout(request.UserId);

        if (!success)
        {
            return NotFound("User does not exist or cart is empty.");
        }

        return Ok();
    }
}