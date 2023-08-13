using CarControl.Domain;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IMovimentoRepository
    {
        Movimento RegistrarEntrada(Movimento movimento);

    }
}
