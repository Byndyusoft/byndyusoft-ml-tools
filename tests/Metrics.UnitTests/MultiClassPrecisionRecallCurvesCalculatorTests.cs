using Byndyusoft.ML.Tools.Metrics.Interfaces;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestInfrastructure;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.PrecisionRecallCurves;
using FluentAssertions;
using NUnit.Framework;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests
{
    [TestFixture]
    public class MultiClassPrecisionRecallCurvesCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            _calculator = new MultiClassPrecisionRecallCurvesCalculator();
        }

        private IMultiClassPrecisionRecallCurvesCalculator _calculator = default!;

        [TestCaseSource(typeof(MultiClassPrecisionRecallCurvesCalculateTestData),
            nameof(MultiClassPrecisionRecallCurvesCalculateTestData.Cases))]
        public void Calculate_ReturnsExpectedResult(MultiClassPrecisionRecallCurvesCalculateTestData data)
        {
            // Act
            var result = _calculator.Calculate(data.Arguments);

            // Assert
            result.MeanAveragePrecision.Should().BeApproximately(data.ExpectedMeanAveragePrecision, data.Epsilon);
            result.PrecisionRecallCurves.Should().BeEquivalentTo(
                data.ExpectedPrecisionRecallCurves,
                o => o.WithStrictOrdering().WithApproximateDoubleValues(data.Epsilon));
        }
    }
}