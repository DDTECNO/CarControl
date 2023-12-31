﻿using CarControl.Domain;
using CarControl.Infrastructure.Interface;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarControl.Infrastructure.Repositories
{
    public class MovimentoRepository : BaseRepository<Movimento>, IMovimentoRepository
    {

        public MovimentoRepository(CarControlContext context) : base(context)
        {

        }


        #region  CRUD

        public Movimento RegistrarEntrada(Movimento movimento)
        {
            _context.Add(movimento);
            _context.SaveChanges();

            return movimento;
        }

        public Movimento RegistrarSaida(Movimento movimento)
        {
            Movimento vagaCadastrada = _dbset.Where(p => p.IdVaga == movimento.IdVaga && p.HrSaida == null).SingleOrDefault();


 
            if (movimento != null)
            {
                vagaCadastrada.HrSaida = movimento.HrSaida;
                vagaCadastrada.DtSaida = movimento.DtSaida;

                _context.SaveChanges();
                return movimento;
            }


            return null;

        }
        public async Task<bool> ConsultaSeTemMovimento(int idVeiculo)
        {
            List<Movimento> movimentos = await _dbset.Where(p => p.IdVeiculo == idVeiculo).ToListAsync() ?? null;

            return movimentos.Any();
        }

        public IEnumerable<Movimento> ConsultaSeTemMovimento(Movimento movimento)
        {
            List<Movimento> movimentos = _dbset.Where(p => p.IdVeiculo == movimento.IdVeiculo && p.DtSaida == null).ToList() ?? null;

            return movimentos;
        }

        public IEnumerable<Movimento> ConsultaSeTemMovimentoPorVaga(int idVaga)
        {
            List<Movimento> movimentos = _dbset.Where(p => p.IdVaga == idVaga && p.DtSaida == null).ToList() ?? null;

            return movimentos;
        }

        public IEnumerable<Movimento> ConsultaTodosMovimentos()
        {
            return _dbset.AsNoTracking().Take(50).ToList();
        }

        public IEnumerable<Movimento> ConsultaMovimentoDoVeiculo(string cpfCondutor)
        {
            //Foi utilizado esse método para conulsta pois o entity não consegue fazer o join passando o ofType com o tipo do dbset,
            //pois ele não possui suporte para consultas desse tipo   
            string sql = "SELECT * FROM Movimento WHERE IdVeiculo IN (SELECT IdVeiculo FROM Veiculo WHERE CpfCondutor = @cpfCondutor)";
            List<Movimento> movimentoDoVeiculo = _dbset.FromSqlRaw(sql, new SqliteParameter("@cpfCondutor", cpfCondutor)).ToList();

            return movimentoDoVeiculo;
        }

        public Movimento ExcluirMovimento(int idMovimento)
        {
            Movimento movimento = _dbset.Where(p => p.IdMovimento == idMovimento).FirstOrDefault();

            if (movimento != null)
            {
                _context.Remove(movimento);
                _context.SaveChanges();
                return movimento;
            }

            return null;

        }



        #endregion
    }
}
