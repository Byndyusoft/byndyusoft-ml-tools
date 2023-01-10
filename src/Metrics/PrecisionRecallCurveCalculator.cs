using System;
using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;
using Byndyusoft.ML.Tools.Metrics.Extensions;
using Byndyusoft.ML.Tools.Metrics.Interfaces;

namespace Byndyusoft.ML.Tools.Metrics
{
    public class PrecisionRecallCurveCalculator : IPrecisionRecallCurveCalculator
    {
        public PrecisionRecallCurve Calculate(string classValue, ClassificationResult[] classificationResults)
        {
            classificationResults = classificationResults.OrderByDescending(i => i.Confidence).ToArray();

            var falsePositives = new int[classificationResults.Length];
            var truePositives = new int[classificationResults.Length];
            var recallValues = new double[classificationResults.Length];
            var precisionValues = new double[classificationResults.Length];

            var totalActualClassElements = 0;

            for (var i = 0; i < classificationResults.Length; i++)
            {
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

            for (var i = 0; i < classificationResults.Length; i++)
            {
                precisionValues[i] = SafeDivideOrOne(truePositives[i], truePositives[i] + falsePositives[i]);
                recallValues[i] = SafeDivideOrOne(truePositives[i], totalActualClassElements);
            }

            var precisionRecallCurvePoints =
                CalculateInterpolatedPrecisionRecallCurveDataPoints(precisionValues, recallValues);

            var averagePrecision = CalculateAveragePrecision(precisionRecallCurvePoints);

            return new PrecisionRecallCurve(
                classValue,
                averagePrecision,
                precisionRecallCurvePoints);
        }

        private static double SafeDivideOrOne(int dividend, int divisor)
        {
            if (divisor == 0)
                return 0;

            return (double)dividend / divisor;
        }

        private static double CalculateAveragePrecision(PrecisionRecallCurveDataPoint[] precisionRecallCurvePoints)
        {
            return
                ParallelEnumerable
                    .Range(1, precisionRecallCurvePoints.Length - 1)
                    .Sum(index =>
                        precisionRecallCurvePoints[index].Precision *
                        (precisionRecallCurvePoints[index].Recall - precisionRecallCurvePoints[index - 1].Recall));
        }

        private static PrecisionRecallCurveDataPoint[] CalculateInterpolatedPrecisionRecallCurveDataPoints(
            double[] precisionValues,
            double[] recallValues)
        {
            if (precisionValues.Length != recallValues.Length)
                throw new ArgumentException(
                    $"Arrays ({nameof(precisionValues)}, {nameof(recallValues)}) must have same size.");

            var inputLength = precisionValues.Length;
            var resultLength = precisionValues.Length + 3;

            var interpolatedPrecisionValues = new double[resultLength];
            var interpolatedRecallValues = new double[resultLength];
            var result = new List<PrecisionRecallCurveDataPoint>(resultLength);

            Array.Copy(precisionValues, 0, interpolatedPrecisionValues, 1, inputLength);
            interpolatedPrecisionValues[0] = 1d;
            interpolatedPrecisionValues[resultLength - 1] = 0d;
            interpolatedPrecisionValues[resultLength - 2] = 0d;

            Array.Copy(recallValues, 0, interpolatedRecallValues, 1, inputLength);
            interpolatedRecallValues[0] = 0d;
            interpolatedRecallValues[resultLength - 1] = 1d;
            interpolatedRecallValues[resultLength - 2] = interpolatedRecallValues[resultLength - 3];

            for (var i = resultLength - 1; i > 0; i--)
                interpolatedPrecisionValues[i - 1] =
                    Math.Max(interpolatedPrecisionValues[i - 1], interpolatedPrecisionValues[i]);


            var lastPoint =
                new PrecisionRecallCurveDataPoint(interpolatedPrecisionValues[0], interpolatedRecallValues[0]);
            result.Add(lastPoint);

            for (var i = 1; i < resultLength - 1; i++)
                // ReSharper disable CompareOfFloatsByEqualityOperator
                if (interpolatedPrecisionValues[i + 1] != lastPoint.Precision &&
                    interpolatedRecallValues[i + 1] != lastPoint.Recall)
                {
                    lastPoint =
                        new PrecisionRecallCurveDataPoint(
                            interpolatedPrecisionValues[i],
                            interpolatedRecallValues[i]);
                    result.Add(lastPoint);
                }
            // ReSharper restore CompareOfFloatsByEqualityOperator

            result.Add(new PrecisionRecallCurveDataPoint(interpolatedPrecisionValues[resultLength - 1],
                interpolatedRecallValues[resultLength - 1]));

            return result.ToArray();
        }
    }
}