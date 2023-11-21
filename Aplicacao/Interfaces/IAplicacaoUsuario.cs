using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoUsuario
    {
        Task<bool> AdicionaUsuario(string nome, string email, string senha, string celular);
        Task<bool> ExisteUsuario(string email, string senha);

        Task<string> RetornaIdUsuario(string email);

        Task<string> RetornarTipoUsuario(string email);
    }
}
