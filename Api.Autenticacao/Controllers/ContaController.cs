using Api.Autenticacao.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Autenticacao.Controllers
{
    [ApiController]
    [Route("api/conta")]
    public class ContaController : ControllerBase
    {

        private readonly SignInManager<UsuarioModel> _signInManager;
        private readonly UserManager<UsuarioModel> _userManager;

        public ContaController(SignInManager<UsuarioModel> signInManager, UserManager<UsuarioModel> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("registrar")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Registrar(RegistrarUsuarioModel registrarUsuarioModel)
        {
            var user = new UsuarioModel
            {
                UserName = registrarUsuarioModel.Email,
                Email = registrarUsuarioModel.Email,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(user, registrarUsuarioModel.Senha);

            if(resultado.Succeeded)
            {
                return Ok(registrarUsuarioModel);
            }
            else
            {

            }

        }

        [HttpPost("login")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var resultado = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Senha, false, true);

            if(resultado.Succeeded)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
