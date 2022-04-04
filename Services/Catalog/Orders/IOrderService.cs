using Data.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Catalog.Checkout;
using ViewModels.Catalog.OrderDetails;
using ViewModels.Catalog.Orders;
using ViewModels.Common;

namespace Services.Catalog.Orders
{
    public interface IOrderService
    {
        public Task<List<Order>> getOrders();

        public Task<PageActionResult> DestroyOrder(int id);

        public Task<PageActionResult> TakeOrder(int id);

        public Task<PageActionResult> CompleteOrder(int id);

        public Task<PageActionResult> ComfirmOrder(CheckOutRequest request);

        public Task<List<OrderRequest>> GetOrderByUser(string userName);

        public Task<OrderDetailRequest> GetOrderDetail(int orderId);

        public Task<List<OrderRequest>> GetAll();
    }
}