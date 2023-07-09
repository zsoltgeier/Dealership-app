using Microsoft.AspNetCore.Mvc;
using SJIDON_HFT_2022231.Logic;
using SJIDON_HFT_2022231.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SJIDON_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {

        ICarLogic cl;
        IDealershipLogic dl;

        public StatController(ICarLogic cl, IDealershipLogic dl)
        {
            this.cl = cl;
            this.dl = dl;
        }

        [HttpGet]
        public IEnumerable<Car> GetCarWhereMoreThan18Employees()
        {
            return cl.GetCarWhereMoreThan18Employees();
        }


        [HttpGet]
        public IEnumerable<Car> GetCarWhereBrandOwnerIsBMWGroup()
        {
            return cl.GetCarWhereBrandOwnerIsBMWGroup();
        }

        [HttpGet]
        public IEnumerable<Dealership> GetDealershipWhereCar313hp()
        {
            return dl.GetDealershipWhereCar313hp();
        }

        [HttpGet]
        public IEnumerable<Dealership> GetDealershipWhereCarModelIsCharger()
        {
            return dl.GetDealershipWhereCarModelIsCharger();
        }

        [HttpGet]
        public IEnumerable<Dealership> GetDealershipWherePriceIs209700()
        {
            return dl.GetDealershipWherePriceIs209700();
        }
    }
}
