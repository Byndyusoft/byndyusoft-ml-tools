using System.Linq;
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

        [TestCaseSource(typeof(CalculateTestDataSource), nameof(CalculateTestDataSource.CalculateCases))]
        public void TestCalculate_ReturnsExpectedResult(CalculateTestData data)
        {
            // Act
            var result = _sut.Calculate(data.Input);

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
            Assert.That(result.WeightedAveragePrecision, Is.EqualTo(testData.ExpectedWeightedAveragePrecision).Within(epsilon));

            var resultCurves = result.PrecisionRecallCurves.ToDictionary(x => x.ClassValue);
            var expectedCurves = testData.ExpectedPrecisionRecallCurves.ToDictionary(x => x.ClassValue);

            Assert.That(resultCurves.Keys, Is.EquivalentTo(expectedCurves.Keys));

            foreach (var classValue in resultCurves.Keys)
                AssertPrecisionRecallCurve(resultCurves[classValue], expectedCurves[classValue]);
        }
    }

    public class CalculateTestData
    {
        public ClassificationResult[] Input { get; set; } = null!;

        public double ExpectedMeanAveragePrecision { get; set; }

        public double ExpectedWeightedAveragePrecision { get; set; }

        public PrecisionRecallCurve[] ExpectedPrecisionRecallCurves { get; set; } = null!;
    }

    public static class CalculateTestDataSource
    {
        public static CalculateTestData[] CalculateCases =
        {
            new()
            {
                Input = new ClassificationResult[]
                {
                    new("1", "1", 0.4d),
                    new("1", "1", 0.6d),
                    new("1", "1", 0.9d),
                    new("1", "2", 0.5d),
                    new("1", "3", 0.5d),
                    new("2", "2", 0.3d),
                    new("2", "1", 0.1d),
                    new("2", "2", 0.9d),
                    new("2", "2", 0.5d),
                    new("2", "2", 0.8d),
                    new("2", "2", 0.9d),
                    new("2", "1", 0.8d),
                    new("3", "3", 0.8d),
                    new("3", "1", 0.6d),
                    new("3", "3", 0.9d),
                    new("3", "2", 0.8d),
                    new("3", "3", 0.5d),
                    new("3", "2", 0.2d)
                },
                ExpectedMeanAveragePrecision = 0.51477324263038549d,
                ExpectedWeightedAveragePrecision = 0.52473544973544972,
                ExpectedPrecisionRecallCurves = new PrecisionRecallCurve[]
                {
                    new(
                        "1",
                        0.45333333333333337d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.2d),
                            new(0.66666666666666663d, 0.2d),
                            new(0.66666666666666663d, 0.4d),
                            new(0.6d, 0.4d),
                            new(0.6d, 0.6d),
                            new(0.5d, 0.6d),
                            new(0d, 0.6d),
                            new(0d, 1d)
                        }),
                    new(
                        "2",
                        0.63265306122448983d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new (1d, 0d),
                            new (1d, 0.42857142857142855d),
                            new (0.75d, 0.42857142857142855d),
                            new (0.7142857142857143d, 0.42857142857142855d),
                            new (0.7142857142857143d, 0.7142857142857143d),
                            new (0.625d, 0.7142857142857143d),
                            new (0.625d, 0.7142857142857143d),
                            new (0d, 0.7142857142857143d),
                            new (0d, 1d)
                        }),
                    new (
                        "3",
                        0.45833333333333331d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new (1d, 0d),
                            new (1d, 0.33333333333333331d),
                            new (0.75d, 0.33333333333333331d),
                            new (0.75d, 0.5d),
                            new (0d, 0.5d),
                            new (0d, 1d)
                        })
                }
            }
        };
    }
}