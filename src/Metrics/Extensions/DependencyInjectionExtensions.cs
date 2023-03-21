using Byndyusoft.ML.Tools.Metrics.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Byndyusoft.ML.Tools.Metrics.Extensions
{
    // TODO Переименовать
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMLMetricsCalculators(this IServiceCollection services)
        {
            services.TryAddSingleton<IPrecisionRecallCurveCalculator, PrecisionRecallCurveCalculator>();
            services.TryAddSingleton<IMultiClassPrecisionRecallCurvesCalculator, MultiClassPrecisionRecallCurvesCalculator>();

            return services;
        }
    }
}