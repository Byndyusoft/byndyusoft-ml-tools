using System;

namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class ClassificationResultWithConfidence : ClassificationResult, IEquatable<ClassificationResultWithConfidence>
    {
        public ClassificationResultWithConfidence(string actualClass, string? predictedClass, double confidence)
            : base(actualClass, predictedClass)
        {
            Confidence = confidence;
        }

        public double Confidence { get; }

        public bool Equals(ClassificationResultWithConfidence? other)
        {
            if (ReferenceEquals(null, other)) 
                return false;

            if (ReferenceEquals(this, other)) 
                return true;

            return base.Equals(other) && Confidence.Equals(other.Confidence);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ClassificationResultWithConfidence);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Confidence);
        }
    }
}