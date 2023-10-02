using AutoMapper;
using CarControl.Common.DTO;
using CarControl.Common.ViewModel;
using CarControl.Domain;
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
        private readonly IMapper _mapper;
        private readonly IVeiculoService _veiculoService;
        private readonly IVagaService _vagaService;
        private readonly IOperacaoService _operacaoService;
        private readonly IMovimentoService _movimentoService;

        public MovimentoController(IVeiculoService veiculoService, IVagaService vagaService, IOperacaoService operacaoService, IMovimentoService movimentoService, IMapper mapper)
        {
            _veiculoService = veiculoService;
            _vagaService = vagaService;
            _operacaoService = operacaoService;
            _movimentoService = movimentoService;
            _mapper = mapper;
        }


        #endregion DEPENDÊNCIAS

        #region GET 

        /// <summary>
        /// Se a chamada partir da tela de vagas ou da tela de veículos, é previamente selecionado a vaga ou veículo para movimentação.
        /// </summary>
        /// <param name="idVeiculo"></param>
        /// <param name="idVaga"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ActionResult> RegistroDeEntrada(int idVeiculo = 0, int idVaga = 0)
        {
            IEnumerable<OperacaoDTO> ops = _operacaoService.ListaOperacao();
            IEnumerable<VagaDTO> vgs = await _vagaService.ListaVaga();
            IEnumerable<VeiculoDTO> vcls = await _veiculoService.ListarVeiculos();

            if (idVeiculo != 0 || idVaga != 0)
            {

                if (idVaga != 0)
                {

                    VagaDTO vaga = _vagaService.ObterVaga(idVaga) ?? throw new ArgumentException("Vaga não encontrada");

                    vgs = new List<VagaDTO>
                    {
                      vaga
                    };

                }
                else
                {
                    vgs = await _vagaService.ListaVaga();
                }

                if (idVeiculo != 0)
                {

                    VeiculoDTO veiculo = _veiculoService.ObterVeiculo(idVeiculo) ?? throw new ArgumentException("Veículo não encontrado");

                    vcls = new List<VeiculoDTO>
                   {
                      veiculo
                   };

                }
                else
                {
                    vcls = await _veiculoService.ListarVeiculos();
                }


                MovimentoDTO movimentoDTO = new MovimentoDTO()
                {
                    Veiculos = vcls,
                    Vagas = vgs,
                    Operacoes = ops,
                };

                MovimentoViewModel movimentoViewModel = _mapper.Map<MovimentoViewModel>(movimentoDTO);

                return View(movimentoViewModel);

            }
            else
            {
                MovimentoDTO movimentoDTO = new MovimentoDTO()
                {
                    Veiculos = vcls,
                    Vagas = vgs,
                    Operacoes = ops,
                };

                MovimentoViewModel movimentoViewModel = _mapper.Map<MovimentoViewModel>(movimentoDTO);

                return View(movimentoViewModel);
            }


        }

        /// <summary>
        /// Se a chamada partir da tela de vagas ou da tela de veículos, é previamente selecionado a vaga ou veículo para movimentação.
        /// </summary>
        /// <param name="idVeiculo"></param>
        /// <param name="idVaga"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ActionResult> RegistroDeSaida(int idVeiculo = 0, int idVaga = 0)
        {
            IEnumerable<VeiculoDTO> vcls = await _veiculoService.ListarVeiculos();
            IEnumerable<VagaDTO> vgs = await _vagaService.ListaVaga();

            if (idVeiculo != 0 || idVaga != 0)
            {
                if (idVeiculo != 0)
                {
                    VeiculoDTO veiculo = _veiculoService.ObterVeiculo(idVeiculo);

                    vcls = new List<VeiculoDTO>
                    {
                      veiculo
                    };

                }
                else
                {
                    vcls = await _veiculoService.ListarVeiculos();
                }
                if (idVaga != 0)
                {

                    VagaDTO vaga = _vagaService.ObterVaga(idVaga) ?? throw new ArgumentException("Vaga não encontrada");

                    vgs = new List<VagaDTO>
                    {
                        vaga
                    };
                }
                else
                {

                    vgs = await _vagaService.ListaVaga();
                }

                MovimentoDTO movimentoDTO = new MovimentoDTO()
                {
                    Veiculos = vcls,
                    Vagas = vgs,

                };

                MovimentoViewModel movimentoViewModel = _mapper.Map<MovimentoViewModel>(movimentoDTO);

                return View(movimentoViewModel);
            }
            else
            {

                MovimentoDTO movimentoDTO = new MovimentoDTO()
                {
                    Veiculos = vcls,
                    Vagas = vgs,

                };

                MovimentoViewModel movimentoViewModel = _mapper.Map<MovimentoViewModel>(movimentoDTO);

                return View(movimentoViewModel);

            }

        }

        #endregion GET

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarVeiculo(VeiculoDTO veiculoDTO)
        {
            try
            {
                Task<VeiculoDTO> buscar = _veiculoService.ObterVeiculoPorCPF(veiculoDTO.CpfCondutor);

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
                MovimentoDTO movimentoDTO = new MovimentoDTO()
                {
                    DtEntrada = movimentoViewModel.DtEntrada,
                    HrEntrada = movimentoViewModel.HrEntrada,
                    IdVeiculo = movimentoViewModel.IdVeiculo,
                    IdVaga = movimentoViewModel.IdVaga,
                    IdTpOperacao = movimentoViewModel.IdOperacao
                };



                if (!_vagaService.VagaEstaOcupada(movimentoDTO.IdVaga))
                {
                    ModelState.AddModelError(string.Empty, "Esta vaga está ocupada.");
                    TempData["ErrorMessage"] = "Esta vaga está ocupada.";

                }

                if (_movimentoService.RegistrarEntrada(movimentoDTO) == null)
                {
                    ModelState.AddModelError(string.Empty, "O veículo já está em uma vaga, se necessário resgistre sua saída.");
                    TempData["ErrorMessageEntrada"] = "O veículo já está em uma vaga, se necessário registre sua saída.";
                    return RedirectToAction("RegistroDeEntrada", "Movimento");
                };

                Vaga atualizaFlVaga = _vagaService.AtualizaFLVaga(movimentoDTO.IdVaga) ?? throw new ArgumentException("Erro ao verificar flag de vaga");

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
                MovimentoDTO movimentoDTO = new MovimentoDTO()
                {
                    DtSaida = movimentoViewModel.DtSaida,
                    HrSaida = movimentoViewModel.HrSaida,
                    IdVaga = movimentoViewModel.IdVaga,

                };


                MovimentoDTO movimentoDeSaida = _movimentoService.RegistrarSaida(movimentoDTO);


                if (movimentoDeSaida == null)
                {
                    ModelState.AddModelError(string.Empty, "A data e hora de saída não pode ser menor que a data e hora de entrada.");
                    TempData["ErrorMessageSaida"] = "A data e hora de saída não pode ser menor que a data e hora de entrada.";
                    return RedirectToAction("RegistroDeSaida", "Movimento");
                }


                Vaga atualizaFlVaga = _vagaService.AtualizaFLVaga(movimentoDTO.IdVaga) ?? throw new ArgumentException("Erro ao verificar flag de vaga");

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
