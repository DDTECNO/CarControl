using AutoMapper;
using CarControl.Common.DTO.Autenticacao;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace CarControl.APIEstacionamentos.ApiEndpoints
{
    public static class LoginEndpoints
    {
        public static void MapLoginEndpoints(this WebApplication app)
        {

            app.MapPost("/registrar", [AllowAnonymous] async (RegistroDeUsuarioDTO registroDeUsuarioDTO, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMapper mapper) =>
            {
                IdentityUser identityUser = mapper.Map<IdentityUser>(registroDeUsuarioDTO);

                IdentityResult result = await userManager.CreateAsync(identityUser, registroDeUsuarioDTO.Senha);

                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Errors);
                }

                await signInManager.SignInAsync(identityUser, false);

                return Results.Ok();
            });

            app.MapPost("/Logins", [AllowAnonymous] async (LoginDTO loginDTO, IAutenticacao autenticacao, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) =>
            {

                IdentityUser? user = await userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    return Results.BadRequest("Tentativa de login inválida.Verifique seu login e senha e tente novamente");
                }

                var result = await signInManager.PasswordSignInAsync(user, loginDTO.Senha,
                    isPersistent: false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return Results.Ok(autenticacao.GeraToken(loginDTO));
                }

                if (user.AccessFailedCount >= 4 || user.LockoutEnd >= DateTime.UtcNow)
                {

                    if (user.LockoutEnd >= DateTime.UtcNow)
                    {
                        return Results.BadRequest("Conta bloqueada por excesso de tentativas");
                    }


                    if (user.AccessFailedCount >= 4)
                    {
                        user.LockoutEnabled = true;
                        await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));

                        return Results.BadRequest("Conta bloqueada por excesso de tentativas");
                    }

                }
                return Results.BadRequest("Tentativa de login inválida. Verifique seu login e senha e tente novamente");


            });

        }
    }
}
