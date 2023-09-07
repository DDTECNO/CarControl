﻿using CarControl.Domain;
using System.Collections.Generic;

namespace CarControl.Infrastructure.Repositories.Interface
{
    public interface IMovimentoRepository
    {
        IEnumerable<Movimento>ConsultaSeTemMovimento(Veiculo veiculo);
        IEnumerable<Movimento> ConsultaSeTemMovimento(Movimento movimento);
        Movimento RegistrarEntrada(Movimento movimento);
        Movimento RegistrarSaida(Movimento movimento);
    }
}
