namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class OneClassClassificationMetrics
    {
        public OneClassClassificationMetrics(string @class, ClassificationMetrics classificationMetrics)
        {
            Class = @class;
            ClassificationMetrics = classificationMetrics;
        }

        public string Class { get; }

        public ClassificationMetrics ClassificationMetrics { get; }
    }
}