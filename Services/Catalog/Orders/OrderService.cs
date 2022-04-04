using Data.EF;
using Data.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Catalog.Checkout;
using ViewModels.Catalog.OrderDetails;
using ViewModels.Catalog.Orders;
using ViewModels.Common;

namespace Services.Catalog.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ShopDbContext _context;

        public OrderService(ShopDbContext context)
        {
            _context = context;
        }

        // -1 da huy don,0 dang cho xu ly, 1 dang van chuyen, 2 hoan thanh
        public async Task<PageActionResult> ComfirmOrder(CheckOutRequest request)
        {
            var order = new Order()
            {
                CustomerAddress = request.user.Address,
                CustomerEmail = request.user.Email,
                CustomerMobile = request.user.Mobile,
                CustommerId = request.user.ID,
                CustomerName = request.user.FullName,
                CreateDate = DateTime.Now,
                Status = 0,
                CreatedBy = request.user.Username,
                CustomerMessage = "OK"
            };
            var addOrder = _context.Orders.Add(order);
            var result = _context.SaveChanges();

            var orderDetails = new List<OrderDetail>();
            foreach (var item in request.carts)
            {
                var detail = new OrderDetail();
                detail.OrderID = order.ID;
                detail.ProductID = item.product.ID;
                detail.Quantity = item.quantity;
                detail.Price = item.product.Price;
                orderDetails.Add(detail);
                var updateProduct = _context.Products.Where(x => x.ID == item.product.ID).FirstOrDefault();
                updateProduct.Quantity = updateProduct.Quantity - item.quantity;
                var addOrderDetail = _context.OrderDetails.Add(detail);
            }

            result = _context.SaveChanges();

            if (result > 0)
            {
                return new PageActionResult { status = true, Message = "Đặt hàng thành công!" };
            }
            else
            {
                return new PageActionResult { status = false, Message = "Đặt hàng Thất bại!" };
            }
        }

        public async Task<PageActionResult> CompleteOrder(int id)
        {
            var order = await _context.Orders.Where(o => o.ID == id).FirstOrDefaultAsync();

            if (order.Status == -1)
            {
                return new PageActionResult { status = false, Message = "Đơn hàng này đã hủy nên không thể Hoàn thành" };
            }

            if (order.Status == 0)
            {
                return new PageActionResult { status = false, Message = "Đơn hàng chưa được nhận nên không thể Hoàn thành" };
            }

            if (order.Status == 2)
            {
                return new PageActionResult { status = false, Message = "Đơn hàng đã Hoàn thành trước đó" };
            }

            order.Status = 2;
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new PageActionResult { status = true, Message = "Đơn hàng đã được hoàn thành" };
            }
            else
            {
                return new PageActionResult { status = false, Message = "Đơn hàng chưa được hoàn thành" };
            }

        }

        public async Task<PageActionResult> DestroyOrder(int id)
        {
            var order = await _context.Orders.Where(o => o.ID == id).FirstOrDefaultAsync();

            if (order.Status == 2)
            {
                return new PageActionResult { status = false, Message = "Đơn hàng này đã hoàn thành nên không thể HỦY" };
            }

            order.Status = -1;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return new PageActionResult { status = true, Message = "Đơn hàng đã được HỦY" };
            }
            else
            {
                return new PageActionResult { status = false, Message = "Đơn hàng chưa được HỦY" };
            }
        }

        public async Task<List<OrderRequest>> GetAll()
        {
            var orders = await _context.Orders.Select(o => new OrderRequest()
            {
                ID = o.ID,
                CustomerAddress = o.CustomerAddress,
                CustomerName = o.CustomerName,
                CustomerMobile = o.CustomerMobile,
                CustommerMessage = o.CustomerMessage,
                CustommerId = o.CustommerId,
                Status = o.Status,
                CreateDate = o.CreateDate,
                TotalPrice = _context.OrderDetails.Where(x => x.OrderID == o.ID).Sum(x => x.Price),
                TotalQuantity = _context.OrderDetails.Where(x => x.OrderID == o.ID).Sum(x => x.Quantity),
            }).ToListAsync();

            return orders;
        }

        public async Task<List<OrderRequest>> GetOrderByUser(string userName)
        {
            var getUser = await _context.User.Where(x => x.Username == userName).FirstOrDefaultAsync();

            var orders = await _context.Orders.Where(x => x.CustommerId == getUser.ID).Select(o => new OrderRequest()
            {
                ID = o.ID,
                CustomerAddress = o.CustomerAddress,
                CustomerName = o.CustomerName,
                CustomerMobile = o.CustomerMobile,
                CustommerMessage = o.CustomerMessage,
                CustommerId = o.CustommerId,
                Status = o.Status,
                CreateDate = o.CreateDate,
                TotalPrice = _context.OrderDetails.Where(x => x.OrderID == o.ID).Sum(x => x.Price),
                TotalQuantity = _context.OrderDetails.Where(x => x.OrderID == o.ID).Sum(x => x.Quantity),
            }).ToListAsync();

            return orders;
        }

        public async Task<OrderDetailRequest> GetOrderDetail(int orderId)
        {
            var orderDetails = await _context.Orders.Where(x => x.ID == orderId).Select(x => new OrderDetailRequest()
            {
                OrderId = x.ID,
                OrderStatus = x.Status,
                CustommerName = x.CustomerName,
                Address = x.CustomerAddress,
                Mobile = x.CustomerMobile,
            }).FirstOrDefaultAsync();

            var getProduct = await _context.OrderDetails.Where(x => x.OrderID == orderId).Select(x => new OrderDetailViewModel()
            {
                OrderID = x.OrderID,
                ProductID = x.ProductID,
                Price = x.Price,
                Quantity = x.Quantity,
                ProductName = _context.Products.Where(p => p.ID == x.ProductID).Select(n => n.Name).FirstOrDefault(),
            }).ToListAsync();

            orderDetails.OrderDetails = getProduct;

            return orderDetails;
        }

        public async Task<List<Order>> getOrders()
        {
            var orders = await _context.Orders.Select(x => new Order()).ToListAsync();
            return orders;
        }

        public async Task<PageActionResult> TakeOrder(int id)
        {
            var order = await _context.Orders.Where(o => o.ID == id).FirstOrDefaultAsync();

            if (order.Status == -1)
            {
                return new PageActionResult { status = false, Message = "Đơn hàng này đã hủy nên không thể nhận đơn" };
            }

            if (order.Status == 1)
            {
                return new PageActionResult { status = false, Message = "Đơn hàng này đang xử lý nên không thể nhận đơn" };
            }

            if (order.Status == 2)
            {
                return new PageActionResult { status = false, Message = "Đơn hàng đã Hoàn thành trước đó" };
            }

            order.Status = 1;
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new PageActionResult { status = true, Message = "Đơn hàng đã đã được nhận" };
            }
            else
            {
                return new PageActionResult { status = false, Message = "Đơn hàng chưa được nhận" };
            }
        }
    }
}