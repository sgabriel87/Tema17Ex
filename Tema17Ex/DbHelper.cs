using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tema17Ex.Data;
using Tema17Ex.Models;

namespace Tema17Ex
{
    public static class DbHelper
    {
        public static void SeedDatabase()
        {
            using (var context = new StoreContext())
            {
                
                context.Database.Migrate();

                
                if (!context.Categories.Any())
                {
                    Console.WriteLine("Seeding Categories...");
                    context.Categories.AddRange(
                        new Category { Name = "Electronics", IconUrl = "https://storage.googleapis.com/gweb-uniblog-publish-prod/images/Old_Electronics_hero_1.width-1300.jpg" },
                        new Category { Name = "Books", IconUrl = "https://qph.cf2.quoracdn.net/main-qimg-e72b377177362dc58915ee917aef63ec-lq" }
                    );
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Categories already exist.");
                }

                
                if (!context.Manufacturers.Any())
                {
                    Console.WriteLine("Seeding Manufacturers...");
                    context.Manufacturers.AddRange(
                        new Manufacturer { Name = "Electronics Manufacturer", Address = "123 Street", CUI = "CUI123" },
                        new Manufacturer { Name = "Books Store", Address = "456 Avenue", CUI = "CUI456" }
                    );
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Manufacturers already exist.");
                }

                
                if (!context.Labels.Any())
                {
                    Console.WriteLine("Seeding Labels...");
                    context.Labels.AddRange(
                        new Label { BarCode = Guid.NewGuid(), Price = 100.0 },
                        new Label { BarCode = Guid.NewGuid(), Price = 200.0 }
                    );
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Labels already exist.");
                }
            }
        }


        public static void AddCategory(StoreContext context, string name, string iconUrl)
        {
            var existingCategory = context.Categories.FirstOrDefault(c => c.Name == name);
            if (existingCategory != null)
            {
                Console.WriteLine($"Category '{name}' already exists.");
                return;
            }
            var category = new Category { Name = name, IconUrl = iconUrl };
            context.Categories.Add(category);
            context.SaveChanges();
            Console.WriteLine($"Category '{name}' added successfully.");
        }


        public static void AddManufacturer(StoreContext context, string name, string address, string cui)
        {
            var existingManufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == name);
            if (existingManufacturer != null)
            {
                Console.WriteLine($"Manufacturer '{name}' already exists.");
                return;
            }
            var manufacturer = new Manufacturer { Name = name, Address = address, CUI = cui };
            context.Manufacturers.Add(manufacturer);
            context.SaveChanges();
            Console.WriteLine($"Manufacturer '{name}' added successfully.");
        }


        public static void UpdateProductPrice(StoreContext context, int productId, double newPrice)
        {
            var product = context.Products.Include(p => p.Label).FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                var oldPrice = product.Label.Price;
                product.Label.Price = newPrice;
                context.SaveChanges();
                Console.WriteLine($"Updated product '{product.Name}': Old Price = {oldPrice}, New Price = {newPrice}");
            }
            else
            {
                Console.WriteLine($"Product with ID {productId} not found.");
            }
        }


        public static double GetTotalStockValue(StoreContext context)
        {
            return context.Products.Include(p => p.Label).Sum(p => p.Stock * p.Label.Price);
        }

        public static double GetStockValueByManufacturer(StoreContext context, int manufacturerId)
        {
            return context.Products
                .Include(p => p.Label)
                .Where(p => p.ManufacturerId == manufacturerId)
                .Sum(p => p.Stock * p.Label.Price);
        }

        public static double GetStockValueByCategory(StoreContext context, int categoryId)
        {
            var totalStockValue = context.Products
                .Where(p => p.CategoryId == categoryId)
                .Sum(p => p.Stock * p.Label.Price);

            return totalStockValue;
        }

        public static void AddProduct(StoreContext context, string name, int stock, int manufacturerId, int categoryId)
        {
            var existingProduct = context.Products.FirstOrDefault(p => p.Name == name && p.CategoryId == categoryId);

            if (existingProduct != null)
            {
                Console.WriteLine($"Product '{name}' already exists in the database.");
                return;
            }

            var label = new Label { BarCode = Guid.NewGuid(), Price = 0.0 };
            context.Labels.Add(label);
            context.SaveChanges();

            var product = new Product
            {
                Name = name,
                Stock = stock,
                ManufacturerId = manufacturerId,
                CategoryId = categoryId,
                LabelId = label.Id
            };
            context.Products.Add(product);
            context.SaveChanges();

            Console.WriteLine($"Product '{name}' added successfully.");
        }
    }
}

