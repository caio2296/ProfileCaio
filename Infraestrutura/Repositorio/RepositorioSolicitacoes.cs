using Dominio.Interfaces;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Generico;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioSolicitacoes:RepositorioGenerico<Solicitacao>,ISolicitacao
    {
        private readonly DbContextOptions<Contexto> _optionsBuilder;
        public RepositorioSolicitacoes()
        {
            _optionsBuilder= new DbContextOptions<Contexto>();
        }

        public async Task<List<Solicitacao>> ListarSolicitacoes(Expression<Func<Solicitacao, bool>> exSolicitacao)
        {
            using (var banco = new Contexto(_optionsBuilder))
            {
                return await banco.Solicitacoes.Where(exSolicitacao).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Solicitacao>> ListarSolicitacoesCustomizada()
        {
            using (var banco = new Contexto(_optionsBuilder))
            {
                var listaSolicitacoes = await (from solicitacoes in banco.Solicitacoes
                                               select new Solicitacao 
                                               {
                                                 Id = solicitacoes.Id,
                                                 Nome= solicitacoes.Nome,
                                                 Email= solicitacoes.Email,
                                                 Descricao= solicitacoes.Descricao,
                                                 DataSolicitacao= solicitacoes.DataSolicitacao,
                                                 Respondida= solicitacoes.Respondida
                                                 
                                               }).AsNoTracking().ToListAsync();
                return listaSolicitacoes;
            }
        }
    }
}
