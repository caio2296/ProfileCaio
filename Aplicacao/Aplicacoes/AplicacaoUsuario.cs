using Aplicacao.Interfaces;
using Dominio.Interfaces;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoUsuario : IAplicacaoUsuario
    {
        private IUsuario _IUsuario;
        public AplicacaoUsuario(IUsuario iusuario)
        {
            _IUsuario = iusuario;
        }
        public async Task<bool> AdicionaUsuario(string nome, string email, string senha, string celular)
        {
            return await _IUsuario.AdicionarUsuario(nome,email,senha,celular);
        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            return await _IUsuario.ExisteUsuario(email, senha);
        }

        public async Task<string> RetornaIdUsuario(string email)
        {
            return await _IUsuario.RetornarIdUsuario(email);
        }

        public async Task<string> RetornarTipoUsuario(string email)
        {
            return await _IUsuario.RetornarTipoUsuario(email);
        }
    }
}
