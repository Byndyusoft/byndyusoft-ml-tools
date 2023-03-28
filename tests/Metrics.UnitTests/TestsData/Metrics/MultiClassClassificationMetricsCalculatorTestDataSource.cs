using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics
{
    public static class MultiClassClassificationMetricsCalculatorTestDataSource
    {
        public static MultiClassClassificationMetricsCalculatorTestData[] Cases =
        {
            new()
            {
                Description = "22 samples",
                Arguments = new ClassificationResult[]
                {
                    new("1", "1"),
                    new("3", "3"),
                    new("3", "3"),
                    new("2", "1"),
                    new("3", "3"),
                    new("4", "1"),
                    new("4", "4"),
                    new("1", "1"),
                    new("1", "1"),
                    new("2", "4"),
                    new("2", "2"),
                    new("2", "2"),
                    new("4", "4"),
                    new("4", "3"),
                    new("1", "1"),
                    new("2", "2"),
                    new("2", "4"),
                    new("1", "4"),
                    new("1", "2"),
                    new("3", "3"),
                    new("2", "3"),
                    new("1", null)
                },
                ExpectedResult = new MultiClassClassificationMetrics(
                    new OneClassClassificationMetrics[]
                    {
                        new("1", new ClassificationMetrics(0.667D, 0.571D, 0.615D)),
                        new("2", new ClassificationMetrics(0.75D, 0.429D, 0.545D)),
                        new("3", new ClassificationMetrics(0.667D, 1D, 0.8D)),
                        new("4", new ClassificationMetrics(0.4D, 0.5D, 0.444D))
                    },
                    new ClassificationMetrics(0.619D, 0.591D, 0.605D),
                    new ClassificationMetrics(0.621D, 0.625D, 0.601D))
            }
        };
    }
}