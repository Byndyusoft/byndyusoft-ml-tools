using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;
using Byndyusoft.ML.Tools.Metrics.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.Extensions
{
    [TestFixture]
    public class ClassificationResultExtensionsTests
    {
        private const string DefaultClass = "1";
        private const string AnotherClass = "2";

        [Test]
        [TestCase(DefaultClass, DefaultClass, ConfusionMatrixValue.TruePositive)]
        [TestCase(DefaultClass, AnotherClass, ConfusionMatrixValue.FalseNegative)]
        [TestCase(AnotherClass, DefaultClass, ConfusionMatrixValue.FalsePositive)]
        [TestCase(AnotherClass, AnotherClass, ConfusionMatrixValue.TrueNegative)]
        [TestCase(DefaultClass, null, ConfusionMatrixValue.FalseNegative)]
        [TestCase(AnotherClass, null, ConfusionMatrixValue.TrueNegative)]
        public void CalculateConfusionMatrixValue_ForDefaultClass(string actualClass, string? predictedClass, ConfusionMatrixValue expectedResult)
        {
            // Arrange
            var classificationResult = new ClassificationResult(actualClass, predictedClass);

            // Act
            var calculateConfusionMatrixValue = classificationResult.CalculateConfusionMatrixValue(DefaultClass);

            // Assert
            calculateConfusionMatrixValue.Should().Be(expectedResult);
        }
    }
}