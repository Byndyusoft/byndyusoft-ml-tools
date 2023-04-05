using System;
using System.Collections.Generic;
using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics
{
    public class PrecisionRecallCurveCalculatorHelperTestData
    {
        public string Description { get; set; } = default!;

        public ClassificationResultWithConfidence[] ClassificationResultsArgument { get; set; } = Array.Empty<ClassificationResultWithConfidence>();

        public string ClassValueArgument { get; set; } = string.Empty;

        public PrecisionRecallCurve? ExpectedResult { get; set; }

        public double Epsilon { get; set; }

        public override string ToString()
        {
            return Description;
        }

        public static IEnumerable<PrecisionRecallCurveCalculatorHelperTestData> CalculateTestDataCases()
        {
            yield return GetCalculateTestDataCase_SampleMetric();
            yield return GetCalculateTestDataCase_PrecisionIsNull();
            yield return GetCalculateTestDataCase_RecallIsNull();
        }

        private static PrecisionRecallCurveCalculatorHelperTestData GetCalculateTestDataCase_SampleMetric()
        {
            var classificationResultsWithConfidence = new ClassificationResultWithConfidence[]
            {
                new("1", "1", 0.5D),
                new("2", "1", 0.7D),
                new("1", "2", 0.8D),
                new("1", "1", 0.9D)
            };

            var expectedDataPoints = new PrecisionRecallCurveDataPoint[]
            {
                new(1D, 0D),
                new(1D, 0.33D),
                new(0.5D, 0.33D),
                new(0.67D, 0.67D),
                new(0D, 1D)
            };
            var expectedPrecisionRecallCurve = new PrecisionRecallCurve("1", 0.64D, expectedDataPoints);

            return new PrecisionRecallCurveCalculatorHelperTestData
            {
                Description = "Test samples for MetricsSample.xlsx for class '1'",
                ClassificationResultsArgument = classificationResultsWithConfidence,
                ClassValueArgument = "1",
                ExpectedResult = expectedPrecisionRecallCurve,
                Epsilon = 0.01D
            };
        }

        private static PrecisionRecallCurveCalculatorHelperTestData GetCalculateTestDataCase_PrecisionIsNull()
        {
            var classificationResultsWithConfidence = new ClassificationResultWithConfidence[]
            {
                new("1", "2", 0.5D)
            };

            return new PrecisionRecallCurveCalculatorHelperTestData
            {
                Description = "Precision is null. Curve cannot be calculated.",
                ClassificationResultsArgument = classificationResultsWithConfidence,
                ClassValueArgument = "1",
                ExpectedResult = null,
                Epsilon = 0.01D
            };
        }

        private static PrecisionRecallCurveCalculatorHelperTestData GetCalculateTestDataCase_RecallIsNull()
        {
            var classificationResultsWithConfidence = new ClassificationResultWithConfidence[]
            {
                new("2", "1", 0.5D)
            };

            return new PrecisionRecallCurveCalculatorHelperTestData
            {
                Description = "Recall is null. Curve cannot be calculated.",
                ClassificationResultsArgument = classificationResultsWithConfidence,
                ClassValueArgument = "1",
                ExpectedResult = null,
                Epsilon = 0.01D
            };
        }
    }
}