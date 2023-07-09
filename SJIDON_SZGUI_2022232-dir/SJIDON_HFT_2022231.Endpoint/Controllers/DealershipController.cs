using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SJIDON_HFT_2022231.Endpoint.Services;
using SJIDON_HFT_2022231.Logic;
using SJIDON_HFT_2022231.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SJIDON_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DealershipController : ControllerBase
    {
        IDealershipLogic dl;
        IHubContext<SignalRHub> hub;

        public DealershipController(IDealershipLogic dl, IHubContext<SignalRHub> hub)
        {
            this.dl = dl;
            this.hub = hub;
        }


        
        [HttpGet]
        public IEnumerable<Dealership> ReadAll()
        {
            return dl.ReadAll();
        }

        
        [HttpGet("{id}")]
        public Dealership Read(int id)
        {
            return dl.Read(id);
        }

        
        [HttpPost]
        public void Create([FromBody] Dealership value)
        {
            dl.Create(value);
            this.hub.Clients.All.SendAsync("DealershipCreated", value);
        }

        
        [HttpPut]
        public void Update([FromBody] Dealership value)
        {
            dl.Update(value);
            this.hub.Clients.All.SendAsync("DealershipUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var dealershipToDelete = this.dl.Read(id);
            dl.Delete(id);
            this.hub.Clients.All.SendAsync("DealershipDeleted", dealershipToDelete);
            this.hub.Clients.All.SendAsync("BrandDeleted", null);
            this.hub.Clients.All.SendAsync("CarDeleted", null);
        }
    }
}
