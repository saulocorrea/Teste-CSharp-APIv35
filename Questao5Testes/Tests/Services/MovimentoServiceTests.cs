using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using Questao5.Services;

namespace Questao5Testes.Tests.Services
{
    [TestClass]
    public class MovimentoServiceTests
    {
        private IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        private ILogger<MovimentoService> _logger = Substitute.For<ILogger<MovimentoService>>();
        private IMovimentoService _movimentoService;

        [TestInitialize]
        public void Initialize()
        {
            _movimentoService = new MovimentoService(_logger, _unitOfWork);
        }

        [TestMethod]
        public void CriarMovimentoComContaInvalidaDeveRetornarErro()
        {
            // Arrange
            var movimentoCommand = new CriarMovimentacaoCommand
            {
                ChaveRequisicao = "1",
                Numero = 1,
                Tipo = "C",
                Valor = 1
            };

            // Act
            var result = _movimentoService.CriarMovimento(movimentoCommand);

            // Assert
            result.Should().Be("INVALID_ACCOUNT");
        }

        [TestMethod]
        public void CriarMovimentoComContaInativaDeveRetornarErro()
        {
            // Arrange
            var movimentoCommand = new CriarMovimentacaoCommand
            {
                ChaveRequisicao = "1",
                Numero = 1,
                Tipo = "C",
                Valor = 1
            };

            var contaCorrente = new ContaCorrente
            {
                Ativo = false,
                Numero = 1,
                IdContaCorrente = "1",
                Nome = "1",
            };

            _unitOfWork.ContaCorrenteRepository.ObterByNumero(Arg.Any<long>()).Returns(contaCorrente);

            // Act
            var result = _movimentoService.CriarMovimento(movimentoCommand);

            // Assert
            result.Should().Be("INACTIVE_ACCOUNT");
        }

        [TestMethod]
        public void CriarMovimentoComContaCorretaDeveRetornarSucesso()
        {
            // Arrange
            var movimentoCommand = new CriarMovimentacaoCommand
            {
                ChaveRequisicao = "1",
                Numero = 1,
                Tipo = "C",
                Valor = 1
            };

            var contaCorrente = new ContaCorrente
            {
                Ativo = true,
                Numero = 1,
                IdContaCorrente = "1",
                Nome = "1",
            };

            _unitOfWork.ContaCorrenteRepository.ObterByNumero(Arg.Any<long>()).Returns(contaCorrente);

            // Act
            var result = _movimentoService.CriarMovimento(movimentoCommand);

            // Assert
            Assert.IsTrue(Guid.TryParse(result, out Guid v));
        }
    }
}
