using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VeiculosController : Controller
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculosController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
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
    }
}
