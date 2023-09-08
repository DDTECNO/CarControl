using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VeiculosController : Controller
    {
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculosController(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Veiculo>> Get()
        {

            var veiculos = _veiculoRepository.ListaVeiculos().ToList();

            if (veiculos == null)
            {

                return NotFound("Produtos não encontrados...");

            }
            return veiculos;

        }

        [HttpGet("{id:int}", Name = "GetVeiculo")]
        public ActionResult<Veiculo> Get(int id)
        {
            var veiculo = _veiculoRepository.ObterVeiculos(id);

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

            _veiculoRepository.Create(veiculo);

            return new CreatedAtRouteResult("GetVeiculo", new { id = veiculo.IdVeiculo }, veiculo);

        }
    }
}
