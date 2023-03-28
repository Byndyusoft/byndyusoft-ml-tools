using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;
using Byndyusoft.ML.Tools.Metrics.Extensions;

namespace Byndyusoft.ML.Tools.Metrics.Helpers
{
    public class MultiClassConfusionMatrixValueCounts
    {
        private readonly Dictionary<string, ConfusionMatrixValueCounts> _countsByClass = new();

        public static MultiClassConfusionMatrixValueCounts Generate(ClassificationResult[] classificationResults)
        {
            var multiClassConfusionMatrixValueCounts = new MultiClassConfusionMatrixValueCounts();

            void Add(ClassificationResult classificationResult, string? @class)
            {
                if (string.IsNullOrEmpty(@class))
                    return;

                var confusionMatrixValue = classificationResult.CalculateConfusionMatrixValue(@class);
                multiClassConfusionMatrixValueCounts.AddCount(@class, confusionMatrixValue);
            }

            // Рассчитываем TP, FP и FN
            foreach (var classificationResult in classificationResults)
            {
                Add(classificationResult, classificationResult.ActualClass);
                if (classificationResult.ActualClass.Equals(classificationResult.PredictedClass) == false)
                    Add(classificationResult, classificationResult.PredictedClass);
            }

            // Дополняем количество по классам значениями TN
            foreach (var (_, confusionMatrixValueCounts) in multiClassConfusionMatrixValueCounts.Enumerate())
            {
                confusionMatrixValueCounts.AddCount(ConfusionMatrixValue.TrueNegative,
                    classificationResults.Length - confusionMatrixValueCounts.GetTotalCount());
            }

            return multiClassConfusionMatrixValueCounts;
        }

        public void AddCount(string @class, ConfusionMatrixValue confusionMatrixValue, int count = 1)
        {
            if (_countsByClass.TryGetValue(@class, out var confusionMatrixValueCounts) == false)
            {
                confusionMatrixValueCounts = new ConfusionMatrixValueCounts();
                _countsByClass.Add(@class, confusionMatrixValueCounts);
            }

            confusionMatrixValueCounts.AddCount(confusionMatrixValue, count);
        }

        public IEnumerable<(string Class, ConfusionMatrixValueCounts ConfusionMatrixValueCounts)> Enumerate()
        {
            return _countsByClass
                .OrderBy(i => i.Key)
                .Select(i => (i.Key, i.Value));
        }

        public ConfusionMatrixValueCounts GetTotalCounts()
        {
            var confusionMatrixValueCounts = new ConfusionMatrixValueCounts();
            foreach (var (confusionMatrixValue, count) in _countsByClass.Values.SelectMany(i => i.Enumerate()))
            {
                confusionMatrixValueCounts.AddCount(confusionMatrixValue, count);
            }

            return confusionMatrixValueCounts;
        }
    }
}