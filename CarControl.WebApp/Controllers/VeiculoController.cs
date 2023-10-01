using AutoMapper;
using CarControl.Domain;
using CarControl.Domain.ViewModel;
using CarControl.Service.DTO;
using CarControl.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class VeiculoController : Controller
    {

        #region DEPENDÊNCIAS
        private readonly IMapper _mapper;
        private readonly IVeiculoService _veiculoService;
        private readonly IMovimentoService _movimentoService;

        public VeiculoController(IVeiculoService veiculoService, IMovimentoService movimentoService, IMapper mapper)
        {
            _veiculoService = veiculoService;
            _movimentoService = movimentoService;
            _mapper = mapper;
        }


        #endregion DEPENDÊNCIAS

        #region GET

        public ActionResult CadastroDeVeiculo()
        {
            return View();
        }

        public async Task<ActionResult> VeiculosCadastrados()
        {
            IEnumerable<VeiculoDTO> veiculos = await _veiculoService.ListarVeiculos();

            IEnumerable<VeiculoViewModel> veiculosViewModel = _mapper.Map<IEnumerable<VeiculoViewModel>>(veiculos);

            return View(veiculosViewModel);
        }


        public ActionResult EditarVeiculo(int id)
        {
            VeiculoDTO veiculo = _veiculoService.ObterVeiculo(id) ?? throw new ArgumentException("Veículo não encontrado");

            VeiculoViewModel veiculoViewModel = _mapper.Map<VeiculoViewModel>(veiculo);

            return View(veiculoViewModel);
        }


        public ActionResult DetalhesDoVeiculo(int id)
        {
            VeiculoDTO veiculo = _veiculoService.ObterVeiculo(id) ?? throw new ArgumentException("Veículo não encontrado");

            VeiculoViewModel veiculoViewModel = _mapper.Map<VeiculoViewModel>(veiculo);

            return View(veiculoViewModel);
        }

        public ActionResult Excluir(int id)
        {
            VeiculoDTO veiculo = _veiculoService.ObterVeiculo(id) ?? throw new ArgumentException("Veículo não encontrado");

            VeiculoViewModel veiculoViewModel = _mapper.Map<VeiculoViewModel>(veiculo);

            return View(veiculoViewModel);

        }
        #endregion GET

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InserirCadastro(VeiculoDTO veiculoDTO)
        {
            try
            {
                _veiculoService.InserirVeiculo(veiculoDTO);

                return RedirectToAction("VeiculosCadastrados");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro interno na aplicação." + ex.Message);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarVeiculoCadastrado(VeiculoDTO veiculoDTO)
        {
            try
            {
                Task<VeiculoDTO> editar = _veiculoService.EditarVeiculo(veiculoDTO) ?? throw new ArgumentException("Veículo não encontrado");

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
                if (_movimentoService.ConsultaSeTemMovimento(idVeiculo).Result)
                {
                    return Json(new { success = false });
                }

                Task<VeiculoDTO> veiculoExcluido = _veiculoService.ExcluirVeiculo(idVeiculo);

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
