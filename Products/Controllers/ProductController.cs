using Products.Repository;
using Products.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepo db;

        public ProductController(IProductRepo _db)
        {
            db = _db;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            
            ViewBag.Suppliers = await db.IncludeSuppliersDropdown();
            ViewBag.Categories = await db.IncludeCategoriesDropdown();
            ViewBag.Manufactures = await db.IncludeManufacturesDropdown();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ProductAddViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await db.MapDataAndSave(viewModel);
                return Json(new { success = true, message = "Added Successfully" });
            }
            ViewBag.Suppliers = await db.IncludeSuppliersDropdown();
            ViewBag.Categories = await db.IncludeCategoriesDropdown();
            ViewBag.Manufactures = await db.IncludeManufacturesDropdown();
            return new HttpStatusCodeResult(400);
        }



        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            if (id > 0)
            {
                var product = await db.GetById(id);
                if (product == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                }
                ProductUpdateViewModel viewModel = new ProductUpdateViewModel(product);

                ViewBag.Categories = await db.IncludeCategoriesDropdown(product.Category);
                ViewBag.Suppliers = await db.IncludeSuppliersDropdown(product.Supplier);
                ViewBag.Manufactures =await db.IncludeManufacturesDropdown(product.Manufacturer);
                return View(viewModel);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ProductUpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await db.UpdateMapAndSave(viewModel);
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
            ViewBag.Categories =await db.IncludeCategoriesDropdown();
            ViewBag.Suppliers =await db.IncludeSuppliersDropdown();
            ViewBag.Manufactures =await db.IncludeManufacturesDropdown();
            return new HttpStatusCodeResult(HttpStatusCode.NotModified);
        }

        public async Task<ActionResult> GetData()
        {
            var data = await db.GetData();
            if (data != null) { 
            return Json(data, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(404);
        }


    }
}