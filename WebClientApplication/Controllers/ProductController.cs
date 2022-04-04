using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Catalog.Orders;
using Services.Catalog.Products;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Catalog.Cart;

namespace WebClientApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public const string CARTKEY = "cart";
        private readonly INotyfService _notyf;

        public ProductController(IProductService productService, IOrderService orderService, INotyfService notyfService)
        {
            _productService = productService;
            _orderService = orderService;
            _notyf = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ComfirmOrder()
        {
            string Username = HttpContext.Session.GetString("SessionUser");
            var items = await _productService.getCheckOut(Username, GetCartItems());

            var comfirm = await _orderService.ComfirmOrder(items);

            if (comfirm.status == true)
            {
                _notyf.Success(comfirm.Message);
                return RedirectToAction("Confirmation", "Product");
            }
            else
            {
                _notyf.Error(comfirm.Message);
                return RedirectToAction("Cart", "Product");
            }
        }

        [Route("/Confirmation", Name = "Confirmation")]
        public async Task<IActionResult> Confirmation()
        {
            ClearCart();
            return View();
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _productService.FindById(id);

            return View(product);
        }

        // Hiện thị giỏ hàng

        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        [Route("/checkout", Name = "checkout")]
        public async Task<IActionResult> CheckOut()
        {
            string Username = HttpContext.Session.GetString("SessionUser");
            if (string.IsNullOrEmpty(Username))
            {
                _notyf.Error("Vui lòng đăng nhập để thanh toán");
                return RedirectToAction("Index", "Login");
            }

            var items = await _productService.getCheckOut(Username, GetCartItems());

            return View(items);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _productService.FindById(id);
            if (product.Quantity == 0)
            {
                _notyf.Error("Sản phẩm này không còn tồn kho");
                return RedirectToAction("Index", "Home");
            }

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ID == id);
            if (cartitem != null)
            {
                cartitem.quantity++;
            }
            else
            {
                cart.Add(new CartItemRequest() { quantity = 1, product = product });
            }
            SaveCartSession(cart);
            _notyf.Success("Thêm vào giỏ hành thành công");

            return RedirectToAction(nameof(Cart));
        }

        private List<CartItemRequest> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItemRequest>>(jsoncart);
            }
            return new List<CartItemRequest>();
        }

        private void SaveCartSession(List<CartItemRequest> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        private void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            _notyf.Success("Cập nhật giỏ hành thành công");
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ID == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            _notyf.Success("Xóa sản phẩm giỏ hành thành công");
            return RedirectToAction(nameof(Cart));
        }
    }
}