using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioInputController : MonoBehaviour {

    [SerializeField] Lasp.SpectrumAnalyzer _inputSpectrum = null;
    [SerializeField] Lasp.AudioLevelTracker _inputLevel = null;
    [SerializeField] bool _logScale = true;
    // マイク入力にするか、audioファイルにするか
    [SerializeField] bool _useAudioFile = false;

    ControlParameters _controlParameters;

    private AudioSource _source;

    void Start() {
        _controlParameters = ControlParameters.GetInstance();
        _source = GetComponent<AudioSource>();
    }

    void Update() {
        if (_useAudioFile) {
            _controlParameters._useAudioFile = true;
        }

        var span = _logScale ? _inputSpectrum.logSpectrumSpan : _inputSpectrum.spectrumSpan;
        _controlParameters._audioMaxValue = GetMaxValue(span);

        if (_controlParameters._useAudioFile) {
            _controlParameters._spectrum = new float[64];
            _source.GetSpectrumData(_controlParameters._spectrum, 0, FFTWindow.BlackmanHarris);
            for(int i = 0; i < _controlParameters._spectrum.Length; i++) {
                _controlParameters._spectrum[i] = 2.0f * _controlParameters._spectrum[i] * _controlParameters._spectrum[i];
            }
        } else {
            _controlParameters._spectrum = span.ToArray();
        }


        var dataSize = _controlParameters._spectrum.Length;
        _controlParameters._rawAudio = new float[dataSize];

        if (_controlParameters._useAudioFile) {
            _source.GetOutputData(_controlParameters._rawAudio, 0);
            for(int i = 0; i < dataSize; i++) {
                _controlParameters._rawAudio[i] = 2.0f * _controlParameters._rawAudio[i] * _controlParameters._rawAudio[i];
            }
        } else {
            if (_inputLevel.audioDataSlice.Length > 0) {
                Array.Copy(
                    _inputLevel.audioDataSlice.ToArray(), 0, _controlParameters._rawAudio, 0, dataSize - 1
                );
            }
        }

    }

    float GetMaxValue(System.ReadOnlySpan<float> source) {
        float maxValue = 0.0f;
        foreach (float value in source) {
            maxValue = maxValue > value ? maxValue : value;
        }

        return maxValue;
    }
}

