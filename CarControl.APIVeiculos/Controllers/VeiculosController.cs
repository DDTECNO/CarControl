using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VeiculosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVeiculoService _veiculoService;
        private readonly IMovimentoService _movimentoService;

        public VeiculosController(IVeiculoService veiculoService, IMovimentoService movimentoService, IMapper mapper)
        {
            _veiculoService = veiculoService;
            _movimentoService = movimentoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoDTO>>> Get()
        {
            try
            {
                IEnumerable<VeiculoDTO> veiculos = await _veiculoService.ListarVeiculos();


                IEnumerable<VeiculoDTO> veiculosDTO = _mapper.Map<IEnumerable<VeiculoDTO>>(veiculos);

                if (veiculos == null || !veiculos.Any())
                {
                    return NotFound("Veículos não encontrados.");
                }

                return Ok(veiculosDTO);
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

                VeiculoDTO veiculosDTO = _mapper.Map<VeiculoDTO>(veiculo);
                return veiculosDTO == null ? (ActionResult<VeiculoDTO>)NotFound("Veículo não encontrado.") : (ActionResult<VeiculoDTO>)veiculosDTO;
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

                VeiculoDTO veiculoExcluidoDTO = _mapper.Map<VeiculoDTO>(veiculoExcluido);

                return Ok(veiculoExcluidoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }

    }
}
