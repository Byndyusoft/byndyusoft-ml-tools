using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Extensions;
using Byndyusoft.ML.Tools.Metrics.Helpers;
using Byndyusoft.ML.Tools.Metrics.Interfaces;

namespace Byndyusoft.ML.Tools.Metrics
{
    public class MultiClassClassificationMetricsCalculator : IMultiClassClassificationMetricsCalculator
    {
        public MultiClassClassificationMetrics Calculate(ClassificationResult[] classificationResults)
        {
            var classificationResultsByClass = classificationResults
                .ToDictionaryOfHashSets(i => i.ActualClass, i => i.PredictedClass);

            var oneClassClassificationMetricsList = new List<OneClassClassificationMetrics>();

            foreach (var (classValue, classificationResultsOfTheClass) in classificationResultsByClass.OrderBy(i => i.Key))
            {
                var oneClassClassificationMetrics = ClassificationMetricsCalculatorHelper.Calculate(classValue, classificationResultsOfTheClass.ToArray());
                oneClassClassificationMetricsList.Add(oneClassClassificationMetrics);
            }

            // TODO рассчитать микро и макро метрики
            return new MultiClassClassificationMetrics(oneClassClassificationMetricsList.ToArray(), null, null);
        }
    }
}