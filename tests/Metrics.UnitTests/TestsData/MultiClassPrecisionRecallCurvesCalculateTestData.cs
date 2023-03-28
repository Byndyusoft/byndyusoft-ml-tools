using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData
{
    public class MultiClassPrecisionRecallCurvesCalculateTestData
    {
        public ClassificationResultWithConfidence[] Arguments { get; set; } = null!;

        public double ExpectedMeanAveragePrecision { get; set; }

        public PrecisionRecallCurve[] ExpectedPrecisionRecallCurves { get; set; } = null!;
    }
}