using Byndyusoft.ML.Tools.Metrics.Dtos;
using NUnit.Framework;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests
{
    [TestFixture]
    public class MultiClassPrecisionRecallCurvesCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            _sut = new MultiClassPrecisionRecallCurvesCalculator(new PrecisionRecallCurveCalculator());
        }

        private MultiClassPrecisionRecallCurvesCalculator _sut = default!;

        [Test]
        public void Test1()
        {
            // Arrange
            var input = new ClassificationResult[]
            {
                new ("1", "1", 0.4d),
                new ("1", "1", 0.6d),
                new ("1", "1", 0.9d),
                new ("1", "2", 0.5d),
                new ("2", "2", 0.3d),
                new ("2", "1", 0.1d),
                new ("2", "2", 0.9d),
                new ("2", "2", 0.5d),
                new ("2", "2", 0.8d),
                new ("2", "2", 0.9d),
                new ("2", "1", 0.8d)
            };

            // Act
            var result = _sut.Calculate(input);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }
    }
}