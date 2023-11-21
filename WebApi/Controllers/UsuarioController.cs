using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Enums;
using Infraestrutura.Configuracoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Win32;
using ProfileCaio.Models;
using ProfileCaio.Token;
using System.Linq;
using System.Text;
using WebApi.Models;

namespace ProfileCaio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAplicacaoUsuario _aplicacaoUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsuarioController(IAplicacaoUsuario aplicacaoUsuario,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _aplicacaoUsuario = aplicacaoUsuario;
            _signInManager = signInManager;
            _userManager= userManager;
        }

        
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet("/api/RegistroUsuarioAdm")]
        public async Task<IActionResult> RegistroUsuarioAdm()
        {
            if (_userManager.Users.Any())
            {
                return Unauthorized(); // Não execute se o banco de dados não estiver vazio
            }
            string caminhoFoto = RetornarCaminhoFotoPerfil();
            Registro registro =  new Registro{
                            username = "caiocesarjck@gmail.com",
                            email = "caiocesarjck@gmail.com",
                            senha = "zxcasd384!A",
                            celular = "(21)98437-0051",
                            tipo = TipoUsuario.Administrador,
                            urlFoto = @caminhoFoto
                        };
            var user = new ApplicationUser
            {
                UserName = registro.email,
                Email = registro.email,
                Celular = registro.celular,
                Tipo = registro.tipo,
                UrlFoto = registro.urlFoto
                
            };

            var resultado = await _userManager.CreateAsync(user, registro.senha);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            // Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);
            if (resultado2.Succeeded)
                return Ok("Administrador Criado");
            else
                return Ok("Erro ao confirmar o administrador");
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [Produces("application/json")]
        [HttpPost("/api/RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody]Registro registro)
        {
            if (string.IsNullOrWhiteSpace(registro.email) || string.IsNullOrWhiteSpace(registro.senha))
                return Ok("Falta alguns dados");

            var user = new ApplicationUser
            {
                UserName = registro.email,
                Email = registro.email,
                Celular = registro.celular,
                Tipo = TipoUsuario.Comum
            };

            var resultado = await _userManager.CreateAsync(user, registro.senha);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            // Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);
            if (resultado2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao confirmar usuários");
        }
        [AllowAnonymous]
        [HttpPost("/api/CriarToken")]
        [Produces("application/json")]
        public async Task<IActionResult> CriarToken([FromBody]Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Unauthorized();
            if (!_userManager.Users.Any())
            {
                return Unauthorized();
            }
            var user = await _userManager.FindByEmailAsync(login.email);
            if (user == null)
                return Unauthorized(); // O usuário não foi encontrado

            var resultado = await _signInManager.CheckPasswordSignInAsync(user, login.senha, lockoutOnFailure: false);
            if (!resultado.Succeeded)
                return Unauthorized(); // Autenticação falhou

            var idUsuario = await _aplicacaoUsuario.RetornaIdUsuario(login.email);
            var tipoUsuario = await _aplicacaoUsuario.RetornarTipoUsuario(login.email);

            var token = new TokenJwtBuilder()
                .AddSecurityKey(JwtSecurityKey.Creater("Secret_Key-12345678"))
                .AddSubject("Empresa - Caio")
                .AddIssuer("Securiry.Bearer")
                .AddAudience("Securiry.Bearer")
                .AddClaim("idUsuario", idUsuario)
                .AddTipoClaim(tipoUsuario)
                .AddExpiry(5)
                .Builder();

            return Ok(token.value); // Autenticação bem-sucedida, retorne o token
        }

        [AllowAnonymous]
        [HttpPost("/api/RegistrarUsuarioFoto")]

        public async Task<IActionResult> RegistrarUsuarioFoto([FromForm] Registro registro)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(registro.email) || string.IsNullOrWhiteSpace(registro.senha))
                    return Ok("Falta alguns dados");
                string nomeFoto = "";

                if (registro != null)
                {
                    // Processar o arquivo aqui (por exemplo, salvar no disco, banco de dados, etc.)
                    var filePath =
                    Path.Combine(RetornarCaminhoFoto(), registro.foto.FileName);

                    nomeFoto = registro.foto.FileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await registro.foto.CopyToAsync(stream);
                    }
                }


                var caminhoArquivo = Path.Combine(RetornarCaminhoFoto(), nomeFoto);

                var user = new ApplicationUser
                {
                    UserName = registro.email,
                    Email = registro.email,
                    Celular = registro.celular,
                    Tipo = TipoUsuario.Comum,
                    UrlFoto = caminhoArquivo
                };

                var resultado = await _userManager.CreateAsync(user, registro.senha);

                if (resultado.Errors.Any())
                {
                    return Ok(resultado.Errors);
                }

                // Geração de Confirmação caso precise
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                //retorno email
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var resultado2 = await _userManager.ConfirmEmailAsync(user, code);
                if (resultado2.Succeeded)
                    return Ok("Usuário Adicionado com Sucesso");
                else
                    return Ok("Erro ao confirmar usuários");

                // Retorne uma resposta de sucesso ou qualquer outra resposta necessária.
                return Ok("Upload de arquivo(s) bem-sucedido!");
            }
            catch (Exception ex)
            {
                // Trate erros aqui e retorne uma resposta de erro apropriada.
                return StatusCode(500, "Erro no servidor: " + ex.Message);
            }
        }

        private static string RetornarCaminhoFotoPerfil()
        {
            string diretorioAtual = Directory.GetCurrentDirectory();
            string diretorioProfile = Directory.GetParent(diretorioAtual).FullName;
            string diretorioInfra = Path.Combine(diretorioProfile, "Infraestrutura");
            string diretorioRepositorio = Path.Combine(diretorioInfra, "Repositorio");
            string diretorioImagem = Path.Combine(diretorioRepositorio, "ImagemPerfil");
            string nomeFoto = "PerfilCaio.jpg";

            string caminhoFoto = Path.Combine(diretorioImagem, nomeFoto);
            return caminhoFoto;
        }

        private static string RetornarCaminhoFoto()
        {
            string diretorioAtual = Directory.GetCurrentDirectory();
            string diretorioProfile = Directory.GetParent(diretorioAtual).FullName;
            string diretorioInfra = Path.Combine(diretorioProfile, "Infraestrutura");
            string diretorioRepositorio = Path.Combine(diretorioInfra, "Repositorio");
            string diretorioImagem = Path.Combine(diretorioRepositorio, "ImagemPerfil");

            return diretorioImagem;
        }
    }

}
