using Microsoft.AspNetCore.Mvc;
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

        public DealershipController(IDealershipLogic dl)
        {
            this.dl = dl;
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
        }

        
        [HttpPut]
        public void Update([FromBody] Dealership value)
        {
            dl.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dl.Delete(id);
        }
    }
}
