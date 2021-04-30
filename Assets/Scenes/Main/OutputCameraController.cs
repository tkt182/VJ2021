using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputCameraController : MonoBehaviour {

    [SerializeField] RenderTexture _inputLayer0;
    [SerializeField] RenderTexture _inputLayer1;

    OutputLayers _outputLayers;

    void Start() {
        _outputLayers = OutputLayers.GetInstance();
    }

    void Update() {
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        RenderTexture input = _inputLayer0;
        if (_outputLayers._displayedLayer == DisplayedLayer.Layer1) {
            input = _inputLayer1;
        }
        Graphics.Blit(input, dst);
    }

}
