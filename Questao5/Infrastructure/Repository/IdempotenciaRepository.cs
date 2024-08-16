using Dapper;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Repository
{
    public class IdempotenciaRepository : RepositoryBase, IIdempotenciaRepository
    {
        public IdempotenciaRepository(IDbTransaction transaction, IDbConnection connection)
            : base(transaction, connection)
        {
        }

        public IEnumerable<Idempotencia> BuscarTodos()
        {
            return Connection.Query<Idempotencia>("SELECT * FROM Idempotencia");
        }

        public Idempotencia BuscarPorChave(string chave)
        {
            return Connection.Query<Idempotencia>(
                "SELECT * FROM Idempotencia WHERE chave_idempotencia = @Chave",
                param: new
                {
                    Chave = chave
                }
            ).FirstOrDefault();
        }

        public void Inserir(Idempotencia entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _ = Connection.ExecuteScalar(
                @"INSERT INTO Idempotencia (
                    chave_idempotencia,
                    requisicao,
                    resultado
                ) VALUES (
                    @Chave_idempotencia,
                    @Requisicao,
                    @Resultado); ",
                param: new
                {
                    Chave_idempotencia = entity.ChaveIdempotencia,
                    Requisicao = entity.Requisicao,
                    Resultado = entity.Resultado
                },
                transaction: Transaction
            );
        }
    }
}