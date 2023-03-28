using System;
using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics
{
    public class MultiClassClassificationMetricsCalculatorTestData
    {
        public string Description { get; set; } = default!;

        public ClassificationResult[] Arguments { get; set; } = Array.Empty<ClassificationResult>();

        public MultiClassClassificationMetrics ExpectedResult { get; set; } = default!;

        public override string ToString() => Description;
    }
}