using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Helpers;
using Byndyusoft.ML.Tools.Metrics.Interfaces;
using Byndyusoft.ML.Tools.Metrics.Settings;

namespace Byndyusoft.ML.Tools.Metrics
{
    public class MultiClassPrecisionRecallCurvesCalculator : IMultiClassPrecisionRecallCurvesCalculator
    {
        public MultiClassPrecisionRecallCurveResult Calculate(
            ClassificationResult[] classificationResults,
            PrecisionRecallCurveSettings? precisionRecallCurveSettings)
        {
            precisionRecallCurveSettings ??= PrecisionRecallCurveSettings.Default();

            var classificationResultsByClass = GenerateClassResultsDictionary(classificationResults);

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

        private static Dictionary<string, HashSet<ClassificationResult>> GenerateClassResultsDictionary(
            ClassificationResult[] classificationResults)
        {
            var classificationResultsByClass = new Dictionary<string, HashSet<ClassificationResult>>();

            foreach (var classificationResult in classificationResults)
            {
                AddToDictionaryOfHashSets(classificationResultsByClass, classificationResult, classificationResult.ActualClass);
                AddToDictionaryOfHashSets(classificationResultsByClass, classificationResult, classificationResult.PredictedClass);
            }

            return classificationResultsByClass;
        }

        private static void AddToDictionaryOfHashSets(
            Dictionary<string, HashSet<ClassificationResult>> dictionaryOfHashSets,
            ClassificationResult classificationResult,
            string? key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            if (dictionaryOfHashSets.TryGetValue(key, out var hashSet) == false)
            {
                hashSet = new HashSet<ClassificationResult>();
                dictionaryOfHashSets.Add(key, hashSet);
            }

            hashSet.Add(classificationResult);
        }
    }
}