using info;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UfsController : ControllerBase
    {

        private readonly APPDbContext _ctx;

        public UfsController(APPDbContext context)
        {
            _ctx = context;
        }

        [HttpGet]
        public async Task<List<UF>> Get()
        {
            return await _ctx.UFs
                    .AsNoTracking()
                    .ToListAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<UF> Get(string id)
        {
            return await _ctx.UFs.FindAsync(id);
        }

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
