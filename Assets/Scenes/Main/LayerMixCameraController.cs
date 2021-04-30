using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMixCameraController : MonoBehaviour
{
    [SerializeField] Shader _shader;

    [SerializeField] int _layerNo;

    [SerializeField] RenderTexture _scene0;
    [SerializeField] RenderTexture _scene1;
    [SerializeField] RenderTexture _scene2;
    [SerializeField] RenderTexture _scene3;

    Material _material;
    LayerSceneStatuses _layerSceneStatuses;
    ControlParameters _controlParameters;

    void Start() {
        _material = new Material(_shader);
        _material.SetTexture("_Chan_0", _scene0);
        _material.SetTexture("_Chan_1", _scene1);
        _material.SetTexture("_Chan_2", _scene2);
        _material.SetTexture("_Chan_3", _scene3);

        _layerSceneStatuses = LayerSceneStatuses.GetInstance();
        _controlParameters = ControlParameters.GetInstance();

    }

    // Update is called once per frame
    void Update() {
        this.SetChannelGain(0, _controlParameters._gain_scene0);
        this.SetChannelGain(1, _controlParameters._gain_scene1);
        this.SetChannelGain(2, _controlParameters._gain_scene2);
        this.SetChannelGain(3, _controlParameters._gain_scene3);

        this.updateLayerSceneStatus();
    }

    void EnableRenderTexture(int sceneNo) {
        if (sceneNo == 0) { _material.SetTexture("_Chan_0", _scene0); } 
        if (sceneNo == 1) { _material.SetTexture("_Chan_1", _scene1); } 
        if (sceneNo == 2) { _material.SetTexture("_Chan_2", _scene2); } 
        if (sceneNo == 3) { _material.SetTexture("_Chan_3", _scene3); } 
    }

    void DisableRenderTexture(int sceneNo) {
        string channelName = "_Chan_" + sceneNo.ToString();
        _material.SetTexture(channelName, null);
    }

    void SetChannelGain(int chan, float value) {
        _material.SetFloat("_Gain_" + chan.ToString(), value);
    }

    void updateLayerSceneStatus() {
        for (int i = 0; i < _layerSceneStatuses._activeSceneNum; i++) {
            RenderSceneCommand command = _layerSceneStatuses.GetLayerSceneCommand(_layerNo, i);
            if (command == RenderSceneCommand.Nothing) continue;
            if (command == RenderSceneCommand.Show) { this.EnableRenderTexture(i); }
            if (command == RenderSceneCommand.Hide) { this.DisableRenderTexture(i); }

            // render commandによるシーンのenable/disableを実施したらcommandをresetする
            _layerSceneStatuses.ResetLayerSceneCommand(_layerNo, i);
        }

    }


    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        Graphics.Blit(src, dst, _material);
    }

}
