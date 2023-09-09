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
        public ActionResult<IEnumerable<Veiculo>> Get()
        {

            var veiculos = _veiculoService.ListaVeiculos().ToList();

            if (veiculos == null)
            {

                return NotFound("Produtos não encontrados...");

            }
            return veiculos;

        }

        [HttpGet("{id:int}", Name = "GetVeiculo")]
        public ActionResult<Veiculo> Get(int id)
        {
            var veiculo = _veiculoService.ObterVeiculos(id);

            if (veiculo == null)
            {
                return NotFound("Produto não encontrado...");
            }
            return veiculo;
        }

        [HttpPost]
        public ActionResult Post(Veiculo veiculo)
        {

            if (veiculo == null)
                return BadRequest();

            _veiculoService.Create(veiculo);

            return new CreatedAtRouteResult("GetVeiculo", new { id = veiculo.IdVeiculo }, veiculo);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Veiculo veiculo)
        {
            if (id != veiculo.IdVeiculo)
            {
                return BadRequest();
            }

            _veiculoService.EditarVeiculo(veiculo);

            return Ok(veiculo);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            //if (_movimentoService.ConsultaSeTemMovimento())
            //{

            //}
            var veiculoExcluido = _veiculoService.ExcluirVeiculo(id);
            if (veiculoExcluido == null)
            {
                return NotFound();
            }

            return Ok(veiculoExcluido);
        
        }

    }
}
