using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.Validators;
using ShoppingCart.Domain.IServices;
using ShoppingCart.Persistence.Repositories;

namespace ShoppingCart.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddValidatorsFromAssemblyContaining<AddToCartRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CheckoutRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<RemoveFromCartRequestValidator>();

            return services;
        }
    }
}
