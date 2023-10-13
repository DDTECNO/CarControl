using AutoMapper;
using CarControl.Common.DTO.Autenticacao;
using CarControl.Common.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CarControl.WebApp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        #region  DEPENDÊNCIAS
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        # endregion DEPENDÊNCIAS

        #region GET
        public IActionResult LoginUsuario()

        {
            return View();
        }

        public IActionResult RegistroDeUsuario()
        {
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        #endregion GET

        #region POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUsuario(LoginViewModel model)
        {
            try
            {
                LoginDTO loginDTO = _mapper.Map<LoginDTO>(model); 

                IdentityUser user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.Verifique seu login e senha e tente novamente");
                    return View(model);
                }

                SignInResult result = await _signInManager.PasswordSignInAsync(user, loginDTO.Senha, isPersistent: false, lockoutOnFailure: true);

                LoginViewModel loginViewModel = _mapper.Map<LoginViewModel>(loginDTO);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (user.AccessFailedCount >= 4 || user.LockoutEnd >= DateTime.UtcNow)
                {

                    if (user.LockoutEnd >= DateTime.UtcNow)
                    {
                        ModelState.AddModelError(string.Empty, "Conta bloqueada por excesso de tentativas");
                        return View(loginViewModel);
                    }


                    if (user.AccessFailedCount >= 4)
                    {
                        user.LockoutEnabled = true;
                        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));

                        ModelState.AddModelError(string.Empty, "Conta bloqueada por excesso de tentativas");
                        return View(loginViewModel);
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida. Verifique seu login e senha e tente novamente");
                    return View(loginViewModel);
                }

                return View(loginViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistroDeUsuario(RegistroDeUsuarioViewModel model)
        {
            try
            {
                RegistroDeUsuarioDTO registroDeUsuarioDTO = _mapper.Map<RegistroDeUsuarioDTO>(model);

                IdentityUser user = _mapper.Map<IdentityUser>(registroDeUsuarioDTO);   

                IdentityResult result = await _userManager.CreateAsync(user, registroDeUsuarioDTO.Senha);

                if (result.Succeeded)
                {
                    return RedirectToAction("LoginUsuario", "Login");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                RegistroDeUsuarioViewModel registroDeUsuarioViewModel = _mapper.Map<RegistroDeUsuarioViewModel>(registroDeUsuarioDTO);

                return View(registroDeUsuarioViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RedefinirSenha(string email, string novaSenha)
        {
            try
            {
                IdentityUser user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, novaSenha);

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        return RedirectToAction("LoginUsuario", "Login");
                    }
                    else
                    {

                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View();
                    }
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Usuário não encontrado. Verifique o e-mail digitado e tente novamente");
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }

        }
        #endregion POST
    }
}
