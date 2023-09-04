using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarControl.Infrastructure.Repositories
{
    public class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
    {

        public VeiculoRepository(CarControlContext context) : base(context)
        {


        }

        #region CRUD
        public Veiculo Create(Veiculo veiculo)
        {

            _dbset.Add(veiculo);
            _context.SaveChanges();

            return veiculo;
        }


        public IList<Veiculo> ListaVeiculos()
        {
            return _dbset.ToList();
        }

        public Veiculo ObterVeiculos(int id)
        {
            var veiculo = _dbset.Where(p => p.IdVeiculo == id).SingleOrDefault();

            return veiculo ?? throw new ArgumentException("Veículo não encontrado");
        }


        public Veiculo EditarVeiculo(Veiculo veiculo)
        {
            var veiculoCadastrado = _dbset.Where(p => p.IdVeiculo == veiculo.IdVeiculo).SingleOrDefault() ?? throw new ArgumentException("Veículo não encontrado");

            veiculoCadastrado.Atualiza(veiculo);

            _context.SaveChanges();

            return veiculoCadastrado;

        }

        public void ExcluirVeiculo(int id)
        {
            var veiculo = _dbset.Where(p => p.IdVeiculo == id).SingleOrDefault() ?? throw new ArgumentException("Veículo não encontrado");
            _dbset.Remove(veiculo);
            _context.SaveChanges();
        }

        public Veiculo ObterVeiculoPorCPF(string cpf)
        {
            var veiculo = _dbset.Where(p => p.CpfCondutor == cpf).SingleOrDefault();
            return veiculo ?? throw new ArgumentException("Veículo não encontrado");
        }

        #endregion CRUD
    }
}
