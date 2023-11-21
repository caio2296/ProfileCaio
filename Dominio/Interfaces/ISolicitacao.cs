using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ISolicitacao : IGenerico<Solicitacao>
    {
        Task<List<Solicitacao>> ListarSolicitacoes(Expression<Func<Solicitacao, bool>> exSolicitacao);
        Task<List<Solicitacao>> ListarSolicitacoesCustomizada();
    }
}
