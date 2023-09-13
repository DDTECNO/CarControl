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
          var vagas =  _vagaService.ListaVaga().ToList();
            
            if (vagas == null) 
            { 
                return NotFound("Nenhuma vaga encontrada");
            }
            return vagas;

        }
    }
}
