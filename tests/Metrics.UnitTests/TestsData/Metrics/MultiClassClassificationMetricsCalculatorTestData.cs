using System;
using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics
{
    public class MultiClassClassificationMetricsCalculatorTestData
    {
        public string Description { get; set; } = default!;

        public ClassificationResult[] Arguments { get; set; } = Array.Empty<ClassificationResult>();

        public MultiClassClassificationMetrics ExpectedResult { get; set; } = default!;

        public double Epsilon { get; set; }

        public override string ToString() => Description;

        public static MultiClassClassificationMetricsCalculatorTestData[] Cases =
        {
            new()
            {
                Description = "Test samples for MetricsSample.xlsx",
                Arguments = ClassificationResultGenerator.GetForMetricSample(),
                ExpectedResult = new MultiClassClassificationMetrics(
                    new OneClassClassificationMetrics[]
                    {
                        new("1", new ClassificationMetrics(0.667D, 0.571D, 0.615D)),
                        new("2", new ClassificationMetrics(0.75D, 0.429D, 0.545D)),
                        new("3", new ClassificationMetrics(0.667D, 1D, 0.8D)),
                        new("4", new ClassificationMetrics(0.4D, 0.5D, 0.444D))
                    },
                    new ClassificationMetrics(0.619D, 0.591D, 0.605D),
                    new ClassificationMetrics(0.621D, 0.625D, 0.601D)),
                Epsilon = 0.001D
            }
        };
    }
}