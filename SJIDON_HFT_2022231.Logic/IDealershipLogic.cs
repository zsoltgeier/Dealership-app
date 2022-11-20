using SJIDON_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Logic
{
    public interface IDealershipLogic
    {
        //CRUD
        void Create(Dealership obj);
        Dealership Read(int id);
        IQueryable<Dealership> ReadAll();
        void Update(Dealership obj);
        void Delete(int id);

    }
}
