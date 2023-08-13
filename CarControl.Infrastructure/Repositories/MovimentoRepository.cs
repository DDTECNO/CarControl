using CarControl.Domain;
using CarControl.Infrastructure.Repositories.Interface;

namespace CarControl.Infrastructure.Repositories
{
    public class MovimentoRepository : BaseRepository<Movimento>, IMovimentoRepository
    {


        public MovimentoRepository(CarControlContext context) : base(context)
        {

        }


        public Movimento RegistrarEntrada(Movimento movimento)
        {
            _dbset.Add(movimento);
            _context.SaveChanges();

            return movimento;
        }
    }
}
