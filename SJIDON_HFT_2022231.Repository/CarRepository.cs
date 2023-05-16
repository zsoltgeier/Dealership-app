using Microsoft.EntityFrameworkCore;
using SJIDON_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Repository
{
    public class CarRepository : Repository<Car>
    {
        public CarRepository(DbContext ctx) : base(ctx)
        {
        }
        public override Car Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Update(Car obj)
        {
            var oldCar = Read(obj.Id);
            oldCar.Id = obj.Id;
            oldCar.Model = obj.Model;
            oldCar.Horsepower = obj.Horsepower;
            oldCar.Price = obj.Price;
            oldCar.Brand_Id = obj.Brand_Id;

            ctx.SaveChanges();
        }
        public override void Delete(int id)
        {
            ctx.Remove(Read(id));
            ctx.SaveChanges();
        }
    }
}
