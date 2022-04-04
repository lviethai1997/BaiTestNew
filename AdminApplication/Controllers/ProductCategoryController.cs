using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.ProductCategories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Catalog.ProductCategories;

namespace AdminApplication.Controllers
{
    public class ProductCategoryController : BaseController
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly INotyfService _notyf;

        public ProductCategoryController(IProductCategoryService productCategoryService, INotyfService notyf)
        {
            _productCategoryService = productCategoryService;
            _notyf = notyf;
        }

        // GET: ProductCategoryController
        public async Task<ActionResult> Index()
        {
            var productCategory = await _productCategoryService.GetAll();
            return View(productCategory);
        }

        // GET: ProductCategoryController/Create
        public async Task<ActionResult> Create()
        {
            var cbParent = await _productCategoryService.GetAll();
            List<object> cate = new List<object>();
            cate.Add(new { ID = 0, Name = "Danh mục cha" });
            foreach (var item in cbParent)
            {
                cate.Add(new { ID = item.ID, Name = item.Name });
            }
            ViewBag.cate = cate;

            return View();
        }

        // POST: ProductCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCategoryRequest collection)
        {
            var cbParent = await _productCategoryService.GetAll();
            List<object> cate = new List<object>();
            cate.Add(new { ID = 0, Name = "Danh mục cha" });
            foreach (var item in cbParent)
            {
                cate.Add(new { ID = item.ID, Name = item.Name });
            }
            ViewBag.cate = cate;

            if (!ModelState.IsValid)
            {
                return View(collection);
            }
            var result = await _productCategoryService.CreateProductCategory(collection);

            if (result.status == true)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Error(result.Message);
                return View(collection);
            }
        }

        // GET: ProductCategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var productCategory = await _productCategoryService.GetById(id);

            var Request = new ProductCategoryRequest{
            ParentID = productCategory.ParentID,
            Name = productCategory.Name,
            Status = productCategory.Status,
            };

            var cbParent = await _productCategoryService.GetAll();
            List<object> cate = new List<object>();
            cate.Add(new { ID = 0, Name = "Danh mục cha" });
            foreach (var item in cbParent)
            {
                cate.Add(new { ID = item.ID, Name = item.Name });
            }
            ViewBag.cate = cate;

            return View(Request);
        }

        // POST: ProductCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductCategoryRequest collection)
        {
            var cbParent = await _productCategoryService.GetAll();
            List<object> cate = new List<object>();
            cate.Add(new { ID = 0, Name = "Danh mục cha" });
            foreach (var item in cbParent)
            {
                cate.Add(new { ID = item.ID, Name = item.Name });
            }
            ViewBag.cate = cate;
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            var result = await _productCategoryService.UpdateProductCategory(id, collection);

            if (result.status == true)
            {
                _notyf.Success(result.Message);
                return RedirectToAction("Index");
            }
            else
            {
                _notyf.Error(result.Message);
                return View(collection);
            }
        }

        // GET: ProductCategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _productCategoryService.GetById(id);
            return View(result);
        }

        // POST: ProductCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var result = await _productCategoryService.DeleteProductCategory(id);

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
            var result = await _productCategoryService.HideProductCategory(id);

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