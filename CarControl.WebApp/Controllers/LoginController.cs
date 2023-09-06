using CarControl.Domain;
using CarControl.Domain.ViewModel;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarControl.WebApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult LoginUsuario()

        {
            return View();
        }

        public IActionResult RegistroDeUsuario()
        {
            return View();
        }

        public IActionResult ContaBloqueada()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUsuario(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.Verifique seu login e senha e tente novamente");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Senha, isPersistent: false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    user.LockoutEnabled = false;
                    return RedirectToAction("Index", "Home");
                }
                if (user.AccessFailedCount >= 4 || user.LockoutEnd >= DateTime.UtcNow)
                {
                    
                    if (user.LockoutEnd >= DateTime.UtcNow)
                    {
                        ModelState.AddModelError(string.Empty, "Conta bloqueada por excesso de tentativas");
                        return View(model);
                    }
                    

                    if (user.AccessFailedCount >= 4)
                    {
                        user.LockoutEnabled = true;
                        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));

                        ModelState.AddModelError(string.Empty, "Conta bloqueada por excesso de tentativas");
                        return View(model);
                    }
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida. Verifique seu login e senha e tente novamente");
                    return View(model);
                }


            }
            return View(model);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistroDeUsuario(RegistroDeUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.NmUsuario, Email = model.Email, PhoneNumber = model.NrTelefone };
                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {

                    return RedirectToAction("LoginUsuario", "Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RedefinirSenha(string email, string novaSenha)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, novaSenha);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    return RedirectToAction("LoginUsuario", "Login");
                }
                else
                {

                    foreach (var error in result.Errors)
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

    }
}
