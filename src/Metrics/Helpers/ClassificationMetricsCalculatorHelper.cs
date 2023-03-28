using System;
using System.Collections.Generic;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;
using Byndyusoft.ML.Tools.Metrics.Extensions;

namespace Byndyusoft.ML.Tools.Metrics.Helpers
{
    public static class ClassificationMetricsCalculatorHelper
    {
        public static OneClassClassificationMetrics Calculate(
            string classValue,
            ClassificationResult[] classificationResults)
        {
            var countsByConfusionMatrixValue = new Dictionary<ConfusionMatrixValue, int>();
            foreach (var confusionMatrixValue in Enum.GetValues<ConfusionMatrixValue>())
                countsByConfusionMatrixValue.Add(confusionMatrixValue, 0);

            foreach (var classificationResult in classificationResults)
            {
                var confusionMatrixValue = classificationResult.CalculateConfusionMatrixValue(classValue);
                countsByConfusionMatrixValue[confusionMatrixValue]++;
            }

            var truePositive = countsByConfusionMatrixValue[ConfusionMatrixValue.TruePositive];
            var falsePositive = countsByConfusionMatrixValue[ConfusionMatrixValue.FalsePositive];
            var falseNegative = countsByConfusionMatrixValue[ConfusionMatrixValue.FalseNegative];

            var precision = CalculatePrecision(truePositive, falsePositive);
            var recall = CalculateRecall(truePositive, falseNegative);
            var f1Score = CalculateF1Score(precision, recall);

            var classificationMetrics = new ClassificationMetrics(precision, recall, f1Score);
            return new OneClassClassificationMetrics(classValue, classificationMetrics);
        }

        private static double? CalculatePrecision(int truePositiveCount, int falsePositiveCount)
        {
            var sum = truePositiveCount + falsePositiveCount;
            if (sum == 0)
                return null;

            return (double)truePositiveCount / sum;
        }

        private static double? CalculateRecall(int truePositiveCount, int falseNegativeCount)
        {
            var sum = truePositiveCount + falseNegativeCount;
            if (sum == 0)
                return null;

            return (double)truePositiveCount / sum;
        }

        private static double? CalculateF1Score(double? precision, double? recall)
        {
            if (precision is null || recall is null)
                return null;

            return 2 * precision.Value * recall.Value / (precision.Value + recall.Value);
        }
    }
}