using System.Collections.Generic;
using System.Linq;
using Byndyusoft.ML.Tools.Metrics.Dtos;
using Byndyusoft.ML.Tools.Metrics.Interfaces;

namespace Byndyusoft.ML.Tools.Metrics
{
    public class MultiClassUserPostActionCountsCalculator : IMultiClassUserPostActionCountsCalculator
    {
        public MultiClassUserPostActionCounts Calculate(ClassificationResult[] classificationResults, string nonTargetClass)
        {
            var oneClassUserPostActonCounts = new List<OneClassUserPostActionCounts>();

            foreach (var grouping in classificationResults.GroupBy(i => i.ActualClass))
            {
                var @class = grouping.Key;
                var userPostActionCounts = CalculateUserPostActionCounts(grouping.ToArray(), nonTargetClass);

                oneClassUserPostActonCounts.Add(new OneClassUserPostActionCounts(@class, userPostActionCounts));
            }

            var totalUserPostActionCounts = CalculateTotalUserPostActionCounts(oneClassUserPostActonCounts);

            return new MultiClassUserPostActionCounts(oneClassUserPostActonCounts.ToArray(), totalUserPostActionCounts);
        }

        private UserPostActionCounts CalculateUserPostActionCounts(ClassificationResult[] classificationResults, string nonTargetClass)
        {
            var userDoesNothingCount = 0;
            var userHasToFillCount = 0;
            var userHasToReplaceCount = 0;

            foreach (var classificationResult in classificationResults)
            {
                if (IsClassTarget(classificationResult.PredictedClass, nonTargetClass) == false)
                    ++userHasToFillCount;
                else if (string.Equals(classificationResult.ActualClass, classificationResult.PredictedClass))
                    ++userDoesNothingCount;
                else
                    ++userHasToReplaceCount;
            }

            return new UserPostActionCounts(userDoesNothingCount, userHasToFillCount, userHasToReplaceCount);
        }

        private bool IsClassTarget(string? @class, string nonTargetClass)
        {
            return string.IsNullOrEmpty(@class) && string.Equals(@class, nonTargetClass) == false;
        }

        private UserPostActionCounts CalculateTotalUserPostActionCounts(
            IEnumerable<OneClassUserPostActionCounts> oneClassUserPostActionCounts)
        {
            var userDoesNothingCount = 0;
            var userHasToFillCount = 0;
            var userHasToReplaceCount = 0;

            foreach (var oneClassUserPostActionCount in oneClassUserPostActionCounts)
            {
                var userPostActionCounts = oneClassUserPostActionCount.UserPostActionCounts;
                userDoesNothingCount += userPostActionCounts.UserDoesNothingCount;
                userHasToFillCount += userPostActionCounts.UserHasToFillCount;
                userHasToReplaceCount += userPostActionCounts.UserHasToReplaceCount;
            }

            return new UserPostActionCounts(userDoesNothingCount, userHasToFillCount, userHasToReplaceCount);
        }
    }
}