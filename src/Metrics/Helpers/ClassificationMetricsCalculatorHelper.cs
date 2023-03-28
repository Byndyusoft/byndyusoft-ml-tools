using System;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;

namespace Byndyusoft.ML.Tools.Metrics.Helpers
{
    public static class ClassificationMetricsCalculatorHelper
    {
        public static ClassificationMetrics Calculate(
            ConfusionMatrixValueCounts confusionMatrixValueCounts)
        {
            var precision = CalculatePrecision(confusionMatrixValueCounts);
            var recall = CalculateRecall(confusionMatrixValueCounts);
            var f1Score = CalculateF1Score(confusionMatrixValueCounts);

            return new ClassificationMetrics(precision, recall, f1Score);
        }

        public static ClassificationMetrics CalculateMicroMetrics(
            MultiClassConfusionMatrixValueCounts multiClassConfusionMatrixValueCounts)
        {
            var totalConfusionMatrixValueCounts = multiClassConfusionMatrixValueCounts.GetTotalCounts();
            return Calculate(totalConfusionMatrixValueCounts);
        }

        public static ClassificationMetrics CalculateMacroMetrics(ClassificationMetrics[] classificationMetricsByClass)
        {
            var precision = GetAverage(classificationMetricsByClass, i => i.Precision);
            var recall = GetAverage(classificationMetricsByClass, i => i.Recall);
            var f1Score = GetAverage(classificationMetricsByClass, i => i.F1Score);

            return new ClassificationMetrics(precision, recall, f1Score);
        }

        private static double? CalculatePrecision(ConfusionMatrixValueCounts confusionMatrixValueCounts)
        {
            var truePositiveCount = confusionMatrixValueCounts.GetCount(ConfusionMatrixValue.TruePositive);
            var sum = truePositiveCount + confusionMatrixValueCounts.GetCount(ConfusionMatrixValue.FalsePositive);
            if (sum == 0)
                return null;

            return (double)truePositiveCount / sum;
        }

        private static double? CalculateRecall(ConfusionMatrixValueCounts confusionMatrixValueCounts)
        {
            var truePositiveCount = confusionMatrixValueCounts.GetCount(ConfusionMatrixValue.TruePositive);
            var sum = truePositiveCount + confusionMatrixValueCounts.GetCount(ConfusionMatrixValue.FalseNegative);
            if (sum == 0)
                return null;

            return (double)truePositiveCount / sum;
        }

        private static double? CalculateF1Score(ConfusionMatrixValueCounts confusionMatrixValueCounts)
        {
            var truePositiveCount = confusionMatrixValueCounts.GetCount(ConfusionMatrixValue.TruePositive);
            var doubledDivisor = 2 * truePositiveCount +
                                 confusionMatrixValueCounts.GetCount(ConfusionMatrixValue.FalsePositive) +
                                 confusionMatrixValueCounts.GetCount(ConfusionMatrixValue.FalseNegative);

            if (doubledDivisor == 0)
                return null;

            return truePositiveCount * 2D / doubledDivisor;
        }

        private static double? GetAverage(ClassificationMetrics[] classificationResults,
            Func<ClassificationMetrics, double?> metricValueGetter)
        {
            return classificationResults.Select(metricValueGetter).Average(i => i);
        }
    }
}