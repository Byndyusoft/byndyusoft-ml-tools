using System.Collections.Generic;
using System.Linq;

namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    // TODO Rename MultiClassClassificationPrecisionRecallCurveCalculationResult
    public class MultiClassPrecisionRecallCurveResult
    {
        public MultiClassPrecisionRecallCurveResult(
            IEnumerable<PrecisionRecallCurve> precisionRecallCurves)
        {
            PrecisionRecallCurves = precisionRecallCurves.ToArray();
            MeanAveragePrecision = PrecisionRecallCurves.Any() ? PrecisionRecallCurves.Average(i => i.AveragePrecision) : 0d;
        }

        public PrecisionRecallCurve[] PrecisionRecallCurves { get; }

        public double MeanAveragePrecision { get; }
    }
}