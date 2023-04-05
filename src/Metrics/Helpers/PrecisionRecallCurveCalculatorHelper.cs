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
        public static PrecisionRecallCurve? Calculate(
            string classValue, 
            ClassificationResultWithConfidence[] classificationResults,
            PrecisionRecallCurveSettings precisionRecallCurveSettings)
        {
            // Сортируем по коэффициентам уверенности. Рассчитываем точки графика по порогам, соответствующим этим коэффициентам
            classificationResults = classificationResults.OrderByDescending(i => i.Confidence).ToArray();

            // Рассчитываем TP и FP для каждого порога
            var falsePositives = new int[classificationResults.Length];
            var truePositives = new int[classificationResults.Length];

            // Количество TP + FN, оно является инвариантным для каждого порога (количество образцов, входящих в этот класс)
            var totalActualClassElements = 0;

            for (var i = 0; i < classificationResults.Length; i++)
            {
                // TP и FP рассчитываются кумулятивно, т.к. чем ниже порог, тем их становится не меньше
                truePositives[i] = i == 0 ? 0 : truePositives[i - 1];
                falsePositives[i] = i == 0 ? 0 : falsePositives[i - 1];

                var confusionMatrixValue = classificationResults[i].CalculateConfusionMatrixValue(classValue);
                switch (confusionMatrixValue)
                {
                    case ConfusionMatrixValue.TruePositive:
                        ++truePositives[i];
                        ++totalActualClassElements;
                        break;
                    case ConfusionMatrixValue.FalsePositive:
                        ++falsePositives[i];
                        break;
                    case ConfusionMatrixValue.FalseNegative:
                        ++totalActualClassElements;
                        break;
                }
            }

            var recallValues = new List<double>(classificationResults.Length);
            var precisionValues = new List<double>(classificationResults.Length);

            for (var i = 0; i < classificationResults.Length; i++)
            {
                if (TryDivide(truePositives[i], truePositives[i] + falsePositives[i], out var precision) &&
                    TryDivide(truePositives[i], totalActualClassElements, out var recall))
                {
                    precisionValues.Add(precision);
                    recallValues.Add(recall);
                }
            }

            if (precisionValues.Any() == false)
                return null;

            var precisionRecallCurvePoints =
                CalculatePrecisionRecallCurveDataPoints(precisionValues, recallValues);

            var averagePrecision = CalculateAveragePrecision(precisionRecallCurvePoints);

            precisionRecallCurvePoints = ReduceDataPointsIfNeeded(precisionRecallCurvePoints, precisionRecallCurveSettings);

            return new PrecisionRecallCurve(
                classValue,
                averagePrecision,
                precisionRecallCurvePoints);
        }

        private static bool TryDivide(int dividend, int divisor, out double result)
        {
            result = default;

            if (divisor == 0)
                return false;

            result = (double)dividend / divisor;
            return true;
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
            var halfSumOfBases = (currentDataPoint.Precision + previousDataPoint.Precision) / 2D;
            var height = currentDataPoint.Recall - previousDataPoint.Recall;
            var trapezoidArea = halfSumOfBases * height;

            return trapezoidArea;
        }

        private static PrecisionRecallCurveDataPoint[] CalculatePrecisionRecallCurveDataPoints(
            List<double> precisionValues,
            List<double> recallValues)
        {
            if (precisionValues.Count != recallValues.Count)
                throw new ArgumentException(
                    $"Arrays ({nameof(precisionValues)}, {nameof(recallValues)}) must have same size.");

            var curveDataPoints = new List<PrecisionRecallCurveDataPoint>(precisionValues.Count + 2);

            var previousDataPoint = new PrecisionRecallCurveDataPoint(1D, 0D);
            curveDataPoints.Add(previousDataPoint);

            void AddDataPointIfNotEqualToPrevious(double precision, double recall)
            {
                if (AreEqual(precision, previousDataPoint.Precision) &&
                    AreEqual(recall, previousDataPoint.Recall))
                {
                    return;
                }

                previousDataPoint = new PrecisionRecallCurveDataPoint(precision, recall);
                curveDataPoints.Add(previousDataPoint);
            }

            for (var index = 0; index < precisionValues.Count; ++index)
            {
                var precision = precisionValues[index];
                var recall = recallValues[index];

                AddDataPointIfNotEqualToPrevious(precision, recall);
            }

            AddDataPointIfNotEqualToPrevious(0D, 1D);

            return curveDataPoints.ToArray();
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