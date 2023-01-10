[![License](https://img.shields.io/badge/License-Apache--2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# .NET ML Tools

## Byndyusoft.ML.Tools.Metrics

Библиотека для рассчёта метрик по результатам работы ML классификатора.

Реализовано вычисление метрик precision-recall для одного и нескольких классов
([IPrecisionRecallCurveCalculator](src/Metrics/Interfaces/IPrecisionRecallCurveCalculator.cs), [IMultiClassPrecisionRecallCurvesCalculator](src/Metrics/Interfaces/IMultiClassPrecisionRecallCurvesCalculator.cs)).

Входными данными для вычисления являются результаты классификации ([ClassificationResult](src/Metrics/Dtos/ClassificationResult.cs)):
```csharp
    var classificationResult = new ClassificationResult(actualClass: "class1", predictedClass: "class1", confidence: 0.9d);
```

Регистрация в DI:
```csharp
    services.AddMLMetricsCalculators();
```

Пример получения precision-recall curve метрик для одного класса (результат [PrecisionRecallCurve](src/Metrics/Dtos/PrecisionRecallCurve.cs)):
```csharp
    public class PrecisionRecallCurveCalculatorExample
    {
        private readonly IPrecisionRecallCurveCalculator _calculator;

        public PrecisionRecallCurveCalculatorExample(IPrecisionRecallCurveCalculator calculator)
        {
            _calculator = calculator;
        }

        public PrecisionRecallCurve Calculate()
        {
            var inputData = new ClassificationResult[]
            {
                new(actualClass: "class1", predictedClass: "class1", confidence: 0.9d),
                new(actualClass: "class1", predictedClass: "class1", confidence: 0.98d),
                new(actualClass: "class1", predictedClass: null, confidence: 0.5d),
                new(actualClass: "class1", predictedClass: "class2", confidence: 0.6d),
                new(actualClass: "class1", predictedClass: "class3", confidence: 0.3d),
                new(actualClass: "class1", predictedClass: "class1", confidence: 0.85d),
                new(actualClass: "class1", predictedClass: "class1", confidence: 0.7d)
            };

            var result = _calculator.Calculate("class1", inputData);

            return result;
        }
    }
```
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
            var inputData = new ClassificationResult[]
            {
                new(actualClass: "class1", predictedClass: "class1", confidence: 0.9d),
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

### Установка

```shell
dotnet add package Byndyusoft.ML.Tools.Metrics
```

# Maintainers
[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)
