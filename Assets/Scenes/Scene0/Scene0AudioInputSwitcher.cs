using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene0AudioInputSwitcher : MonoBehaviour
{

    ControlParameters _controlParameters;
    void Start() {
        // Scene0単体起動ならAudioを有効にする
        _controlParameters = ControlParameters.GetInstance();
        if (!_controlParameters._main_scene_is_loaded) {
            // 非アクティブなので、親からたどる
            GameObject parent = GameObject.Find("Scene0Root");
            GameObject scene0AudioInput = parent.transform.Find("Scene0AudioInput").gameObject;
            scene0AudioInput.SetActive(true);
        }
    }
}
