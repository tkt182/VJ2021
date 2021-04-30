using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerSceneToggleController : MonoBehaviour {

    public int _layerNo;
    public int _sceneNo;

    private Button _button;
    private Color _normalColor;
    private Color _pressedColor;

    private Material _buttonMaterial; // 各ボタンの色を保存するために必要

    LayerSceneStatuses _layerSceneStatuses;


    void Start() {
        _buttonMaterial = new Material(Shader.Find("UI/Unlit/Text"));

        _normalColor = new Color(1.0f, 1.0f, 1.0f);
        _pressedColor = new Color(4.0f, 0.5f, 0.5f);

        _layerSceneStatuses = LayerSceneStatuses.GetInstance();
        this.gameObject.GetComponent<Image>().material = _buttonMaterial;
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => { Click(_layerNo, _sceneNo); });
        // statusを更新したあとにボタンの色を変更
        this.ChangeButtonColor(_layerNo, _sceneNo);
    }

    void Update() {
    }

    void Click(int layerNo, int sceneNo) {

        _layerSceneStatuses.ToggleSceneStatus(layerNo, sceneNo);
        // statusを更新したあとにボタンの色を変更
        this.ChangeButtonColor(layerNo, sceneNo);
    }

    void ChangeButtonColor(int layerNo, int sceneNo){
        if (_layerSceneStatuses.GetSceneStatus(layerNo, sceneNo) == SceneStatus.Showed) {
            this.gameObject.GetComponent<Image>().material.color = _pressedColor;
        } else {
            this.gameObject.GetComponent<Image>().material.color = _normalColor;
        }
    }

}
