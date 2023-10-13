using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class VagasController : Controller
    {
        private readonly IVagaService _vagaService;

        public VagasController(IVagaService vagaService)
        {
            _vagaService = vagaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VagaDTO>>> Get()
        {
            try
            {
                IEnumerable<VagaDTO> vagas = await _vagaService.ListaVaga();

                return !vagas.Any() ? (ActionResult<IEnumerable<VagaDTO>>)NotFound("Nenhuma vaga encontrada") : Ok(vagas);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação");
            }


        }
    }
}
