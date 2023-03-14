using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1KeyboardInputController : MonoBehaviour
{

    ControlParameters _controlParameters;

    void Start()
    {
        _controlParameters = ControlParameters.GetInstance();
    }

    void Update()
    {
        GetEscapeKey();
        GetKeyDown(KeyCode.H);
    }

    void GetKeyDown(UnityEngine.KeyCode keyCode) {
        if (Input.GetKeyDown(keyCode)) {
            Debug.Log(keyCode);
            _controlParameters.SetKeyboradInputValue(keyCode, true);
        }
        _controlParameters.UpdateScene1Parameters();
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
