using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Extensions;
using Byndyusoft.ML.Tools.Metrics.Helpers;
using Byndyusoft.ML.Tools.Metrics.Interfaces;
using Byndyusoft.ML.Tools.Metrics.Settings;

namespace Byndyusoft.ML.Tools.Metrics
{
    public class MultiClassPrecisionRecallCurvesCalculator : IMultiClassPrecisionRecallCurvesCalculator
    {
        public MultiClassPrecisionRecallCurveResult Calculate(
            ClassificationResultWithConfidence[] classificationResults,
            PrecisionRecallCurveSettings? precisionRecallCurveSettings)
        {
            precisionRecallCurveSettings ??= PrecisionRecallCurveSettings.DefaultWithoutCurveDataPointReducing();

            var classificationResultsByClass = GenerateClassificationResultsByClassDictionary(classificationResults);

            var precisionRecallCurves = new List<PrecisionRecallCurve>();

            foreach (var keyValuePair in classificationResultsByClass.OrderBy(i => i.Key))
            {
                var classValue = keyValuePair.Key;
                if (string.IsNullOrEmpty(classValue))
                    continue;
                var classificationResultsOfTheClass = keyValuePair.Value;
                var precisionRecallCurve = PrecisionRecallCurveCalculatorHelper.Calculate(
                    classValue,
                    classificationResultsOfTheClass.ToArray(),
                    precisionRecallCurveSettings);

                precisionRecallCurves.Add(precisionRecallCurve);
            }

            return new MultiClassPrecisionRecallCurveResult(precisionRecallCurves);
        }

        private static IDictionary<string, HashSet<T>> GenerateClassificationResultsByClassDictionary<T>(
            T[] classificationResults)
            where T : ClassificationResult
        {
            // TODO Добавить уверенность по  ActualClass во входных данных, сейчас используется одна уверенность, это неправильно
            var classificationResultsByClass = classificationResults
                .ToDictionaryOfHashSets(i => i.ActualClass, i => i.PredictedClass);

            return classificationResultsByClass;
        }
    }
}