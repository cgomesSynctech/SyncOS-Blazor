using info;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {

        private readonly APPDbContext _ctx;

        public MunicipiosController(APPDbContext context)
        {
            _ctx = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Municipio>>> Get()
        {
            try
            {
                return await _ctx.Municipios
                    .Include(i => i.UF)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.Erro(e.Message);
                return BadRequest();
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Municipio>> Get(int id)
        {
            try
            {
                return await _ctx.Municipios
                    .FindAsync(id);
            }
            catch (Exception e)
            {
                Logger.Erro(e.Message);
                return BadRequest();
            }
        }
    }
}
