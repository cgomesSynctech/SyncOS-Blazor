using System;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class DesenvCard
    {
        public int Id { get; set; }
        public int ChamadoModuloId { get; set; }
        public int DesenvCardStatusId { get; set; }
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
        public string Detalhes { get; set; }
        public string Solucao { get; set; }
        public string Observacao { get; set; }
        public string Resolucao { get; set; }
        public string VersaoSuapp { get; set; }
        public string LinkBase { get; set; }
        public string Arquivo { get; set; }
        public string Arquivosolucao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        public string Rotulo { get; set; }
        public int? ClienteId { get; set; }
        public string ClienteNome { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual DesenvCardStatus DesenvCardStatus { get; set; }
        public virtual ChamadoModulo ChamadoModulo { get; set; }

    }
}
