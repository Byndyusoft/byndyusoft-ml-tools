using Byndyusoft.ML.Tools.Metrics.Helpers;
using Byndyusoft.ML.Tools.Metrics.Settings;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestInfrastructure;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics;
using FluentAssertions;
using NUnit.Framework;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.Helpers
{
    [TestFixture]
    public class PrecisionRecallCurveCalculatorHelperTests
    {
        [TestCaseSource(typeof(PrecisionRecallCurveCalculatorHelperTestData), nameof(PrecisionRecallCurveCalculatorHelperTestData.CalculateTestDataCases))]
        public void Calculate_WithoutReducingCurveDataPoints_ReturnsExpectedResult(PrecisionRecallCurveCalculatorHelperTestData testData)
        {
            // Act
            var precisionRecallCurve = PrecisionRecallCurveCalculatorHelper.Calculate(
                testData.ClassValueArgument,
                testData.ClassificationResultsArgument,
                PrecisionRecallCurveSettings.DefaultWithoutCurveDataPointReducing());

            // Assert
            precisionRecallCurve
                .Should()
                .BeEquivalentTo(testData.ExpectedResult,
                    o => o.WithStrictOrdering().WithApproximateDoubleValues(testData.Epsilon));
        }
    }
}