namespace Byndyusoft.ML.Tools.Metrics.Settings
{
    /// <summary>
    ///     Настройки построения кривых Precision Recall
    /// </summary>
    public class PrecisionRecallCurveSettings
    {
        /// <summary>
        ///     Минимальное значение настройки максимального количества точек на графике. <see cref="MaxDataPointsCountInCurve" />>
        /// </summary>
        public static readonly int MinimalMaxDataPointsCount = 3;

        /// <summary>
        ///     Максимальное количество точек на графике. Если количество меньше минимального, то количество точек на графике
        ///     уменьшаться не будет.
        /// </summary>
        public int MaxDataPointsCountInCurve { get; set; } = -1;

        /// <summary>
        ///     Порог расстояния от точки до кривой. Если расстояние меньше порога, то эту точку не нужно сохранять.
        /// </summary>
        public double ReduceInterpolationTolerance { get; set; }

        /// <summary>
        ///     Настройки по умолчанию, без уменьшения количества точек на графике.
        /// </summary>
        public static PrecisionRecallCurveSettings DefaultWithoutCurveDataPointReducing()
        {
            return new PrecisionRecallCurveSettings();
        }

        /// <summary>
        ///     Нужно ли уменьшать количество точек на графике.
        /// </summary>
        public bool CurveDataPointsShouldBeReduced()
        {
            return MaxDataPointsCountInCurve >= MinimalMaxDataPointsCount;
        }
    }
}