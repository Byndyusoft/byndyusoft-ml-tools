using System;
using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.PrecisionRecallCurves
{
    public class MultiClassPrecisionRecallCurvesCalculateTestData
    {
        public ClassificationResultWithConfidence[] Arguments { get; set; } = Array.Empty<ClassificationResultWithConfidence>();

        public double ExpectedMeanAveragePrecision { get; set; }

        public PrecisionRecallCurve[] ExpectedPrecisionRecallCurves { get; set; } = Array.Empty<PrecisionRecallCurve>();
    }
}