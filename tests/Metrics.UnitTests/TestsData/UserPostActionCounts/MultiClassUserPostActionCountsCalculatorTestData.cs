using System;
using Byndyusoft.ML.Tools.Metrics.Dtos;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.UserPostActionCounts
{
    public class MultiClassUserPostActionCountsCalculatorTestData
    {
        private static string DefaultNonTargetClass = "NonTarget";

        public string Description { get; set; } = default!;

        public ClassificationResult[] ClassificationResultsArgument { get; set; } = Array.Empty<ClassificationResult>();

        public string NonTargetClassArgument { get; set; } = default!;

        public MultiClassUserPostActionCounts ExpectedUserPostActionCounts { get; set; } = default!;

        public static MultiClassUserPostActionCountsCalculatorTestData[] Cases =
        {
            new()
            {
                Description = "Default test case",
                ClassificationResultsArgument = new ClassificationResult[]
                {
                    new(actualClass: "1", predictedClass: "1"),
                    new(actualClass: "1", predictedClass: null),
                    new(actualClass: "1", predictedClass: "2"),
                    new(actualClass: "1", predictedClass: DefaultNonTargetClass),
                    new(actualClass: "2", predictedClass: "2"),
                    new(actualClass: "2", predictedClass: "2")
                },
                NonTargetClassArgument = DefaultNonTargetClass,
                ExpectedUserPostActionCounts = new MultiClassUserPostActionCounts(
                    new OneClassUserPostActionCounts[]
                    {
                        new("1",
                            new Dtos.UserPostActionCounts(
                                userDoesNothingCount: 1, 
                                userHasToFillCount: 2,
                                userHasToReplaceCount: 1)),
                        new("2",
                            new Dtos.UserPostActionCounts(
                                userDoesNothingCount: 2, 
                                userHasToFillCount: 0,
                                userHasToReplaceCount: 0)),
                    },
                    totalUserPostActionCounts: new Dtos.UserPostActionCounts(
                        userDoesNothingCount: 3, 
                        userHasToFillCount: 2,
                        userHasToReplaceCount: 1))
            }
        };
    }
}