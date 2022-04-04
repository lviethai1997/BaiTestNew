using Microsoft.AspNetCore.Mvc;
using Services.Catalog.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Catalog.Products;

namespace WebClientApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllForClient();
            IEnumerable<ProductListRequest> productsList = products;
            return View(productsList);
        }
    }
}