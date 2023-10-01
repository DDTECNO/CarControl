using AutoMapper;
using CarControl.Domain;
using CarControl.Domain.ViewModel;
using CarControl.Service.DTO;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class VagasController : Controller
    {

        #region DEPENDÊNCIAS
        private readonly IVagaService _vagaService;
        private readonly IMapper _mapper;

        public VagasController(IVagaService vagaService, IMapper mapper)
        {
            _vagaService = vagaService;
            _mapper = mapper;
        }


        #endregion DEPENDÊNCIAS

        #region GET
        public async Task<ActionResult> ConsultarVagas()
        {
          IEnumerable<VagaDTO> vagas = await _vagaService.ListaVaga();

          IEnumerable<VagaViewModel> vagaViewModel = _mapper.Map<IEnumerable<VagaViewModel>>(vagas);
 
          return View(vagaViewModel);
        }
        #endregion GET

    }
}
