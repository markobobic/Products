using Products.DAL;
using Products.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Products.Helpers
{
    public static class Dropdown
    {

        public static async Task<List<SelectListItem>> GenerateDropdownManufactures(ProductsDbContext db, [Optional] Manufacturer manufacturer)
        {
            List<SelectListItem> data = new List<SelectListItem>();

            if (manufacturer == null)
            {
                data = await db.Manufacturers.Select(manufact => new SelectListItem()
                {
                    Value = manufact.Id.ToString(),
                    Text = manufact.Name
                }).ToListAsync();
                return data;
            }
            data = await db.Manufacturers.Select(manufac => new SelectListItem()
            {
                Value = manufac.Id.ToString(),
                Text = manufac.Name
            }).ToListAsync();
            SelectListItem currentManufacturer = new SelectListItem() { Value = manufacturer.Id.ToString(), Text = manufacturer.Name, Selected = true };
            data.Add(currentManufacturer);
            return data;
        }
        public static async Task<List<SelectListItem>> GenerateDropdownSupplaiers(ProductsDbContext db, [Optional] Supplier supplier)
        {
            List<SelectListItem> data = new List<SelectListItem>();

            if (supplier == null)
            {
                data = await db.Suppliers.Select(sup => new SelectListItem()
                {
                    Value = sup.Id.ToString(),
                    Text = sup.Name
                }).ToListAsync();
                return data;
            }
            data = await db.Suppliers.Select(sup => new SelectListItem()
            {
                Value = sup.Id.ToString(),
                Text = sup.Name
            }).ToListAsync();
            SelectListItem currentSupplier = new SelectListItem() { Value = supplier.Id.ToString(), Text = supplier.Name, Selected = true };
            data.Add(currentSupplier);
            return data;
        }

        public static async Task<List<SelectListItem>> GenerateCategories(ProductsDbContext db, [Optional] Category category)
        {
            List<SelectListItem> data = new List<SelectListItem>();

            if (category == null)
            {
                data = await db.Categories.Select(cat => new SelectListItem()
                {
                    Value = cat.Id.ToString(),
                    Text = cat.Name
                }).ToListAsync();
                return data;
            }
            data = await db.Categories.Select(cat => new SelectListItem()
            {
                Value = cat.Id.ToString(),
                Text = cat.Name
            }).ToListAsync();
            SelectListItem currentCategory = new SelectListItem() { Value = category.Id.ToString(), Text = category.Name, Selected = true };
            data.Add(currentCategory);
            return data;
        }


    }
}