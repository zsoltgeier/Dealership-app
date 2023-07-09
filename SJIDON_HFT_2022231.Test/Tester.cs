using Moq;
using NUnit.Framework;
using SJIDON_HFT_2022231.Logic;
using SJIDON_HFT_2022231.Models;
using SJIDON_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Humanizer.In;

namespace SJIDON_HFT_2022231.Test
{
    [TestFixture]
    public class Tester
    {
        CarLogic carl;
        BrandLogic brandl;
        DealershipLogic dealershipl;

        [SetUp]
        public void Setup()
        {
            Mock<IRepository<Car>> mockCarRepo = new Mock<IRepository<Car>>();
            Mock <IRepository<Brand>> mockBrandRepo = new Mock<IRepository<Brand>>();
            Mock<IRepository<Dealership>> mockDealershipRepo = new Mock<IRepository<Dealership>>();


            mockCarRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(
                new Car()
                {
                    Id = 1,
                    Model = "Impreza WRX STI",
                    Horsepower = 280,
                    Price = 22000,
                    Brand_Id = 1
                });

            mockCarRepo.Setup(x => x.ReadAll()).Returns(FakeCarObject);
            mockBrandRepo.Setup(x => x.ReadAll()).Returns(FakeBrandObject);
            mockDealershipRepo.Setup(x => x.ReadAll()).Returns(FakeDealershipObject);

            carl = new CarLogic(mockDealershipRepo.Object, mockBrandRepo.Object, mockCarRepo.Object);
            dealershipl = new DealershipLogic(mockDealershipRepo.Object, mockBrandRepo.Object, mockCarRepo.Object);
            brandl = new BrandLogic(mockBrandRepo.Object);
        }

        //CRUD

        [TestCase("TestModel", 100, 100, true)]
        [TestCase("TestModel", -100, 100, false)]
        [TestCase("", 100, 100, false)]
        public void CreateCarTest(string model, int horsepower, int price, bool result)
        {
            if (result)
            {
                Assert.That(() => { carl.Create(new Car() { Model = model, Horsepower = horsepower, Price = price }); }, Throws.Nothing);
            }
            else
            {
                Assert.That(() => { carl.Create(new Car() { Model = model, Horsepower = horsepower, Price = price }); }, Throws.Exception);
            }
        }

        [TestCase("TestName", "TestOwner", true)]
        [TestCase("TestName123", "TestOwner123", false)]
        [TestCase("", "", false)]
        public void CreateBrandTest(string name, string owner, bool result)
        {
            if (result)
            {
                Assert.That(() => { brandl.Create(new Brand() { Name = name, Owner = owner }); }, Throws.Nothing);
            }
            else
            {
                Assert.That(() => { brandl.Create(new Brand() { Name = name, Owner = owner }); }, Throws.Exception);
            }
        }

        [TestCase("TestName", 100, true)]
        [TestCase("TestName123", 100, false)]
        [TestCase("", 100, false)]
        public void CreateDealershipTest(string name, int employees, bool result)
        {
            if (result)
            {
                Assert.That(() => { dealershipl.Create(new Dealership() { Name = name, Employees = employees }); }, Throws.Nothing);
            }
            else
            {
                Assert.That(() => { dealershipl.Create(new Dealership() { Name = name, Employees = employees }); }, Throws.Exception);
            }
        }

