using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Dto
{
    public class CheckoutRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
