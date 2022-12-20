using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics
{
    public class PrecisionRecallCurve
    {
        public PrecisionRecallCurve(string classValue, double averagePrecision, PrecisionRecallCurveDataPoint[] dataPoints)
        {
            ClassValue = classValue;
            AveragePrecision = averagePrecision;
            DataPoints = dataPoints;
        }

        public string ClassValue { get; }

        public double AveragePrecision { get; }

        public PrecisionRecallCurveDataPoint[] DataPoints { get; }
    }
}