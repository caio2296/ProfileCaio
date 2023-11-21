using Entidades.Enums;

namespace WebApi.Models
{
    public class Registro
    {
        public string username { get; set; }
        = string.Empty;
        public string senha { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string celular { get; set; } = string.Empty;
        public TipoUsuario? tipo { get; set; }
        public string urlFoto { get; set; } = string.Empty;
        public IFormFile? foto { get; set; }
    }
}
