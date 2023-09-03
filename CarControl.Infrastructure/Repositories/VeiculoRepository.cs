using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
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

        public Veiculo obterVeiculos(int id)
        {
            var veiculo = _context.Set<Veiculo>().Where(p => p.IdVeiculo == id).SingleOrDefault();

            if (veiculo == null)
            {
                throw new ArgumentException("Veículo não encontrado");
            }

            return veiculo;
        }


        public Veiculo EditarVeiculo(Veiculo veiculo)
        {
            var veiculoCadastrado = _dbset.Where(p => p.IdVeiculo == veiculo.IdVeiculo).SingleOrDefault();

            if (veiculoCadastrado == null)
            {
                throw new ArgumentException("Veículo não encontrado");
            }

            veiculoCadastrado.Atualiza(veiculo);

            _context.SaveChanges();

            return veiculoCadastrado;

        }

        public void ExcluirVeiculo(int id)
        {
            var veiculo = _context.Set<Veiculo>().Where(p => p.IdVeiculo == id).SingleOrDefault();

            if (veiculo == null)
            {
                throw new ArgumentException("Veículo não encontrado");
            }

            _dbset.Remove(veiculo);
            _context.SaveChanges();
        }

        public Veiculo obterVeiculoPorCPF(string cpf)
        {
            var veiculo = _context.Set<Veiculo>().Where(p => p.CpfCondutor == cpf).SingleOrDefault();
            if (veiculo == null)
            {
                throw new ArgumentException("Veículo não encontrado");
            }

            return veiculo;
        }

        #endregion CRUD
    }
}
