using System;
using Byndyusoft.ML.Tools.Metrics.Helpers;
using Byndyusoft.ML.Tools.Metrics.UnitTests.TestsData.Metrics;
using FluentAssertions;
using NUnit.Framework;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.Helpers
{
    [TestFixture]
    public class MultiClassConfusionMatrixValueCountsTests
    {
        [TestCaseSource(typeof(MultiClassConfusionMatrixValueCountsGenerateTestData),
            nameof(MultiClassConfusionMatrixValueCountsGenerateTestData.Cases))]
        public void Generate_ReturnsExpectedResult(MultiClassConfusionMatrixValueCountsGenerateTestData testData)
        {
            Console.WriteLine(testData.Description);

            // Act
            var multiClassConfusionMatrixValueCounts = MultiClassConfusionMatrixValueCounts.Generate(testData.Argument);

            // Assert
            AssertMultiClassConfusionMatrixValueCounts(multiClassConfusionMatrixValueCounts, testData.ExpectedResult);
        }

        private static void AssertMultiClassConfusionMatrixValueCounts(
            MultiClassConfusionMatrixValueCounts multiClassConfusionMatrixValueCounts, MultiClassConfusionMatrixValueCounts expectedResult)
        {
            multiClassConfusionMatrixValueCounts.Enumerate().Should().BeEquivalentTo(
                expectedResult.Enumerate(),
                o => o
                    .Using<ConfusionMatrixValueCounts>(ctx =>
                        ctx.Subject.Enumerate().Should().BeEquivalentTo(ctx.Expectation.Enumerate()))
                    .WhenTypeIs<ConfusionMatrixValueCounts>());
        }
    }
}