namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class PrecisionRecallCurveDataPoint
    {
        public PrecisionRecallCurveDataPoint(double precision, double recall)
        {
            Precision = precision;
            Recall = recall;
        }

        public double Precision { get; }

        public double Recall { get; }
    }
}