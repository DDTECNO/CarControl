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
                List<Movimento> movimentos = _movimentoService.ConsultaTodosMovimentos().ToList();

                return movimentos.Count == 0 ? (ActionResult<IEnumerable<Movimento>>)NotFound("Nenhum movimento encontrado") : (ActionResult<IEnumerable<Movimento>>)movimentos;
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
                List<Movimento> movimento = _movimentoService.ConsultaMovimentoDoVeiculo(cpfCondutor).ToList();

                return movimento.Count == 0 ? (ActionResult<IEnumerable<Movimento>>)NotFound("Movimento não encontrado para o condutor.") : (ActionResult<IEnumerable<Movimento>>)movimento;
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

                Movimento registroDeEntrada = _movimentoService.RegistrarEntrada(movimento);


                if (registroDeEntrada == null)
                {
                    return BadRequest("Já existe uma entrada sem registro para o veículo em questão. Registre sua saída.");
                }

                Vaga atualizaFlVaga = _vagaService.AtualizaFLVaga(movimento.IdVaga);

                if (atualizaFlVaga == null)
                {
                    return BadRequest("Erro ao verificar flag de vaga");
                }

                Veiculo cpfCondutor = _veiculoService.ObterVeiculos(movimento.IdVeiculo);


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
                    return BadRequest("Vaga não encontrada");
                }

                Movimento movimentoSaida = _movimentoService.RegistrarSaida(movimento);

                if (movimentoSaida == null)
                {
                    return BadRequest("A data e hora de saída não pode ser menor que a data e hora de entrada.");
                }

                Vaga atualizaFlVaga = _vagaService.AtualizaFLVaga(movimento.IdVaga);

                if (atualizaFlVaga == null)
                {
                    return BadRequest("Erro ao verificar flag de vaga");
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
                Movimento movimentoExcluido = _movimentoService.ExcluirMovimento(id);

                return movimentoExcluido == null ? NotFound("Movimento não encontrado.") : Ok(movimentoExcluido);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }


        }
    }
}
