namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class ClassificationResultWithConfidence : ClassificationResult
    {
        public ClassificationResultWithConfidence(string actualClass, string? predictedClass, double confidence)
            : base(actualClass, predictedClass)
        {
            Confidence = confidence;
        }

        public double Confidence { get; }
    }
}