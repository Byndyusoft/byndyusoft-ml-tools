using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.Interfaces
{
    public interface IMultiClassUserPostActionCountsCalculator
    {
        MultiClassUserPostActionCounts Calculate(ClassificationResult[] classificationResults, string nonTargetClass);
    }
}