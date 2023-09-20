using CarControl.Domain;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class VeiculoController : Controller
    {

        #region DEPENDÊNCIAS
        private readonly IVeiculoService _veiculoService;
        private readonly IMovimentoService _movimentoService;

        public VeiculoController(IVeiculoService veiculoService, IMovimentoService movimentoService)
        {
            _veiculoService = veiculoService;
            _movimentoService = movimentoService;
        }


        #endregion DEPENDÊNCIAS

        #region GET

        public ActionResult CadastroDeVeiculo()
        {
            return View();
        }

        public async Task<ActionResult> VeiculosCadastrados()
        {
            return View(await _veiculoService.ListaVeiculos());
        }


        public ActionResult EditarVeiculo(int id)
        {
            Veiculo viewEditar = _veiculoService.ObterVeiculos(id) ?? throw new ArgumentException("Veículo não encontrado");
            return View(viewEditar);
        }


        public ActionResult DetalhesDoVeiculo(int id)
        {
            Veiculo viewDetalhes = _veiculoService.ObterVeiculos(id) ?? throw new ArgumentException("Veículo não encontrado");

            return View(viewDetalhes);
        }

        public ActionResult Excluir(int id)
        {
            Veiculo viewExcluir = _veiculoService.ObterVeiculos(id) ?? throw new ArgumentException("Veículo não encontrado");

            return View(viewExcluir);

        }
        #endregion GET

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InserirCadastro(Veiculo veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _veiculoService.Create(veiculo);
                }
                return RedirectToAction("VeiculosCadastrados");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarVeiculoCadastrado(Veiculo veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Veiculo editar = _veiculoService.EditarVeiculo(veiculo) ?? throw new ArgumentException("Veículo não encontrado");
                }
                return RedirectToAction("VeiculosCadastrados");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirVeiculo(int idVeiculo)
        {
            try
            {
                if (_movimentoService.ConsultaSeTemMovimento(idVeiculo))
                {
                    return Json(new { success = false });
                }

                Veiculo veiculoExcluido = _veiculoService.ExcluirVeiculo(idVeiculo);
                if (veiculoExcluido == null)
                {
                    return Json(new { success = false });
                }


                return Json(new { success = true, redirectUrl = Url.Action("VeiculosCadastrados") });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }

        }
        #endregion POST
    }
}
