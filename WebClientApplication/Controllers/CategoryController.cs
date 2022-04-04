using Microsoft.AspNetCore.Mvc;
using Services.Catalog.ProductCategories;
using Services.Catalog.Products;
using System.Threading.Tasks;

namespace WebClientApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        public CategoryController(IProductCategoryService productCategoryService,IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;  
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProductInCategory(int id)
        {
            var products = await _productService.getProductByCateId(id);

            var cateInfor = await _productCategoryService.GetById(id);

            ViewBag.Category = cateInfor;

            return View(products);
        }
    }
}
