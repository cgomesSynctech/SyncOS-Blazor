using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Chamado
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int TecnicoId { get; set; }
        public int ChamadoStatusId { get; set; }
        public int ChamadoModuloId { get; set; }
        public int ChamadoMotivoId { get; set; }
        public int ChamadoFormaId { get; set; }
        public int ChamadoPrioridadeId { get; set; }

        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataAbertura { get; set; }
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataAtendimento { get; set; }
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataFinalizacao { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório. Favor informar a alegação.")]
        public string AlegacaoCliente { get; set; }
        public string AlegacaoTecnico { get; set; }
        public string Resolucao { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Campo Obrigatório. Favor informar quem solicitou.")]
        public string Interlocutor { get; set; }
        public bool IsSolucionado { get; set; }
        public bool IsImprocedente { get; set; }
        public string ProtocoloAutorizacao { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Tecnico { get; set; }
        public virtual ChamadoStatus Status { get; set; }
        public virtual ChamadoPrioridade Prioridade { get; set; }
        public virtual ChamadoMotivo Motivo { get; set; }
        public virtual ChamadoModulo Modulo { get; set; }
        public virtual ChamadoForma Forma { get; set; }
    }
}
