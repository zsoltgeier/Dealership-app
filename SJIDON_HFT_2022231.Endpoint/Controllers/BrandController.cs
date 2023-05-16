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
    public class BrandController : ControllerBase
    {
        IBrandLogic bl;
        IHubContext<SignalRHub> hub;
        public BrandController(IBrandLogic bl, IHubContext<SignalRHub> hub)
        {
            this.bl = bl;
            this.hub = hub;
        }



        [HttpGet]
        public IEnumerable<Brand> ReadAll()
        {
            return bl.ReadAll();
        }


        [HttpGet("{id}")]
        public Brand Read(int id)
        {
            return bl.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Brand value)
        {
            bl.Create(value);
            this.hub.Clients.All.SendAsync("BrandCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Brand value)
        {
            bl.Update(value);
            this.hub.Clients.All.SendAsync("BrandUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var brandToDelete = this.bl.Read(id);
            bl.Delete(id);
            this.hub.Clients.All.SendAsync("BrandDeleted", brandToDelete);
            this.hub.Clients.All.SendAsync("CarDeleted", null);
        }
    }
}
