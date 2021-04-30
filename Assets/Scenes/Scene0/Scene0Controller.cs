using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene0Controller : MonoBehaviour {

    [SerializeField] GameObject _object;

    [SerializeField] bool _enableAudioInput;

    [SerializeField] float _audioBoost;

    ControlParameters _controlParameters;

    void Start() {
        _controlParameters = ControlParameters.GetInstance();
        _audioBoost = 1.0f;
    }

    void Update() {
        if (_enableAudioInput) {
            float audioValue = _controlParameters._audioMaxValue;
            audioValue *= _audioBoost;
            _object.transform.localScale = new Vector3(audioValue, audioValue, audioValue);
        }
    }
}
