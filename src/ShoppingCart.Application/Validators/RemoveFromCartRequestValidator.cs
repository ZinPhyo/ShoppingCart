using FluentValidation;
using ShoppingCart.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Validators
{
    public class RemoveFromCartRequestValidator : AbstractValidator<RemoveFromCartRequest>
    {
        public RemoveFromCartRequestValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}
