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
                        new("1", new ClassificationMetrics(0.667D, 0.571D, 0.615D, 9)),
                        new("2", new ClassificationMetrics(0.75D, 0.429D, 0.545D, 8)),
                        new("3", new ClassificationMetrics(0.667D, 1D, 0.8D, 6)),
                        new("4", new ClassificationMetrics(0.4D, 0.5D, 0.444D, 7))
                    },
                    new ClassificationMetrics(0.619D, 0.591D, 0.605D, 22),
                    new ClassificationMetrics(0.621D, 0.625D, 0.601D, null)),
                Epsilon = 0.001D
            },
            new()
            {
                Description = "Cases without precision and recall",
                Arguments = new ClassificationResult[]
                {
                    new("1", "1"),
                    new("1", "2"),
                    new("3", null)
                },
                ExpectedResult = new MultiClassClassificationMetrics(
                    new OneClassClassificationMetrics[]
                    {
                        new("1", new ClassificationMetrics(1D, 0.5D, 0.667D, 2)),
                        new("2", new ClassificationMetrics(0D, null, 0D, 1)),
                        new("3", new ClassificationMetrics(null, 0D, 0D, 1))
                    },
                    new ClassificationMetrics(0.5D, 0.333D, 0.4D, 3),
                    new ClassificationMetrics(0.5D, 0.25D, 0.222D, null)),
                Epsilon = 0.001D
            }
        };
    }
}