using Products.DAL;
using Products.Models;
using Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Products.Helpers;
using System.Web.Mvc;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.IO;

namespace Products.Repository
{
    public class ProductRepo : GenericRepository<Product>,IProductRepo
    {
        private readonly ProductsDbContext db;
        public ProductRepo(ProductsDbContext _db) : base(_db)
        {
            db = _db;
        }

        public async Task<List<ProductViewModel>> GetData()
        {
            var all = FindAll().Include(x => x.Category).Include(x => x.Manufacturer).Include(x => x.Supplier);
            var data = await all.Select(x => new ProductViewModel
            {
                Id=x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                CategoryName = x.Category.Name,
                ManufacturerName = x.Manufacturer.Name,
                SupplierName = x.Supplier.Name

            }).ToListAsync();
            return data;
        }

        public async Task<List<SelectListItem>> IncludeManufacturesDropdown([Optional] Manufacturer manufacturer)
        {
            return await Dropdown.GenerateDropdownManufactures(db,manufacturer);

        }
        public async Task<List<SelectListItem>> IncludeSuppliersDropdown([Optional] Supplier supplier)
        {
            return await Dropdown.GenerateDropdownSupplaiers(db,supplier);

        }
        public async Task<List<SelectListItem>> IncludeCategoriesDropdown([Optional] Category category)
        {
            return await Dropdown.GenerateCategories(db,category);

        }
        public async Task MapDataAndSave(ProductAddViewModel viewModel)
        {
            if (viewModel != null)
            {
                var product = new Product();
                var supplier = await db.Suppliers.Include(x=>x.Products).Where(x => x.Id == viewModel.SupplierId).FirstOrDefaultAsync();
                var manufacturer = await db.Manufacturers.SingleOrDefaultAsync(x=>x.Id==viewModel.ManufacturerId);
                var category = await db.Categories.Include(x=>x.Products).SingleOrDefaultAsync(x => x.Id == viewModel.CategoryId);
                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                product.Supplier = supplier;
                product.Manufacturer = manufacturer;
                product.Category = category;
                product.Description = viewModel.Description;
                Create(product);
                await db.SaveChangesAsync();
                JsonCRUD.AddToJson(product, JsonPath.PathToProducts);
                return;
            }
            throw new System.ArgumentException();
            
        }

        public async Task UpdateMapAndSave(ProductUpdateViewModel viewModel)
        {
            var currentProduct = await db.Products.Include(x=>x.Manufacturer)
                .Include(x=>x.Supplier).Include(x=>x.Category)
                .SingleOrDefaultAsync(x => x.Id == viewModel.Id);
            currentProduct.CategoryId = viewModel.CategoryId;
            currentProduct.ManufacturerId = viewModel.ManufacturerId;
            currentProduct.SupplierId = viewModel.SupplierId;
            currentProduct.Price = viewModel.Price;
            currentProduct.Description = viewModel.Description;
            currentProduct.Name = viewModel.Name;
            Update(currentProduct);
            await SaveAsync();
            JsonCRUD.UpdateJson(currentProduct, JsonPath.PathToProducts);

        }

       

        public Task<Product> GetById(int id)
        {
            return FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}


