namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class ClassificationMetrics
    {
        public ClassificationMetrics(string classValue, double? precision, double? recall, double? f1Score)
        {
            ClassValue = classValue;
            Precision = precision;
            Recall = recall;
            F1Score = f1Score;
        }

        public string ClassValue { get; }

        public double? Precision { get; }

        public double? Recall { get; }
        
        public double? F1Score { get; }
    }
}