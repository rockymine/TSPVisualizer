using Microsoft.AspNetCore.Components;
using TSPVisualizer.Core;

namespace TSPVisualizer.Components;

public partial class AlgorithmSettingsComponent : ComponentBase {
    private Algorithm? _algorithm;
    [Parameter]
    public Algorithm? Algorithm {
        get => _algorithm;
        set {
            if (value == _algorithm)
                return;

            _algorithm = value;
            AlgorithmChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<Algorithm> AlgorithmChanged { get; set; }

    private AlgorithmParameter? _algorithmParameter;
    [Parameter]
    public AlgorithmParameter? AlgorithmParameter {
        get => _algorithmParameter;
        set {
            if (value == _algorithmParameter)
                return;

            _algorithmParameter = value;
            AlgorithmParameterChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<AlgorithmParameter> AlgorithmParameterChanged { get; set; }
}

