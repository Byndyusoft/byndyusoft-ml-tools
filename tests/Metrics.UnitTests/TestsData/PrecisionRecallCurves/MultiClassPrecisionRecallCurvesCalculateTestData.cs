using System;
using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.PrecisionRecallCurves
{
    public class MultiClassPrecisionRecallCurvesCalculateTestData
    {
        public ClassificationResultWithConfidence[] Arguments { get; set; } = Array.Empty<ClassificationResultWithConfidence>();

        public double ExpectedMeanAveragePrecision { get; set; }

        public PrecisionRecallCurve[] ExpectedPrecisionRecallCurves { get; set; } = Array.Empty<PrecisionRecallCurve>();

        public static MultiClassPrecisionRecallCurvesCalculateTestData[] Cases =
        {
            new()
            {
                Arguments = new ClassificationResultWithConfidence[]
                {
                    new("1", "1", 0.98d),
                    new("1", "1", 0.95d),
                    new("1", "1", 0.92d),
                    new("1", "2", 0.4d),
                    new("1", "3", 0.5d),
                    new("1", "1", 0.3d),
                    new("2", "2", 0.95d),
                    new("2", "2", 0.96d),
                    new("2", "2", 0.9d),
                    new("2", "1", 0.5d),
                    new("3", "3", 0.98d),
                    new("3", "3", 0.9d),
                    new("3", "1", 0.6d),
                    new("3", "2", 0.5d),
                },
                ExpectedMeanAveragePrecision = 0.62037037037037035d,
                ExpectedPrecisionRecallCurves = new PrecisionRecallCurve[]
                {
                    new(
                        "1",
                        0.611111111111111d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.5d),
                            new(0.66666666666666663d, 0.5d),
                            new(0.66666666666666663d, 0.66666666666666663d),
                            new(0d, 0.66666666666666663d),
                            new(0d, 1d)
                        }),
                    new(
                        "2",
                        0.75d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.75d),
                            new(0d, 0.75d),
                            new(0d, 1d)
                        }),
                    new(
                        "3",
                        0.5d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.5d),
                            new(0d, 0.5d),
                            new(0d, 1d)
                        })
                }
            },
            new()
            {
                Arguments = new ClassificationResultWithConfidence[]
                {
                    new("1", "1", 0.4d),
                    new("1", "1", 0.6d),
                    new("1", "1", 0.9d),
                    new("1", "2", 0.5d),
                    new("1", "3", 0.5d),
                    new("2", "2", 0.3d),
                    new("2", "1", 0.1d),
                    new("2", "2", 0.9d),
                    new("2", "2", 0.5d),
                    new("2", "2", 0.8d),
                    new("2", "2", 0.9d),
                    new("2", "1", 0.8d),
                    new("3", "3", 0.8d),
                    new("3", "1", 0.6d),
                    new("3", "3", 0.9d),
                    new("3", "2", 0.8d),
                    new("3", "3", 0.5d),
                    new("3", "2", 0.2d)
                },
                ExpectedMeanAveragePrecision = 0.51477324263038549d,
                ExpectedPrecisionRecallCurves = new PrecisionRecallCurve[]
                {
                    new(
                        "1",
                        0.45333333333333337d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.2d),
                            new(0.66666666666666663d, 0.2d),
                            new(0.66666666666666663d, 0.4d),
                            new(0.6d, 0.4d),
                            new(0.6d, 0.6d),
                            new(0d, 0.6d),
                            new(0d, 1d)
                        }),
                    new(
                        "2",
                        0.63265306122448983d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.42857142857142855d),
                            new(0.7142857142857143d, 0.42857142857142855d),
                            new(0.7142857142857143d, 0.7142857142857143d),
                            new(0d, 0.7142857142857143d),
                            new(0d, 1d)
                        }),
                    new(
                        "3",
                        0.45833333333333331d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.33333333333333331d),
                            new(0.75d, 0.33333333333333331d),
                            new(0.75d, 0.5d),
                            new(0d, 0.5d),
                            new(0d, 1d)
                        })
                }
            }
        };
    }
}