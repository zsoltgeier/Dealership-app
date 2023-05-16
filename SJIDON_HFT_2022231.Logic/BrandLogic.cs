using SJIDON_HFT_2022231.Models;
using SJIDON_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Logic
{
    public class BrandLogic : IBrandLogic
    {
        IRepository<Brand> brandRepo;

        public BrandLogic(IRepository<Brand> brandRepo)
        {
            this.brandRepo = brandRepo;
        }

        public void Create(Brand obj)
        {
            if (obj.Name.Any(c => char.IsDigit(c)) || obj.Owner.Any(c => char.IsDigit(c)))
            {
                throw new ArgumentException("Name and owner can't contain numbers");
            }
            if (obj.Name == "" || obj.Owner == "")
            {
                throw new ArgumentNullException("Can't be null");
            }
            brandRepo.Create(obj);
        }

        public void Delete(int id)
        {
            brandRepo.Delete(id);
        }

        public Brand Read(int id)
        {
            if (id < brandRepo.ReadAll().Count() + 1)
                return brandRepo.Read(id);
            else
                throw new IndexOutOfRangeException("Id is too big!");
        }

        public IQueryable<Brand> ReadAll()
        {
            return brandRepo.ReadAll();
        }

        public void Update(Brand obj)
        {
            brandRepo.Update(obj);
        }
    }
}
