using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerEffectCameraController : MonoBehaviour {
    [SerializeField] RenderTexture _input;
    [SerializeField] Shader _shader;
    Material _material;
    ControlParameters _controlParameters;


    void Start() {
        _material = new Material(_shader);
        _controlParameters = ControlParameters.GetInstance();
    }

    void Update() {
        _material.SetFloat("_audioValue", _controlParameters._audioMaxValue);

        // 力技でLayerごとに対応させる
        if (this.gameObject.name.Equals("Layer0EffectCamera")) {
            _material.SetInteger("_effect_0_shiftRGB",
                _controlParameters.GetEffect0Layer0Status() ? 1 : 0);
        }

        if (this.gameObject.name.Equals("Layer1EffectCamera")) {
            _material.SetInteger("_effect_0_shiftRGB",
                _controlParameters.GetEffect0Layer1Status() ? 1 : 0);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        var texture = _input != null ? _input : src;
        Graphics.Blit(texture, dst, _material);
    }

}
