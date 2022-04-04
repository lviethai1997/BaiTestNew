using Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Catalog.Cart;

namespace ViewModels.Catalog.Checkout
{
    public class CheckOutRequest
    {
        public User user { get; set; }
        public List<CartItemRequest> carts { get; set; }
    }
}
