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
    public class VeiculosController : Controller
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IMovimentoService _movimentoService;

        public VeiculosController(IVeiculoService veiculoService, IMovimentoService movimentoService)
        {
            _veiculoService = veiculoService;
            _movimentoService = movimentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoDTO>>> Get()
        {
            try
            {
                IEnumerable<VeiculoDTO> veiculos = await _veiculoService.ListarVeiculos();



                if (veiculos == null || !veiculos.Any())
                {
                    return NotFound("Veículos não encontrados.");
                }

                return Ok(veiculos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação");
            }

        }

        [HttpGet("{id:int}", Name = "GetVeiculo")]
        public ActionResult<VeiculoDTO> Get(int id)
        {
            try
            {
                VeiculoDTO veiculo = _veiculoService.ObterVeiculo(id);

                return veiculo == null ? (ActionResult<VeiculoDTO>)NotFound("Veículo não encontrado.") : (ActionResult<VeiculoDTO>)veiculo;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post(VeiculoDTO veiculoDTO)
        {
            try
            {
                if (veiculoDTO == null)
                {
                    return BadRequest();
                }

                await _veiculoService.InserirVeiculo(veiculoDTO);

                return new CreatedAtRouteResult("GetVeiculo", new { id = veiculoDTO.IdVeiculo }, veiculoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação");
            }


        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, VeiculoDTO veiculoDTO)
        {
            try
            {
                if (id != veiculoDTO.IdVeiculo)
                {
                    return BadRequest("Véículo não encontrado");
                }


                await _veiculoService.EditarVeiculo(veiculoDTO);

                return Ok(veiculoDTO);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool movimentacaoVeiculo = await _movimentoService.ConsultaSeTemMovimento(id);


                if (movimentacaoVeiculo)
                {
                    return BadRequest("O veículo possuí movimentações");
                }
                VeiculoDTO veiculoExcluido = await _veiculoService.ExcluirVeiculo(id);


                if (veiculoExcluido == null)
                {
                    return NotFound("O veículo não foi encontrado");
                }


                return Ok(veiculoExcluido);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }

    }
}
