using Microsoft.EntityFrameworkCore;
using SJIDON_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Repository
{
    public class CarDbContext : DbContext
    {
        public virtual DbSet<Dealership> Dealerships { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }

        public CarDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("cardb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasOne(brand => brand.Dealership)
                      .WithMany(dealership => dealership.Brands)
                      .HasForeignKey(brand => brand.Dealership_Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasOne(car => car.Brand)
                      .WithMany(brand => brand.Cars)
                      .HasForeignKey(car => car.Brand_Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            Dealership dealership1 = new Dealership() { Id = 1, Name = "Japanese car dealership", Employees = 16 };
            Dealership dealership2 = new Dealership() { Id = 2, Name = "German car dealership", Employees = 20 };
            Dealership dealership3 = new Dealership() { Id = 3, Name = "American car dealership", Employees = 14 };

            Brand brand1 = new Brand() { Id = 1, Name = "Subaru", Owner = "Subaru Corp.", Dealership_Id = 1 };
            Brand brand2 = new Brand() { Id = 2, Name = "Nissan", Owner = "Renault-Nissan-Mitsubishi Alliance", Dealership_Id = 1 };
            Brand brand3 = new Brand() { Id = 3, Name = "Audi", Owner = "Volkswagen Group", Dealership_Id = 2 };
            Brand brand4 = new Brand() { Id = 4, Name = "BMW", Owner = "BMW Group", Dealership_Id = 2 };
            Brand brand5 = new Brand() { Id = 5, Name = "Dodge", Owner = "Stellantis", Dealership_Id = 3 };
            Brand brand6 = new Brand() { Id = 6, Name = "Ford", Owner = "Ford Motor Co.", Dealership_Id = 3 };

            Car car1 = new Car() { Id = 1, Model = "Impreza WRX STI", Horsepower = 280, Price = 22000, Brand_Id = 1 };
            Car car2 = new Car() { Id = 2, Model = "BRZ", Horsepower = 228, Price = 28000, Brand_Id = 1 };
            Car car3 = new Car() { Id = 3, Model = "GT-R R34", Horsepower = 280, Price = 25000, Brand_Id = 2 };
            Car car4 = new Car() { Id = 4, Model = "350Z", Horsepower = 313, Price = 19000, Brand_Id = 2 };
            Car car5 = new Car() { Id = 5, Model = "R8", Horsepower = 620, Price = 209700, Brand_Id = 3 };
            Car car6 = new Car() { Id = 6, Model = "RS5", Horsepower = 450, Price = 77000, Brand_Id = 3 };
            Car car7 = new Car() { Id = 7, Model = "M4", Horsepower = 503, Price = 76000, Brand_Id = 4 };
            Car car8 = new Car() { Id = 8, Model = "320i", Horsepower = 150, Price = 6000, Brand_Id = 4 };
            Car car9 = new Car() { Id = 9, Model = "Charger", Horsepower = 807, Price = 115000, Brand_Id = 5 };
            Car car10 = new Car() { Id = 10, Model = "Challenger", Horsepower = 717, Price = 71000, Brand_Id = 5 };
            Car car11 = new Car() { Id = 11, Model = "Mustang GT", Horsepower = 450, Price = 39000, Brand_Id = 6 };
            Car car12 = new Car() { Id = 12, Model = "Focus RS", Horsepower = 350, Price = 37000, Brand_Id = 6 };

            modelBuilder.Entity<Dealership>().HasData(dealership1, dealership2, dealership3);
            modelBuilder.Entity<Brand>().HasData(brand1,brand2, brand3, brand4, brand5, brand6);
            modelBuilder.Entity<Car>().HasData(car1, car2, car3, car4, car5, car6, car7, car8, car9, car10, car11, car12);

        }
    }
}
