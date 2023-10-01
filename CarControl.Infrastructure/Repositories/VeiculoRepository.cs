using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarControl.Infrastructure.Repositories
{
    public  class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
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


        public async Task<IEnumerable<Veiculo>> ListaVeiculos()
        {
            return await _dbset.AsNoTracking().Take(10).ToListAsync();
        }

        public Veiculo ObterVeiculo(int id)
        {
            Veiculo veiculo = _dbset.Where(p => p.IdVeiculo == id).SingleOrDefault();

            return veiculo;
        }


        public async Task<Veiculo> EditarVeiculo(Veiculo veiculo)
        {
  

            if (veiculo != null)
            {
                _context.Entry(veiculo).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return veiculo;
            }

            return null;

        }

        public async Task<Veiculo> ExcluirVeiculo(int id)
        {
            Veiculo veiculo =await _dbset.Where(p => p.IdVeiculo == id).SingleOrDefaultAsync();

            if (veiculo != null)
            {
                _context.Remove(veiculo);
                 await _context.SaveChangesAsync();
                return veiculo;
            }

            return null;

        }

        public async Task<Veiculo> ObterVeiculoPorCPF(string cpf)
        {
            return await _dbset.Where(p => p.CpfCondutor == cpf).FirstOrDefaultAsync();
        }

        #endregion CRUD
    }
}
