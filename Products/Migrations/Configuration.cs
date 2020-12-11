namespace Products.Migrations
{
    using Newtonsoft.Json;
    using Products.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Products.DAL.ProductsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Products.DAL.ProductsDbContext context)
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>()
                { new Manufacturer() { Id = 1, Name = "ProizvodjacA" },
                  new Manufacturer() { Id = 2, Name = "ProizvodjacB" },
                  new Manufacturer() { Id = 3, Name = "ProizvodjacC" } };
            List<Supplier> suppliers = new List<Supplier>()
                {
                  new Supplier() { Id = 1, Name = "DobavljacA",Products=new List<Product>()},
                  new Supplier() { Id = 2, Name = "DobavljacB", Products=new List<Product>() },
                  new Supplier() { Id = 3, Name = "DobavljacC",Products=new List<Product>() }
                };

            List<Category> categories = new List<Category>()
                {
                new Category() { Id = 1, Name = "KategorijaA"},
                new Category() { Id = 2, Name = "KategorijaB" },
                new Category() { Id = 3, Name = "KategorijaC" }
                };
            List<Product> products = new List<Product>()
                {
                  new Product() { Id = 1, Description = "Description",
                  CategoryId = 1, ManufacturerId = 1, Name = "ProizvodA", Price = 200, SupplierId=1 },
                 new Product()
                {
                    Id = 2,
                    Description = "Description",
                    CategoryId = 2,
                    ManufacturerId = 2,
                    Name = "ProizvodB",
                    Price = 300,
                   SupplierId=2,


                },
                  new Product()
                {
                    Id = 3,
                    Description = "Description",
                    CategoryId = 3,
                    ManufacturerId = 3,
                    Name = "ProizvodC",
                    Price = 350,
                    SupplierId=3
                }
                };

            context.Manufacturers.AddRange(manufacturers);
            categories[0].Products.Add(products[0]);
            categories[1].Products.Add(products[1]);
            categories[2].Products.Add(products[2]);
            suppliers[0].Products.Add(products[0]);
            suppliers[1].Products.Add(products[1]);
            suppliers[2].Products.Add(products[2]);

            context.Categories.AddRange(categories);
            context.Suppliers.AddRange(suppliers);
            context.Products.AddRange(products);
            context.SaveChanges();

            var jsonCategories = context.Categories.
                Include(x => x.Products.Select(y => y.Manufacturer))
                .Include(x => x.Products.Select(y => y.Supplier)).ToList();
            var jsonManufactures = context.Manufacturers.ToList();
            var jsonSuppliers = context.Suppliers.Include(x => x.Products).ToList();
            var jsonProducts = context.Products.Include(x => x.Category)
                .Include(x => x.Manufacturer).Include(x => x.Supplier).ToList();
            using (StreamWriter file = File.CreateText("..\\JSONData\\Categories.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, jsonCategories);
            }
            using (StreamWriter file = File.CreateText("..\\JSONData\\Manufactures.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, jsonManufactures);
            }
            using (StreamWriter file = File.CreateText("..\\JSONData\\Suppliers.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, jsonSuppliers);
            }
            using (StreamWriter file = File.CreateText("..\\JSONData\\Products.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, jsonProducts);
            }




        }

    }
}