        [TestCase(20)]
        [TestCase(50)]
        [TestCase(100)]
        public void GetOneCar_ThrowsException_WhenIdIsTooBig(int idx)
        {
            Assert.That(() => carl.Read(idx), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void GetOneCar_ReturnsCorrectInstance()
        {
            Assert.That(carl.Read(1).Model, Is.EqualTo("Impreza WRX STI"));
        }

        [Test]
        public void GetAllCar_ReturnsExactNumberOfInstances()
        {
            Assert.That(carl.ReadAll().Count, Is.EqualTo(12));
        }

        //non-CRUD

        [Test]
        public void GetDealershipWhereCarModelIsCharger_ReturnsCorrectInstance()
        {
            Assert.That(dealershipl.GetDealershipWhereCarModelIsCharger().First().Name, Is.EqualTo("American car dealership"));
        }

        [Test]
        public void GetCarWhereMoreThan18Employees_ReturnsCorrectInstance()
        {
            Assert.That(carl.GetCarWhereMoreThan18Employees().Count(), Is.EqualTo(4));
        }

        [Test]
        public void GetCarWhereBrandOwnerIsBMWGroup_ReturnsCorrectInstance()
        {
            Assert.That(carl.GetCarWhereBrandOwnerIsBMWGroup().Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetDealershipWhereCar313hp_ReturnsCorrectInstance()
        {
            Assert.That(dealershipl.GetDealershipWhereCar313hp().First().Name, Is.EqualTo("Japanese car dealership"));
        }

        [Test]
        public void GetDealershipWherePriceIs209700_ReturnsCorrectInstance()
        {
            Assert.That(dealershipl.GetDealershipWherePriceIs209700().First().Name, Is.EqualTo("German car dealership"));
        }

        private IQueryable<Car> FakeCarObject()
        {
            Dealership dealership1 = new Dealership() { Id = 1, Name = "Japanese car dealership", Employees = 16 };
            Dealership dealership2 = new Dealership() { Id = 2, Name = "German car dealership", Employees = 20 };
            Dealership dealership3 = new Dealership() { Id = 3, Name = "American car dealership", Employees = 14 };

            dealership1.Brands = new List<Brand>();
            dealership2.Brands = new List<Brand>();
            dealership3.Brands = new List<Brand>();



            Brand brand1 = new Brand() { Id = 1, Name = "Subaru", Owner = "Subaru Corp.", Dealership_Id = 1 };
            Brand brand2 = new Brand() { Id = 2, Name = "Nissan", Owner = "Renault-Nissan-Mitsubishi Alliance", Dealership_Id = 1 };
            Brand brand3 = new Brand() { Id = 3, Name = "Audi", Owner = "Volkswagen Group", Dealership_Id = 2 };
            Brand brand4 = new Brand() { Id = 4, Name = "BMW", Owner = "BMW Group", Dealership_Id = 2 };
            Brand brand5 = new Brand() { Id = 5, Name = "Dodge", Owner = "Stellantis", Dealership_Id = 3 };
            Brand brand6 = new Brand() { Id = 6, Name = "Ford", Owner = "Ford Motor Co.", Dealership_Id = 3 };

            brand1.Dealership = dealership1;
            brand2.Dealership = dealership1;
            brand3.Dealership = dealership2;
            brand4.Dealership = dealership2;
            brand5.Dealership = dealership3;
            brand6.Dealership = dealership3;

            brand1.Dealership_Id = dealership1.Id; dealership1.Brands.Add(brand1);
            brand1.Dealership_Id = dealership1.Id; dealership1.Brands.Add(brand2);
            brand1.Dealership_Id = dealership2.Id; dealership2.Brands.Add(brand3);
            brand1.Dealership_Id = dealership2.Id; dealership2.Brands.Add(brand4);
            brand1.Dealership_Id = dealership3.Id; dealership3.Brands.Add(brand5);
            brand1.Dealership_Id = dealership3.Id; dealership3.Brands.Add(brand6);

            brand1.Cars = new List<Car>();
            brand2.Cars = new List<Car>();
            brand3.Cars = new List<Car>();
            brand4.Cars = new List<Car>();
            brand5.Cars = new List<Car>();
            brand6.Cars = new List<Car>();



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

            car1.Brand = brand1;
            car2.Brand = brand1;
            car3.Brand = brand2;
            car4.Brand = brand2;
            car5.Brand = brand3;
            car6.Brand = brand3;
            car7.Brand = brand4;
            car8.Brand = brand4;
            car9.Brand = brand5;
            car10.Brand = brand5;
            car11.Brand = brand6;
            car12.Brand = brand6;

            car1.Brand_Id = brand1.Id; brand1.Cars.Add(car1);
            car2.Brand_Id = brand1.Id; brand1.Cars.Add(car2);
            car3.Brand_Id = brand2.Id; brand2.Cars.Add(car3);
            car4.Brand_Id = brand2.Id; brand2.Cars.Add(car4);
            car5.Brand_Id = brand3.Id; brand3.Cars.Add(car5);
            car6.Brand_Id = brand3.Id; brand3.Cars.Add(car6);
            car7.Brand_Id = brand4.Id; brand4.Cars.Add(car7);
            car8.Brand_Id = brand4.Id; brand4.Cars.Add(car8);
            car9.Brand_Id = brand5.Id; brand5.Cars.Add(car9);
            car10.Brand_Id = brand5.Id; brand5.Cars.Add(car10);
            car11.Brand_Id = brand6.Id; brand6.Cars.Add(car11);
            car12.Brand_Id = brand6.Id; brand6.Cars.Add(car12);



            List<Car> car = new List<Car>();
            car.Add(car1);
            car.Add(car2);
            car.Add(car3);
            car.Add(car4);
            car.Add(car5);
            car.Add(car6);
            car.Add(car7);
            car.Add(car8);
            car.Add(car9);
            car.Add(car10);
            car.Add(car11);
            car.Add(car12);

            return car.AsQueryable();
        }

        private IQueryable<Brand> FakeBrandObject()
        {
            Dealership dealership1 = new Dealership() { Id = 1, Name = "Japanese car dealership", Employees = 16 };
            Dealership dealership2 = new Dealership() { Id = 2, Name = "German car dealership", Employees = 20 };
            Dealership dealership3 = new Dealership() { Id = 3, Name = "American car dealership", Employees = 14 };

            dealership1.Brands = new List<Brand>();
            dealership2.Brands = new List<Brand>();
            dealership3.Brands = new List<Brand>();



            Brand brand1 = new Brand() { Id = 1, Name = "Subaru", Owner = "Subaru Corp.", Dealership_Id = 1 };
            Brand brand2 = new Brand() { Id = 2, Name = "Nissan", Owner = "Renault-Nissan-Mitsubishi Alliance", Dealership_Id = 1 };
            Brand brand3 = new Brand() { Id = 3, Name = "Audi", Owner = "Volkswagen Group", Dealership_Id = 2 };
            Brand brand4 = new Brand() { Id = 4, Name = "BMW", Owner = "BMW Group", Dealership_Id = 2 };
            Brand brand5 = new Brand() { Id = 5, Name = "Dodge", Owner = "Stellantis", Dealership_Id = 3 };
            Brand brand6 = new Brand() { Id = 6, Name = "Ford", Owner = "Ford Motor Co.", Dealership_Id = 3 };

            brand1.Dealership = dealership1;
            brand2.Dealership = dealership1;
            brand3.Dealership = dealership2;
            brand4.Dealership = dealership2;
            brand5.Dealership = dealership3;
            brand6.Dealership = dealership3;

            brand1.Dealership_Id = dealership1.Id; dealership1.Brands.Add(brand1);
            brand1.Dealership_Id = dealership1.Id; dealership1.Brands.Add(brand2);
            brand1.Dealership_Id = dealership2.Id; dealership2.Brands.Add(brand3);
            brand1.Dealership_Id = dealership2.Id; dealership2.Brands.Add(brand4);
            brand1.Dealership_Id = dealership3.Id; dealership3.Brands.Add(brand5);
            brand1.Dealership_Id = dealership3.Id; dealership3.Brands.Add(brand6);

            brand1.Cars = new List<Car>();
            brand2.Cars = new List<Car>();
            brand3.Cars = new List<Car>();
            brand4.Cars = new List<Car>();
            brand5.Cars = new List<Car>();
            brand6.Cars = new List<Car>();



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

            car1.Brand = brand1;
            car2.Brand = brand1;
            car3.Brand = brand2;
            car4.Brand = brand2;
            car5.Brand = brand3;
            car6.Brand = brand3;
            car7.Brand = brand4;
            car8.Brand = brand4;
            car9.Brand = brand5;
            car10.Brand = brand5;
            car11.Brand = brand6;
            car12.Brand = brand6;

            car1.Brand_Id = brand1.Id; brand1.Cars.Add(car1);
            car2.Brand_Id = brand1.Id; brand1.Cars.Add(car2);
            car3.Brand_Id = brand2.Id; brand2.Cars.Add(car3);
            car4.Brand_Id = brand2.Id; brand2.Cars.Add(car4);
            car5.Brand_Id = brand3.Id; brand3.Cars.Add(car5);
            car6.Brand_Id = brand3.Id; brand3.Cars.Add(car6);
            car7.Brand_Id = brand4.Id; brand4.Cars.Add(car7);
            car8.Brand_Id = brand4.Id; brand4.Cars.Add(car8);
            car9.Brand_Id = brand5.Id; brand5.Cars.Add(car9);
            car10.Brand_Id = brand5.Id; brand5.Cars.Add(car10);
            car11.Brand_Id = brand6.Id; brand6.Cars.Add(car11);
            car12.Brand_Id = brand6.Id; brand6.Cars.Add(car12);



            List<Brand> brand = new List<Brand>();
            brand.Add(brand1);
            brand.Add(brand2);
            brand.Add(brand3);
            brand.Add(brand4);
            brand.Add(brand5);
            brand.Add(brand6);
            
            return brand.AsQueryable();
        }

        private IQueryable<Dealership> FakeDealershipObject()
        {
            Dealership dealership1 = new Dealership() { Id = 1, Name = "Japanese car dealership", Employees = 16 };
            Dealership dealership2 = new Dealership() { Id = 2, Name = "German car dealership", Employees = 20 };
            Dealership dealership3 = new Dealership() { Id = 3, Name = "American car dealership", Employees = 14 };

            dealership1.Brands = new List<Brand>();
            dealership2.Brands = new List<Brand>();
            dealership3.Brands = new List<Brand>();



            Brand brand1 = new Brand() { Id = 1, Name = "Subaru", Owner = "Subaru Corp.", Dealership_Id = 1 };
            Brand brand2 = new Brand() { Id = 2, Name = "Nissan", Owner = "Renault-Nissan-Mitsubishi Alliance", Dealership_Id = 1 };
            Brand brand3 = new Brand() { Id = 3, Name = "Audi", Owner = "Volkswagen Group", Dealership_Id = 2 };
            Brand brand4 = new Brand() { Id = 4, Name = "BMW", Owner = "BMW Group", Dealership_Id = 2 };
            Brand brand5 = new Brand() { Id = 5, Name = "Dodge", Owner = "Stellantis", Dealership_Id = 3 };
            Brand brand6 = new Brand() { Id = 6, Name = "Ford", Owner = "Ford Motor Co.", Dealership_Id = 3 };

            brand1.Dealership = dealership1;
            brand2.Dealership = dealership1;
            brand3.Dealership = dealership2;
            brand4.Dealership = dealership2;
            brand5.Dealership = dealership3;
            brand6.Dealership = dealership3;

            brand1.Dealership_Id = dealership1.Id; dealership1.Brands.Add(brand1);
            brand1.Dealership_Id = dealership1.Id; dealership1.Brands.Add(brand2);
            brand1.Dealership_Id = dealership2.Id; dealership2.Brands.Add(brand3);
            brand1.Dealership_Id = dealership2.Id; dealership2.Brands.Add(brand4);
            brand1.Dealership_Id = dealership3.Id; dealership3.Brands.Add(brand5);
            brand1.Dealership_Id = dealership3.Id; dealership3.Brands.Add(brand6);

            brand1.Cars = new List<Car>();
            brand2.Cars = new List<Car>();
            brand3.Cars = new List<Car>();
            brand4.Cars = new List<Car>();
            brand5.Cars = new List<Car>();
            brand6.Cars = new List<Car>();



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

            car1.Brand = brand1;
            car2.Brand = brand1;
            car3.Brand = brand2;
            car4.Brand = brand2;
            car5.Brand = brand3;
            car6.Brand = brand3;
            car7.Brand = brand4;
            car8.Brand = brand4;
            car9.Brand = brand5;
            car10.Brand = brand5;
            car11.Brand = brand6;
            car12.Brand = brand6;

            car1.Brand_Id = brand1.Id; brand1.Cars.Add(car1);
            car2.Brand_Id = brand1.Id; brand1.Cars.Add(car2);
            car3.Brand_Id = brand2.Id; brand2.Cars.Add(car3);
            car4.Brand_Id = brand2.Id; brand2.Cars.Add(car4);
            car5.Brand_Id = brand3.Id; brand3.Cars.Add(car5);
            car6.Brand_Id = brand3.Id; brand3.Cars.Add(car6);
            car7.Brand_Id = brand4.Id; brand4.Cars.Add(car7);
            car8.Brand_Id = brand4.Id; brand4.Cars.Add(car8);
            car9.Brand_Id = brand5.Id; brand5.Cars.Add(car9);
            car10.Brand_Id = brand5.Id; brand5.Cars.Add(car10);
            car11.Brand_Id = brand6.Id; brand6.Cars.Add(car11);
            car12.Brand_Id = brand6.Id; brand6.Cars.Add(car12);



            List<Dealership> dealership = new List<Dealership>();
            dealership.Add(dealership1);
            dealership.Add(dealership2);
            dealership.Add(dealership3);

            return dealership.AsQueryable();
        }
    }
}
