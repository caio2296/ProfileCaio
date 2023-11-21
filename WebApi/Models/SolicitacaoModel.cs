namespace ProfileCaio.Models
{
    public class SolicitacaoModel
    {
        public int IdSolicitacao { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Respondida { get; set; }

    }
}
