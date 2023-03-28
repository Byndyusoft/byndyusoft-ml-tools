using System;
using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Enums;

namespace Byndyusoft.ML.Tools.Metrics.Helpers
{
    public class ConfusionMatrixValueCounts
    {
        private readonly Dictionary<ConfusionMatrixValue, int> _countsByConfusionMatrixValue;

        public ConfusionMatrixValueCounts()
        {
            _countsByConfusionMatrixValue = Enum.GetValues<ConfusionMatrixValue>().ToDictionary(i => i, _ => 0);
        }

        public void AddCount(ConfusionMatrixValue confusionMatrixValue, int count = 1)
        {
            if (count < 0)
                throw new ArgumentException("Count must be non-negative", nameof(count));

            _countsByConfusionMatrixValue[confusionMatrixValue] += count;
        }

        public int GetCount(ConfusionMatrixValue confusionMatrixValue)
        {
            return _countsByConfusionMatrixValue[confusionMatrixValue];
        }

        public int GetTotalCount() => _countsByConfusionMatrixValue.Sum(i => i.Value);

        public IEnumerable<(ConfusionMatrixValue ConfusionMatrixValue, int Count)> Enumerate()
        {
            return _countsByConfusionMatrixValue.Select(i => (i.Key, i.Value));
        }
    }
}