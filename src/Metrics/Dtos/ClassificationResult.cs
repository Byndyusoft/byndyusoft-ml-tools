namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class ClassificationResult
    {
        public ClassificationResult(string actualClass, string? predictedClass)
        {
            ActualClass = actualClass;
            PredictedClass = predictedClass;
        }

        public string ActualClass { get; }

        public string? PredictedClass { get; }
    }
}