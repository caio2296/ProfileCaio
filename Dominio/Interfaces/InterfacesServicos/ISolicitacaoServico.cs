using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfacesServicos
{
    public interface ISolicitacaoServico
    {
        Task AdicionarSolicitacao(Solicitacao solicitacao);

        Task AtualizarSolicitacao(Solicitacao solicitacao);
        Task<List<Solicitacao>> BuscarSolicitacoesRespondidas();
        Task<List<SolicitacaoModelView>> BuscarSolicitacoesCustomizada();

    }
}
