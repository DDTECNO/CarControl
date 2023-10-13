using CarControl.Common.DTO.Autenticacao;
using CarControl.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarControl.Service.Autenticacao
{
    public class Autenticacao : IAutenticacao
    {
        private readonly IConfiguration _configuration;

        public Autenticacao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gera o token para autenticação nas API's.
        /// </summary>
        /// <param name="registroDeUsuarioDTO"></param>
        /// <returns></returns>
        public object GeraToken(LoginDTO registroDeUsuarioDTO)
        {
            //Define as declarações do usuário (não obrigatório)
            Claim[] claimsUser = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, registroDeUsuarioDTO.Email),
                new Claim("system","CarControl"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            string chave = _configuration["Jwt:Key"];
            //Gera uma chave com base no algoritmo simétrico, passando a chave de segurança definida na appsettings.json
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
            SigningCredentials crendenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiração do Token 
            string expiration = _configuration["TokenConfiguration:ExpireHours"];
            DateTime addExpiration = DateTime.UtcNow.AddMinutes(double.Parse(expiration));

            //Gerando um token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claimsUser,
                expires: addExpiration,
                signingCredentials: crendenciais

                );

            //retornando o token (serializado) junto com as informações de autenticação
            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = addExpiration,
                Message = "Token JWT Ok"

            };


        }
    }
}
