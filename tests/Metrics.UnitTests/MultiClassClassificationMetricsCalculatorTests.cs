using System;
using Byndyusoft.ML.Tools.Metrics.Interfaces;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestInfrastructure;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics;
using FluentAssertions;
using NUnit.Framework;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests
{
    [TestFixture]
    public class MultiClassClassificationMetricsCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            _calculator = new MultiClassClassificationMetricsCalculator();
        }

        private IMultiClassClassificationMetricsCalculator _calculator = default!;

        [TestCaseSource(typeof(MultiClassClassificationMetricsCalculatorTestData),
            nameof(MultiClassClassificationMetricsCalculatorTestData.Cases))]
        public void Calculate_ReturnsExpectedResult(MultiClassClassificationMetricsCalculatorTestData testData)
        {
            // Arrange
            Console.WriteLine(testData.Description);

            // Act
            var multiClassClassificationMetrics = _calculator.Calculate(testData.Arguments);

            // Assert
            multiClassClassificationMetrics.Should().BeEquivalentTo(testData.ExpectedResult,
                o => o.WithApproximateDoubleValues(testData.Epsilon));
        }
    }
}