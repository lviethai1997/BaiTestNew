using Data.Entites;
using System.Collections.Generic;

namespace ViewModels.Catalog.OrderDetails
{
    public class OrderDetailRequest
    {
        public int OrderId { get; set; }
        public int OrderStatus { get; set; }    
        public string CustommerName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}