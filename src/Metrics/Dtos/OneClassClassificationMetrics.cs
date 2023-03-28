namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class OneClassClassificationMetrics
    {
        public OneClassClassificationMetrics(string classValue, ClassificationMetrics classificationMetrics)
        {
            ClassValue = classValue;
            ClassificationMetrics = classificationMetrics;
        }

        public string ClassValue { get; }

        public ClassificationMetrics ClassificationMetrics { get; }
    }
}