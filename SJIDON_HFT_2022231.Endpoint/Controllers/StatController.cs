using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SJIDON_HFT_2022231.Endpoint.Services;
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
        IHubContext<SignalRHub> hub;

        public StatController(ICarLogic cl, IDealershipLogic dl ,IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.dl = dl;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Car> GetCarWhereMoreThan18Employees()
        {
            return cl.GetCarWhereMoreThan18Employees();
            this.hub.Clients.All.SendAsync("GetCarWhereMoreThan18Employees", cl.GetCarWhereMoreThan18Employees());
        }


        [HttpGet]
        public IEnumerable<Car> GetCarWhereBrandOwnerIsBMWGroup()
        {
            return cl.GetCarWhereBrandOwnerIsBMWGroup();
            this.hub.Clients.All.SendAsync("GetCarWhereBrandOwnerIsBMWGroup", cl.GetCarWhereBrandOwnerIsBMWGroup());
        }

        [HttpGet]
        public IEnumerable<Dealership> GetDealershipWhereCar313hp()
        {
            return dl.GetDealershipWhereCar313hp();
            this.hub.Clients.All.SendAsync("GetDealershipWhereCar313hp", dl.GetDealershipWhereCar313hp());
        }

        [HttpGet]
        public IEnumerable<Dealership> GetDealershipWhereCarModelIsCharger()
        {
            return dl.GetDealershipWhereCarModelIsCharger();
            this.hub.Clients.All.SendAsync("GetDealershipWhereCarModelIsCharger", dl.GetDealershipWhereCarModelIsCharger());
        }

        [HttpGet]
        public IEnumerable<Dealership> GetDealershipWherePriceIs209700()
        {
            return dl.GetDealershipWherePriceIs209700();
            this.hub.Clients.All.SendAsync("GetDealershipWherePriceIs209700", dl.GetDealershipWherePriceIs209700());
        }
    }
}
