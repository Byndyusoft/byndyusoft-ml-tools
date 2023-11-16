namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class MultiClassUserPostActionCounts
    {
        public MultiClassUserPostActionCounts(
            OneClassUserPostActionCounts[] oneClassUserPostActionCounts,
            UserPostActionCounts totalUserPostActionCounts)
        {
            OneClassUserPostActionCounts = oneClassUserPostActionCounts;
            TotalUserPostActionCounts = totalUserPostActionCounts;
        }

        public OneClassUserPostActionCounts[] OneClassUserPostActionCounts { get; }

        public UserPostActionCounts TotalUserPostActionCounts { get; }
    }
}