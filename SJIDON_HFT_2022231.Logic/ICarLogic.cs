using SJIDON_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Logic
{
    internal interface ICarLogic
    {
        //CRUD
        void Create(Car obj);
        Car Read(int id);
        IQueryable<Car> ReadAll();
        void Update(Car obj);
        void Delete(int id);
    }
}
