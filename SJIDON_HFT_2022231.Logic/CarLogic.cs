using SJIDON_HFT_2022231.Models;
using SJIDON_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Logic
{
    public class CarLogic : ICarLogic
    {
        IRepository<Dealership> dealershipRepo;
        IRepository<Brand> brandRepo;
        IRepository<Car> carRepo;

        public CarLogic(IRepository<Car> carRepo)
        {
            this.carRepo = carRepo;
        }

        public void Create(Car obj)
        {
            if (obj.Model == "")
            {
                throw new ArgumentNullException("Model can't be null");
            }
            if (obj.Price < 0 || obj.Horsepower < 0)
            {
                throw new ArgumentException("Negative price and horsepower is not allowed");
            }
            carRepo.Create(obj);
        }

        public void Delete(int id)
        {
            carRepo.Delete(id);
        }

        public Car Read(int id)
        {
            if (id < carRepo.ReadAll().Count() + 1)
                return carRepo.Read(id);
            else
                throw new IndexOutOfRangeException("Id is to big!");
        }

        public IQueryable<Car> ReadAll()
        {
            return carRepo.ReadAll();
        }

        public void Update(Car obj)
        {
            carRepo.Update(obj);
        }

        public IEnumerable<Car> GetCarFromJapaneseDealership()
        {
            var q = from cars in carRepo.ReadAll()
                    join brands in brandRepo.ReadAll()
                    on cars.Brand_Id equals brands.Id
                    join dealerships in dealershipRepo.ReadAll()
                    on brands.Dealership_Id equals dealerships.Id
                    where dealerships.Name == "Japanese car dealership"
                    select cars;
            return q;
        }

        public IEnumerable<Car> GetCarWhereMoreThan18Employees()
        {
            var q = from cars in carRepo.ReadAll()
                    join brands in brandRepo.ReadAll()
                    on cars.Brand_Id equals brands.Id
                    join dealerships in dealershipRepo.ReadAll()
                    on brands.Dealership_Id equals dealerships.Id
                    where dealerships.Employees > 18
                    select cars;
            return q;
        }
        public IEnumerable<Car> GetCarWhereBrandOwnerIsBMWGroup()
        {
            var q = from cars in carRepo.ReadAll()
                    join brands in brandRepo.ReadAll()
                    on cars.Brand_Id equals brands.Id
                    where brands.Owner == "BMW Group"
                    select cars;
            return q;
        }
    }
}
