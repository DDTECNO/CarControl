using CarControl.Domain;
using CarControl.Domain.ViewModel;
using CarControl.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarControl.WebApp.Controllers
{
    public class RegistroDeMovimentoController : Controller
    {
        #region REPOSITORY
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IVagaRepository _vagaRepository;
        private readonly IOperacaoRepository _operacaoRepository;
        private readonly IMovimentoRepository _movimentoRepository; 


        public RegistroDeMovimentoController(IVeiculoRepository veiculoRepository, IVagaRepository vagaRepository, IOperacaoRepository operacaoRepository, IMovimentoRepository movimentoRepository)
        {
            this._veiculoRepository = veiculoRepository;
            this._vagaRepository = vagaRepository;
            this._operacaoRepository = operacaoRepository;
            this._movimentoRepository = movimentoRepository;
        }
        #endregion REPOSITORY

        #region GET 
        public ActionResult RegistroDeEntrada()
        {
            
            
            IList<Veiculo> veiculos = _veiculoRepository.ListaVeiculos();
            IList<Vaga> vagas = _vagaRepository.ListaVaga();
            IList<Operacao> operacoes = _operacaoRepository.ListaOperacao();



            var movimentoViewModel = new MovimentoViewModel()
            {
                Veiculos = veiculos,
                Vagas=vagas,    
                Operacoes=operacoes,    
            };
            
            
            return View(movimentoViewModel);
        }

   

        #endregion GET


        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarVeiculo(Veiculo veiculo)
        {
           return View( _veiculoRepository.obterVeiculoPorCPF(veiculo.CpfCondutor));
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistroDeEntradaDeVeiculo(MovimentoViewModel movimentoViewModel)
        {
            

         
            if (ModelState.IsValid)
            {
                //_movimentoRepository.RegistrarEntrada(movimento);
            }
            return RedirectToAction("VeiculosCadastrados");

        }


        #endregion POST
    }
}
