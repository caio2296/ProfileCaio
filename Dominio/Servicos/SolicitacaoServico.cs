using Dominio.Interfaces;
using Dominio.Interfaces.InterfacesServicos;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio.Servicos
{
    public class SolicitacaoServico : ISolicitacaoServico
    {
        private readonly ISolicitacao _ISolicitacao;
        public SolicitacaoServico(ISolicitacao iSolicitacao)
        {
            _ISolicitacao = iSolicitacao;
        }
        public async Task AdicionarSolicitacao(Solicitacao solicitacao)
        {
            var validarNome = solicitacao.ValidarPropriedadeString(solicitacao.Nome, "Nome");
            var validarEmail = solicitacao.ValidarPropriedadeString(solicitacao.Email, "Email");
            var validarDescricao = solicitacao.ValidarPropriedadeString(solicitacao.Descricao, "Descricao");
            if (validarNome && validarEmail && validarDescricao)
            {
                solicitacao.DataSolicitacao = DateTime.Now;
                await _ISolicitacao.Adicionar(solicitacao);
            }
        }

        public async Task AtualizarSolicitacao(Solicitacao solicitacao)
        {
            var validarNome = solicitacao.ValidarPropriedadeString(solicitacao.Nome, "Nome");
            var validarEmail = solicitacao.ValidarPropriedadeString(solicitacao.Email, "Email");
            var validarDescricao = solicitacao.ValidarPropriedadeString(solicitacao.Descricao, "Descricao");
            if (validarNome && validarEmail && validarDescricao)
            {
                solicitacao.DataSolicitacao = DateTime.Now;
                solicitacao.Respondida = true;
                await _ISolicitacao.Atualizar(solicitacao);
            }
        }
        public async Task<List<Solicitacao>> BuscarSolicitacoesRespondidas()
        {
            return await _ISolicitacao.ListarSolicitacoes(s => s.Respondida);
        }

        public async Task<List<SolicitacaoModelView>> BuscarSolicitacoesCustomizada()
        {
            var listarSolicitacoesCustomizada = await _ISolicitacao.ListarSolicitacoesCustomizada();

            var retorno = (
                from Solicicoes in listarSolicitacoesCustomizada
                select new SolicitacaoModelView
                {
                    Id = Solicicoes.Id,
                    Nome = Solicicoes.Nome,
                    Email=Solicicoes.Email,
                    Descricao = Solicicoes.Descricao,
                    DataSolicitacao =
                     string.Concat(Solicicoes.DataSolicitacao.Day, "/", Solicicoes.DataSolicitacao.Month, "/", Solicicoes.DataSolicitacao.Year),
                  
                }).ToList();
            return retorno;
        }
    }
}
