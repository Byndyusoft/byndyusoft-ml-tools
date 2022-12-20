using System;
using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Interfaces;

namespace Byndyusoft.ML.Tools.Metrics
{
    public class MultiClassPrecisionRecallCurvesCalculator : IMultiClassPrecisionRecallCurvesCalculator
    {
        private readonly IPrecisionRecallCurveCalculator _precisionRecallCurveCalculator;

        public MultiClassPrecisionRecallCurvesCalculator(IPrecisionRecallCurveCalculator precisionRecallCurveCalculator)
        {
            _precisionRecallCurveCalculator = precisionRecallCurveCalculator;
        }

        public MultiClassPrecisionRecallCurveResult Calculate(ClassificationResult[] classificationResults)
        {
            var classificationResultsByClass = GenerateClassResultsDictionary(classificationResults);
            var actualClassesWeights = CountClassWeights(classificationResultsByClass.Keys.ToArray(), classificationResults);

            var precisionRecallCurves = new List<PrecisionRecallCurve>();

            foreach (var keyValuePair in classificationResultsByClass.OrderBy(i => i.Key))
            {
                var classValue = keyValuePair.Key;
                if (string.IsNullOrEmpty(classValue))
                    continue;
                var classificationResultsOfTheClass = keyValuePair.Value;
                var precisionRecallCurve = _precisionRecallCurveCalculator.Calculate(
                    classValue,
                    classificationResultsOfTheClass.ToArray());

                precisionRecallCurves.Add(precisionRecallCurve);
            }

            var weightedAveragePrecision = 
            precisionRecallCurves
                .AsParallel()
                .Select(curve => curve.AveragePrecision * actualClassesWeights[curve.ClassValue])
                .Sum();

            return new MultiClassPrecisionRecallCurveResult(precisionRecallCurves, weightedAveragePrecision);
        }

        private static Dictionary<string, double> CountClassWeights(
            string[] allClasses,
            ClassificationResult[] classificationResults)
        {
            var result = new Dictionary<string, double>();
            var classesCount = new Dictionary<string, int>();
            var totalCount = classificationResults.Length;

            foreach (var @class in allClasses)
                classesCount[@class] = 0;

            foreach (var classificationResult in classificationResults)
                classesCount[classificationResult.ActualClass]++;

            foreach (var @class in allClasses)
                result[@class] = (double)classesCount[@class] / totalCount;

            return result;
        }

        private static Dictionary<string, HashSet<ClassificationResult>> GenerateClassResultsDictionary(
            ClassificationResult[] classificationResults)
        {
            var result = new Dictionary<string, HashSet<ClassificationResult>>();

            foreach (var classificationResult in classificationResults)
                AddToDictionaryOfHashSets(result, classificationResult);

            return result;
        }

        private static void AddToDictionaryOfHashSets(
            Dictionary<string, HashSet<ClassificationResult>> dictionaryOfHashSets,
            ClassificationResult classificationResult)
        {
            var keys = new [] {classificationResult.ActualClass, classificationResult.PredictedClass};

            foreach (var key in keys)
            if (string.IsNullOrEmpty(key) == false)
            {
                if (dictionaryOfHashSets.TryGetValue(key!, out var hashSet) == false)
                {
                    hashSet = new HashSet<ClassificationResult>();
                    dictionaryOfHashSets.Add(key!, hashSet);
                }

                hashSet.Add(classificationResult);
            }
        }
    }
}