using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Catalog.Products
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống Tên sản phẩm")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống Danh mục sản phẩm")]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống Tồn kho")]
        public int Quantity { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImagePath { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống Giá sản phẩm")]
        public Decimal Price { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống Thông tin sản phẩm")]
        public string Description { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int Status { get; set; }
    }
}