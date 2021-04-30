using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubSceneController : MonoBehaviour
{
    [SerializeField] Camera _sceneCamera;
    [SerializeField] RenderTexture _outputTexture;

    void Start() {
        _sceneCamera.targetTexture = _outputTexture;
    }

}
