using ConsoleTools;
using SJIDON_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace SJIDON_HFT_2022231.Client
{
    internal class Program
    {
        public static RestService rserv = new RestService("http://localhost:12969");
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);


            var menu = new ConsoleMenu()
               .Add("CRUD methods", () => CrudMenu())
               .Add("non-CRUD methods", () => NonCrudMenu())
               .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

        private static void CrudMenu()
        {

            var menu = new ConsoleMenu()
                .Add("Create element", CreatePreMenu)
                .Add("Get one element", ReadPreMenu)
                .Add("Get all element", ReadAllPreMenu)
                .Add("Update element", UpdatePreMenu)
                .Add("Delete element", DeletePreMenu)
                .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

        private static void NonCrudMenu()
        {
            var menu = new ConsoleMenu()
               .Add("Get car(s) that are owned by BMW Group", GetCarWhereBrandOwnerIsBMWGroup)
               .Add("Get car(s) that have more than 18 employees at their dealerships", GetCarWhereMoreThan18Employees)
               .Add("Get dealership(s) that have a car with 313 horsepower", GetDealershipWhereCar313hp)
               .Add("Get dealership(s) that have a car model named Charger", GetDealershipWhereCarModelIsCharger)
               .Add("Get dealership(s) that have a car that costs 209700", GetDealershipWherePriceIs209700)
               .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

        private static void PreMenu(Action car, Action brand, Action dealership)
        {
            var menu = new ConsoleMenu()
                .Add("Car", car)
                .Add("Brand", brand)
                .Add("Dealership", dealership)
                .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

        private static void CreatePreMenu()
        {
            PreMenu(CreateCar, CreateBrand, CreateDealership);
        }

        private static void CreateDealership()
        {
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Number of employees:");
            int employees = int.Parse(Console.ReadLine());
            rserv.Post<Dealership>(new Dealership() { Name = name, Employees = employees }, "dealership");
        }

        private static void CreateBrand()
        {
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Owner:");
            string owner = Console.ReadLine();
            Console.WriteLine("Dealership id: ");
            int dealershipid = int.Parse(Console.ReadLine());
            rserv.Post<Brand>(new Brand() { Name = name, Owner = owner, Dealership_Id = dealershipid }, "brand");
        }

        private static void CreateCar()
        {
            Console.WriteLine("Model: ");
            string model = Console.ReadLine();
            Console.WriteLine("Horsepower: ");
            int horsepower = int.Parse(Console.ReadLine());
            Console.WriteLine("Price:");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine("Brand id: ");
            int brandid = int.Parse(Console.ReadLine());
            rserv.Post<Car>(new Car() { Model = model, Horsepower = horsepower, Brand_Id = brandid }, "car");
        }

        private static void ReadPreMenu()
        {
            PreMenu(ReadCar, ReadBrand, ReadDealership);
        }

        private static void ReadDealership()
        {
            Console.WriteLine("Search for desired with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            var getdealership = rserv.Get<Dealership>(id, "dealership");
            Console.WriteLine($"Id: {getdealership.Id}, Name: {getdealership.Name}, Employees: {getdealership.Employees}");
            Console.ReadLine();

        }

        private static void ReadBrand()
        {
            Console.WriteLine("Search for desired with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            var getbrand = rserv.Get<Brand>(id, "brand");
            Console.WriteLine($"Id: {getbrand.Id}, Name: {getbrand.Name}, Owner: {getbrand.Owner}, DealershipID: {getbrand.Dealership_Id}");
            Console.ReadLine();

        }

        private static void ReadCar()
        {
            Console.WriteLine("Search for desired with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            var getcar = rserv.Get<Car>(id, "car");
            Console.WriteLine($"Id: {getcar.Id}, Model: {getcar.Model}, Horsepower: {getcar.Horsepower}, Price: {getcar.Price}, BrandID: {getcar.Brand_Id}");
            Console.ReadLine();

        }

        private static void ReadAllPreMenu()
        {
            PreMenu(PrintAllCars, PrintAllBrands, PrintAllDealerships);
        }

        private static void PrintAllCars()
        {
            var cars = rserv.Get<Car>("car");
            Console.WriteLine("-------------Cars-------------");
            CarToConsole(cars);
            Console.ReadLine();
        }

        private static void PrintAllBrands()
        {
            var brands = rserv.Get<Brand>("brand");
            Console.WriteLine("-------------Brands-------------");
            BrandToConsole(brands);
            Console.ReadLine();
        }

        private static void PrintAllDealerships()
        {
            var dealerships = rserv.Get<Dealership>("dealership");
            Console.WriteLine("-------------Dealerships-------------");
            DealershipToConsole(dealerships);
            Console.ReadLine();
        }

        private static void UpdatePreMenu()
        {
            PreMenu(UpdateCar, UpdateBrand,  UpdateDealership);
        }

        private static void UpdateDealership()
        {
            Console.WriteLine("Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Employees:");
            int employees = int.Parse(Console.ReadLine());
            Dealership input = new Dealership() { Id = id, Name = name, Employees = employees };
            rserv.Put(input, "dealership");
        }

        private static void UpdateBrand()
        {
            Console.WriteLine("Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Owner:");
            string owner = Console.ReadLine();
            Console.WriteLine("Dealership id: ");
            int dealershipid = int.Parse(Console.ReadLine());
            Brand input = new Brand() { Id = id, Name = name, Owner = owner, Dealership_Id = dealershipid };
            rserv.Put(input, "brand");
        }

        private static void UpdateCar()
        {
            Console.WriteLine("Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Model: ");
            string model = Console.ReadLine();
            Console.WriteLine("Horsepower:");
            int horsepower = int.Parse(Console.ReadLine());
            Console.WriteLine("Price:");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine("Brand id: ");
            int brandid = int.Parse(Console.ReadLine());
            Car input = new Car() { Id = id, Model = model, Horsepower = horsepower, Price = price, Brand_Id = brandid };
            rserv.Put(input, "car");
        }

        private static void DeletePreMenu()
        {
            PreMenu(DeleteCar, DeleteBrand, DeleteDealership);
        }

        private static void DeleteDealership()
        {
            Console.WriteLine("Delete element with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            rserv.Delete(id, "dealership");
        }

        private static void DeleteBrand()
        {
            Console.WriteLine("Delete element with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            rserv.Delete(id, "brand");
        }

        private static void DeleteCar()
        {
            Console.WriteLine("Delete element with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            rserv.Delete(id, "car");
        }



        private static void GetDealershipWhereCar313hp()
        {
            var output = rserv.Get<Dealership>("stat/GetDealershipWhereCar313hp");
            DealershipToConsole(output);
            Console.ReadLine();
        }
        private static void GetDealershipWhereCarModelIsCharger()
        {
            var output = rserv.Get<Dealership>("stat/GetDealershipWhereCarModelIsCharger");
            DealershipToConsole(output);
            Console.ReadLine();
        }

        private static void GetDealershipWherePriceIs209700()
        {
            var output = rserv.Get<Dealership>("stat/GetDealershipWherePriceIs209700");
            DealershipToConsole(output);
            Console.ReadLine();
        }
        private static void GetCarWhereMoreThan18Employees()
        {
            var output = rserv.Get<Car>("stat/GetCarWhereMoreThan18Employees");
            CarToConsole(output);
            Console.ReadLine();
        }
        private static void GetCarWhereBrandOwnerIsBMWGroup()
        {
            var output = rserv.Get<Car>("stat/GetCarWhereBrandOwnerIsBMWGroup");
            CarToConsole(output);
            Console.ReadLine();
        }



        private static void CarToConsole(IEnumerable<Car> input)
        {
            foreach (var item in input)
            {
                Console.WriteLine($"Id: {item.Id}, Model: {item.Model}, Horsepower: {item.Horsepower}, Price: {item.Price}, BrandID: {item.Brand_Id}");
            }
        }
        private static void BrandToConsole(IEnumerable<Brand> input)
        {
            foreach (var item in input)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Owner: {item.Owner}, DealershipID: {item.Dealership_Id}");
            }
        }
        private static void DealershipToConsole(IEnumerable<Dealership> input)
        {
            foreach (var item in input)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Employees: {item.Employees}");
            }
        }
    }
}
