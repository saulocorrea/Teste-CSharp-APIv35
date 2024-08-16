using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repository
{
    public interface IContaCorrenteRepository
    {
        double CalcularSaldo(string idContaCorrente);
        ContaCorrente ObterByNumero(long numero);
    }
}
