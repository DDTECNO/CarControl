using CarControl.Common.DTO;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CarControl.APIEstacionamentos.ApiEndpoints
{
    public static class VeiculoEndpoints
    {
        public static void MapVeiculosEndpoint(this WebApplication app)
        {
            app.MapPost("/Veiculos", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (VeiculoDTO veiculo, IVeiculoService vs) =>
            {

                await vs.InserirVeiculo(veiculo);

                return Results.Created($"/Veiculos/{veiculo.IdVeiculo}", veiculo);
            });

            app.MapGet("/Veiculos", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (IVeiculoService vs) => await vs.ListarVeiculos()).RequireAuthorization();

            app.MapGet("/Veiculos/{cpf}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (string cpf, IVeiculoService vs) => await vs.ObterVeiculoPorCPF(cpf)).RequireAuthorization();

            app.MapPut("/Veiculos/{Id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (int id, VeiculoDTO veiculoDTO, IVeiculoService vs) =>
            {

                if (veiculoDTO.IdVeiculo != id)
                {
                    return Results.BadRequest();
                }

                VeiculoDTO veiculoAtualizado = await vs.EditarVeiculo(veiculoDTO);

                return veiculoAtualizado is null ? Results.NotFound() : Results.Ok(veiculoAtualizado);

            }).RequireAuthorization();

            app.MapDelete("/Veiculos/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (int id, IVeiculoService vs, IMovimentoService ms) =>
            {
                bool movimentacaoVeiculo = await ms.ConsultaSeTemMovimento(id);
                if (movimentacaoVeiculo)
                {
                    return Results.BadRequest("O veículo possuí movimentações");
                }

                VeiculoDTO veiculoExcluido = await vs.ExcluirVeiculo(id);

                if (veiculoExcluido == null)
                {
                    return Results.NotFound("O veículo não foi encontrado");
                }

                return Results.NoContent();
            }).RequireAuthorization();


        }
    }
}
