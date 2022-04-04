using Data.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Catalog.ProductCategories;
using ViewModels.Common;

namespace Services.Catalog.ProductCategories
{
    public interface IProductCategoryService
    {
        Task<PageActionResult> CreateProductCategory(ProductCategoryRequest request);

        Task<PageActionResult> UpdateProductCategory(int id,ProductCategoryRequest request);

        Task<PageActionResult> DeleteProductCategory(int id);

        Task<PageActionResult> HideProductCategory(int id);

        Task<ProductCategory> GetById(int id);
        Task<List<ProductCategory>> GetAll();

        Task<List<CbCategories>> getCategory();
        Task<List<ProductCategory>> GetAllForClient();
    }
}