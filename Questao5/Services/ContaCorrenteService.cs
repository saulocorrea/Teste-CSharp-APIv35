using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database;
using System.Globalization;

namespace Questao5.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly ILogger<ContaCorrenteService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ContaCorrenteService(ILogger<ContaCorrenteService> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public ConsultarSaldoResponse ConsultarSaldo(ConsultarSaldoQuery query)
        {
            var contaCorrente = _unitOfWork.ContaCorrenteRepository.ObterByNumero(query.Numero);
            if (contaCorrente == null)
            {
                throw new Exception("INVALID_ACCOUNT");
            }

            if (contaCorrente.Ativo == false)
            {
                throw new Exception("INACTIVE_ACCOUNT");
            }

            double saldoValor = _unitOfWork.ContaCorrenteRepository.CalcularSaldo(contaCorrente.IdContaCorrente);
            
            return new ConsultarSaldoResponse
            {
                Numero = contaCorrente.Numero,
                NomeTitular = contaCorrente.Nome,
                ValorSaldo = saldoValor.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")),//.ToString("0.00"),
                DataHoraConsulta = DateTime.Now.ToString()
            };
        }
    }
}
