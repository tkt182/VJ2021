using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputController : MonoBehaviour {
    ControlParameters _controlParameters;

    void Start() {
        _controlParameters = ControlParameters.GetInstance();
    }

    void Update() {
        GetEscapeKey();

        // Layer0
        GetKeyDown(KeyCode.A);

        // Layer1
        GetKeyDown(KeyCode.Z);

    }

    void GetKeyDown(UnityEngine.KeyCode keyCode) {
        if (Input.GetKeyDown(keyCode)) {
            _controlParameters.SetKeyboradInputValue(keyCode, true);
        }
        _controlParameters.UpdateEffectStatus();
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
