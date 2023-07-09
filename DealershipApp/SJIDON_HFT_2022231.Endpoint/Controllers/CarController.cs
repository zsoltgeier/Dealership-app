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
    public class CarController : ControllerBase
    {
        ICarLogic cl;
        IHubContext<SignalRHub> hub;


        public CarController(ICarLogic cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Car> ReadAll()
        {
            return cl.ReadAll();
        }


        [HttpGet("{id}")]
        public Car Read(int id)
        {
            return cl.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Car value)
        {
            cl.Create(value);
            this.hub.Clients.All.SendAsync("CarCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Car value)
        {
            cl.Update(value);
            this.hub.Clients.All.SendAsync("CarUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var carToDelete = this.cl.Read(id);
            cl.Delete(id);
            this.hub.Clients.All.SendAsync("CarDeleted", carToDelete);
        }
    }
}
