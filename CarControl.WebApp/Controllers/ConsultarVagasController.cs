using CarControl.Infrastructure.Repositories.Interface;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class ConsultarVagasController : Controller
    {

        #region REPOSITORY
        private readonly IVagaService _vagaService;

        public ConsultarVagasController(IVagaService vagaService)
        {
            _vagaService = vagaService;
        }


        #endregion REPOSITORY


        public ActionResult ConsultarVagas()
        {
            return View(_vagaService.ListaVaga());
        }

    
    }
}
