using Data.EF;
using Data.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Catalog.ProductCategories;
using ViewModels.Common;

namespace Services.Catalog.ProductCategories
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ShopDbContext _context;

        public ProductCategoryService(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<PageActionResult> CreateProductCategory(ProductCategoryRequest request)
        {
            var productCategory = new ProductCategory()
            {
                Name = request.Name,
                ParentID = request.ParentID,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Status = request.Status,
            };

            await _context.ProductCategories.AddAsync(productCategory);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new PageActionResult { status = true, Message = "Tạo danh mục sản phẩm Thành Công!" };
            }
            else
            {
                return new PageActionResult { status = false, Message = "Tạo danh mục sản phẩm Thất bại!" };
            }
        }

        public async Task<PageActionResult> DeleteProductCategory(int id)
        {
            var FindProductCategory = await _context.ProductCategories.FindAsync(id);

            if (FindProductCategory == null)
            {
                return new PageActionResult { status = false, Message = "Không tồn tại danh mục này" };
            }

            _context.Remove(FindProductCategory);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new PageActionResult { status = true, Message = "Xóa danh mục sản phẩm thành công!" };
            }
            else
            {
                return new PageActionResult { status = false, Message = "Xóa danh mục sản phẩm Thất bại!" };
            }
        }

        public async Task<List<ProductCategory>> GetAllForClient()
        {
            var getAll = await _context.ProductCategories.Where(x => x.Status == 0).Select(x => new ProductCategory()
            {
                ID = x.ID,
                Name = x.Name,
                ParentID = x.ParentID,
                CreateTime = x.CreateTime,
                UpdateTime = x.UpdateTime,
                Status = x.Status,
            }).ToListAsync();

            return new List<ProductCategory>(getAll);
        }

        public async Task<List<ProductCategory>> GetAll()
        {
            var getAll = await _context.ProductCategories.Select(x => new ProductCategory()
            {
                ID = x.ID,
                Name = x.Name,
                ParentID = x.ParentID,
                CreateTime = x.CreateTime,
                UpdateTime = x.UpdateTime,
                Status = x.Status,
            }).ToListAsync();

            return new List<ProductCategory>(getAll);
        }

        public async Task<ProductCategory> GetById(int id)
        {
            var FindById = await _context.ProductCategories.FindAsync(id);

            if (FindById == null)
            {
                return null;
            }

            return FindById;
        }

        public async Task<List<CbCategories>> getCategory()
        {
            var category = await _context.ProductCategories.OrderBy(x => x.ID).Select(x => new CbCategories()
            {
                ID = x.ID,
                Name = x.Name
            }).ToListAsync();

            return category;
        }

        public async Task<PageActionResult> HideProductCategory(int id)
        {
            var findById = await _context.ProductCategories.FindAsync(id);

            if (findById == null)
            {
                return new PageActionResult { status = false, Message = "Không tồn tại danh mục này" };
            }


            findById.Status = findById.Status == 0 ? 1 : 0;
            var productInCate = await _context.Products.Where(x => x.CategoryID == id).ToListAsync();

            foreach (var product in productInCate)
            {
                product.Status = findById.Status;
            }

            var result = await _context.SaveChangesAsync();

            string message = "";

            if (findById.Status == 0)
            {
                message = "Hiện Danh mục sản phẩm thành công";
            }
            else
            {
                message = "Ẩn Danh mục sản phẩm thành công";
            }

            if (result > 0)
            {
                return new PageActionResult { status = true, Message = message };
            }
            else
            {
                return new PageActionResult { status = false, Message = message };
            }
        }

        public async Task<PageActionResult> UpdateProductCategory(int id, ProductCategoryRequest request)
        {
            var findByid = await _context.ProductCategories.FindAsync(id);

            if (findByid == null)
            {
                return new PageActionResult { status = false, Message = "Không tồn tại danh mục này" };
            }

            findByid.Name = request.Name;
            findByid.ParentID = request.ParentID;
            findByid.UpdateTime = DateTime.Now;
            findByid.CreateTime = request.CreateTime;
            findByid.Status = request.Status;

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new PageActionResult { status = true, Message = "Cập nhật danh mục sản phẩm thành công!" };
            }
            else
            {
                return new PageActionResult { status = false, Message = "Cập nhật danh mục sản phẩm thất bại!" };
            }

        }
    }
}