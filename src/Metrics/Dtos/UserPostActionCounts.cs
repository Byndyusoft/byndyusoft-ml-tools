namespace Byndyusoft.ML.Tools.Metrics.Dtos
{
    public class UserPostActionCounts
    {
        public UserPostActionCounts(
            int userDoesNothingCount,
            int userHasToFillCount,
            int userHasToReplaceCount)
        {
            UserDoesNothingCount = userDoesNothingCount;
            UserHasToFillCount = userHasToFillCount;
            UserHasToReplaceCount = userHasToReplaceCount;
        }

        public int UserDoesNothingCount { get; }

        public int UserHasToFillCount { get; }

        public int UserHasToReplaceCount { get; }
    }
}