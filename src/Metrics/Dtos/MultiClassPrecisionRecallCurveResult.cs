using System.Collections.Generic;
using System.Linq;

namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class MultiClassPrecisionRecallCurveResult
    {
        public MultiClassPrecisionRecallCurveResult(
            IEnumerable<PrecisionRecallCurve> precisionRecallCurves,
            double weightedAveragePrecision)
        {
            PrecisionRecallCurves = precisionRecallCurves.ToArray();
            MeanAveragePrecision = PrecisionRecallCurves.Any() ? PrecisionRecallCurves.Average(i => i.AveragePrecision) : 0d;
            WeightedAveragePrecision = weightedAveragePrecision;
        }

        public PrecisionRecallCurve[] PrecisionRecallCurves { get; }

        public double MeanAveragePrecision { get; }

        // TODO Удалить
        public double WeightedAveragePrecision { get; }
    }
}