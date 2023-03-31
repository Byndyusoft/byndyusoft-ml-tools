using System;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Settings;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.PrecisionRecallCurves
{
    public class MultiClassPrecisionRecallCurvesCalculateTestData
    {
        public ClassificationResultWithConfidence[] ClassificationResultsArgument { get; set; } = Array.Empty<ClassificationResultWithConfidence>();

        public PrecisionRecallCurveSettings SettingsArgument { get; set; } = default!;

        public double ExpectedMeanAveragePrecision { get; set; }

        public PrecisionRecallCurve[] ExpectedPrecisionRecallCurves { get; set; } = Array.Empty<PrecisionRecallCurve>();

        public double Epsilon { get; set; }

        public static MultiClassPrecisionRecallCurvesCalculateTestData[] Cases =
        {
            new()
            {
                ClassificationResultsArgument = new ClassificationResultWithConfidence[]
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
                SettingsArgument = PrecisionRecallCurveSettings
                    .DefaultWithoutCurveDataPointReducing()
                    .WithMaxDataPointsCountInCurve(10)
                    .WithCurveInterpolationTolerance(0.001),
                ExpectedMeanAveragePrecision = 0.736111d,
                ExpectedPrecisionRecallCurves = new PrecisionRecallCurve[]
                {
                    new(
                        "1",
                        0.716667d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.5d),
                            new(0.6d, 0.5d),
                            new(0.666667d, 0.666667d),
                            new(0d, 1d)
                        }),
                    new(
                        "2",
                        0.825d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.75d),
                            new(0.6d, 0.75d),
                            new(0d, 1d)
                        }),
                    new(
                        "3",
                        0.666667d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.5d),
                            new(0.666667d, 0.5d),
                            new(0d, 1d)
                        })
                },
                Epsilon = 0.000001D,
            },
            new()
            {
                ClassificationResultsArgument = new ClassificationResultWithConfidence[]
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
                SettingsArgument = PrecisionRecallCurveSettings
                    .DefaultWithoutCurveDataPointReducing()
                    .WithMaxDataPointsCountInCurve(10)
                    .WithCurveInterpolationTolerance(0.001),
                ExpectedMeanAveragePrecision = 0.624176d,
                ExpectedPrecisionRecallCurves = new PrecisionRecallCurve[]
                {
                    new(
                        "1",
                        0.526667d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.2d),
                            new(0.5d, 0.2d),
                            new(0.666667d, 0.4d),
                            new(0.5d, 0.4d),
                            new(0.6d, 0.6d),
                            new(0.5d, 0.6d),
                            new(0d, 1d)
                        }),
                    new(
                        "2",
                        0.706973d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.428571d),
                            new(0.6d, 0.428571d),
                            new(0.666667d, 0.571429d),
                            new(0.714286d, 0.714286d),
                            new(0.625d, 0.714286d),
                            new(0d, 1d)
                        }),
                    new(
                        "3",
                        0.638889d,
                        new PrecisionRecallCurveDataPoint[]
                        {
                            new(1d, 0d),
                            new(1d, 0.333333d),
                            new(0.666667d, 0.333333d),
                            new(0.75d, 0.5d),
                            new(0d, 1d)
                        })
                },
                Epsilon = 0.000001D
            }
        };
    }
}