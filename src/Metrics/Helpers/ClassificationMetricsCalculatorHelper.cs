using System;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;

namespace Byndyusoft.ML.Tools.Metrics.Helpers
{
    public static class ClassificationMetricsCalculatorHelper
    {
        public static ClassificationMetrics CalculateClassificationMetricsForOneClass(
            ConfusionMatrix confusionMatrix)
        {
            var sampleCount = confusionMatrix.GetCount(ConfusionMatrixValue.TruePositive);
            sampleCount += confusionMatrix.GetCount(ConfusionMatrixValue.FalseNegative);
            sampleCount +=confusionMatrix.GetCount(ConfusionMatrixValue.FalsePositive);

            return CalculateClassificationMetrics(confusionMatrix, sampleCount);
        }

        public static ClassificationMetrics CalculateMicroMetrics(
            MultiClassConfusionMatrices multiClassConfusionMatrices)
        {
            var totalConfusionMatrixValueCounts = multiClassConfusionMatrices.GetTotalConfusionMatrix();
            return CalculateClassificationMetrics(totalConfusionMatrixValueCounts, multiClassConfusionMatrices.SampleCount);
        }

        public static ClassificationMetrics CalculateMacroMetrics(ClassificationMetrics[] classificationMetricsByClass)
        {
            var precision = GetAverage(classificationMetricsByClass, i => i.Precision);
            var recall = GetAverage(classificationMetricsByClass, i => i.Recall);
            var f1Score = GetAverage(classificationMetricsByClass, i => i.F1Score);

            return new ClassificationMetrics(precision, recall, f1Score, null);
        }

        private static ClassificationMetrics CalculateClassificationMetrics(
            ConfusionMatrix confusionMatrix, int sampleCount)
        {
            var precision = CalculatePrecision(confusionMatrix);
            var recall = CalculateRecall(confusionMatrix);
            var f1Score = CalculateF1Score(confusionMatrix);

            return new ClassificationMetrics(precision, recall, f1Score, sampleCount);
        }

        private static double? CalculatePrecision(ConfusionMatrix confusionMatrix)
        {
            var truePositiveCount = confusionMatrix.GetCount(ConfusionMatrixValue.TruePositive);
            var divisor = truePositiveCount + confusionMatrix.GetCount(ConfusionMatrixValue.FalsePositive);
            if (divisor == 0)
                return null;

            return (double)truePositiveCount / divisor;
        }

        private static double? CalculateRecall(ConfusionMatrix confusionMatrix)
        {
            var truePositiveCount = confusionMatrix.GetCount(ConfusionMatrixValue.TruePositive);
            var divisor = truePositiveCount + confusionMatrix.GetCount(ConfusionMatrixValue.FalseNegative);
            if (divisor == 0)
                return null;

            return (double)truePositiveCount / divisor;
        }

        private static double? CalculateF1Score(ConfusionMatrix confusionMatrix)
        {
            var truePositiveCount = confusionMatrix.GetCount(ConfusionMatrixValue.TruePositive);
            var divisor = 2 * truePositiveCount +
                          confusionMatrix.GetCount(ConfusionMatrixValue.FalsePositive) +
                          confusionMatrix.GetCount(ConfusionMatrixValue.FalseNegative);

            if (divisor == 0)
                return null;

            return truePositiveCount * 2D / divisor;
        }

        private static double? GetAverage(ClassificationMetrics[] classificationResults,
            Func<ClassificationMetrics, double?> metricValueGetter)
        {
            return classificationResults.Select(metricValueGetter).Average();
        }
    }
}