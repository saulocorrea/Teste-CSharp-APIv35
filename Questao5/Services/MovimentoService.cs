using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Services
{
    public class MovimentoService : IMovimentoService
    {
        private readonly ILogger<MovimentoService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public MovimentoService(ILogger<MovimentoService> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public string CriarMovimento(CriarMovimentacaoCommand command)
        {
            if (command.Valor < 0)
            {
                return "INVALID_VALUE";
            }

            string[] tipos = ["C", "D"];
            if (!tipos.Contains(command.Tipo))
            {
                return "INVALID_TYPE";
            }

            var contaCorrente = _unitOfWork.ContaCorrenteRepository.ObterByNumero(command.Numero);
            if (contaCorrente == null)
            {
                return "INVALID_ACCOUNT";
            }

            if (contaCorrente.Ativo == false)
            {
                return "INACTIVE_ACCOUNT";
            }

            var movimento = new Movimento
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContacorrente = contaCorrente.IdContaCorrente,
                TipoMovimento = command.Tipo,
                DataMovimento = DateTime.Now.ToString(),
                Valor = command.Valor
            };

            try
            {
                _unitOfWork.Begin();
                _unitOfWork.MovimentoRepository.Add(movimento);

                var idempotencia = new Idempotencia
                {
                    ChaveIdempotencia = command.ChaveRequisicao,
                    Requisicao = JsonConvert.SerializeObject(command),
                    Resultado = movimento.IdMovimento
                };
                _unitOfWork.IdempotenciaRepository.Inserir(idempotencia);

                _unitOfWork.Commit();

                return movimento.IdMovimento;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir movimento");
                _unitOfWork.Rollback();
            }

            return "ERROR_UNSUCCESS";
        }

        public Idempotencia ConsultarIdempotencia(CriarMovimentacaoCommand command)
        {
            return _unitOfWork.IdempotenciaRepository.BuscarPorChave(command.ChaveRequisicao);
        }
    }
}
