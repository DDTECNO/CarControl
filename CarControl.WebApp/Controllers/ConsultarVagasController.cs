using CarControl.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.WebApp.Controllers
{
    public class ConsultarVagasController : Controller
    {

        #region REPOSITORY
        private readonly IVagaRepository _vagaRepository;

        public ConsultarVagasController(IVagaRepository vagaRepository)
        {
            this._vagaRepository = vagaRepository;
        }

        #endregion REPOSITORY


        public ActionResult ConsultarVagas()
        {
            return View(_vagaRepository.ListaVaga());
        }


    
    }
}
