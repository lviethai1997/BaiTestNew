using Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Catalog.Cart;
using ViewModels.Catalog.Checkout;
using ViewModels.Catalog.ProductCategories;
using ViewModels.Catalog.Products;
using ViewModels.Common;

namespace Services.Catalog.Products
{
    public interface IProductService
    {
        Task<PageActionResult> CreateProduct(ProductRequest request);

        Task<PageActionResult> UpdateProduct(int id, ProductRequest request);

        Task<PageActionResult> DeleteProduct(int id);

        Task<PageActionResult> HideProduct(int id);

        Task<List<ProductInCategoryRequest>> getProductByCateId(int id);

        Task<ProductRequest> GetById(int id);
        Task<List<ProductListRequest>> GetAll();
        Task<List<ProductListRequest>> GetAllForClient();

        Task<Product> FindById(int id);

        Task<CheckOutRequest> getCheckOut(string userName, List<CartItemRequest> request);
       
    }
}
