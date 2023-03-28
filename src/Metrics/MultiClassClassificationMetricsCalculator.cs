using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Helpers;
using Byndyusoft.ML.Tools.Metrics.Interfaces;

namespace Byndyusoft.ML.Tools.Metrics
{
    public class MultiClassClassificationMetricsCalculator : IMultiClassClassificationMetricsCalculator
    {
        public MultiClassClassificationMetrics Calculate(ClassificationResult[] classificationResults)
        {
            var multiClassConfusionMatrixValueCounts = MultiClassConfusionMatrixValueCounts.Generate(classificationResults);

            var oneClassClassificationMetricsList = new List<OneClassClassificationMetrics>();

            foreach (var (classValue, confusionMatrixValueCounts) in multiClassConfusionMatrixValueCounts.Enumerate())
            {
                var classificationMetrics = ClassificationMetricsCalculatorHelper.Calculate(confusionMatrixValueCounts);
                var oneClassClassificationMetrics = new OneClassClassificationMetrics(classValue, classificationMetrics);
                oneClassClassificationMetricsList.Add(oneClassClassificationMetrics);
            }

            var macroMetrics = ClassificationMetricsCalculatorHelper.CalculateMacroMetrics(
                oneClassClassificationMetricsList.Select(i => i.ClassificationMetrics).ToArray());
            var microMetrics = ClassificationMetricsCalculatorHelper.CalculateMicroMetrics(multiClassConfusionMatrixValueCounts);


            return new MultiClassClassificationMetrics(oneClassClassificationMetricsList.ToArray(), microMetrics, macroMetrics);
        }
    }
}