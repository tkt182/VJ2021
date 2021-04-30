using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewController : MonoBehaviour {
    [SerializeField] GameObject _preview0;
    [SerializeField] GameObject _preview1;
    [SerializeField] RenderTexture _previewTexture0;
    [SerializeField] RenderTexture _previewTexture1;


    void Start() {
        _preview0.GetComponent<Renderer>().material.mainTexture = _previewTexture0;
        _preview1.GetComponent<Renderer>().material.mainTexture = _previewTexture1;
    }

    void Update() {
    }
}
