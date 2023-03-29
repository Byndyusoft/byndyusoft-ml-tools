using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics
{
    public static class ClassificationResultGenerator
    {
        public static ClassificationResult[] GetForMetricSample()
        {
            return new ClassificationResult[]
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
            };
        }
    }
}