using System;
using System.Collections.Generic;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;
using Byndyusoft.ML.Tools.Metrics.Helpers;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics
{
    public class MultiClassConfusionMatrixValueCountsGenerateTestData
    {
        public string Description { get; set; } = default!;

        public ClassificationResult[] Argument { get; set; } = Array.Empty<ClassificationResult>();

        public MultiClassConfusionMatrices ExpectedResult { get; set; } = default!;

        public override string ToString()
        {
            return Description;
        }

        public static IEnumerable<MultiClassConfusionMatrixValueCountsGenerateTestData> Cases()
        {
            yield return GetCaseForMetricSample();
        }

        private static MultiClassConfusionMatrixValueCountsGenerateTestData GetCaseForMetricSample()
        {
            var argument = ClassificationResultGenerator.GetForMetricSample();

            var expectedCounts = new MultiClassConfusionMatrices(22);

            expectedCounts.AddCount("1", ConfusionMatrixValue.TruePositive, 4);
            expectedCounts.AddCount("1", ConfusionMatrixValue.FalsePositive, 2);
            expectedCounts.AddCount("1", ConfusionMatrixValue.FalseNegative, 3);
            expectedCounts.AddCount("1", ConfusionMatrixValue.TrueNegative, 13);

            expectedCounts.AddCount("2", ConfusionMatrixValue.TruePositive, 3);
            expectedCounts.AddCount("2", ConfusionMatrixValue.FalsePositive);
            expectedCounts.AddCount("2", ConfusionMatrixValue.FalseNegative, 4);
            expectedCounts.AddCount("2", ConfusionMatrixValue.TrueNegative, 14);

            expectedCounts.AddCount("3", ConfusionMatrixValue.TruePositive, 4);
            expectedCounts.AddCount("3", ConfusionMatrixValue.FalsePositive, 2);
            expectedCounts.AddCount("3", ConfusionMatrixValue.FalseNegative, 0);
            expectedCounts.AddCount("3", ConfusionMatrixValue.TrueNegative, 16);

            expectedCounts.AddCount("4", ConfusionMatrixValue.TruePositive, 2);
            expectedCounts.AddCount("4", ConfusionMatrixValue.FalsePositive, 3);
            expectedCounts.AddCount("4", ConfusionMatrixValue.FalseNegative, 2);
            expectedCounts.AddCount("4", ConfusionMatrixValue.TrueNegative, 15);

            return new MultiClassConfusionMatrixValueCountsGenerateTestData
            {
                Description = "Test samples for MetricsSample.xlsx",
                Argument = argument,
                ExpectedResult = expectedCounts
            };
        }
    }
}