using System;
using Byndyusoft.ML.Tools.Metrics.Helpers;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestInfrastructure;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics;
using FluentAssertions;
using NUnit.Framework;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.Helpers
{
    [TestFixture]
    public class ClassificationMetricsCalculatorHelperTests
    {
        [TestCaseSource(typeof(ClassificationMetricsCalculatorHelperCalculateTestData),
            nameof(ClassificationMetricsCalculatorHelperCalculateTestData.CalculateTestDataCases))]
        public void CalculateClassificationMetrics_ReturnsExpectedResult(ClassificationMetricsCalculatorHelperCalculateTestData testData)
        {
            Console.WriteLine(testData.Description);

            // Act
            var classificationMetrics = ClassificationMetricsCalculatorHelper.CalculateClassificationMetrics(testData.Argument);

            // Assert
            classificationMetrics.Should()
                .BeEquivalentTo(testData.ExpectedResult, o => o.WithApproximateDoubleValues(testData.Epsilon));
        }
    }
}