using AutoMapper;
using CarControl.Common.DTO.Autenticacao;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAutenticacao _autenticacao;
        private readonly IMapper _mapper;

        public LoginsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAutenticacao autenticacao, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _autenticacao = autenticacao;
            _mapper = mapper;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<string>> RegistrarUsuario([FromBody] RegistroDeUsuarioDTO registroDeUsuarioDTO)
        {
            IdentityUser identityUser = _mapper.Map<IdentityUser>(registroDeUsuarioDTO);

            IdentityResult result = await _userManager.CreateAsync(identityUser, registroDeUsuarioDTO.Senha);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(identityUser, false);

            return Ok();

        }


        [HttpPost("login")]
        public async Task<ActionResult> LoginUsuario([FromBody] LoginDTO loginDTO)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return BadRequest("Tentativa de login inválida.Verifique seu login e senha e tente novamente");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Senha,
                isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Ok(_autenticacao.GeraToken(loginDTO));
            }

            if (user.AccessFailedCount >= 4 || user.LockoutEnd >= DateTime.UtcNow)
            {

                if (user.LockoutEnd >= DateTime.UtcNow)
                {
                    ModelState.AddModelError(string.Empty, "Conta bloqueada por excesso de tentativas");
                    return BadRequest(ModelState);
                }


                if (user.AccessFailedCount >= 4)
                {
                    user.LockoutEnabled = true;
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));

                    ModelState.AddModelError(string.Empty, "Conta bloqueada por excesso de tentativas");
                    return BadRequest(ModelState);
                }

            }
            ModelState.AddModelError(string.Empty, "Tentativa de login inválida. Verifique seu login e senha e tente novamente");
            return BadRequest(ModelState);


        }




    }
}
