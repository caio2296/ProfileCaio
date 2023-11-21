using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileCaio.Models;

namespace ProfileCaio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SolicitacaoController : ControllerBase
    {
        private readonly IAplicacaoSolicitacoes _aplicacaoSolicitacao;
        public SolicitacaoController(IAplicacaoSolicitacoes aplicacaoSolicitacao)
        {
            _aplicacaoSolicitacao = aplicacaoSolicitacao;
        }
        [Authorize]
        [HttpGet("/api/ListarSolicitacoesCustomizadas")]
        [Produces("application/json")]
        public async Task<List<SolicitacaoModelView>> ListarSolicitacoesCustomizadas()
        {
            return await _aplicacaoSolicitacao.ListarSolicitacoesCustomizada();
        }
        [Authorize]
        [HttpPost("/api/ListarSolicitacoesRespondidas")]
        [Produces("application/json")]
        public async Task<List<Solicitacao>> ListarSolicitacoesRespondidas()
        {
            return await _aplicacaoSolicitacao.BuscarSolicitacoesRespondidas();
        }
        [AllowAnonymous]
        [HttpPost("/api/AdicionarSolicitacao")]
        [Produces("application/json")]
        public async Task<List<Notifica>> AdicionarSolicitacao(SolicitacaoModel solicitacao)
        {
            var novaSolicitacao = new Solicitacao();
            novaSolicitacao.Nome = solicitacao.Nome;
            novaSolicitacao.Email = solicitacao.Email;
            novaSolicitacao.Descricao = solicitacao.Descricao;
            novaSolicitacao.Respondida = false;
            await _aplicacaoSolicitacao.AdicionaSolicitacao(novaSolicitacao);
            return novaSolicitacao.Notificacoes;
        }
        [Authorize]
        [HttpPost("/api/AtualizarSolicitacao")]
        [Produces("application/json")]
        public async Task<List<Notifica>> AtualizarSolicitacao(SolicitacaoModel solicitacao)
        {
            var novaSolicitacao = await _aplicacaoSolicitacao.BuscarPorId(solicitacao.IdSolicitacao);
            novaSolicitacao.Nome= solicitacao.Nome;
            novaSolicitacao.Email= solicitacao.Email;
            novaSolicitacao.Descricao= solicitacao.Descricao;
            novaSolicitacao.Respondida= solicitacao.Respondida;
            await _aplicacaoSolicitacao.AtualizaSolicitacao(novaSolicitacao);
            return novaSolicitacao.Notificacoes;
        }
        [Authorize]
        [HttpPost("/api/ExcluirSolicitacao")]
        [Produces("application/json")]
        public async Task<List<Notifica>> ExcluirSolicitacao(SolicitacaoModel solicitacao)
        {
            var solicitacaoExcluir = await _aplicacaoSolicitacao.BuscarPorId(solicitacao.IdSolicitacao);
            await _aplicacaoSolicitacao.Excluir(solicitacaoExcluir);
            return solicitacaoExcluir.Notificacoes;
        }
        [Authorize]
        [HttpPost("/api/BuscarPorId")]
        [Produces("application/json")]
        public async Task<Solicitacao> BuscarPorId(SolicitacaoModel solicitacao)
        {
            return await _aplicacaoSolicitacao.BuscarPorId(solicitacao.IdSolicitacao);
        }
        
    }
}
