using CarControl.Domain;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class CadastroDeVeiculoController : Controller
    {

        #region DEPENDÊNCIAS
        private readonly IVeiculoService _veiculoService;
        private readonly IMovimentoService _movimentoService;

        public CadastroDeVeiculoController(IVeiculoService veiculoService, IMovimentoService movimentoService)
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

        public ActionResult VeiculosCadastrados()
        {
            return View(_veiculoService.ListaVeiculos());
        }


        public ActionResult EditarVeiculo(int id)
        {
            return View(_veiculoService.ObterVeiculos(id));
        }


        public ActionResult DetalhesDoVeiculo(int id)
        {
            return View(_veiculoService.ObterVeiculos(id));
        }

        public ActionResult Excluir(int id)
        {
            return View(_veiculoService.ObterVeiculos(id));
        }
        #endregion GET

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InserirCadastro(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _veiculoService.Create(veiculo);
            }
            return RedirectToAction("VeiculosCadastrados");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarVeiculoCadastrado(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _veiculoService.EditarVeiculo(veiculo);
            }
            return RedirectToAction("VeiculosCadastrados");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirVeiculo(Veiculo veiculo)
        {
            if (_movimentoService.ConsultaSeTemMovimento(veiculo))
            {
                ModelState.AddModelError(string.Empty, "O veículo possuí movimentações, sendo assim não pode ser excluído.");
                return View("Excluir");
            }

            _veiculoService.ExcluirVeiculo(veiculo.IdVeiculo);


            return RedirectToAction("VeiculosCadastrados");
        }
        #endregion POST
    }
}
