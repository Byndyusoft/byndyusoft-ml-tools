using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.Interfaces
{
    public interface IMultiClassClassificationMetricsCalculator
    {
        MultiClassClassificationMetrics Calculate(ClassificationResult[] classificationResults);
    }
}