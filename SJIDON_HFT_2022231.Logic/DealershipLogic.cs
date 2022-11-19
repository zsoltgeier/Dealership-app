using SJIDON_HFT_2022231.Models;
using SJIDON_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Logic
{
    public class DealershipLogic : IDealershipLogic
    {
        IRepository<Dealership> dealershipRepo;

        public DealershipLogic(IRepository<Dealership> dealershipRepo)
        {
            this.dealershipRepo = dealershipRepo;
        }

        public void Create(Dealership obj)
        {
            if (obj.Name.Any(c => char.IsDigit(c)))
            {
                throw new ArgumentException("Name can't contain numbers");
            }
            if (obj.Name == "")
            {
                throw new ArgumentNullException("Name can't be null");
            }
            dealershipRepo.Create(obj);
        }

        public void Delete(int id)
        {
            dealershipRepo.Delete(id);
        }

        public Dealership Read(int id)
        {
            if (id < dealershipRepo.ReadAll().Count() + 1)
                return dealershipRepo.Read(id);
            else
                throw new IndexOutOfRangeException("Id is too big!");
        }

        public IQueryable<Dealership> ReadAll()
        {
            return dealershipRepo.ReadAll();
        }

        public void Update(Dealership obj)
        {
            dealershipRepo.Update(obj);
        }
    }
}
