using System;

namespace Byndyusoft.ML.Tools.Metrics.Settings
{
    /// <summary>
    ///     Класс для настройки построения кривых Precision Recall в fluent синтаксисе
    /// </summary>
    public static class PrecisionRecallCurveSettingsExtensions
    {
        /// <summary>
        ///     Задание максимального количества точек на графике. Должно быть не меньше
        ///     <see cref="PrecisionRecallCurveSettings.MinimalMaxDataPointsCount" />
        /// </summary>
        public static PrecisionRecallCurveSettings WithMaxDataPointsCountInCurve(
            this PrecisionRecallCurveSettings settings,
            int maxDataPointsCount)
        {
            if (maxDataPointsCount < PrecisionRecallCurveSettings.MinimalMaxDataPointsCount)
                throw new ArgumentException("Максимальное количество точек на графике должно быть не меньше 3",
                    nameof(maxDataPointsCount));
            settings.MaxDataPointsCountInCurve = maxDataPointsCount;
            return settings;
        }

        /// <summary>
        ///     Задание порога расстояния от точки до кривой. Если расстояние меньше порога, то эту точку не нужно сохранять.
        /// </summary>
        public static PrecisionRecallCurveSettings WithCurveInterpolationTolerance(
            this PrecisionRecallCurveSettings settings,
            double tolerance)
        {
            settings.ReduceInterpolationTolerance = tolerance;
            return settings;
        }
    }
}