using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;

namespace Byndyusoft.ML.Tools.Metrics.Extensions
{
    public static class ClassificationResultExtensions
    {
        public static ConfusionMatrixValue CalculateConfusionMatrixValue(
            this ClassificationResultWithConfidence classResult,
            string currentClassValue)
        {
            var isPredictedTrue = string.Equals(classResult.PredictedClass, currentClassValue);
            var isActualTrue = string.Equals(classResult.ActualClass, currentClassValue);

            if (isPredictedTrue)
            {
                if (isActualTrue)
                    return ConfusionMatrixValue.TruePositive;
                return
                    ConfusionMatrixValue.FalsePositive;
            }

            if (isActualTrue)
                return ConfusionMatrixValue.FalseNegative;

            return ConfusionMatrixValue.TrueNegative;
        }
    }
}