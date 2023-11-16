namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class OneClassUserPostActionCounts
    {
        public OneClassUserPostActionCounts(string @class, UserPostActionCounts userPostActionCounts)
        {
            Class = @class;
            UserPostActionCounts = userPostActionCounts;
        }

        public string Class { get; }

        public UserPostActionCounts UserPostActionCounts { get; }
    }
}