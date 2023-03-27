using System;

namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class ClassificationResult : IEquatable<ClassificationResult>
    {
        public ClassificationResult(string actualClass, string? predictedClass)
        {
            ActualClass = actualClass;
            PredictedClass = predictedClass;
        }

        public string ActualClass { get; }

        public string? PredictedClass { get; }

        public bool Equals(ClassificationResult? other)
        {
            if (ReferenceEquals(null, other)) 
                return false;

            if (ReferenceEquals(this, other)) 
                return true;

            return ActualClass == other.ActualClass && PredictedClass == other.PredictedClass;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ClassificationResult);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ActualClass, PredictedClass);
        }
    }
}