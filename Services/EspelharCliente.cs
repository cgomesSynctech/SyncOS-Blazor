using info;
using Modelos;
using Newtonsoft.Json;
using Services;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class EspelharCliente
    {
        private APPDbContext _ctx;
        private readonly Cliente cliente;

        public EspelharCliente(APPDbContext _ctx, Cliente cliente)
        {
            this.cliente = cliente;
            this._ctx = _ctx;
        }

        public async Task<bool> Salvar()
        {
            try
            {
                var json = JsonConvert.SerializeObject(cliente);
                var result = await new ConsumirWS().WSInsertUpdate(json);
                //var result = ConsumirWS.WSInsertUpdate(json);
                return true;
            }
            catch (Exception e)
            {
                Logger.Erro(e.Message);
                return false;
            }
        }


    }
}
