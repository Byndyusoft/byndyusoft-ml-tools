﻿namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class ClassificationMetrics
    {
        public ClassificationMetrics(double? precision, double? recall, double? f1Score, int? sampleCount)
        {
            Precision = precision;
            Recall = recall;
            F1Score = f1Score;
            SampleCount = sampleCount;
        }

        public double? Precision { get; }

        public double? Recall { get; }

        public double? F1Score { get; }
        
        public int? SampleCount { get; }
    }
}