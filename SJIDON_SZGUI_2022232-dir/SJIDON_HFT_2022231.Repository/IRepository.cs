using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJIDON_HFT_2022231.Repository
{
    public interface IRepository<T> where T : class
    {
        void Create(T obj);
        T Read(int id);
        IQueryable<T> ReadAll();
        void Update(T obj);
        void Delete(int id);
    }
}
