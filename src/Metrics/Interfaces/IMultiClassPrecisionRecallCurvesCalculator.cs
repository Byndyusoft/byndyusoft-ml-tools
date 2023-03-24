using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Settings;

namespace Byndyusoft.ML.Tools.Metrics.Interfaces
{
    public interface IMultiClassPrecisionRecallCurvesCalculator
    {
        MultiClassPrecisionRecallCurveResult Calculate(
            ClassificationResultWithConfidence[] classificationResults,
            PrecisionRecallCurveSettings? precisionRecallCurveSettings = null);
    }
}