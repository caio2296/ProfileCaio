using Aplicacao.Interfaces.Generico;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoSolicitacoes : IGenericaAplicacao<Solicitacao>
    {
        Task AdicionaSolicitacao(Solicitacao solicitacao);
        Task AtualizaSolicitacao(Solicitacao solicitacao);
        Task<List<Solicitacao>> BuscarSolicitacoesRespondidas();
        Task<List<SolicitacaoModelView>> ListarSolicitacoesCustomizada();
    }
}
