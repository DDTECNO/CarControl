using CarControl.Domain;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VagasController : Controller
    {
        private readonly IVagaService _vagaService;

        public VagasController(IVagaService vagaService)
        {
            _vagaService = vagaService;
        }

        [HttpGet]   
        public ActionResult<IEnumerable<Vaga>> Get()
        {
            try
            {
                List<Vaga> vagas = _vagaService.ListaVaga().ToList();

                return vagas.Count == 0 ? (ActionResult<IEnumerable<Vaga>>)NotFound("Nenhuma vaga encontrada") : (ActionResult<IEnumerable<Vaga>>)vagas;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação");
            }


        }
    }
}
