using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Products.Helpers
{
    public static class JsonPath
    {
        public static string PathToProducts = AppDomain.CurrentDomain.BaseDirectory + @"\JSONData\Products.json";
        public static string PathToSuppliers = AppDomain.CurrentDomain.BaseDirectory + @"\JSONData\Suppliers.json";
        public static string PathToManafactures = AppDomain.CurrentDomain.BaseDirectory + @"\JSONData\Manufactures.json";
        public static string PathToCategories = AppDomain.CurrentDomain.BaseDirectory + @"\JSONData\Categories.json";
    }
}