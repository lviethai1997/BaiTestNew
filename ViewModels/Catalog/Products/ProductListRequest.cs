using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Catalog.Products
{
    public class ProductListRequest
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string CateName { get; set; }

        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int Status { get; set; }
    }
}
