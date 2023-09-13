﻿using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class VagasController : Controller
    {

        #region DEPENDÊNCIAS
        private readonly IVagaService _vagaService;

        public VagasController(IVagaService vagaService)
        {
            _vagaService = vagaService;
        }


        #endregion DEPENDÊNCIAS

        #region GET
        public ActionResult ConsultarVagas()
        {
            return View(_vagaService.ListaVaga());
        }
        #endregion GET

    }
}