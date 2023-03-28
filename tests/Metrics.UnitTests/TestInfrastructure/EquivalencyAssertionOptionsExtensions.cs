using FluentAssertions;
using FluentAssertions.Equivalency;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestInfrastructure
{
    public static class EquivalencyAssertionOptionsExtensions
    {
        public static EquivalencyAssertionOptions<T> WithApproximateDoubleValues<T>(this EquivalencyAssertionOptions<T> options, double epsilon)
        {
            return options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, epsilon))
                .WhenTypeIs<double>();
        }
    }
}