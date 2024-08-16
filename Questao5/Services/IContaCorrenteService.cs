using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Services
{
    public interface IContaCorrenteService
    {
        ConsultarSaldoResponse ConsultarSaldo(ConsultarSaldoQuery query);
    }
}