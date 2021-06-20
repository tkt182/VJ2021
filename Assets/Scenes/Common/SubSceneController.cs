using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubSceneController : MonoBehaviour
{
    [SerializeField] RenderTexture _outputTexture;

    void Start() {
        // シーンに複数カメラが存在する場合もある
        foreach (Camera camera in this.GetComponentsInChildren<Camera>()) {
            camera.targetTexture = _outputTexture;
        }

    }

}
