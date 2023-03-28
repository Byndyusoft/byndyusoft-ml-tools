namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class MultiClassClassificationMetrics
    {
        public MultiClassClassificationMetrics(
            OneClassClassificationMetrics[] oneClassClassificationMetrics,
            ClassificationMetrics microMetrics,
            ClassificationMetrics macroMetrics)
        {
            OneClassClassificationMetrics = oneClassClassificationMetrics;
            MicroMetrics = microMetrics;
            MacroMetrics = macroMetrics;
        }

        public OneClassClassificationMetrics[] OneClassClassificationMetrics { get; }

        public ClassificationMetrics MicroMetrics { get; }

        public ClassificationMetrics MacroMetrics { get; }
    }
}