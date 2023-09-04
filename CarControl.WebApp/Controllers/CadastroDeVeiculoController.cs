using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.WebApp.Controllers
{

    public class CadastroDeVeiculoController : Controller
    {

        #region REPOSITORY
        private readonly IVeiculoRepository _veiculoRepository;

        public CadastroDeVeiculoController(IVeiculoRepository veiculoRepository)
        {
            this._veiculoRepository = veiculoRepository;
        }
        #endregion REPOSITORY

        #region GET

        public ActionResult CadastroDeVeiculo()
        {
            return View();
        }

        public ActionResult VeiculosCadastrados()
        {
            return View(_veiculoRepository.ListaVeiculos());
        }


        public ActionResult EditarVeiculo(int id)
        {
            return View(_veiculoRepository.ObterVeiculos(id));
        }


        public ActionResult DetalhesDoVeiculo(int id)
        {
            return View(_veiculoRepository.ObterVeiculos(id));
        }

        public ActionResult Excluir(int id)
        {
            return View(_veiculoRepository.ObterVeiculos(id));
        }
        #endregion GET

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InserirCadastro(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _veiculoRepository.Create(veiculo);
            }
            return RedirectToAction("VeiculosCadastrados");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarVeiculoCadastrado(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _veiculoRepository.EditarVeiculo(veiculo);
            }
            return RedirectToAction("VeiculosCadastrados");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirVeiculo(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _veiculoRepository.ExcluirVeiculo(veiculo.IdVeiculo);
            } 
            
            return RedirectToAction("VeiculosCadastrados");
        }
        #endregion POST
    }
}
