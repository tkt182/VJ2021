using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputSwitcher : MonoBehaviour {

    private Color _normalColor;
    private Color _pressedColor;
    private Material _buttonMaterial; // 各ボタンの色を保存するために必要

    [SerializeField] public int _layerNo;
    OutputLayers _outputLayers;


    void Start() {
        _buttonMaterial = new Material(Shader.Find("UI/Unlit/Text"));

        _normalColor = new Color(1.0f, 1.0f, 1.0f);
        _pressedColor = new Color(4.0f, 0.5f, 0.5f);

        _outputLayers = OutputLayers.GetInstance();
        this.gameObject.GetComponent<Image>().material = _buttonMaterial;
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => { Click(_layerNo); });
    }

    void Update() {
        this.ChangeButtonColor(_layerNo);
    }

    void ChangeButtonColor(int layerNo){
        if ((int)_outputLayers._displayedLayer == layerNo) {
            this.gameObject.GetComponent<Image>().material.color = _pressedColor;
        } else {
            this.gameObject.GetComponent<Image>().material.color = _normalColor;
        }
    }

    void Click(int layerNo) {
        if (layerNo == 0) _outputLayers._displayedLayer = DisplayedLayer.Layer0;
        if (layerNo == 1) _outputLayers._displayedLayer = DisplayedLayer.Layer1;
    }
}
