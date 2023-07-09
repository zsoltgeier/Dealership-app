using Microsoft.AspNetCore.Mvc;
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

        public CarController(ICarLogic cl)
        {
            this.cl = cl;
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
        }


        [HttpPut]
        public void Update([FromBody] Car value)
        {
            cl.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cl.Delete(id);
        }
    }
}
