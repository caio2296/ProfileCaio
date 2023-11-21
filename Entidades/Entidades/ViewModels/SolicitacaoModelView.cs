using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades.ViewModels
{
    public class SolicitacaoModelView
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public string? Email { get; set; }
        public string? Descricao { get; set; }
        public string? DataSolicitacao { get; set; }
        public bool Respondida { get; set; }

    }
}
