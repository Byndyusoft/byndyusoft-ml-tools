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
            var multiClassConfusionMatrices = MultiClassConfusionMatrices.Generate(classificationResults);

            var oneClassClassificationMetricsList = new List<OneClassClassificationMetrics>();

            foreach (var (@class, confusionMatrix) in multiClassConfusionMatrices.Enumerate())
            {
                var classificationMetrics = ClassificationMetricsCalculatorHelper.CalculateClassificationMetrics(confusionMatrix);
                var oneClassClassificationMetrics = new OneClassClassificationMetrics(@class, classificationMetrics);
                oneClassClassificationMetricsList.Add(oneClassClassificationMetrics);
            }

            var macroMetrics = ClassificationMetricsCalculatorHelper.CalculateMacroMetrics(
                oneClassClassificationMetricsList.Select(i => i.ClassificationMetrics).ToArray());
            var microMetrics = ClassificationMetricsCalculatorHelper.CalculateMicroMetrics(multiClassConfusionMatrices);


            return new MultiClassClassificationMetrics(oneClassClassificationMetricsList.ToArray(), microMetrics, macroMetrics);
        }
    }
}