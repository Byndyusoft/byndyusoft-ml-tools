using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData
{
    public class CalculateTestData
    {
        public ClassificationResult[] Input { get; set; } = null!;

        public double ExpectedMeanAveragePrecision { get; set; }

        public double ExpectedWeightedAveragePrecision { get; set; }

        public PrecisionRecallCurve[] ExpectedPrecisionRecallCurves { get; set; } = null!;
    }
}