using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class ControlParameters {

    public float _gain_scene0 { get; set; }
    public float _gain_scene1 { get; set; }
    public float _gain_scene2 { get; set; }
    public float _gain_scene3 { get; set; }

    public bool _effect0_layer0_shiftRGB { get; set; } // Aキーでコントロール
    public bool _effect0_layer1_shiftRGB { get; set; } // Zキーでコントロール

    public float _audioMaxValue { get; set; }


    private Dictionary<UnityEngine.KeyCode, bool> _keybordInput =
     new Dictionary<UnityEngine.KeyCode, bool>();

    private static ControlParameters _singleInstance = new ControlParameters();

    public static ControlParameters GetInstance(){
        return _singleInstance;
    }

    private ControlParameters(){
        _gain_scene0 = 1.0f;
        _gain_scene1 = 1.0f;
        _gain_scene2 = 1.0f;
        _gain_scene3 = 1.0f;

        _effect0_layer0_shiftRGB = false;

        _audioMaxValue = 0.0f;

        // キーボード入力の状態管理用Hashを初期化
        _keybordInput[KeyCode.A] = false;
        _keybordInput[KeyCode.Z] = false;
    }

    public void SetValueByChannel(int channel, float value) {
        if (channel == 0) {
            _gain_scene0 = value;
        }
        if (channel == 1) {
            _gain_scene1 = value;
        }
        if (channel == 2) {
            _gain_scene2 = value;
        }
        if (channel == 3) {
            _gain_scene3 = value;
        }
    }

    public void SetKeyboradInputValue(UnityEngine.KeyCode keyCode, bool value) {
        _keybordInput[keyCode] = !_keybordInput[keyCode];
    }

    public void UpdateEffectStatus() {
        // Aキー : Layer0 effect0(Shift RGB)のON/OFFに割り当てている
        if (_keybordInput.ContainsKey(KeyCode.A)) {
            _effect0_layer0_shiftRGB = _keybordInput[KeyCode.A];
        }
        // Zキー : Layer1 effect0(Shift RGB)のON/OFFに割り当てている
        if (_keybordInput.ContainsKey(KeyCode.Z)) {
            _effect0_layer1_shiftRGB = _keybordInput[KeyCode.Z];
        }
    }

    public bool GetEffect0Layer0Status() {
        return _effect0_layer0_shiftRGB;
    }
    public bool GetEffect0Layer1Status() {
        return _effect0_layer1_shiftRGB;
    }
}
