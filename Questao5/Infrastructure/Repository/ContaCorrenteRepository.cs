using Dapper;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Repository
{
    public class ContaCorrenteRepository : RepositoryBase, IContaCorrenteRepository
    {
        public ContaCorrenteRepository(IDbTransaction transaction, IDbConnection connection)
            : base(transaction, connection)
        {
        }

        public double CalcularSaldo(string idContaCorrente)
        {
            double saldo = 0;

            ChecarStatusConexao();

            saldo = Connection.Query<double>(
                "select IFNULL(sum(total), 0) total from (" +
                "    select sum(valor) as total " +
                "    from movimento m_cred where m_cred.idcontacorrente = @IdContaCorrente and tipomovimento = 'C'" +
                "    union all " +
                "    select sum(valor) * -1 " +
                "    from movimento m_deb where m_deb.idcontacorrente = @IdContaCorrente and tipomovimento = 'D' " +
                ") a",
                param: new { IdContaCorrente = idContaCorrente }
            ).FirstOrDefault();

            return saldo;
        }

        public ContaCorrente ObterByNumero(long numero)
        {
            ChecarStatusConexao();

            return Connection.Query<ContaCorrente>(
                "SELECT * FROM ContaCorrente WHERE Numero = @Numero",
                param: new { Numero = numero }
            ).FirstOrDefault();
        }
    }
}