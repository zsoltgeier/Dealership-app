using Microsoft.EntityFrameworkCore;
using SJIDON_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Repository
{
    public class BrandRepository : Repository<Brand>
    {
        public BrandRepository(DbContext ctx) : base(ctx)
        {
        }
        public override Brand Read(int id)
        {
            return ReadAll().SingleOrDefault(x => x.Id == id);
        }
        public override void Update(Brand obj)
        {
            var oldBrand = Read(obj.Id);
            oldBrand.Id = obj.Id;
            oldBrand.Name = obj.Name;
            oldBrand.Owner = obj.Owner;
            oldBrand.Dealership_Id = obj.Dealership_Id;

            ctx.SaveChanges();
        }
        public override void Delete(int id)
        {
            ctx.Remove(Read(id));
            ctx.SaveChanges();
        }
    }
}
