using System;
using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;
using Byndyusoft.ML.Tools.Metrics.Extensions;
using Byndyusoft.ML.Tools.Metrics.Settings;

namespace Byndyusoft.ML.Tools.Metrics.Helpers
{
    public static class PrecisionRecallCurveCalculatorHelper
    {
        // TODO Comment
        // TODO Test
        public static PrecisionRecallCurve Calculate(
            string classValue, 
            ClassificationResultWithConfidence[] classificationResults,
            PrecisionRecallCurveSettings precisionRecallCurveSettings)
        {
            classificationResults = classificationResults.OrderByDescending(i => i.Confidence).ToArray();

            var cumulativeFalsePositives = new int[classificationResults.Length];
            var cumulativeTruePositives = new int[classificationResults.Length];
            var recallValues = new double[classificationResults.Length];
            var precisionValues = new double[classificationResults.Length];

            var totalActualClassElements = 0;

            for (var i = 0; i < classificationResults.Length; i++)
            {
                cumulativeTruePositives[i] = i == 0 ? 0 : cumulativeTruePositives[i - 1];
                cumulativeFalsePositives[i] = i == 0 ? 0 : cumulativeFalsePositives[i - 1];

                var confusionMatrixValue = classificationResults[i].CalculateConfusionMatrixValue(classValue);
                switch (confusionMatrixValue)
                {
                    case ConfusionMatrixValue.TruePositive:
                        ++cumulativeTruePositives[i];
                        ++totalActualClassElements;
                        break;
                    case ConfusionMatrixValue.FalsePositive:
                        ++cumulativeFalsePositives[i];
                        break;
                    case ConfusionMatrixValue.FalseNegative:
                        ++totalActualClassElements;
                        break;
                }
            }

            for (var i = 0; i < classificationResults.Length; i++)
            {
                precisionValues[i] = SafeDivideOrDefault(cumulativeTruePositives[i], cumulativeTruePositives[i] + cumulativeFalsePositives[i], 1D);
                recallValues[i] = SafeDivideOrDefault(cumulativeTruePositives[i], totalActualClassElements, 0D);
            }

            var precisionRecallCurvePoints =
                CalculateInterpolatedPrecisionRecallCurveDataPoints(precisionValues, recallValues);

            var averagePrecision = CalculateAveragePrecision(precisionRecallCurvePoints);

            precisionRecallCurvePoints = ReduceDataPointsIfNeeded(precisionRecallCurvePoints, precisionRecallCurveSettings);

            return new PrecisionRecallCurve(
                classValue,
                averagePrecision,
                precisionRecallCurvePoints);
        }

        private static double SafeDivideOrDefault(int dividend, int divisor, double defaultValue)
        {
            if (divisor == 0)
                return defaultValue;

            return (double)dividend / divisor;
        }

        private static double CalculateAveragePrecision(PrecisionRecallCurveDataPoint[] precisionRecallCurvePoints)
        {
            return
                ParallelEnumerable
                    .Range(1, precisionRecallCurvePoints.Length - 1)
                    .Sum(index =>
                        CalculateAreaUnderSegment(
                            precisionRecallCurvePoints[index],
                            precisionRecallCurvePoints[index - 1]));
        }

        private static double CalculateAreaUnderSegment(PrecisionRecallCurveDataPoint currentDataPoint, PrecisionRecallCurveDataPoint previousDataPoint)
        {
            var basesHalfSum = (currentDataPoint.Precision + previousDataPoint.Precision) / 2D;
            var height = currentDataPoint.Recall - previousDataPoint.Recall;
            var trapezoidArea = basesHalfSum * height;

            return trapezoidArea;
        }

        private static PrecisionRecallCurveDataPoint[] CalculateInterpolatedPrecisionRecallCurveDataPoints(
            double[] precisionValues,
            double[] recallValues)
        {
            if (precisionValues.Length != recallValues.Length)
                throw new ArgumentException(
                    $"Arrays ({nameof(precisionValues)}, {nameof(recallValues)}) must have same size.");

            var orderedIndices = recallValues
                .Select((recall, index) => (Recall: recall, Index: index))
                .OrderBy(i => i.Recall)
                .ThenBy(i => i.Index)
                .Select(i => i.Index)
                .ToArray();

            var resultDataPoints = new List<PrecisionRecallCurveDataPoint>(precisionValues.Length + 2);

            var previousDataPoint = new PrecisionRecallCurveDataPoint(1D, 0D);
            resultDataPoints.Add(previousDataPoint);

            foreach (var index in orderedIndices)
            {
                var precision = precisionValues[index];
                var recall = recallValues[index];

                if (AreEqual(precision, previousDataPoint.Precision) &&
                    AreEqual(recall, previousDataPoint.Recall))
                {
                    continue;
                }

                previousDataPoint = new PrecisionRecallCurveDataPoint(precision, recall);
                resultDataPoints.Add(previousDataPoint);
            }

            return resultDataPoints.ToArray();
        }

        private static bool AreEqual(double left, double right)
        {
            return Math.Abs(left - right) < double.Epsilon;
        }

        private static PrecisionRecallCurveDataPoint[] ReduceDataPointsIfNeeded(
            PrecisionRecallCurveDataPoint[] precisionRecallCurvePoints,
            PrecisionRecallCurveSettings precisionRecallCurveSettings)
        {
            if (precisionRecallCurveSettings.CurveDataPointsShouldBeReduced())
            {
                var points = precisionRecallCurvePoints.Select(i => new Point(i.Precision, i.Recall)).ToArray();
                var reducedPoints = DouglasPeuckerInterpolation.Interpolate(
                    points,
                    precisionRecallCurveSettings.MaxDataPointsCountInCurve,
                    precisionRecallCurveSettings.ReduceInterpolationTolerance);
                precisionRecallCurvePoints =
                    reducedPoints.Select(i => new PrecisionRecallCurveDataPoint(i.X, i.Y)).ToArray();
            }

            return precisionRecallCurvePoints;
        }
    }
}