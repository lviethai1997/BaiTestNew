using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog.Orders;
using System.Threading.Tasks;

namespace AdminApplication.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly INotyfService _notyf;

        public OrderController(IOrderService orderService, INotyfService notyfService)
        {
            _orderService = orderService;
            _notyf = notyfService;
        }

        public async Task<IActionResult> Index()
        {
            var listOrders = await _orderService.GetAll();
            return View(listOrders);
        }

        public async Task<IActionResult> OrderDetail(int id)
        {
            var orderDetail = await _orderService.GetOrderDetail(id);
            return View(orderDetail);
        }

        public async Task<IActionResult> TakeOrder(int id)
        {
            var result = await _orderService.TakeOrder(id);

            if (result.status == true)
            {
                _notyf.Success(result.Message);
            }
            else
            {
                _notyf.Error(result.Message);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CompleteOrder(int id)
        {
            var result = await _orderService.CompleteOrder(id);
            if (result.status == true)
            {
                _notyf.Success(result.Message);
            }
            else
            {
                _notyf.Error(result.Message);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DestroyOrder(int id)
        {
            var result = await _orderService.DestroyOrder(id);
            if (result.status == true)
            {
                _notyf.Success(result.Message);
            }
            else
            {
                _notyf.Error(result.Message);
            }
            return RedirectToAction("Index");
        }
    }
}