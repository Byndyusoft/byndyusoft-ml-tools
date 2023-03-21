using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData;
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

        [TestCaseSource(typeof(CalculateTestDataSource), nameof(CalculateTestDataSource.CalculateCases))]
        public void TestCalculate_ReturnsExpectedResult(CalculateTestData data)
        {
            // Act
            var result = _sut.Calculate(data.Arguments);

            // Assert
            AssertResult(result, data);
        }

        private void AssertResult(MultiClassPrecisionRecallCurveResult result, CalculateTestData testData)
        {
            const double epsilon = 0.000001d;

            void AssertPrecisionRecallCurve(PrecisionRecallCurve actualCurve, PrecisionRecallCurve expectedCurve)
            {
                Assert.That(actualCurve.AveragePrecision, Is.EqualTo(expectedCurve.AveragePrecision).Within(epsilon));
                Assert.That(actualCurve.DataPoints, Is.Not.Null);
                Assert.That(actualCurve.DataPoints.Length, Is.EqualTo(expectedCurve.DataPoints.Length));

                for (var i = 0; i < actualCurve.DataPoints.Length; i++)
                {
                    var actualPoint = actualCurve.DataPoints[i];
                    var expectedPoint = expectedCurve.DataPoints[i];
                    Assert.That(actualPoint.Precision, Is.EqualTo(expectedPoint.Precision).Within(epsilon));
                    Assert.That(actualPoint.Recall, Is.EqualTo(expectedPoint.Recall).Within(epsilon));
                }
            }

            Assert.That(result, Is.Not.Null);
            Assert.That(result.MeanAveragePrecision, Is.EqualTo(testData.ExpectedMeanAveragePrecision).Within(epsilon));
            Assert.That(result.WeightedAveragePrecision,
                Is.EqualTo(testData.ExpectedWeightedAveragePrecision).Within(epsilon));

            var resultCurves = result.PrecisionRecallCurves.ToDictionary(x => x.ClassValue);
            var expectedCurves = testData.ExpectedPrecisionRecallCurves.ToDictionary(x => x.ClassValue);

            Assert.That(resultCurves.Keys, Is.EquivalentTo(expectedCurves.Keys));

            foreach (var classValue in resultCurves.Keys)
                AssertPrecisionRecallCurve(resultCurves[classValue], expectedCurves[classValue]);
        }
    }
}