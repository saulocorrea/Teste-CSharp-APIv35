using Dapper;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Repository
{
    public class MovimentoRepository : RepositoryBase, IMovimentoRepository
    {
        public MovimentoRepository(IDbTransaction transaction, IDbConnection connection)
            : base(transaction, connection)
        {

        }

        public void Add(Movimento entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _ = Connection.ExecuteScalar(
                @"INSERT INTO Movimento (
                    idmovimento,
                    idcontacorrente,
                    datamovimento,
                    tipomovimento,
                    valor
                ) VALUES (
                    @Idmovimento, 
                    @Idcontacorrente, 
                    @Datamovimento,  
                    @Tipomovimento, 
                    @Valor); ",
                param: new
                {
                    Idmovimento = entity.IdMovimento,
                    Idcontacorrente = entity.IdContacorrente,
                    Datamovimento = entity.DataMovimento,
                    Tipomovimento = entity.TipoMovimento,
                    Valor = entity.Valor
                },
                transaction: Transaction
            );
        }
    }
}