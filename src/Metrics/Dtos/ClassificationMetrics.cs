namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class ClassificationMetrics
    {
        public ClassificationMetrics(double? precision, double? recall, double? f1Score)
        {
            Precision = precision;
            Recall = recall;
            F1Score = f1Score;
        }

        public double? Precision { get; }

        public double? Recall { get; }

        public double? F1Score { get; }
    }
}