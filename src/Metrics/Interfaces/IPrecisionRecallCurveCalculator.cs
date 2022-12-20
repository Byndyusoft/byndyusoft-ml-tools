using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.Interfaces
{
    public interface IPrecisionRecallCurveCalculator
    {
        PrecisionRecallCurve Calculate(string classValue, ClassificationResult[] classificationResults);
    }
}