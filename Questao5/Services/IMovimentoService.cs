using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;

namespace Questao5.Services
{
    public interface IMovimentoService
    {
        string CriarMovimento(CriarMovimentacaoCommand command);
        Idempotencia ConsultarIdempotencia(CriarMovimentacaoCommand command);
    }
}