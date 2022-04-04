using Data.Entites;
using ViewModels.Catalog.Products;

namespace ViewModels.Catalog.Cart
{
    public class CartItemRequest
    {
        public int quantity { set; get; }
        public Product product { set; get; }
    }
}