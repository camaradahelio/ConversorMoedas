using Api.Autenticacao.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Api.Autenticacao.Controllers
{
    [ApiController]
    [Route("api/conta")]
    public class ContaController : ControllerBase
    {

        private readonly SignInManager<UsuarioModel> _signInManager;
        private readonly UserManager<UsuarioModel> _userManager;
        private readonly AppSettings _appSettings;

        public ContaController(SignInManager<UsuarioModel> signInManager,
                                UserManager<UsuarioModel> userManager,
                                IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        private IActionResult IsInvalidModelStateResponse(ModelStateDictionary modelState)
        {
            string mensagem = "Erro na passagem de parâmetros.";
            var detalhes = modelState.Values.SelectMany(e => e.Errors.Select(x => x.ErrorMessage)).ToArray();
            ValidacaoModel validacao = new ValidacaoModel(mensagem, detalhes);
            return BadRequest(validacao);
        }

        [HttpPost("registrar")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Registrar(RegistrarUsuarioModel registrarUsuarioModel)
        {
            if (!ModelState.IsValid)
            {
                return IsInvalidModelStateResponse(ModelState);
            }

            var user = new UsuarioModel
            {
                UserName = registrarUsuarioModel.Email,
                Email = registrarUsuarioModel.Email,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(user, registrarUsuarioModel.Senha);

            if (resultado.Succeeded)
            {
                return Ok(registrarUsuarioModel);
            }
            else
            {
                string mensagem = "Erro ao tentar criar usuário";
                var detalhes = resultado.Errors.Select(e => e.Description).ToArray();
                ValidacaoModel validacaoModel = new ValidacaoModel(mensagem, detalhes);
                return BadRequest(validacaoModel);
            }
        }

        [HttpPost("login")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return IsInvalidModelStateResponse(ModelState);
            }

            var resultado = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Senha, false, true);

            if (resultado.Succeeded)
            {
                return Ok(this.GerarJwtToken());
            }
            else
            {
                return Unauthorized();
            }
        }

        public string GerarJwtToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(_appSettings.Chave);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)                
            });

            var encodedToken = tokenHandler.WriteToken(token);  

            return encodedToken;    
        }
    }
}
