using CarControl.Domain;
using CarControl.Domain.ViewModel;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class MovimentoController : Controller
    {
        #region DEPENDÊNCIAS
        private readonly IVeiculoService _veiculoService;
        private readonly IVagaService _vagaService;
        private readonly IOperacaoService _operacaoService;
        private readonly IMovimentoService _movimentoService;

        public MovimentoController(IVeiculoService veiculoService, IVagaService vagaService, IOperacaoService operacaoService, IMovimentoService movimentoService)
        {
            _veiculoService = veiculoService;
            _vagaService = vagaService;
            _operacaoService = operacaoService;
            _movimentoService = movimentoService;
        }


        #endregion DEPENDÊNCIAS

        #region GET 


        public async Task<ActionResult> RegistroDeEntrada(int idVeiculo = 0, int idVaga = 0)
        {
            if (idVeiculo != 0)
            {
                Veiculo veiculo = _veiculoService.ObterVeiculos(idVeiculo) ?? throw new ArgumentException("Veículo não encontrado");
                
                IEnumerable<Veiculo> vcls = new List<Veiculo>
                {
                    veiculo
                };

                IEnumerable<Vaga> vgs = _vagaService.ListaVaga();
                IEnumerable<Operacao> ops = _operacaoService.ListaOperacao();
                MovimentoViewModel mvViewModel1 = new MovimentoViewModel()
                {
                    Veiculos = vcls,
                    Vagas = vgs,
                    Operacoes = ops,

                };

                return View(mvViewModel1);

            }
            if (idVaga != 0) {

                Vaga vaga = _vagaService.ObterVaga(idVaga) ?? throw new ArgumentException("Vaga não encontrada");


                IEnumerable<Vaga> vgs2 = new List<Vaga>
                {
                    vaga
                };

                IEnumerable<Veiculo> vcls2 = await _veiculoService.ListaVeiculos();
                IEnumerable<Operacao> ops = _operacaoService.ListaOperacao();
                MovimentoViewModel mvViewModel2 = new MovimentoViewModel()
                {
                    Veiculos = vcls2,
                    Vagas = vgs2,
                    Operacoes = ops,

                };

                return View(mvViewModel2);

            }


            IEnumerable<Veiculo> veiculos = await _veiculoService.ListaVeiculos();
            IEnumerable<Vaga> vagas = _vagaService.ListaVaga();
            IEnumerable<Operacao> operacoes = _operacaoService.ListaOperacao();

            MovimentoViewModel movimentoViewModel = new MovimentoViewModel()
            {
                Veiculos = veiculos,
                Vagas = vagas,
                Operacoes = operacoes,
            };



            return View(movimentoViewModel);
        }


        public async Task<ActionResult> RegistroDeSaida(int idVeiculo = 0, int idVaga = 0)
        {

            if (idVeiculo != 0)
            {
                Veiculo veiculo = _veiculoService.ObterVeiculos(idVeiculo);

                IEnumerable<Veiculo> vcls = new List<Veiculo>
                {
                    veiculo
                };

                IEnumerable<Vaga> vgs = _vagaService.ListaVaga();
                MovimentoViewModel mvViewModel1 = new MovimentoViewModel()
                {
                    Veiculos = vcls,
                    Vagas = vgs,

                };

                return View(mvViewModel1);

            }
            if (idVaga != 0)
            {

                Vaga vaga = _vagaService.ObterVaga(idVaga) ?? throw new ArgumentException("Vaga não encontrada");
                
                IEnumerable<Vaga> vgs2 = new List<Vaga>
                {
                    vaga
                };

                IEnumerable<Veiculo> vcls2 = await _veiculoService.ListaVeiculos();
                MovimentoViewModel mvViewModel2 = new MovimentoViewModel()
                {
                    Veiculos = vcls2,
                    Vagas = vgs2,

                };

                return View(mvViewModel2);

            }



            IEnumerable<Veiculo> veiculos = await _veiculoService.ListaVeiculos();
            IEnumerable<Vaga> vagas = _vagaService.ListaVaga();

            MovimentoViewModel movimentoViewModel = new MovimentoViewModel()
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
            try
            {
                Task<Veiculo> buscar = _veiculoService.ObterVeiculoPorCPF(veiculo.CpfCondutor);

                return buscar == null ? throw new ArgumentException("Veículo não encontrado") : (ActionResult)View(buscar);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistroDeEntradaDeVeiculo(MovimentoViewModel movimentoViewModel)
        {
            try
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
                        return RedirectToAction("RegistroDeEntrada", "Movimento");
                    };

                    Vaga atualizaFlVaga = _vagaService.AtualizaFLVaga(movimento.IdVaga) ?? throw new ArgumentException("Erro ao verificar flag de vaga");
                }
                return RedirectToAction("ConsultarVagas", "Vagas");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistroDeSaidaDeVeiculo(MovimentoViewModel movimentoViewModel)
        {

            try
            {
                Movimento movimento = new Movimento()
                {
                    DtSaida = movimentoViewModel.DtSaida,
                    HrSaida = movimentoViewModel.HrSaida,
                    IdVaga = movimentoViewModel.IdVaga,

                };

                if (ModelState.IsValid)
                {
                    Movimento movimentoDeSaida = _movimentoService.RegistrarSaida(movimento);


                    if (movimentoDeSaida == null)
                    {
                        ModelState.AddModelError(string.Empty, "A data e hora de saída não pode ser menor que a data e hora de entrada.");
                        TempData["ErrorMessageSaida"] = "A data e hora de saída não pode ser menor que a data e hora de entrada.";
                        return RedirectToAction("RegistroDeSaida", "Movimento");
                    }


                    Vaga atualizaFlVaga = _vagaService.AtualizaFLVaga(movimento.IdVaga) ?? throw new ArgumentException("Erro ao verificar flag de vaga");
                }
                return RedirectToAction("ConsultarVagas", "Vagas");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }


        }

        #endregion POST
    }
}
