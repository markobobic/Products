using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.ViewModels
{
    public class ProductViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public string SupplierName { get; set; }

    }
}