using System;

namespace ViewModels.Catalog.Orders
{
    public class OrderRequest
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string CustommerMessage { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustommerId { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}