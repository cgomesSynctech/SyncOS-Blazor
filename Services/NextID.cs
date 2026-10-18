using info;
using Modelos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public static class NextID
    {
        public static async Task<int> GetNextId(string tipo, APPDbContext _ctx)
        {
			try
			{
				var generator = new GeradorID();

                if (!_ctx.Generators.Any(a => a.Nome == tipo))
				{
					generator.Nome = tipo;
					generator.UltimoID = 0;
                    _ctx.Add(generator);
                    await _ctx.SaveChangesAsync();
                }
                generator = _ctx.Generators.FirstOrDefault(f => f.Nome == tipo);
                generator.UltimoID++;
                _ctx.Update(generator);
                await _ctx.SaveChangesAsync();
                return generator.UltimoID;

            }
			catch (Exception)
			{
                return -1;  
			}
        }
    }
}
