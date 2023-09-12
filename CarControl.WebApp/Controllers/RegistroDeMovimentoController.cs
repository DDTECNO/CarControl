using CarControl.Domain;
using CarControl.Domain.ViewModel;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class RegistroDeMovimentoController : Controller
    {
        #region DEPENDÊNCIAS
        private readonly IVeiculoService _veiculoService;
        private readonly IVagaService _vagaService;
        private readonly IOperacaoService _operacaoService;
        private readonly IMovimentoService _movimentoService;

        public RegistroDeMovimentoController(IVeiculoService veiculoService, IVagaService vagaService, IOperacaoService operacaoService, IMovimentoService movimentoService)
        {
            _veiculoService = veiculoService;
            _vagaService = vagaService;
            _operacaoService = operacaoService;
            _movimentoService = movimentoService;
        }


        #endregion DEPENDÊNCIAS

        #region GET 


        public ActionResult RegistroDeEntrada(int idVeiculo = 0, int idVaga = 0)
        {
            if (idVeiculo != 0)
            {
                Veiculo veiculo = _veiculoService.ObterVeiculos(idVeiculo);

                if (veiculo == null) { throw new ArgumentException("Veículo não encontrado"); }

                IEnumerable<Veiculo> vcls = new List<Veiculo>
                {
                    veiculo
                };

                IEnumerable<Vaga> vgs = _vagaService.ListaVaga();
                IEnumerable<Operacao> ops = _operacaoService.ListaOperacao();
                var mvViewModel1 = new MovimentoViewModel()
                {
                    Veiculos = vcls,
                    Vagas = vgs,
                    Operacoes = ops,

                };

                return View(mvViewModel1);

            }
            if (idVaga != 0) {

                Vaga vaga = _vagaService.ObterVaga(idVaga);

                if (vaga == null)
                {
                    throw new ArgumentException("Vaga não encontrada");               
                }

                IEnumerable<Vaga> vgs2 = new List<Vaga>
                {
                    vaga
                };

                IEnumerable<Veiculo> vcls2 = _veiculoService.ListaVeiculos();
                IEnumerable<Operacao> ops = _operacaoService.ListaOperacao();
                var mvViewModel2 = new MovimentoViewModel()
                {
                    Veiculos = vcls2,
                    Vagas = vgs2,
                    Operacoes = ops,

                };

                return View(mvViewModel2);

            }


            IEnumerable<Veiculo> veiculos = _veiculoService.ListaVeiculos();
            IEnumerable<Vaga> vagas = _vagaService.ListaVaga();
            IEnumerable<Operacao> operacoes = _operacaoService.ListaOperacao();

            var movimentoViewModel = new MovimentoViewModel()
            {
                Veiculos = veiculos,
                Vagas = vagas,
                Operacoes = operacoes,
            };



            return View(movimentoViewModel);
        }


        public ActionResult RegistroDeSaida(int idVeiculo = 0, int idVaga = 0)
        {

            if (idVeiculo != 0)
            {
                Veiculo veiculo = _veiculoService.ObterVeiculos(idVeiculo);

                IEnumerable<Veiculo> vcls = new List<Veiculo>
                {
                    veiculo
                };

                IEnumerable<Vaga> vgs = _vagaService.ListaVaga();
                var mvViewModel1 = new MovimentoViewModel()
                {
                    Veiculos = vcls,
                    Vagas = vgs,

                };

                return View(mvViewModel1);

            }
            if (idVaga != 0)
            {

                Vaga vaga = _vagaService.ObterVaga(idVaga);

                if (vaga == null)
                {
                    throw new ArgumentException("Vaga não encontrada");
                }

                IEnumerable<Vaga> vgs2 = new List<Vaga>
                {
                    vaga
                };

                IEnumerable<Veiculo> vcls2 = _veiculoService.ListaVeiculos();
                var mvViewModel2 = new MovimentoViewModel()
                {
                    Veiculos = vcls2,
                    Vagas = vgs2,

                };

                return View(mvViewModel2);

            }



            IEnumerable<Veiculo> veiculos = _veiculoService.ListaVeiculos();
            IEnumerable<Vaga> vagas = _vagaService.ListaVaga();

            var movimentoViewModel = new MovimentoViewModel()
            {
                Veiculos = veiculos,
                Vagas = vagas,
             
            };

            return View(movimentoViewModel);
        }


        #endregion GET

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarVeiculo(Veiculo veiculo)
        {
            var buscar = _veiculoService.ObterVeiculoPorCPF(veiculo.CpfCondutor);
            if (buscar == null)
            {
                throw new ArgumentException("Veículo não encontrado");
            }

            return View(buscar);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistroDeEntradaDeVeiculo(MovimentoViewModel movimentoViewModel)
        {


            Movimento movimento = new Movimento()
            {
                DtEntrada = movimentoViewModel.DtEntrada,
                HrEntrada = movimentoViewModel.HrEntrada,
                IdVeiculo = movimentoViewModel.IdVeiculo,
                IdVaga = movimentoViewModel.IdVaga,             
                IdTpOperacao = movimentoViewModel.IdOperacao
            };


            if (ModelState.IsValid)
            {
                if (!_vagaService.VagaEstaOcupada(movimento.IdVaga))
                {
                    ModelState.AddModelError(string.Empty, "Esta vaga está ocupada.");
                    TempData["ErrorMessage"] = "Esta vaga está ocupada.";
                 
                }

                if (_movimentoService.RegistrarEntrada(movimento) == null)
                {
                    ModelState.AddModelError(string.Empty, "O veículo já está em uma vaga, se necessário resgistre sua saída.");
                    TempData["ErrorMessageEntrada"] = "O veículo já está em uma vaga, se necessário registre sua saída.";  
                    return RedirectToAction("RegistroDeEntrada", "RegistroDeMovimento");
                };

                var atualizaFlVaga = _vagaService.AtualizaFLVaga(movimento.IdVaga);

                if(atualizaFlVaga == null)
                {
                    throw new ArgumentException("Erro ao verificar flag de vaga");
                }

            }
            return RedirectToAction("ConsultarVagas", "ConsultarVagas");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistroDeSaidaDeVeiculo(MovimentoViewModel movimentoViewModel)
        {


            Movimento movimento = new Movimento()
            {
                DtSaida = movimentoViewModel.DtSaida,
                HrSaida = movimentoViewModel.HrSaida,
                IdVaga = movimentoViewModel.IdVaga,
              
            };

            if (ModelState.IsValid)
            {
                var movimentoDeSaida = _movimentoService.RegistrarSaida(movimento);
               

                if (movimentoDeSaida == null)
                {
                    ModelState.AddModelError(string.Empty, "A data e hora de saída não pode ser menor que a data e hora de entrada.");
                    TempData["ErrorMessageSaida"] = "A data e hora de saída não pode ser menor que a data e hora de entrada.";
                    return RedirectToAction("RegistroDeSaida", "RegistroDeMovimento");
                }

                
                var atualizaFlVaga = _vagaService.AtualizaFLVaga(movimento.IdVaga);
               
                if (atualizaFlVaga == null)
                {
                    throw new ArgumentException("Erro ao verificar flag de vaga");
                }
            }
            return RedirectToAction("ConsultarVagas", "ConsultarVagas");

        }

        #endregion POST
    }
}
