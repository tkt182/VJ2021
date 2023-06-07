using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1AudioInputSwitcher : MonoBehaviour
{

    ControlParameters _controlParameters;
    void Start() {
        // Scene1単体起動ならAudioを有効にする
        _controlParameters = ControlParameters.GetInstance();
        if (!_controlParameters._main_scene_is_loaded) {
            // 非アクティブなので、親からたどる
            GameObject parent = GameObject.Find("Scene1Root");
            GameObject scene1AudioInput = parent.transform.Find("Scene1AudioInput").gameObject;
            scene1AudioInput.SetActive(true);
        }
    }
}
