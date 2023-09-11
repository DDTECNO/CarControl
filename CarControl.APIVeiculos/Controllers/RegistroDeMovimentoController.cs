using CarControl.Domain;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistroDeMovimentoController : Controller
    {
        private readonly IMovimentoService _movimentoService;
        private readonly IVeiculoService _veiculoService;

        public RegistroDeMovimentoController(IMovimentoService movimentoService, IVeiculoService veiculoService)
        {
            _movimentoService = movimentoService;
            _veiculoService = veiculoService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Movimento>> Get() 
        {

            var movimentos = _movimentoService.ConsultaTodosMovimentos().ToList();

            if (movimentos == null)
            {
                return NotFound("Nenhum movimento encontrado");
            }

            return movimentos;   

        }


        [HttpGet("{cpfCondutor}", Name = "GetRegitro")]
        public ActionResult<Movimento> Get(string cpfCondutor)
        {

            var movimento = _movimentoService.ConsultaMovimentoDoVeiculo(cpfCondutor);

            if(movimento == null) 
            {
                return NotFound("Movimento não encontrado");         
            }
            return movimento;

        }



        [HttpPost]   
        public  ActionResult PostEntrada(Movimento movimento)
        {
            if (movimento == null)
            {
                return BadRequest();
            }
            
            var registroDeEntrada = _movimentoService.RegistrarEntrada(movimento);


            if (registroDeEntrada == null)
            {
                return BadRequest("Já existe uma entrada sem registro para o veículo em questão. Resgitre sua saída");
            }

            var cpfCondutor = _veiculoService.ObterVeiculos(movimento.IdVeiculo);
            

            return new CreatedAtRouteResult("GetRegitro", new { cpfCondutor = cpfCondutor.CpfCondutor}, movimento);
            
        }
    }
}
