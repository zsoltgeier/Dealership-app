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
    }
}
