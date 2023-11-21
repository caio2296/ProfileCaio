using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfacesServicos;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoSolicitacao : IAplicacaoSolicitacoes
    {
        private ISolicitacaoServico _ISolicitacaoServico;
        private ISolicitacao _ISolicitacao;

        public AplicacaoSolicitacao(ISolicitacaoServico iSolicitacaoServico, ISolicitacao iSolicitacao)
        {
            _ISolicitacaoServico = iSolicitacaoServico;
            _ISolicitacao = iSolicitacao;
        }

        public async Task Adicionar(Solicitacao Objeto)
        {
            await _ISolicitacao.Adicionar(Objeto);
        }

        public async Task AdicionaSolicitacao(Solicitacao solicitacao)
        {
            await _ISolicitacaoServico.AdicionarSolicitacao(solicitacao);
        }

        public async Task Atualizar(Solicitacao Objeto)
        {
           await _ISolicitacao.Atualizar(Objeto);
        }

        public async Task AtualizaSolicitacao(Solicitacao solicitacao)
        {
            await _ISolicitacaoServico.AtualizarSolicitacao(solicitacao);
        }

        public async Task<Solicitacao> BuscarPorId(int id)
        {
            return await _ISolicitacao.BuscarPorId(id);
        }

        public async Task Excluir(Solicitacao Objeto)
        {
            await _ISolicitacao.Excluir(Objeto);
        }

        public async Task<List<Solicitacao>> Listar()
        {
           return await _ISolicitacao.Listar();
        }

        public async Task<List<SolicitacaoModelView>> ListarSolicitacoesCustomizada()
        {
            return await _ISolicitacaoServico.BuscarSolicitacoesCustomizada();
        }

        public async Task<List<Solicitacao>> BuscarSolicitacoesRespondidas()
        {
            return await _ISolicitacaoServico.BuscarSolicitacoesRespondidas();
        }
    }
}
