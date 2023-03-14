using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public sealed class ControlParameters {

    public float _gain_scene0 { get; set; }
    public float _gain_scene1 { get; set; }
    public float _gain_scene2 { get; set; }
    public float _gain_scene3 { get; set; }

    public bool _effect0_layer0_shiftRGB { get; set; } // Aキーでコントロール
    public bool _effect0_layer1_shiftRGB { get; set; } // Zキーでコントロール

    public bool _main_scene_is_loaded; // 主にデバッグ用

    public int _scene0_move_type { get; set; }
    public int _scene0_camera_switch_counter { get; set; }

    public bool _scene1_flag { get; set; }

    public float _audioMaxValue { get; set; }
    public float[] _spectrum { get; set; }
    public float[] _rawAudio { get; set; }

    public bool _useAudioFile { get; set; }

    private Dictionary<UnityEngine.KeyCode, bool> _keyboardInput =
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

        _main_scene_is_loaded = false;

        _effect0_layer0_shiftRGB = false;
        _scene0_move_type = 0;
        _scene0_camera_switch_counter = 0;

        _audioMaxValue = 0.0f;
        _useAudioFile = false;

        // キーボード入力の状態管理用Hashを初期化
        _keyboardInput[KeyCode.A] = false;
        _keyboardInput[KeyCode.Z] = false;

        _keyboardInput[KeyCode.V] = false;
        _keyboardInput[KeyCode.B] = false;
        _keyboardInput[KeyCode.N] = false;
        _keyboardInput[KeyCode.M] = false;

        _keyboardInput[KeyCode.H] = false;

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
        _keyboardInput[keyCode] = !_keyboardInput[keyCode];
    }

    public void UpdateEffectStatus() {
        // Aキー : Layer0 effect0(Shift RGB)のON/OFFに割り当てている
        if (_keyboardInput.ContainsKey(KeyCode.A)) {
            _effect0_layer0_shiftRGB = _keyboardInput[KeyCode.A];
        }
        // Zキー : Layer1 effect0(Shift RGB)のON/OFFに割り当てている
        if (_keyboardInput.ContainsKey(KeyCode.Z)) {
            _effect0_layer1_shiftRGB = _keyboardInput[KeyCode.Z];
        }
    }

    public void UpdateScene0Parameters() {
        if (_keyboardInput[KeyCode.V] == true) {
            _scene0_move_type = 0;
            // 一度状態を初期化
            _keyboardInput[KeyCode.V] = false;
            _keyboardInput[KeyCode.B] = false;
            _keyboardInput[KeyCode.N] = false;
        }

        if (_keyboardInput[KeyCode.B] == true) {
            _scene0_move_type = 1;
            // 一度状態を初期化
            _keyboardInput[KeyCode.V] = false;
            _keyboardInput[KeyCode.B] = false;
            _keyboardInput[KeyCode.N] = false;
        }

        if (_keyboardInput[KeyCode.N] == true) {
            _scene0_move_type = 2;
            // 一度状態を初期化
            _keyboardInput[KeyCode.V] = false;
            _keyboardInput[KeyCode.B] = false;
            _keyboardInput[KeyCode.N] = false;
        }

        if (_keyboardInput[KeyCode.M] == true) {
            _scene0_camera_switch_counter++;
            _keyboardInput[KeyCode.M] = false; 
        }
    }

    public void UpdateScene1Parameters() {
        _scene1_flag = _keyboardInput[KeyCode.H];
    }

    public bool GetEffect0Layer0Status() {
        return _effect0_layer0_shiftRGB;
    }
    public bool GetEffect0Layer1Status() {
        return _effect0_layer1_shiftRGB;
    }
}
