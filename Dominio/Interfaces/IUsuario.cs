﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IUsuario
    {
        Task<bool> AdicionarUsuario(string nome,string email, string senha,string celular);
        Task<bool> ExisteUsuario(string email, string senha);
        Task<string> RetornarIdUsuario(string email);

        Task<string> RetornarTipoUsuario(string email);
    }
}
