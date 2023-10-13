using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Domain;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class MovimentosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovimentoService _movimentoService;
        private readonly IVeiculoService _veiculoService;
        private readonly IVagaService _vagaService;

        public MovimentosController(IMovimentoService movimentoService, IVeiculoService veiculoService, IVagaService vagaService, IMapper mapper)
        {
            _movimentoService = movimentoService;
            _veiculoService = veiculoService;
            _vagaService = vagaService;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<MovimentoDTO>> Get()
        {
            try
            {

                IEnumerable<Movimento> movimentos = _movimentoService.ConsultaTodosMovimentos().ToList();

                IEnumerable<MovimentoDTO> movimentosDTO = _mapper.Map<IEnumerable<MovimentoDTO>>(movimentos);


                return !movimentos.Any() ? (ActionResult<IEnumerable<MovimentoDTO>>)NotFound("Nenhum movimento encontrado") : Ok(movimentosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }


        }


        [HttpGet("{cpfCondutor}", Name = "GetRegitro")]
        public ActionResult<IEnumerable<MovimentoDTO>> Get(string cpfCondutor)
        {
            try
            {
                IEnumerable<Movimento> movimento = _movimentoService.ConsultaMovimentoDoVeiculo(cpfCondutor).ToList();

                IEnumerable<MovimentoDTO> movimentoDTO = _mapper.Map<IEnumerable<MovimentoDTO>>(movimento);

                return !movimento.Any() ? (ActionResult<IEnumerable<MovimentoDTO>>)NotFound("Movimento não encontrado para o condutor.") : Ok(movimentoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }

        }



        [HttpPost]
        public ActionResult Post(MovimentoDTO movimentoDTO)
        {
            try
            {
                if (movimentoDTO == null)
                {
                    return BadRequest();
                }

                if (!_vagaService.VagaEstaOcupada(movimentoDTO.IdVaga))
                {
                    return BadRequest("Esta vaga está ocupada");
                }


                MovimentoDTO registroDeEntrada = _movimentoService.RegistrarEntrada(movimentoDTO);


                if (registroDeEntrada == null)
                {
                    return BadRequest("Já existe uma entrada sem registro para o veículo em questão. Registre sua saída.");
                }

                Vaga atualizaFlVaga = _vagaService.AtualizaFLVaga(movimentoDTO.IdVaga);

                if (atualizaFlVaga == null)
                {
                    return BadRequest("Erro ao verificar flag de vaga");
                }

                VeiculoDTO cpfCondutor = _veiculoService.ObterVeiculo(movimentoDTO.IdVeiculo);



                return new CreatedAtRouteResult("GetRegitro", new { cpfCondutor = cpfCondutor.CpfCondutor }, movimentoDTO);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");

            }

        }

        [HttpPut("{idVaga:int}")]
        public ActionResult Put(int idVaga, MovimentoDTO movimentoDTO)
        {
            try
            {
                if (idVaga != movimentoDTO.IdVaga)
                {
                    return BadRequest("Vaga não encontrada");
                }

                MovimentoDTO movimentoSaida = _movimentoService.RegistrarSaida(movimentoDTO);

                if (movimentoSaida == null)
                {
                    return BadRequest("A data e hora de saída não pode ser menor que a data e hora de entrada.");
                }

                Vaga atualizaFlVaga = _vagaService.AtualizaFLVaga(movimentoDTO.IdVaga);

                if (atualizaFlVaga == null)
                {
                    return BadRequest("Erro ao verificar flag de vaga");
                }

                return Ok(movimentoDTO);
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
                Movimento movimentoExcluido = _movimentoService.ExcluirMovimento(id);

                MovimentoDTO saidaRegistrada = _mapper.Map<MovimentoDTO>(movimentoExcluido);

                return movimentoExcluido == null ? NotFound("Movimento não encontrado.") : Ok(saidaRegistrada);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação.");
            }


        }
    }
}
