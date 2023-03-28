using System;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Helpers;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics
{
    public class MultiClassConfusionMatrixValueCountsGenerateTestData
    {
        public string Description { get; set; } = default!;

        public ClassificationResult[] Argument { get; set; } = Array.Empty<ClassificationResult>();

        public MultiClassConfusionMatrixValueCounts ExpectedResult { get; set; } = default!;

        public override string ToString()
        {
            return Description;
        }
    }
}