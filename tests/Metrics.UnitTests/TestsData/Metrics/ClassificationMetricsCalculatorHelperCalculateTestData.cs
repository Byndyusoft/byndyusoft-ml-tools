﻿using System.Collections.Generic;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Enums;
using Byndyusoft.ML.Tools.Metrics.Helpers;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics
{
    public class ClassificationMetricsCalculatorHelperCalculateTestData
    {
        public string Description { get; set; } = default!;

        public ConfusionMatrix Argument { get; set; } = default!;

        public ClassificationMetrics ExpectedResult { get; set; } = default!;

        public double Epsilon { get; set; }

        public override string ToString()
        {
            return Description;
        }

        public static IEnumerable<ClassificationMetricsCalculatorHelperCalculateTestData> CalculateTestDataCases()
        {
            yield return GetCalculateTestDataCase_SampleMetric();
            yield return GetCalculateTestDataCase_PrecisionIsNotCalculated();
            yield return GetCalculateTestDataCase_RecallIsNotCalculated();
            yield return GetCalculateTestDataCase_PrecisionAndRecallAreNotCalculated();
        }

        private static ClassificationMetricsCalculatorHelperCalculateTestData GetCalculateTestDataCase_SampleMetric()
        {
            var argument = new ConfusionMatrix();
            argument.AddCount(ConfusionMatrixValue.TruePositive, 4);
            argument.AddCount(ConfusionMatrixValue.FalsePositive, 2);
            argument.AddCount(ConfusionMatrixValue.FalseNegative, 3);
            argument.AddCount(ConfusionMatrixValue.TrueNegative, 13);

            return new ClassificationMetricsCalculatorHelperCalculateTestData
            {
                Description = "Test samples for MetricsSample.xlsx for class '1'",
                Argument = argument,
                ExpectedResult = new ClassificationMetrics(0.667D, 0.571D, 0.615D, 9),
                Epsilon = 0.001D
            };
        }

        private static ClassificationMetricsCalculatorHelperCalculateTestData GetCalculateTestDataCase_PrecisionIsNotCalculated()
        {
            var argument = new ConfusionMatrix();
            argument.AddCount(ConfusionMatrixValue.TruePositive, 0);
            argument.AddCount(ConfusionMatrixValue.FalsePositive, 0);
            argument.AddCount(ConfusionMatrixValue.FalseNegative, 3);
            argument.AddCount(ConfusionMatrixValue.TrueNegative, 13);

            return new ClassificationMetricsCalculatorHelperCalculateTestData
            {
                Description = "Precision cannot be calculated",
                Argument = argument,
                ExpectedResult = new ClassificationMetrics(null, 0, 0, 3),
                Epsilon = 0.001D
            };
        }

        private static ClassificationMetricsCalculatorHelperCalculateTestData GetCalculateTestDataCase_RecallIsNotCalculated()
        {
            var argument = new ConfusionMatrix();
            argument.AddCount(ConfusionMatrixValue.TruePositive, 0);
            argument.AddCount(ConfusionMatrixValue.FalsePositive, 2);
            argument.AddCount(ConfusionMatrixValue.FalseNegative, 0);
            argument.AddCount(ConfusionMatrixValue.TrueNegative, 13);

            return new ClassificationMetricsCalculatorHelperCalculateTestData
            {
                Description = "Recall cannot be calculated",
                Argument = argument,
                ExpectedResult = new ClassificationMetrics(0, null, 0, 2),
                Epsilon = 0.001D
            };
        }

        private static ClassificationMetricsCalculatorHelperCalculateTestData GetCalculateTestDataCase_PrecisionAndRecallAreNotCalculated()
        {
            var argument = new ConfusionMatrix();
            argument.AddCount(ConfusionMatrixValue.TruePositive, 0);
            argument.AddCount(ConfusionMatrixValue.FalsePositive, 0);
            argument.AddCount(ConfusionMatrixValue.FalseNegative, 0);
            argument.AddCount(ConfusionMatrixValue.TrueNegative, 13);

            return new ClassificationMetricsCalculatorHelperCalculateTestData
            {
                Description = "Precision and recall cannot be calculated",
                Argument = argument,
                ExpectedResult = new ClassificationMetrics(null, null, null, 0),
                Epsilon = 0.001D
            };
        }
    }
}