using ApiMutants.Application.Commands;
using ApiMutants.Application.Interfaces;
using ApiMutants.Domain.NonEntities;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApiMutants.Tests
{
    [TestClass]
    public class MutantsCommandHandlerTests
    {
        private Mock<ILogger<MutantsCommandHandler>> _loggerMock;
        private Mock<IMutantsService> _mutantServiceMock;
        private MutantsCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<MutantsCommandHandler>>();
            _mutantServiceMock = new Mock<IMutantsService>();

            _handler = new MutantsCommandHandler(_loggerMock.Object, _mutantServiceMock.Object);
        }

        [TestMethod]
        public async Task Handle_ShouldReturnTrue_WhenMutantServiceReturnsTrue()
        {
            // Arrange
            var dnaTable = new Domain.NonEntities.Mutants();
            var request = new MutantsRqst(dnaTable);

            _mutantServiceMock.Setup(service => service.isMutant(dnaTable)).Returns(true);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            _mutantServiceMock.Verify(service => service.isMutant(dnaTable), Times.Once);
        }

        [TestMethod]
        public async Task Handle_ShouldReturnFalse_WhenMutantServiceReturnsFalse()
        {
            var dnaTable = new Domain.NonEntities.Mutants();
            var request = new MutantsRqst(dnaTable);

            _mutantServiceMock.Setup(service => service.isMutant(dnaTable)).Returns(false);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsFalse(result);
            _mutantServiceMock.Verify(service => service.isMutant(dnaTable), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Handle_ShouldThrowException_WhenMutantServiceThrowsException()
        {
            var dnaTable = new Mutants();
            var request = new MutantsRqst(dnaTable);

            _mutantServiceMock.Setup(service => service.isMutant(dnaTable)).Throws(new Exception("Error"));

            await _handler.Handle(request, CancellationToken.None);
        }
    }
}