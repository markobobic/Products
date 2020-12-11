using Products.Models;
using Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Products.Repository
{
    public interface IProductRepo 
    {
        Task<List<ProductViewModel>> GetData();
        Task<List<SelectListItem>> IncludeManufacturesDropdown([Optional] Manufacturer manufacturer);
        Task<List<SelectListItem>> IncludeSuppliersDropdown([Optional] Supplier supplier);
        Task<List<SelectListItem>> IncludeCategoriesDropdown([Optional] Category category);
        Task MapDataAndSave(ProductAddViewModel viewModel);
        Task<Product> GetById(int id);
        Task UpdateMapAndSave(ProductUpdateViewModel viewModel);
    }
}