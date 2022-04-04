using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Catalog.ProductCategories
{
    public class ProductCategoryRequest
    {
        [Required(ErrorMessage ="Không được để trống tên danh mục sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Không được để trống danh mục sản phẩm")]
        public int ParentID { get; set; }

        [Required(ErrorMessage = "Không được để trống danh mục sản phẩm")]
        public int Status { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
