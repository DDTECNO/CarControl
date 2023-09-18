using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarControl.Infrastructure.Repositories
{
    public class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
    {

        public VeiculoRepository(CarControlContext context) : base(context)
        {


        }

        #region CRUD
        public async Task<Veiculo> Create(Veiculo veiculo)
        {

            _dbset.Add(veiculo);
            await _context.SaveChangesAsync();

            return veiculo;
        }


        public IEnumerable<Veiculo> ListaVeiculos()
        {
            return _dbset.AsNoTracking().Take(10).ToList();
        }

        public Veiculo ObterVeiculos(int id)
        {
            Veiculo veiculo = _dbset.Where(p => p.IdVeiculo == id).SingleOrDefault();

            return veiculo;
        }


        public Veiculo EditarVeiculo(Veiculo veiculo)
        {
            Veiculo veiculoCadastrado = _dbset.Where(p => p.IdVeiculo == veiculo.IdVeiculo).SingleOrDefault();

            if (veiculoCadastrado != null)
            {
                veiculoCadastrado.Atualiza(veiculo);

                _context.SaveChanges();
                return veiculoCadastrado;
            }

            return null;

        }

        public Veiculo ExcluirVeiculo(int id)
        {
            Veiculo veiculo = _dbset.Where(p => p.IdVeiculo == id).SingleOrDefault();

            if (veiculo != null)
            {
                _context.Remove(veiculo);
                _context.SaveChanges();
                return veiculo;
            }

            return null;

        }

        public Veiculo ObterVeiculoPorCPF(string cpf)
        {
            return _dbset.Where(p => p.CpfCondutor == cpf).SingleOrDefault();
        }

        #endregion CRUD
    }
}
