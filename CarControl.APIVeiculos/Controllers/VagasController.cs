using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.APIVeiculos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VagasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVagaService _vagaService;

        public VagasController(IVagaService vagaService, IMapper mapper)
        {
            _vagaService = vagaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VagaDTO>>> Get()
        {
            try
            {
                IEnumerable<VagaDTO> vagas = await _vagaService.ListaVaga();

                var vagasDTO = _mapper.Map<IEnumerable<VagaDTO>>(vagas);

                return !vagas.Any() ? (ActionResult<IEnumerable<VagaDTO>>)NotFound("Nenhuma vaga encontrada") : Ok(vagasDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solictação");
            }


        }
    }
}
