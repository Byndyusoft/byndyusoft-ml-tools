namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class ClassificationResult
    {
        public ClassificationResult(string actualClass, string? predictedClass, double confidence)
        {
            ActualClass = actualClass;
            PredictedClass = predictedClass;
            Confidence = confidence;
        }

        public string ActualClass { get; }

        public string? PredictedClass { get; }

        public double Confidence { get; }
    }
}