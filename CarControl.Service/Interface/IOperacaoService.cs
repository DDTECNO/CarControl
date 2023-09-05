using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Service.Interface
{
    public interface IOperacaoService
    {
        IEnumerable<Operacao> ListaOperacao();

    }
}
