using System.Collections.Generic;

namespace Modelos
{
    public class VMClienteProsp
    {
        public int SetorId { get; set; }
        public string SetorDescricao { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public List<ClienteProsp> ClientesPaginados { get; set; }
    }
}
