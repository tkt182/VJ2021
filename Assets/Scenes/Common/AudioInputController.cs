using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInputController : MonoBehaviour {

    [SerializeField] Lasp.SpectrumAnalyzer _input = null;
    [SerializeField] bool _logScale = true;

    ControlParameters _controlParameters;

    void Start() {
        _controlParameters = ControlParameters.GetInstance();
    }

    void Update() {
        var span = _logScale ? _input.logSpectrumSpan : _input.spectrumSpan;
        _controlParameters._audioMaxValue = GetMaxValue(span);
    }

    float GetMaxValue(System.ReadOnlySpan<float> source) {
        float maxValue = 0.0f;
        foreach (float value in source) {
            maxValue = maxValue > value ? maxValue : value;
        }

        return maxValue;
    }
}

