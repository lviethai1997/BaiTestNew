using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.ProductCategories;
using Services.Catalog.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Catalog.ProductCategories;
using ViewModels.Catalog.Products;

namespace AdminApplication.Controllers
{

    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly INotyfService _notyf;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, INotyfService notyfService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _notyf = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Category = _productCategoryService.getCategory();
            List<CbCategories> cate = await Category;

            ViewBag.category = cate;

            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var Category = _productCategoryService.getCategory();
            List<CbCategories> cate = await Category;
            ViewBag.category = cate;

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _productService.CreateProduct(request);

            if (result.status == true)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Error(result.Message);
                return View(request);
            }

          
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var getById = await _productService.GetById(id);
            var Category = _productCategoryService.getCategory();

            List<CbCategories> cate = await Category;
            ViewBag.category = cate;
            return View(getById);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductRequest request)
        {
            var Category = _productCategoryService.getCategory();
            List<CbCategories> cate = await Category;
            ViewBag.category = cate;

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _productService.UpdateProduct(id, request);
            if (result.status == true)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Error(result.Message);
                return View(request);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var getById = await _productService.GetById(id);
            return View(getById);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, IFormCollection collections)
        {
            var result = await _productService.DeleteProduct(id);
            if (result.status == true)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Error(result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Hide(int id)
        {
            var result = await _productService.HideProduct(id);

            if (result.status == true)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Error(result.Message);
                return RedirectToAction("Index");
            }
        }
    }
}