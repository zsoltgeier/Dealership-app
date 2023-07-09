using Microsoft.AspNetCore.Mvc;
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

        public BrandController(IBrandLogic bl)
        {
            this.bl = bl;
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
        }


        [HttpPut]
        public void Update([FromBody] Brand value)
        {
            bl.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            bl.Delete(id);
        }
    }
}
