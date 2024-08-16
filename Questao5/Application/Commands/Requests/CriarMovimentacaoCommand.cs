using MediatR;

namespace Questao5.Application.Commands.Requests
{
    public class CriarMovimentacaoCommand : IRequest<string>
    {
        public string ChaveRequisicao { get; set; }
        public long Numero { get; set; }
        public double Valor { get; set; }
        public string Tipo { get; set; }
    }
}
