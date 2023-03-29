[![License](https://img.shields.io/badge/License-Apache--2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# .NET ML Tools

## Byndyusoft.ML.Tools.Metrics [![Nuget](https://img.shields.io/nuget/v/Byndyusoft.ML.Tools.Metrics.svg)](https://www.nuget.org/packages/Byndyusoft.ML.Tools.Metrics/) [![Downloads](https://img.shields.io/nuget/dt/Byndyusoft.ML.Tools.Metrics.svg)](https://www.nuget.org/packages/Byndyusoft.ML.Tools.Metrics/)

Библиотека для расчёта метрик по результатам работы ML классификатора.

### Регистрация в DI

```csharp
    services.AddMLMetricsCalculators();
```

### Вычисление точек графика кривой precision-recall для многоклассовой классификации

([IMultiClassPrecisionRecallCurvesCalculator](src/Metrics/Interfaces/IMultiClassPrecisionRecallCurvesCalculator.cs)).

#### Входные данные

Входными данными для вычисления являются результаты классификации ([ClassificationResultWithConfidence](src/Metrics/Dtos/ClassificationResultWithConfidence.cs)):
```csharp
    var classificationResult = new ClassificationResultWithConfidence(actualClass: "class1", predictedClass: "class1", confidence: 0.9d);
```

#### Пример

Пример получения precision-recall curve метрик по нескольким классам (результат [MultiClassPrecisionRecallCurveResult](src/Metrics/Dtos/MultiClassPrecisionRecallCurveResult.cs)):
```csharp
    public class MultiClassPrecisionRecallCurveCalculatorExample
    {
        private readonly IMultiClassPrecisionRecallCurvesCalculator _calculator;

        public MultiClassPrecisionRecallCurveCalculatorExample(IMultiClassPrecisionRecallCurvesCalculator calculator)
        {
            _calculator = calculator;
        }

        public MultiClassPrecisionRecallCurveResult Calculate()
        {
            var inputData = new ClassificationResultWithConfidence[]
            {
                new(actualClass: "class1", predictedClass: "class1", confidence: 0.99d),
                new(actualClass: "class1", predictedClass: "class1", confidence: 0.98d),
                new(actualClass: "class1", predictedClass: null, confidence: 0.5d),
                new(actualClass: "class2", predictedClass: "class2", confidence: 0.6d),
                new(actualClass: "class2", predictedClass: "class3", confidence: 0.3d),
                new(actualClass: "class3", predictedClass: "class3", confidence: 0.85d),
                new(actualClass: "class3", predictedClass: "class3", confidence: 0.7d)
            };

            var result = _calculator.Calculate(inputData);

            return result;
        }
    }
```

### Вычисление метрик для многоклассовой классификации

Рассчитываются следующие метрики:
* Precision, Recall, F1Score для каждого класса
* Macro-Precision, Macro-Recall, Macro-F1Score для всех классов
* Micro-Precision, Micro-Recall, Micro-F1Score для всех классов

([IMultiClassClassificationMetricsCalculator](src/Metrics/Interfaces/IMultiClassClassificationMetricsCalculator.cs)).

#### Входные данные

Входными данными для вычисления являются результаты классификации ([ClassificationResult](src/Metrics/Dtos/ClassificationResult.cs)):
```csharp
    var classificationResult = new ClassificationResult(actualClass: "class1", predictedClass: "class1");
```

#### Пример

Пример получения метрик (результат [MultiClassClassificationMetrics](src/Metrics/Dtos/MultiClassClassificationMetrics.cs)):
```csharp
    public class MultiClassClassificationMetricsCalculatorExample
    {
        private readonly IMultiClassClassificationMetricsCalculator _calculator;

        public MultiClassClassificationMetricsCalculatorExample(IMultiClassClassificationMetricsCalculator calculator)
        {
            _calculator = calculator;
        }

        public MultiClassClassificationMetrics Calculate()
        {
            var inputData = new ClassificationResult[]
            {
                new(actualClass: "class1", predictedClass: "class1"),
                new(actualClass: "class1", predictedClass: "class1"),
                new(actualClass: "class1", predictedClass: null),
                new(actualClass: "class2", predictedClass: "class2"),
                new(actualClass: "class2", predictedClass: "class3"),
                new(actualClass: "class3", predictedClass: "class3"),
                new(actualClass: "class3", predictedClass: "class3")
            };

            var result = _calculator.Calculate(inputData);

            return result;
        }
    }
```

### Установка

```shell
dotnet add package Byndyusoft.ML.Tools.Metrics
```

# Maintainers
[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)
