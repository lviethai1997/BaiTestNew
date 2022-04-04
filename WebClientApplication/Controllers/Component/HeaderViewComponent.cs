using Data.Entites;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.ProductCategories;
using Services.Catalog.Users;
using System.Threading.Tasks;

namespace WebClientApplication.Controllers.Component
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IUserService _userService;

        public HeaderViewComponent(IProductCategoryService productCategoryService, IUserService userService)
        {
            _productCategoryService = productCategoryService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cate = await _productCategoryService.GetAllForClient();
            return View(cate);
        }
    }
}
