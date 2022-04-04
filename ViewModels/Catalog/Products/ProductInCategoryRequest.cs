using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Catalog.Products
{
    public class ProductInCategoryRequest
    {
        public string ProductName { get; set; }

        public string? ImagePath { get; set; }

        public Decimal Price { get; set; }

        public int ProductId { get; set; }

        public int CateId { get; set; }
        public string CateName { get; set; }
    }
}
