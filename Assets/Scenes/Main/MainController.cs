using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    ControlParameters _controlParameters;

    void Awake() {
        int count = Display.displays.Length;
        for (int i = 0; i < count; i++) {
            Display.displays[i].Activate();
        } 
    }

    void Start() {
        _controlParameters = ControlParameters.GetInstance();
        _controlParameters._main_scene_is_loaded = true;
    }

}
