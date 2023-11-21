﻿namespace Aplicacao.Interfaces.Generico
{
    public interface IGenericaAplicacao<T> where T : class
    {
        Task Adicionar(T Objeto);
        Task Atualizar(T Objeto);
        Task Excluir(T Objeto);
        Task<T> BuscarPorId(int id);
        Task<List<T>> Listar();
    }
}
