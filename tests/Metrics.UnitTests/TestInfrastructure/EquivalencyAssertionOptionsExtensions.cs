using System;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace Byndyusoft.ML.Tools.Metrics.UnitTests.TestInfrastructure
{
    public static class EquivalencyAssertionOptionsExtensions
    {
        public static EquivalencyAssertionOptions<T> WithTypeAssertion<T, TProperty>(this EquivalencyAssertionOptions<T> options, Action<TProperty, TProperty> assertAction)
        {
            return options
                .Using<TProperty>(ctx => assertAction(ctx.Subject, ctx.Expectation))
                .WhenTypeIs<TProperty>();
        }

        public static EquivalencyAssertionOptions<T> WithApproximateDoubleValues<T>(this EquivalencyAssertionOptions<T> options, double epsilon)
        {
            return options.WithTypeAssertion<T, double>((actual, expected) =>
                actual.Should().BeApproximately(expected, epsilon));
        }
    }
}