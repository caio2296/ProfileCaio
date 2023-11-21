using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    [Table("TB_Solicitacao")]
    public class Solicitacao: Notifica
    {
        [Column("SLT_ID")]
        public int Id { get; set; }
        [Column("SLT_Nome")]
        public string? Nome { get; set; }
        [Column("SLT_Email")]
        public string? Email { get; set; }
        [Column("SLT_Descricao")]
        public string? Descricao { get; set; }
        [Column("SLT_DataSolicitacao")]
        public DateTime DataSolicitacao { get; set; }
        [Column("SLT_Respondida")]
        public bool Respondida { get; set; }
    }
}
