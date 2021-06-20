using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene0KeyboardInputController : MonoBehaviour {

    ControlParameters _controlParameters;

    void Start() {
        _controlParameters = ControlParameters.GetInstance();
    }

    void Update() {
        // Scene0単体で起動している場合のみ動かす
        if (!_controlParameters._main_scene_is_loaded) {
            GetEscapeKey();
            GetKeyDown(KeyCode.V);
            GetKeyDown(KeyCode.B);
            GetKeyDown(KeyCode.N);
            GetKeyDown(KeyCode.M);
        }
    }

    void GetKeyDown(UnityEngine.KeyCode keyCode) {
        if (Input.GetKeyDown(keyCode)) {
            Debug.Log(keyCode);
            _controlParameters.SetKeyboradInputValue(keyCode, true);
        }
        _controlParameters.UpdateScene0Parameters();
    }

    void GetEscapeKey() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            #if UNITY_EDITOR
              UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
              UnityEngine.Application.Quit();
            #endif
        }
    }
}
