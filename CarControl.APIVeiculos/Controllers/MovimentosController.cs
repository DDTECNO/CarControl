using CarControl.Domain;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovimentosController : Controller
    {
        private readonly IMovimentoService _movimentoService;
        private readonly IVeiculoService _veiculoService;
        private readonly IVagaService _vagaService;

        public MovimentosController(IMovimentoService movimentoService, IVeiculoService veiculoService, IVagaService vagaService)
        {
            _movimentoService = movimentoService;
            _veiculoService = veiculoService;
            _vagaService = vagaService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Movimento>> Get() 
        {
            try
            {
                var movimentos = _movimentoService.ConsultaTodosMovimentos().ToList();

                if (movimentos.Count == 0)
                {
                    return NotFound("Nenhum movimento encontrado");
                }

                return movimentos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }


        }


        [HttpGet("{cpfCondutor}", Name = "GetRegitro")]
        public ActionResult<IEnumerable<Movimento>>Get(string cpfCondutor)
        {
            try
            {
                var movimento = _movimentoService.ConsultaMovimentoDoVeiculo(cpfCondutor).ToList();

                if (movimento.Count == 0)
                {
                    return NotFound("Movimento não encontrado para o condutor.");
                }
                return movimento;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }



        [HttpPost]   
        public  ActionResult Post(Movimento movimento)
        {
            try
            {
                if (movimento == null)
                {
                    return BadRequest();
                }

                if (!_vagaService.VagaEstaOcupada(movimento.IdVaga))
                {
                    return BadRequest("Esta vaga está ocupada");
                }

                var registroDeEntrada = _movimentoService.RegistrarEntrada(movimento);


                if (registroDeEntrada == null)
                {
                    return BadRequest("Já existe uma entrada sem registro para o veículo em questão. Registre sua saída.");
                }

                var cpfCondutor = _veiculoService.ObterVeiculos(movimento.IdVeiculo);


                return new CreatedAtRouteResult("GetRegitro", new { cpfCondutor = cpfCondutor.CpfCondutor }, movimento);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
                
            }
                      
        }

        [HttpPut ("{idVaga:int}")]
        public ActionResult Put(int idVaga, Movimento movimento) 
        {
            try
            {
                if (idVaga != movimento.IdVaga)
                {
                    return BadRequest();
                }

                var movimentoSaida = _movimentoService.RegistrarSaida(movimento);

                if (movimentoSaida == null)
                {
                    return BadRequest("A data e hora de saída não pode ser menor que a data e hora de entrada.");
                }
                return Ok(movimento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }

        [HttpDelete ("{id:int}")]
        public ActionResult Delete(int id) 
        {
            try
            {
                var movimentoExcluido = _movimentoService.ExcluirMovimento(id);

                if (movimentoExcluido == null)
                {
                    return NotFound("Movimento não encontrado.");
                }
                return Ok(movimentoExcluido);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }


        }
    }
}
