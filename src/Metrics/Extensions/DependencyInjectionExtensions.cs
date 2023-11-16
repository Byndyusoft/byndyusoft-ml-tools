using Byndyusoft.ML.Tools.Metrics;
using Byndyusoft.ML.Tools.Metrics.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMLMetricsCalculators(this IServiceCollection services)
        {
            services.TryAddSingleton<IMultiClassPrecisionRecallCurvesCalculator, MultiClassPrecisionRecallCurvesCalculator>();
            services.TryAddSingleton<IMultiClassClassificationMetricsCalculator, MultiClassClassificationMetricsCalculator>();
            services.TryAddSingleton<IMultiClassUserPostActionCountsCalculator, MultiClassUserPostActionCountsCalculator>();

            return services;
        }
    }
}