using CarControl.Domain;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<Veiculo>>> Get()
        {
            try
            {
               var veiculos = await _veiculoService.ListaVeiculos();

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
        public ActionResult<Veiculo> Get(int id)
        {
            try
            {
                Veiculo veiculo = _veiculoService.ObterVeiculos(id);

                return veiculo == null ? (ActionResult<Veiculo>)NotFound("Veículo não encontrado.") : (ActionResult<Veiculo>)veiculo;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }

        [HttpPost]
        public ActionResult Post(Veiculo veiculo)
        {
            try
            {
                if (veiculo == null)
                {
                    return BadRequest();
                }

                _veiculoService.Create(veiculo);

                return new CreatedAtRouteResult("GetVeiculo", new { id = veiculo.IdVeiculo }, veiculo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação");
            }


        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Veiculo veiculo)
        {
            try
            {
                if (id != veiculo.IdVeiculo)
                {
                    return BadRequest("Véículo não encontrado");
                }

                _veiculoService.EditarVeiculo(veiculo);

                return Ok(veiculo);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_movimentoService.ConsultaSeTemMovimento(id))
                {
                    return BadRequest("O veículo possuí movimentações");
                }
                Veiculo veiculoExcluido = _veiculoService.ExcluirVeiculo(id);
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
