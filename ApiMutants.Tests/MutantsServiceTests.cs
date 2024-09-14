using ApiMutants.Domain.Config;
using ApiMutants.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMutants.Tests
{
    [TestClass]
    public class MutantsServiceTests
    {
        private MutantsService _mutantsService;
        private Mock<ILogger<MutantsService>> _loggerMock;
        private Mock<IOptions<SequenceConfig>> _optionsMock;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<MutantsService>>();
            _optionsMock = new Mock<IOptions<SequenceConfig>>();

            _optionsMock.Setup(x => x.Value).Returns(new SequenceConfig()
            { 
                MinQuantity = 4
            });

            _mutantsService = new MutantsService(_optionsMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public void IsMutant_ShouldReturnTrue_WhenValidMutantDNA()
        {
            // Arrange
            var dna = new List<string>
            {
                "ATGGGG",
                "CAGTGC",
                "TGATGA",
                "GGAAGA",
                "CCCCTA",
                "TCACTA"
            };
            var dnaData = new Domain.NonEntities.Mutants { DNA = dna }; 

            var result = _mutantsService.isMutant(dnaData);

            Assert.IsTrue(result); 
        }

        [TestMethod]
        public void IsMutant_ShouldReturnFalse_WhenNoMutantDNA()
        {
            var dna = new List<string>
            {
                "ATGCGA",
                "CAGTGC",
                "TTATTT",
                "AGACGG",
                "GCGTCA",
                "TCACTG"
            };
            var dnaData = new Domain.NonEntities.Mutants { DNA = dna };

            var result = _mutantsService.isMutant(dnaData);

            Assert.IsFalse(result);
        }
               

        [TestMethod]
        public void IsMutant_ShouldHandleEmptyDNA()
        {           
            var dnaData = new Domain.NonEntities.Mutants ();

            try
            {
                var result = _mutantsService.isMutant(dnaData);
                Assert.Fail("Debio lanzar una excepción");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Lista No contiene la cantidad minima de secuencias.");
            }
        }

        [TestMethod]
        public void IsMutant_ShouldThrowException_WhenSmallDNA()
        {
            var dna = new List<string>
            {
                "ATG",
                "CAG",
                "TGA"
            };
            var dnaData = new Domain.NonEntities.Mutants { DNA = dna };

            try
            {
                var result = _mutantsService.isMutant(dnaData);
                Assert.Fail("Debio lanzar una excepción");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Lista No contiene la cantidad minima de secuencias.");
            }
        }

    }
}
