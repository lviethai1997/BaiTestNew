using Data.Entites;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProductCategory>().HasData(
            new ProductCategory() { ID = 1, Name = "CPU", ParentID = 0, CreateTime = DateTime.Now, UpdateTime = DateTime.Now },
            new ProductCategory() { ID = 2, Name = "Ram", ParentID = 0, CreateTime = DateTime.Now, UpdateTime = DateTime.Now },
            new ProductCategory() { ID = 3, Name = "PSU", ParentID = 0, CreateTime = DateTime.Now, UpdateTime = DateTime.Now },
            new ProductCategory() { ID = 5, Name = "Mainboard", ParentID = 0, CreateTime = DateTime.Now, UpdateTime = DateTime.Now }
            );

            modelBuilder.Entity<Product>().HasData(
               new Product() { Price = 800000, ID = 1, Name = "Ram Kingston 8G", CategoryID = 2, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/fbc4ad81-9d8d-475b-a849-fb0e37e0fa38.png" },
               new Product() { Price = 1200000, ID = 2, Name = "RAM APACER 8GB PANTHER DDR4 BUS 3200", CategoryID = 2, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/346d040f-f5ff-439b-8603-ff971ee67f1e.jpg" },
               new Product() { Price = 1800000, ID = 3, Name = "Ram Fury ", CategoryID = 2, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/8dc9fbf6-67f4-4de8-87e4-81b7bf3b1a76.png" },
               new Product() { Price = 1300000, ID = 4, Name = "Ram Gskill", CategoryID = 2, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/e74efbc7-f443-4e87-9c40-ee646d8b4fc4.png" },
               new Product() { Price = 5800000, ID = 5, Name = "CPU Core i5 12400f", CategoryID = 1, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/867e15b0-8fcb-4588-beb4-e93a1e3b43d7.jpg" },
               new Product() { Price = 7000000, ID = 6, Name = "CPU Intel Core I7-12700 12C/20T 25MB Cache 2.70 GHz Up to 4.9 GHz", CategoryID = 1, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/74c46dfb-63db-41e6-b073-e61fcd38595e.jpg" },
               new Product() { Price = 4000000, ID = 7, Name = "Mainboard Gigabyte B660M-DS3H DDR4 Socket LGA 1700", CategoryID = 5, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/84aee26b-753e-478e-b79f-224f06cde1e3.jpg" },
               new Product() { Price = 4700000, ID = 8, Name = "Mainboard Gigabyte B660M Aorus Pro DDR4 Socket LGA 1700", CategoryID = 5, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/31a531fa-8c49-417e-9afc-5a2b834b52a2.jpg" },
               new Product() { Price = 2500000, ID = 9, Name = "Nguồn Cooler Master Elite P400 V3 300W", CategoryID = 3, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/f6e9fa37-5de0-4a92-946e-621025d6774f.jpg" },
               new Product() { Price = 3500000, ID = 10, Name = "Nguồn Cooler Master MWE 650 BRONZE V2 230V 650W 80PLUS BRONZE", CategoryID = 3, Quantity = 15, Description = "ok", CreateTime = DateTime.Now, UpdatedDate = DateTime.Now, Status = 0, ImagePath = "/user-content/fe0c4d80-68d5-4c35-a214-f4a0c39693a6.jpg" }
               );

            modelBuilder.Entity<User>().HasData(
                new User() { ID = 1, Username = "admin", Password = "0e7517141fb53f21ee439b355b5a1d0a", FullName = "Administrator", Address = "Binh Duong", Email = "lviethai1997@gmail.com", Mobile = "18184846456", Role = 0, Status = 0 },
                new User() { ID = 2, Username = "user", Password = "0e7517141fb53f21ee439b355b5a1d0a", FullName = "hai le", Address = "Binh Duong", Email = "lviethai1997@gmail.com", Mobile = "18184846456", Role = 0, Status = 0 }
                );


        }
    }
}