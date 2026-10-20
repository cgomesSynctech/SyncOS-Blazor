using Modelos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class DesenvCardHistorico
    {
        public int Id { get; set; }
        public int DesenvCardId { get; set; }
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
        public string UsuarioResposta { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual DesenvCardStatus DesenvCardStatus { get; set; }
        public virtual DesenvCard DesenvCard { get; set; }

    }
}
