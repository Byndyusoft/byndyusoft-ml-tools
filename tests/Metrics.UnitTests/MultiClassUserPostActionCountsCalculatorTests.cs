using System;
using Byndyusoft.ML.Tools.Metrics.Interfaces;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.UserPostActionCounts;
using FluentAssertions;
using NUnit.Framework;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests
{
    [TestFixture]
    public class MultiClassUserPostActionCountsCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            _calculator = new MultiClassUserPostActionCountsCalculator();
        }

        private IMultiClassUserPostActionCountsCalculator _calculator = default!;

        [TestCaseSource(typeof(MultiClassUserPostActionCountsCalculatorTestData),
            nameof(MultiClassUserPostActionCountsCalculatorTestData.Cases))]
        public void Calculate_ReturnsExpectedResult(MultiClassUserPostActionCountsCalculatorTestData testData)
        {
            // Arrange
            Console.WriteLine(testData.Description);

            // Act
            var multiClassUserPostActionCounts = _calculator.Calculate(
                testData.ClassificationResultsArgument,
                testData.NonTargetClassArgument);

            // Assert
            multiClassUserPostActionCounts.Should().BeEquivalentTo(testData.ExpectedUserPostActionCounts);
        }
    }
}