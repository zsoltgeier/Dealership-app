using Microsoft.EntityFrameworkCore;
using SJIDON_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Repository
{
    public class DealershipRepository : Repository<Dealership>
    {
        public DealershipRepository(DbContext ctx) : base(ctx)
        {
        }
        public override Dealership Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Update(Dealership obj)
        {
            var oldDealership = Read(obj.Id);
            oldDealership.Id = obj.Id;
            oldDealership.Name = obj.Name;
            oldDealership.Employees = obj.Employees;

            ctx.SaveChanges();
        }
        public override void Delete(int id)
        {
            ctx.Remove(Read(id));
            ctx.SaveChanges();
        }
    }
}
