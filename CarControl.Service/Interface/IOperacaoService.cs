using CarControl.Common.DTO;
using System.Collections.Generic;

namespace CarControl.Service.Interface
{
    public interface IOperacaoService
    {
        IEnumerable<OperacaoDTO> ListaOperacao();

    }
}
